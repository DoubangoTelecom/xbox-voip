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
	machine tsip_machine_parser_header_Max_Forwards;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}
	
	action parse_value{
		hdr_maxf.MaxForward = TSK_RagelState.Parser.GetInt32(data, p, tag_start);
	}

	action eob{
	}

	Max_Forwards = "Max-Forwards"i HCOLON (DIGIT+)>tag %parse_value;
	
	# Entry point
	main := Max_Forwards :>CRLF @eob;

}%%

using System;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderMaxForwards : TSIP_Header
	{
		private Int32 mMaxForw;

		public const Int32 TSIP_HEADER_MAX_FORWARDS_NONE = -1;
		public const Int32 TSIP_HEADER_MAX_FORWARDS_DEFAULT = 70;

		public TSIP_HeaderMaxForwards()
            : this(TSIP_HEADER_MAX_FORWARDS_NONE)
        {
        }
		
		public TSIP_HeaderMaxForwards(Int32 maxForw)
            : base(tsip_header_type_t.Max_Forwards)
        {
			mMaxForw = maxForw;
        }

		public override String Value
        {
            get 
            { 
               return this.MaxForward.ToString();
            }
            set
            {
                Int32 maxForw;
                if (Int32.TryParse(value, out maxForw))
                {
                    this.MaxForward = maxForw;
                }
            }
        }

		public Int32 MaxForward
        {
            get 
            { 
               return mMaxForw;
            }
            set
            {
                mMaxForw = value;
            }
        }

		%%write data;

		public static TSIP_HeaderMaxForwards Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderMaxForwards hdr_maxf = new TSIP_HeaderMaxForwards();
			
			int tag_start = 0;

			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Content-Length' header.");
				hdr_maxf.Dispose();
				hdr_maxf = null;
			}
			
			return hdr_maxf;
		}
	}
}