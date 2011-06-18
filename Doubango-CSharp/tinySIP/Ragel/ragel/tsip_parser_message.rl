/* Copyright (C) 2010-2011 Mamadou Diop. 
* Copyright (C) 2011 Doubango Telecom <http://www.doubango.org>
*
* Contact: Mamadou Diop <diopmamadou(at)doubango.org>
*	
* This file is part of Open Source Xbox-VoIP Project <http://code.google.com/p/xbox-voip/>
*
* Xbox-VoIP is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*	
* XBox-Voip is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*	
* You should have received a copy of the GNU General Public License
* along with XBox-Voip.
*/

/***********************************
*	Ragel state machine.
*/
%%{
	machine tsip_machine_parser_message;

	#/* Tag the buffer (start point). */
	action tag
	{
		state.TagStart = p;
	}

	#/* SIP method */
	action parse_method
	{
		int len;
		state.TagEnd = p;
		len = (int)(state.TagEnd  - state.TagStart);
        String method = Encoding.UTF8.GetString(data, state.TagStart, len);
        if(message == null)
        {
            message = new TSIP_Request(method, null, null, null, null,0);
        }
        else
        {
            state.CS = tsip_machine_parser_message_error;
        }
	}

	#/* Request URI parsing */
	action parse_requesturi
	{
		int len;
		state.TagEnd = p;
		len = (int)(state.TagEnd  - state.TagStart);
        if ((message as TSIP_Request).Uri == null)
        {
            (message as TSIP_Request).Uri = TSIP_ParserUri.Parse(Encoding.UTF8.GetString(state.Data, state.TagStart, len));
        }
	}

	#/* Sip Version */
	action parse_sipversion
	{
		int len;
		state.TagEnd = p;
		len = (int)(state.TagEnd  - state.TagStart);
		
		if(message == null)
		{
            message = new TSIP_Response(0, null, null);
		}
        message.Version = Encoding.UTF8.GetString(state.Data, state.TagStart, len);
	}

	#/* Status Code */
	action parse_status_code
	{
		int len;
		state.TagEnd = p;
		len = (int)(state.TagEnd  - state.TagStart);
        
        if (message == null)
        {
            message = new TSIP_Response(0, null, null);
        }

        UInt16 statusCode = 0;
        if (UInt16.TryParse(Encoding.UTF8.GetString(state.Data, state.TagStart, len), out statusCode))
        {
            (message as TSIP_Response).StatusCode = statusCode;
        }
	}

	#/* Reason Phrase */
	action parse_reason_phrase
	{
		int len;
		state.TagEnd = p;
		len = (int)(state.TagEnd  - state.TagStart);
        (message as TSIP_Response).ReasonPhrase = Encoding.UTF8.GetString(state.Data, state.TagStart, len);
	}

	#/* Parse sip header */
	action parse_header
	{
		int len;
		state.TagEnd = p;
		len = (int)(state.TagEnd  - state.TagStart);
		
		if(!TSIP_ParserHeader.Parse(state, ref message)){
			TSK_Debug.Error("Failed to parse header at {0}", state.TagStart);
		}
	}

	#/* End-Of-Headers */
	action eoh
	{
		state.CS = cs;
		state.P = p;
		state.PE = pe;
		state.EOF = eof;

		TSIP_ParserMessage.EoH(ref state, ref message, extractContent);

		cs = state.CS;
		p = state.P;
		pe = state.PE;
		eof = state.EOF;
	}

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	include tsip_machine_message "./ragel/tsip_machine_message.rl";
	
	# Entry point
	main := SIP_message;
}%%

using System;
using Doubango.tinySIP;
using Doubango.tinySAK;
using System.Text;
using System.IO;

public static class TSIP_ParserMessage
{
	%%write data;

	public static TSIP_Message Parse(byte[] content, Boolean extractContent)
	{
		TSK_RagelState state = TSK_RagelState.Init(content);
        byte[] data = content;
		TSIP_Message message = null;

		// Ragel init
		TSIP_ParserMessage.Init(ref state);

		// State mechine execution
		message = TSIP_ParserMessage.Execute(ref state, extractContent);
		
		// Check result
		if(message != null && state.CS < %%{ write first_final; }%% )
		{
			message.Dispose();
			message = null;
		}
		return message;
	}

	private static void Init(ref TSK_RagelState state)
	{
		int cs = 0;
		
		// Ragel machine initialization
		%% write init;

		state.CS = cs;
	}

	private static TSIP_Message Execute(ref TSK_RagelState state, Boolean extractContent)
	{
		int cs = state.CS;
		int p = state.P;
		int pe = state.PE;
		int eof = state.EOF;
		byte[] data = state.Data;
		TSIP_Message message = null;

		%% write exec;

		state.CS = cs;
		state.P = p;
		state.PE = pe;
		state.EOF = eof;

		return message;
	}

	// Check if we have ",CRLF" ==> See WWW-Authenticate header
	// As :>CRLF is preceded by any+ ==> p will be at least (start + 1)
	// p point to CR
	private static Boolean PrevNotComma(TSK_RagelState state, int p)
	{
		return (state.PE <= p) || ((char)state.Data[p-1] != ',');
	}

	private static void EoH(ref TSK_RagelState state, ref TSIP_Message message, Boolean extractContent)
    {
        int cs = state.CS;
        int p = state.P;
        int pe = state.PE;
        int eof = state.EOF;

        if (extractContent && message != null)
        {
            int clen = (int)(message.ContentLength != null ? message.ContentLength.Length : 0);
            if ((p + clen) < pe && message.Content == null)
            {
                byte[] content = new byte[clen];
                Buffer.BlockCopy(state.Data, p + 1, content, 0, clen);
                message.AddContent(null, content);
                p = (p + clen);
            }
            else
            {
                p = (pe - 1);
            }
        }
    }
}