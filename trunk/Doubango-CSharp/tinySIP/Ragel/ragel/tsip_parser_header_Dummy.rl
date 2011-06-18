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
	machine tsip_machine_parser_header_Dummy;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}

	action parse_name{
		hdr_Dummy.Name = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_value{
		hdr_Dummy.Value = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action eob{
	}
		
	Dummy = token>tag %parse_name SP* HCOLON SP*<: any*>tag %parse_value;
	
	# Entry point
	main := Dummy :>CRLF @eob;

}%%


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderDummy : TSIP_Header
    {
        private String mName;
        private String mValue;

        public TSIP_HeaderDummy() 
            : this(null, null)
        {
        }

        public TSIP_HeaderDummy(String name, String value)
            : base(tsip_header_type_t.Dummy)
        {
            mName = name;
            mValue = value;
        }

       public override string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public override String Value
        {
            get { return mValue; }
            set { mValue = value; }
        }

		%%write data;

		public static TSIP_HeaderDummy Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderDummy hdr_Dummy = new TSIP_HeaderDummy();
			
			int tag_start = 0;
			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse 'Dummy' header");
				hdr_Dummy.Dispose();
				hdr_Dummy = null;
			}
			
			return hdr_Dummy;
		}
    }
}
