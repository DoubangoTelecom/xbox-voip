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
	machine tsip_machine_parser_header_To;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}
	
	action parse_uri{
		int len = (int)(p  - tag_start);
		if(hdr_to != null && hdr_to.Uri == null){
			if((hdr_to.Uri = TSIP_ParserUri.Parse(data.Substring(tag_start, len))) != null && !String.IsNullOrEmpty(hdr_to.DisplayName)){
				hdr_to.Uri.DisplayName = hdr_to.DisplayName;
			}
		}
	}

	action parse_display_name{
		hdr_to.DisplayName = TSK_RagelState.Parser.GetString(data, p, tag_start);
		hdr_to.DisplayName = TSK_String.UnQuote(hdr_to.DisplayName);
	}

	action parse_tag{
		hdr_to.Tag = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_param{
		hdr_to.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, hdr_to.Params);
	}

	action eob{
	}
	
	my_tag_param = "tag"i EQUAL token>tag %parse_tag;
	to_param = my_tag_param@1 | (generic_param)@0 >tag %parse_param;
	
	URI = (scheme HCOLON any+)>tag %parse_uri;
	display_name = (( token LWS )+ | quoted_string)>tag %parse_display_name;
	my_name_addr = display_name? :>LAQUOT<: URI :>RAQUOT;

	To = ( "To"i | "t"i ) HCOLON ( my_name_addr | URI ) ( SEMI to_param )*;
	
	# Entry point
	main := To :>CRLF @eob;

}%%

using System;
using Doubango_CSharp.tinySAK;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderTo : TSIP_Header
	{
		private String mDisplayName;
		private TSIP_Uri mUri;
		private String mTag;

		public TSIP_HeaderTo()
			: this(null,null,null)
		{
		}

		public TSIP_HeaderTo(String displayName, TSIP_Uri uri, String tag)
			: base(tsip_header_type_t.From)
		{
			this.DisplayName = displayName;
			this.Uri = uri;
			this.Tag = tag;
		}

		public override String Value
        {
            get 
            { 
               // Uri with hacked display-name
                String ret = TSIP_Uri.Serialize(this.Uri, true, true);
                if (ret != null && this.Tag != null)
                {
                    ret += String.Format(";tag={0}", this.Tag);
                }
                return ret;
            }
            set { TSK_Debug.Error("Not implemented"); }
        }

		public String DisplayName
		{
			get { return mDisplayName; }
			set { mDisplayName = value;}
		}

		public TSIP_Uri Uri
			{
			get { return mUri; }
			set { mUri = value;}
		}

		public String Tag
		{
			get { return mTag; }
			set { mTag = value;}
		}

		%%write data;

		public static TSIP_HeaderTo Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderTo hdr_to = new TSIP_HeaderTo();

			int tag_start = 0;
			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'To' header.");
				hdr_to.Dispose();
				hdr_to = null;
			}
			
			return hdr_to;
		}
	}
}