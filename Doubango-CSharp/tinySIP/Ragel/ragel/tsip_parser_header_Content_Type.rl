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
	machine tsip_machine_parser_header_Content_Type;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}

	action parse_content_type{
		hdr_ctype.CType = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_param{		
		 hdr_ctype.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, hdr_ctype.Params);
	}

	action eob{
	}

	extension_token = ietf_token | x_token;

	m_attribute = token;
	m_value = token | quoted_string;
	m_parameter = (m_attribute EQUAL m_value)>tag %parse_param;

	discrete_type = "text"i | "image"i | "audio"i | "video"i | "application"i | extension_token;
	composite_type = "message"i | "multipart"i | extension_token;
	m_type = discrete_type | composite_type;
	m_subtype = extension_token | iana_token;

	media_type = (m_type SLASH m_subtype)@1 >tag %parse_content_type ((SEMI m_parameter)*)@0;

	Content_Type = ( "Content-Type"i | "c"i ) HCOLON media_type;
	
	# Entry point
	main := Content_Type :>CRLF @eob;

}%%

using System;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderContentType : TSIP_Header
	{
		private String mCType;

		public TSIP_HeaderContentType()
            : this(null)
        {
        }
		
		public TSIP_HeaderContentType(String cType)
            : base(tsip_header_type_t.Content_Type)
        {
			mCType = cType;
        }

		public override String Value
        {
            get 
            { 
               return this.CType;
            }
            set
            {
                this.CType = value;
            }
        }

		public String CType
        {
            get 
            { 
               return mCType;
            }
            set
            {
                mCType = value;
            }
        }

		%%write data;

		public static TSIP_HeaderContentType Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderContentType hdr_ctype = new TSIP_HeaderContentType();
			
			int tag_start = 0;

			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Content-Type' header.");
				hdr_ctype.Dispose();
				hdr_ctype = null;
			}
			
			return hdr_ctype;
		}
	}
}
