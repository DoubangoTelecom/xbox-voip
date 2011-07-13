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
	machine tsip_machine_parser_headers;


	# /*== Accept: ==*/
	action parse_header_Accept
	{
		TSK_Debug.Warn("parse_header_Accept NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(header != null)
        {
            message.AddHeader(header);
        }
	}

	# /*== Accept-Contact: ==*/
	action parse_header_Accept_Contact 
	{
		TSK_Debug.Warn("parse_header_Accept_Contact NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(header != null)
        {
            message.AddHeader(header);
        }
	}

	# /*== Accept-Encoding: ==*/
	action parse_header_Accept_Encoding
	{
		TSK_Debug.Warn("parse_header_Accept_Encoding NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(header != null)
        {
            message.AddHeader(header);
        }
	}

	# /*== Accept-Language: ==*/
	action parse_header_Accept_Language
	{
		TSK_Debug.Warn("parse_header_Accept_Language NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(header != null)
        {
            message.AddHeader(header);
        }
	}

	# /*== Accept-Resource-Priority : ==*/
	action parse_header_Accept_Resource_Priority 
	{
		TSK_Debug.Warn("parse_header_Accept_Resource_Priority NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(header != null)
        {
            message.AddHeader(header);
        }
	}

	# /*== Alert-Info: ==*/
	action parse_header_Alert_Info
	{
		TSK_Debug.Warn("parse_header_Alert_Info NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(header != null)
        {
            message.AddHeader(header);
        }
	}

	# /*== Allow: ==*/
	action parse_header_Allow
	{
		TSIP_HeaderAllow header = TSIP_HeaderAllow.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Allow-Events: ==*/
	action parse_header_Allow_Events
	{
		TSIP_HeaderAllowEvents header = TSIP_HeaderAllowEvents.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Authentication-Info: ==*/
	action parse_header_Authentication_Info 
	{
		TSK_Debug.Warn("parse_header_Authentication_Info NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Authorization: ==*/
	action parse_header_Authorization 
	{
		TSIP_HeaderAuthorization header = TSIP_HeaderAuthorization.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Call-ID: ==*/
	action parse_header_Call_ID
	{
		if(message.CallId == null)
        {
            message.CallId = TSIP_HeaderCallId.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        }
        else
        {
            TSK_Debug.Warn("The message already have 'Call-ID' header.");
            TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
            message.AddHeader(header);
        }
	}

	# /*== Call-Info: ==*/
	action parse_header_Call_Info
	{
		TSK_Debug.Warn("parse_header_Call_Info NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(header != null)
        {
            message.AddHeader(header);
        }
	}

	# /*== Contact: ==*/
	action parse_header_Contact 
	{
		List<TSIP_HeaderContact> headers = TSIP_HeaderContact.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(headers != null)
        {
            message.AddHeaders(headers.ToArray());
        }
	}

	# /*== Content-Disposition: ==*/
	action parse_header_Content_Disposition
	{
		TSK_Debug.Warn("parse_header_Content_Disposition NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(header != null)
        {
            message.AddHeader(header);
        }
	}

	# /*== Content-Encoding: ==*/
	action parse_header_Content_Encoding
	{
		TSK_Debug.Warn("parse_header_Content_Encoding NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(header != null)
        {
            message.AddHeader(header);
        }
	}

	# /*== Content-Language: ==*/
	action parse_header_Content_Language
	{
		TSK_Debug.Warn("parse_header_Content_Language NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(header != null)
        {
            message.AddHeader(header);
        }
	}

	# /*== Content-Length: ==*/
	action parse_header_Content_Length
	{
		if(message.ContentLength == null)
        {
            message.ContentLength = TSIP_HeaderContentLength.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        }
        else
        {
            TSK_Debug.Warn("The message already have 'Content-Length' header.");
            TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
            message.AddHeader(header);
        }
	}

	# /*== Content-Type: ==*/
	action parse_header_Content_Type
	{
		if(message.ContentType == null)
        {
            message.ContentType = TSIP_HeaderContentType.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        }
        else
        {
            TSK_Debug.Warn("The message already have 'Content-Length' header.");
            TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
            message.AddHeader(header);
        }
	}

	# /*== CSeq: ==*/
	action parse_header_CSeq
	{
		if(message.CSeq == null)
        {
            message.CSeq = TSIP_HeaderCSeq.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        }
        else
        {
            TSK_Debug.Warn("The message already have 'CSeq' header.");
            TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
            message.AddHeader(header);
        }
	}

	# /*== Date: ==*/
	action parse_header_Date
	{
		TSK_Debug.Warn("parse_header_Date NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Error-Info: ==*/
	action parse_header_Error_Info
	{
		TSK_Debug.Warn("parse_header_Error_Info NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Event: ==*/
	action parse_header_Event
	{
		TSK_Debug.Warn("parse_header_Event NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Event_t *header = tsip_header_Event_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Expires: ==*/
	action parse_header_Expires
	{
		if(message.Expires == null)
        {
            message.Expires = TSIP_HeaderExpires.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        }
        else
        {
            TSK_Debug.Warn("The message already have 'Expires' header.");
            TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
            message.AddHeader(header);
        }
	}

	# /*== From: ==*/
	action parse_header_From
	{
		if(message.From == null)
        {
            message.From = TSIP_HeaderFrom.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        }
        else
        {
            TSK_Debug.Warn("The message already have 'From' header.");
            TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
            message.AddHeader(header);
        }
	}

	# /*== History-Info: ==*/
	action parse_header_History_Info 
	{
		TSK_Debug.Warn("parse_header_History_Info NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Identity: ==*/
	action parse_header_Identity
	{
		TSK_Debug.Warn("parse_header_Identity NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Identity-Info: ==*/
	action parse_header_Identity_Info
	{
		TSK_Debug.Warn("parse_header_Identity_Info NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== In_Reply-To: ==*/
	action parse_header_In_Reply_To
	{
		TSK_Debug.Warn("parse_header_In_Reply_To NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Join: ==*/
	action parse_header_Join
	{
		TSK_Debug.Warn("parse_header_Join NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Max-Forwards: ==*/
	action parse_header_Max_Forwards
	{
		TSIP_HeaderMaxForwards header = TSIP_HeaderMaxForwards.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== MIME-Version: ==*/
	action parse_header_MIME_Version
	{
		TSK_Debug.Warn("parse_header_MIME_Version NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Min-Expires: ==*/
	action parse_header_Min_Expires
	{
		TSK_Debug.Warn("parse_header_Min_Expires NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Min_Expires_t *header = tsip_header_Min_Expires_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Min-SE: ==*/
	action parse_header_Min_SE
	{
		TSK_Debug.Warn("parse_header_Min_SE NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Min_SE_t *header = tsip_header_Min_SE_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Organization: ==*/
	action parse_header_Organization 
	{
		TSK_Debug.Warn("parse_header_Organization NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-Access-Network-Info: ==*/
	action parse_header_P_Access_Network_Info 
	{
		TSK_Debug.Warn("parse_header_P_Access_Network_Info NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_P_Access_Network_Info_t *header = tsip_header_P_Access_Network_Info_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== P-Answer-State: ==*/
	action parse_header_P_Answer_State
	{
		TSK_Debug.Warn("parse_header_P_Answer_State NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-Asserted-Identity: ==*/
	action parse_header_P_Asserted_Identity 
	{
		TSK_Debug.Warn("parse_header_P_Asserted_Identity NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_P_Asserted_Identities_L_t* headers =  tsip_header_P_Asserted_Identity_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADERS(headers);
	}

	# /*== P-Associated-URI: ==*/
	action parse_header_P_Associated_URI
	{
		TSK_Debug.Warn("parse_header_P_Associated_URI NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_P_Associated_URIs_L_t* headers =  tsip_header_P_Associated_URI_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADERS(headers);
	}

	# /*== P-Called-Party-ID: ==*/
	action parse_header_P_Called_Party_ID
	{
		TSK_Debug.Warn("parse_header_P_Called_Party_ID NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-Charging-Function-Addresses : ==*/
	action parse_header_P_Charging_Function_Addresses 
	{
		TSK_Debug.Warn("parse_header_P_Charging_Function_Addresses NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_P_Charging_Function_Addressess_L_t* headers =  tsip_header_P_Charging_Function_Addresses_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADERS(headers);
	}

	# /*== P_Charging_Vector: ==*/
	action parse_header_P_Charging_Vector
	{
		TSK_Debug.Warn("parse_header_P_Charging_Vector NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-DCS-Billing-Info: ==*/
	action parse_header_P_DCS_Billing_Info
	{
		TSK_Debug.Warn("parse_header_P_DCS_Billing_Info NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-DCS-LAES: ==*/
	action parse_header_P_DCS_LAES
	{
		TSK_Debug.Warn("parse_header_P_DCS_LAES NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-DCS-OSPS: ==*/
	action parse_header_P_DCS_OSPS
	{
		TSK_Debug.Warn("parse_header_P_DCS_OSPS NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-DCS-Redirect: ==*/
	action parse_header_P_DCS_Redirect
	{
		TSK_Debug.Warn("parse_header_P_DCS_Redirect NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-DCS-Trace-Party-ID: ==*/
	action parse_header_P_DCS_Trace_Party_ID
	{
		TSK_Debug.Warn("parse_header_P_DCS_Trace_Party_ID NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-Early-Media: ==*/
	action parse_header_P_Early_Media
	{
		TSK_Debug.Warn("parse_header_P_Early_Media NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-Media-Authorization: ==*/
	action parse_header_P_Media_Authorization
	{
		TSK_Debug.Warn("parse_header_P_Media_Authorization NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-Preferred-Identity: ==*/
	action parse_header_P_Preferred_Identity
	{
		TSK_Debug.Warn("parse_header_P_Preferred_Identity NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);

		//tsip_header_P_Preferred_Identity_t *header = tsip_header_P_Preferred_Identity_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== P-Profile-Key: ==*/
	action parse_header_P_Profile_Key
	{
		TSK_Debug.Warn("parse_header_P_Profile_Key NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-User-Database: ==*/
	action parse_header_P_User_Database
	{
		TSK_Debug.Warn("parse_header_P_User_Database NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== P-Visited-Network-ID: ==*/
	action parse_header_P_Visited_Network_ID
	{
		TSK_Debug.Warn("parse_header_P_Visited_Network_ID NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Path: ==*/
	action parse_header_Path
	{
		TSK_Debug.Warn("parse_header_Path NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Paths_L_t* headers =  tsip_header_Path_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADERS(headers);
	}

	# /* == Priority: ==*/
	action parse_header_Priority
	{
		TSK_Debug.Warn("parse_header_Priority NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Privacy: ==*/
	action parse_header_Privacy
	{
		TSK_Debug.Warn("parse_header_Privacy NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Privacy_t *header = tsip_header_Privacy_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Authenticate: ==*/
	action parse_header_Proxy_Authenticate
	{
		TSK_Debug.Warn("parse_header_Proxy_Authenticate NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Proxy_Authenticate_t *header = tsip_header_Proxy_Authenticate_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Proxy-Authorization: ==*/
	action parse_header_Proxy_Authorization
	{
		TSK_Debug.Warn("parse_header_Proxy_Authorization NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Proxy_Authorization_t *header = tsip_header_Proxy_Authorization_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Proxy-Require: ==*/
	action parse_header_Proxy_Require 
	{
		TSIP_HeaderRequire header = TSIP_HeaderRequire.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== RAck: ==*/
	action parse_header_RAck
	{
		TSK_Debug.Warn("parse_header_Proxy_Authorization NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_RAck_t *header = tsip_header_RAck_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Reason: ==*/
	action parse_header_Reason 
	{
		TSK_Debug.Warn("parse_header_Reason NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Record-Route: ==*/
	action parse_header_Record_Route 
	{
		List<TSIP_HeaderRecordRoute> headers = TSIP_HeaderRecordRoute.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(headers != null)
        {
            message.AddHeaders(headers.ToArray());
        }
	}

	# /*== Refer-Sub: ==*/
	action parse_header_Refer_Sub 
	{
		TSK_Debug.Warn("parse_header_Refer_Sub NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Refer_Sub_t *header = tsip_header_Refer_Sub_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Refer-To: ==*/
	action parse_header_Refer_To
	{
		TSK_Debug.Warn("parse_header_Refer_To NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Refer_To_t *header = tsip_header_Refer_To_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Referred-By: ==*/
	action parse_header_Referred_By
	{
		TSK_Debug.Warn("parse_header_Referred_By NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Referred_By_t *header = tsip_header_Referred_By_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Reject-Contact: ==*/
	action parse_header_Reject_Contact
	{
		TSK_Debug.Warn("parse_header_Reject_Contact NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Replaces: ==*/
	action parse_header_Replaces
	{
		TSK_Debug.Warn("parse_header_Replaces NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Reply-To: ==*/
	action parse_header_Reply_To
	{
		TSK_Debug.Warn("parse_header_Reply_To NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Request-Disposition: ==*/
	action parse_header_Request_Disposition
	{
		TSK_Debug.Warn("parse_header_Request_Disposition NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Require: ==*/
	action parse_header_Require
	{
		TSIP_HeaderRequire header = TSIP_HeaderRequire.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Resource-Priority: ==*/
	action parse_header_Resource_Priority
	{
		TSK_Debug.Warn("parse_header_Resource_Priority NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Retry-After: ==*/
	action parse_header_Retry_After
	{
		TSK_Debug.Warn("parse_header_Retry_After NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Route: ==*/
	action parse_header_Route
	{
		List<TSIP_HeaderRoute> headers = TSIP_HeaderRoute.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if (headers != null)
        {
            message.AddHeaders(headers.ToArray());
        }
	}

	# /*== RSeq: ==*/
	action parse_header_RSeq
	{
		TSK_Debug.Warn("parse_header_RSeq NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_RSeq_t *header = tsip_header_RSeq_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Security_Client: ==*/
	action parse_header_Security_Client
	{
		TSK_Debug.Warn("parse_header_Security_Client NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Security_Clients_L_t* headers =  tsip_header_Security_Client_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADERS(headers);
	}

	# /*== Security-Server: ==*/
	action parse_header_Security_Server
	{
		TSK_Debug.Warn("parse_header_Security_Server NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Security_Servers_L_t* headers =  tsip_header_Security_Server_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADERS(headers);
	}

	# /*== Security-Verify: ==*/
	action parse_header_Security_Verify
	{
		TSK_Debug.Warn("parse_header_Security_Verify NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Security_Verifies_L_t* headers =  tsip_header_Security_Verify_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADERS(headers);
	}

	# /*== Server: ==*/
	action parse_header_Server
	{
		TSK_Debug.Warn("parse_header_Server NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Server_t *header = tsip_header_Server_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Service-Route: ==*/
	action parse_header_Service_Route
	{
		TSK_Debug.Warn("parse_header_Service_Route NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Service_Routes_L_t* headers =  tsip_header_Service_Route_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADERS(headers);
	}

	# /*== Session-Expires: ==*/
	action parse_header_Session_Expires
	{
		TSK_Debug.Warn("parse_header_Session_Expires NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Session_Expires_t *header = tsip_header_Session_Expires_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== SIP-ETag: ==*/
	action parse_header_SIP_ETag
	{
		TSK_Debug.Warn("parse_header_SIP_ETag NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_SIP_ETag_t *header = tsip_header_SIP_ETag_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== SIP-If-Match: ==*/
	action parse_header_SIP_If_Match
	{
		TSK_Debug.Warn("parse_header_SIP_If_Match NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_SIP_If_Match_t *header = tsip_header_SIP_If_Match_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Subject: ==*/
	action parse_header_Subject
	{
		TSK_Debug.Warn("parse_header_Subject NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Subscription-State: ==*/
	action parse_header_Subscription_State
	{
		TSK_Debug.Warn("parse_header_Subscription_State NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Subscription_State_t* header =  tsip_header_Subscription_State_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Supported: ==*/
	action parse_header_Supported
	{
		TSIP_HeaderSupported header = TSIP_HeaderSupported.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Target-Dialog: ==*/
	action parse_header_Target_Dialog
	{
		TSK_Debug.Warn("parse_header_Target_Dialog NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== Timestamp: ==*/
	action parse_header_Timestamp
	{
		TSK_Debug.Warn("parse_header_Timestamp NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== To: ==*/
	action parse_header_To
	{
		 if(message.To == null)
        {
            message.To = TSIP_HeaderTo.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        }
        else
        {
            TSK_Debug.Warn("The message already have 'To' header.");
            TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
            message.AddHeader(header);
        }
	}

	# /*== Unsupported: ==*/
	action parse_header_Unsupported
	{
		TSK_Debug.Warn("parse_header_Unsupported NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	# /*== User-Agent: ==*/
	action parse_header_User_Agent
	{
		TSK_Debug.Warn("parse_header_User_Agent NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_User_Agent_t *header = tsip_header_User_Agent_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADER(header);
	}

	# /*== Via: ==*/
	action parse_header_Via
	{	
		List<TSIP_HeaderVia> headers = TSIP_HeaderVia.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        if(headers != null)
        {
            message.AddHeaders(headers.ToArray());
        }
	}

	# /*== Warning: ==*/
	action parse_header_Warning 
	{
		TSK_Debug.Warn("parse_header_Warning NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
		//tsip_header_Warnings_L_t* headers =  tsip_header_Warning_parse(state->tag_start, (state->tag_end-state->tag_start));
		//ADD_HEADERS(headers);
	}

	# /*== WWW-Authenticate: ==*/
	action parse_header_WWW_Authenticate
	{
		TSIP_HeaderWWWAuthenticate header = TSIP_HeaderWWWAuthenticate.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}
		
	# /*== extension_header: ==*/
	action parse_header_extension_header
	{
		TSK_Debug.Warn("parse_header_extension_header NOT IMPLEMENTED. Will be added as Dummy header");
        TSIP_HeaderDummy header = TSIP_HeaderDummy.Parse(Encoding.UTF8.GetString(data, state.TagStart, (state.TagEnd - state.TagStart)));
        message.AddHeader(header);
	}

	action prev_not_comma{
		PrevNotComma(state, p)
	}

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	include tsip_machine_header "./ragel/tsip_machine_header.rl";

	# Entry point
	main := HEADER;
}%%


using System;
using Doubango.tinySIP;
using Doubango.tinySIP.Headers;
using Doubango.tinySAK;
using System.Text;
using System.IO;
using System.Collections.Generic;

public static class TSIP_ParserHeader
{
	%%write data;

	public static Boolean Parse(TSK_RagelState state, ref TSIP_Message message)
	{
		int cs = 0;
		int p = state.TagStart;
		int pe = state.TagEnd;
		int eof = pe;
		byte[] data = state.Data;
		
		%%write init;
		%%write exec;
		
		return ( cs >= %%{ write first_final; }%% );
	}

	// Check if we have ",CRLF" ==> See WWW-Authenticate header
	// As :>CRLF is preceded by any+ ==> p will be at least (start + 1)
	// p point to CR
	private static Boolean PrevNotComma(TSK_RagelState state, int p)
	{
		return (state.PE <= p) || ((char)state.Data[p-1] != ',');
	}
}