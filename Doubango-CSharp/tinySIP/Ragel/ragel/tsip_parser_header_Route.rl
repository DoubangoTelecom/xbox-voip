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
	machine tsip_machine_parser_header_Route;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}
	
	action create_route{
		if(curr_route == null){
			curr_route = new TSIP_HeaderRoute();
		}
	}

	action parse_display_name{
		if(curr_route != null){
			curr_route.DisplayName = TSK_RagelState.Parser.GetString(data, p, tag_start);
			curr_route.DisplayName = TSK_String.UnQuote(curr_route.DisplayName);
		}
	}

	action parse_uri{
		if(curr_route != null && curr_route.Uri == null){
			int len = (int)(p  - tag_start);
            if ((curr_route.Uri = TSIP_ParserUri.Parse(data.Substring(tag_start, len))) != null && !String.IsNullOrEmpty(curr_route.DisplayName))
            {
                curr_route.Uri.DisplayName = curr_route.DisplayName;
			}
		}
	}

	action parse_param{
		if(curr_route != null){
			curr_route.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, curr_route.Params);
		}
	}

	action add_route{
		if(curr_route != null){
			hdr_routes.Add(curr_route);
            curr_route = null;
		}
	}

	action eob{
	}

	
	URI = (scheme HCOLON any+)>tag %parse_uri;
	display_name = (( token LWS )+ | quoted_string)>tag %parse_display_name;
	my_name_addr = display_name? :>LAQUOT<: URI :>RAQUOT;

	rr_param = (generic_param)>tag %parse_param;
	
	#route_param	= 	(my_name_addr ( SEMI rr_param )*)>create_route %add_route;
	#Route	= 	"Route"i HCOLON route_param (COMMA route_param)*;

	route_value	= 	(my_name_addr ( SEMI rr_param )*) >create_route %add_route;
	Route	= 		"Route"i HCOLON route_value (COMMA route_value)*;

	# Entry point
	main := Route :>CRLF @eob;

}%%

using System;
using Doubango.tinySAK;
using System.Collections.Generic;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderRoute : TSIP_Header
	{
		private String mDisplayName;
		private TSIP_Uri mUri;

		public TSIP_HeaderRoute()
            : this(null)
        {
        }

		public TSIP_HeaderRoute(TSIP_Uri uri)
            : base(tsip_header_type_t.Record_Route)
        {
			mUri = uri;
        }

		public override String Value
        {
            get 
            { 
				// Uri with hacked display-name
                return  TSIP_Uri.Serialize(this.Uri, true, true);
            }
            set { TSK_Debug.Error("Not implemented"); }
        }

		public String DisplayName
        {
            get { return mDisplayName; }
            set { mDisplayName = value; }
        }
		
		public TSIP_Uri Uri
        {
            get { return mUri; }
            set { mUri = value; }
        }

		%%write data;

		public static List<TSIP_HeaderRoute> Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			List<TSIP_HeaderRoute> hdr_routes = new List<TSIP_HeaderRoute>();
			TSIP_HeaderRoute curr_route = null;

			int tag_start = 0;

			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Route' header.");
				hdr_routes.Clear();
				hdr_routes = null;
				curr_route.Dispose();
				curr_route = null;
			}
			
			return hdr_routes;
		}
	}
}