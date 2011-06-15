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
	machine tsip_machine_parser_header_Content_Length;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}

	action parse_content_length{
		hdr_clength.Length = TSK_RagelState.Parser.GetUInt32(data, p, tag_start);
	}

	action eob{
	}
	
	Content_Length = ( "Content-Length"i | "l"i ) HCOLON (DIGIT+)>tag %parse_content_length;
	
	# Entry point
	main := Content_Length :>CRLF @eob;

}%%


using System;
using Doubango_CSharp.tinySAK;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderContentLength : TSIP_Header
	{
		private UInt32 mLength;

		public TSIP_HeaderContentLength()
            : this(0)
        {
        }
		
		public TSIP_HeaderContentLength(UInt32 length)
            : base(tsip_header_type_t.Content_Length)
        {
			mLength = length;
        }

		public override String Value
        {
            get 
            { 
               return this.Length.ToString();
            }
            set
            {
                UInt32 length;
                if (UInt32.TryParse(value, out length))
                {
                    this.Length = length;
                }
            }
        }

		public UInt32 Length
        {
            get 
            { 
               return mLength;
            }
            set
            {
                mLength = value;
            }
        }

		%%write data;

		public static TSIP_HeaderContentLength Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderContentLength hdr_clength = new TSIP_HeaderContentLength();
			
			int tag_start = 0;

			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Content-Length' header.");
				hdr_clength.Dispose();
				hdr_clength = null;
			}
			
			return hdr_clength;
		}
	}
}