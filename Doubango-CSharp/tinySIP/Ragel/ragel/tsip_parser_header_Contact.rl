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
	machine tsip_machine_parser_header_Contact;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}

	action create_contact{
		if(curr_contact == null){
			curr_contact = new TSIP_HeaderContact();
		}
	}

	action parse_display_name{
		if(curr_contact != null){
			curr_contact.DisplayName = TSK_RagelState.Parser.GetString(data, p, tag_start);
			curr_contact.DisplayName = TSK_String.UnQuote(curr_contact.DisplayName);
		}
	}

	action parse_uri{
		if(curr_contact != null && curr_contact.Uri == null){
			int len = (int)(p  - tag_start);
            if ((curr_contact.Uri = TSIP_ParserUri.Parse(data.Substring(tag_start, len))) != null && !String.IsNullOrEmpty(curr_contact.DisplayName))
            {
                curr_contact.Uri.DisplayName = curr_contact.DisplayName;
			}
		}
	}

	action parse_expires{
		if(curr_contact != null){
			curr_contact.Expires = TSK_RagelState.Parser.GetInt64(data, p, tag_start);
		}
	}

	action parse_param{
		if(curr_contact != null){
			curr_contact.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, curr_contact.Params);
		}
	}

	action add_contact{
		if(curr_contact != null){
			hdr_contacts.Add(curr_contact);
            curr_contact = null;
		}
	}
	
	action eob{
	}
	
	URI = (scheme HCOLON any+)>tag %parse_uri;
	display_name = (( token LWS )+ | quoted_string)>tag %parse_display_name;
	my_name_addr = display_name? :>LAQUOT<: URI :>RAQUOT;
	
	c_p_expires = "expires"i EQUAL delta_seconds>tag %parse_expires;
	contact_extension = (generic_param)>tag %parse_param;
	contact_params = c_p_expires@1 | contact_extension@0;
	contact_param = ( (my_name_addr | URI) :> (SEMI contact_params)* ) >create_contact %add_contact;
	Contact = ( "Contact"i | "m"i ) HCOLON ( STAR | ( contact_param ( COMMA contact_param )* ) );
	
	# Entry point
	main := Contact :>CRLF @eob;

}%%

using System;
using Doubango_CSharp.tinySAK;
using System.Collections.Generic;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderContact : TSIP_Header
	{
		private String mDisplayName;
		private TSIP_Uri mUri;
		private Int64 mExpires;

		public TSIP_HeaderContact()
            : this(null, null, 0)
        {
        }

		public TSIP_HeaderContact(String displayName, TSIP_Uri uri, Int64 expires)
            : base(tsip_header_type_t.Contact)
        {
			mDisplayName = displayName;
			mUri = uri;
			mExpires = expires;
        }

		public override String Value
        {
            get 
            { 
				// Uri with hacked display-name
                String ret = TSIP_Uri.Serialize(this.Uri, true, true);
                // Expires 
                if (this.Expires >= 0)
                {
                    ret += String.Format(";expires={0}", this.Expires);
                }
                return ret;
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

		public Int64 Expires
        {
            get { return mExpires; }
            set { mExpires = value; }
        }

		%%write data;

		public static List<TSIP_HeaderContact> Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			List<TSIP_HeaderContact> hdr_contacts = new List<TSIP_HeaderContact>();
			TSIP_HeaderContact curr_contact = null;

			int tag_start = 0;

			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Contact' header.");
				hdr_contacts.Clear();
				hdr_contacts = null;
				curr_contact.Dispose();
				curr_contact = null;
			}
			
			return hdr_contacts;
		}
	}
}