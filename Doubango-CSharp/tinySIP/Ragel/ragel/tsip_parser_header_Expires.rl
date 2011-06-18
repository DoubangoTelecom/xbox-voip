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
	machine tsip_machine_parser_header_Expires;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}

	action parse_delta_seconds{
		hdr_expires.DeltaSeconds = TSK_RagelState.Parser.GetInt64(data, p, tag_start);
	}

	action eob{
	}
		
	Expires = "Expires"i HCOLON delta_seconds>tag %parse_delta_seconds;
	
	# Entry point
	main := Expires :>CRLF @eob;

}%%

using System;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderExpires : TSIP_Header
    {
		private Int64 mDeltaSeconds;
		private const Int64 TSIP_HEADER_EXPIRES_NONE = -1;
		private const Int64 TSIP_HEADER_EXPIRES_DEFAULT = 600000;

        public TSIP_HeaderExpires()
            : this(TSIP_HEADER_EXPIRES_NONE)
        {
        }

        public TSIP_HeaderExpires(Int64 deltaSeconds)
            : base(tsip_header_type_t.Expires)
        {
            mDeltaSeconds = deltaSeconds;
        }

		public Int64 DeltaSeconds
		{
			get{ return  mDeltaSeconds; }
			set{  mDeltaSeconds = value; }
		}

		public override String Value
        {
            get { return mDeltaSeconds >= 0 ? mDeltaSeconds.ToString() : String.Empty; }
            set 
            { 
                Int64 seconds;
                if (Int64.TryParse(value, out seconds))
                {
                    mDeltaSeconds = seconds;
                }
            }
        }

		%%write data;

		public static TSIP_HeaderExpires Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderExpires hdr_expires = new TSIP_HeaderExpires();
			
			int tag_start = 0;
			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse 'Expires' header.");
				hdr_expires.Dispose();
				hdr_expires = null;
			}
			
			return hdr_expires;
		}
    }
}