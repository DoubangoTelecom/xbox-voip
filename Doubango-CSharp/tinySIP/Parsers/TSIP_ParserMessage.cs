
/* #line 1 "./ragel/tsip_parser_message.rl" */
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

/* #line 139 "./ragel/tsip_parser_message.rl" */


using System;
using Doubango.tinySIP;
using Doubango.tinySAK;
using System.Text;
using System.IO;

public static class TSIP_ParserMessage
{
	
/* #line 41 "../Parsers/TSIP_ParserMessage.cs" */
static readonly sbyte[] _tsip_machine_parser_message_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2, 1, 
	3, 1, 4, 1, 5, 1, 6, 1, 
	7, 2, 0, 5, 2, 6, 0
};

static readonly sbyte[] _tsip_machine_parser_message_cond_offsets =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 1, 2, 2, 2, 2, 
	2, 2, 2, 2, 2, 2, 2, 2, 
	2, 2, 2, 2, 2, 2
};

static readonly sbyte[] _tsip_machine_parser_message_cond_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 1, 1, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0
};

static readonly int[] _tsip_machine_parser_message_cond_keys =  new int [] {
	'\u000d', '\u000d', '\u000a', '\u000a', 0
};

static readonly sbyte[] _tsip_machine_parser_message_cond_spaces =  new sbyte [] {
	0, 0, 0
};

static readonly byte[] _tsip_machine_parser_message_key_offsets =  new byte [] {
	0, 0, 16, 31, 35, 47, 50, 50, 
	51, 53, 55, 57, 58, 60, 63, 65, 
	68, 69, 70, 76, 77, 78, 79, 96, 
	113, 127, 129, 132, 134, 137, 139, 141, 
	143, 144, 160, 176, 182, 188
};

static readonly int[] _tsip_machine_parser_message_trans_keys =  new int [] {
	'\u0021', '\u0025', '\u0027', '\u0053', '\u0073', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0041', 
	'\u005a', '\u0061', '\u007a', '\u0009', '\u0020', '\u002b', '\u003a', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u0009', 
	'\u0020', '\u003a', '\u0020', '\u0053', '\u0073', '\u0049', '\u0069', '\u0050', 
	'\u0070', '\u002f', '\u0030', '\u0039', '\u002e', '\u0030', '\u0039', '\u0030', 
	'\u0039', '\u000d', '\u0030', '\u0039', '\u000a', '\u000d', 65549, 131085, 
	'\u0000', '\u000c', '\u000e', '\uffff', 131082, '\u000d', '\u000a', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u0049', '\u0069', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u0050', '\u0070', '\u007e', '\u002a', 
	'\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u0020', '\u0021', '\u0025', '\u0027', '\u002f', '\u007e', '\u002a', 
	'\u002b', '\u002d', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0030', 
	'\u0039', '\u002e', '\u0030', '\u0039', '\u0030', '\u0039', '\u0020', '\u0030', 
	'\u0039', '\u0030', '\u0039', '\u0030', '\u0039', '\u0030', '\u0039', '\u0020', 
	'\u0009', '\u000d', '\u0025', '\u003d', '\u005f', '\u007e', '\u0020', '\u0021', 
	'\u0024', '\u003b', '\u003f', '\u005a', '\u0061', '\u007a', '\u0080', '\u00ff', 
	'\u0009', '\u000d', '\u0025', '\u003d', '\u005f', '\u007e', '\u0020', '\u0021', 
	'\u0024', '\u003b', '\u003f', '\u005a', '\u0061', '\u007a', '\u0080', '\u00ff', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u0030', '\u0039', 
	'\u0041', '\u0046', '\u0061', '\u0066', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_message_single_lengths =  new sbyte [] {
	0, 6, 5, 0, 4, 3, 0, 1, 
	2, 2, 2, 1, 0, 1, 0, 1, 
	1, 1, 2, 1, 1, 1, 7, 7, 
	6, 0, 1, 0, 1, 0, 0, 0, 
	1, 6, 6, 0, 0, 0
};

static readonly sbyte[] _tsip_machine_parser_message_range_lengths =  new sbyte [] {
	0, 5, 5, 2, 4, 0, 0, 0, 
	0, 0, 0, 0, 1, 1, 1, 1, 
	0, 0, 2, 0, 0, 0, 5, 5, 
	4, 1, 1, 1, 1, 1, 1, 1, 
	0, 5, 5, 3, 3, 0
};

static readonly byte[] _tsip_machine_parser_message_index_offsets =  new byte [] {
	0, 0, 12, 23, 26, 35, 39, 40, 
	42, 45, 48, 51, 53, 55, 58, 60, 
	63, 65, 67, 72, 74, 76, 78, 91, 
	104, 115, 117, 120, 122, 125, 127, 129, 
	131, 133, 145, 157, 161, 165
};

static readonly sbyte[] _tsip_machine_parser_message_indicies =  new sbyte [] {
	0, 0, 0, 2, 2, 0, 0, 0, 
	0, 0, 0, 1, 3, 4, 4, 4, 
	4, 4, 4, 4, 4, 4, 1, 5, 
	5, 1, 6, 6, 7, 8, 7, 7, 
	7, 7, 1, 6, 6, 8, 1, 9, 
	10, 9, 11, 11, 1, 12, 12, 1, 
	13, 13, 1, 14, 1, 15, 1, 16, 
	15, 1, 17, 1, 18, 17, 1, 19, 
	1, 21, 20, 22, 23, 22, 22, 1, 
	24, 1, 26, 25, 27, 1, 3, 4, 
	4, 4, 28, 28, 4, 4, 4, 4, 
	4, 4, 1, 3, 4, 4, 4, 29, 
	29, 4, 4, 4, 4, 4, 4, 1, 
	3, 4, 4, 4, 30, 4, 4, 4, 
	4, 4, 1, 31, 1, 32, 31, 1, 
	33, 1, 34, 33, 1, 35, 1, 36, 
	1, 37, 1, 38, 1, 39, 40, 41, 
	39, 39, 39, 39, 39, 39, 39, 39, 
	1, 42, 43, 44, 42, 42, 42, 42, 
	42, 42, 42, 42, 1, 45, 45, 45, 
	1, 42, 42, 42, 1, 46, 0
};

static readonly sbyte[] _tsip_machine_parser_message_trans_targs =  new sbyte [] {
	2, 0, 22, 3, 2, 4, 5, 4, 
	6, 7, 8, 9, 10, 11, 12, 13, 
	14, 15, 16, 17, 18, 21, 18, 19, 
	20, 18, 21, 37, 23, 24, 25, 26, 
	27, 28, 29, 30, 31, 32, 33, 34, 
	16, 35, 34, 16, 35, 36, 37
};

static readonly sbyte[] _tsip_machine_parser_message_trans_actions =  new sbyte [] {
	1, 0, 1, 3, 0, 1, 0, 0, 
	0, 0, 5, 1, 0, 0, 0, 0, 
	0, 0, 7, 0, 1, 0, 0, 0, 
	0, 20, 13, 15, 0, 0, 0, 0, 
	0, 0, 7, 1, 0, 0, 9, 1, 
	17, 1, 0, 11, 0, 0, 0
};

const int tsip_machine_parser_message_start = 1;
const int tsip_machine_parser_message_first_final = 37;
const int tsip_machine_parser_message_error = 0;

const int tsip_machine_parser_message_en_main = 1;


/* #line 150 "./ragel/tsip_parser_message.rl" */

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
		if(message != null && state.CS < 
/* #line 196 "../Parsers/TSIP_ParserMessage.cs" */
37
/* #line 164 "./ragel/tsip_parser_message.rl" */
 )
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
		
/* #line 213 "../Parsers/TSIP_ParserMessage.cs" */
	{
	cs = tsip_machine_parser_message_start;
	}

/* #line 178 "./ragel/tsip_parser_message.rl" */

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

		
/* #line 233 "../Parsers/TSIP_ParserMessage.cs" */
	{
	sbyte _klen;
	byte _trans;
	int _widec;
	sbyte _acts;
	sbyte _nacts;
	byte _keys;

	if ( p == pe )
		goto _test_eof;
	if ( cs == 0 )
		goto _out;
_resume:
	_widec = data[p];
	_klen = _tsip_machine_parser_message_cond_lengths[cs];
	_keys = (byte) (_tsip_machine_parser_message_cond_offsets[cs]*2);
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( _widec < _tsip_machine_parser_message_cond_keys[_mid] )
				_upper = (short) (_mid - 2);
			else if ( _widec > _tsip_machine_parser_message_cond_keys[_mid+1] )
				_lower = (short) (_mid + 2);
			else {
				switch ( _tsip_machine_parser_message_cond_spaces[_tsip_machine_parser_message_cond_offsets[cs] + ((_mid - _keys)>>1)] ) {
	case 0: {
		_widec = (int)(65536u + (data[p] - 0u));
		if ( 
/* #line 29 "./ragel/tsip_parser_message.rl" */

		PrevNotComma(state, p)
	 ) _widec += 65536;
		break;
	}
		default: break;
				}
				break;
			}
		}
	}

	_keys = _tsip_machine_parser_message_key_offsets[cs];
	_trans = (byte)_tsip_machine_parser_message_index_offsets[cs];

	_klen = _tsip_machine_parser_message_single_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + ((_upper-_lower) >> 1));
			if ( _widec < _tsip_machine_parser_message_trans_keys[_mid] )
				_upper = (short) (_mid - 1);
			else if ( _widec > _tsip_machine_parser_message_trans_keys[_mid] )
				_lower = (short) (_mid + 1);
			else {
				_trans += (byte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (byte) _klen;
		_trans += (byte) _klen;
	}

	_klen = _tsip_machine_parser_message_range_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( _widec < _tsip_machine_parser_message_trans_keys[_mid] )
				_upper = (short) (_mid - 2);
			else if ( _widec > _tsip_machine_parser_message_trans_keys[_mid+1] )
				_lower = (short) (_mid + 2);
			else {
				_trans += (byte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (byte) _klen;
	}

_match:
	_trans = (byte)_tsip_machine_parser_message_indicies[_trans];
	cs = _tsip_machine_parser_message_trans_targs[_trans];

	if ( _tsip_machine_parser_message_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_message_trans_actions[_trans];
	_nacts = _tsip_machine_parser_message_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_message_actions[_acts++] )
		{
	case 0:
/* #line 30 "./ragel/tsip_parser_message.rl" */
	{
		state.TagStart = p;
	}
	break;
	case 1:
/* #line 36 "./ragel/tsip_parser_message.rl" */
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
	break;
	case 2:
/* #line 53 "./ragel/tsip_parser_message.rl" */
	{
		int len;
		state.TagEnd = p;
		len = (int)(state.TagEnd  - state.TagStart);
        if ((message as TSIP_Request).Uri == null)
        {
            (message as TSIP_Request).Uri = TSIP_ParserUri.Parse(Encoding.UTF8.GetString(state.Data, state.TagStart, len));
        }
	}
	break;
	case 3:
/* #line 65 "./ragel/tsip_parser_message.rl" */
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
	break;
	case 4:
/* #line 79 "./ragel/tsip_parser_message.rl" */
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
	break;
	case 5:
/* #line 98 "./ragel/tsip_parser_message.rl" */
	{
		int len;
		state.TagEnd = p;
		len = (int)(state.TagEnd  - state.TagStart);
        (message as TSIP_Response).ReasonPhrase = Encoding.UTF8.GetString(state.Data, state.TagStart, len);
	}
	break;
	case 6:
/* #line 107 "./ragel/tsip_parser_message.rl" */
	{
		int len;
		state.TagEnd = p;
		len = (int)(state.TagEnd  - state.TagStart);
		
		if(!TSIP_ParserHeader.Parse(state, ref message)){
			TSK_Debug.Error("Failed to parse header at {0}", state.TagStart);
		}
	}
	break;
	case 7:
/* #line 119 "./ragel/tsip_parser_message.rl" */
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
	break;
/* #line 447 "../Parsers/TSIP_ParserMessage.cs" */
		default: break;
		}
	}

_again:
	if ( cs == 0 )
		goto _out;
	if ( ++p != pe )
		goto _resume;
	_test_eof: {}
	_out: {}
	}

/* #line 192 "./ragel/tsip_parser_message.rl" */

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