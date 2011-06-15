/* Copyright (C) 2010-2011 Mamadou Diop. 
* Copyright (C) 2011 Doubango Telecom <http://www.doubango.org>
*
* Contact: Mamadou Diop <diopmamadou(at)doubango(dot)org>
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
	machine tsip_machine_parser_header_Call_ID;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}
	
	action parse_value{
		hdr_call_id.Id = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action eob{
	}

	callid = word ( "@" word )?;
	Call_ID = ( "Call-ID"i | "i"i ) HCOLON callid>tag %parse_value;
	
	# Entry point
	main := Call_ID :>CRLF @eob;

}%%

using System;
using Doubango_CSharp.tinySAK;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderCallId : TSIP_Header
	{
		private String mId;

		public TSIP_HeaderCallId()
            : this(null)
        {
        }

        public TSIP_HeaderCallId(String id)
            : base(tsip_header_type_t.Call_ID)
        {
           mId = id;
        }

		public override String Value
        {
            get 
            { 
               return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }

		public String Id
        {
            get 
            { 
               return mId;
            }
            set
            {
                mId = value;
            }
        }

		%%write data;

		public static TSIP_HeaderCallId Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderCallId hdr_call_id = new TSIP_HeaderCallId();
			
			int tag_start = 0;

			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Call-ID' header.");
				hdr_call_id.Dispose();
				hdr_call_id = null;
			}
			
			return hdr_call_id;
		}

		public static String RandomCallId()
		{
			return System.Guid.NewGuid().ToString();
		}
	}
}