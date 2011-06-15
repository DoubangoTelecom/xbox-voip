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

using System;
using Doubango_CSharp.tinySIP;
using Doubango_CSharp.tinySAK;

/***********************************
*	Ragel state machine.
*/
%%{
	machine tsip_machine_parser_uri;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	#include tsip_machine_userinfo;
		
	action tag{
		tag_start = p;
	}

	#/* Sets URI type */
	action is_sip { uri.Scheme = "sip"; uri.Type = tsip_uri_type_t.Sip; }
	action is_sips { uri.Scheme = "sips"; uri.Type = tsip_uri_type_t.Sips; }
	action is_tel { uri.Scheme = "tel"; uri.Type = tsip_uri_type_t.Tel; }

	#/* Sets HOST type */
	action is_ipv4 { uri.HostType = tsip_host_type_t.IPv4; }
	action is_ipv6 { uri.HostType = tsip_host_type_t.IPv6; }
	action is_hostname { uri.HostType = tsip_host_type_t.Hostname; }

	action parse_scheme{
		uri.Scheme = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_user_name{
		uri.UserName = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_password{
		uri.Password = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_host{
		uri.Host = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_port{
		uri.Port = (short)TSK_RagelState.Parser.GetInt32(data, p, tag_start);
	}

	action parse_param{
		TSK_Param param = TSK_RagelState.Parser.GetParam(data, p, tag_start);
        if (param != null)
        {
            uri.Params.Add(param);
        }
	}

	action eob{
	}

	my_uri_parameter = (pname ( "=" pvalue )?) >tag %parse_param;
	uri_parameters = ( ";" my_uri_parameter )*;

	sip_usrinfo		:= ( ( user>tag %parse_user_name ) :> ( ':' password>tag %parse_password )? :>> '@' ) @{ fgoto main; };
	
	main			:= |*
							("sip:"i>tag %is_sip | "sips:"i>tag %is_sips) @100
							{
								if(TSK_String.Contains(data.Substring(te),(pe - te), "@")){
									fgoto sip_usrinfo;
								}
							};
							
							("tel:"i %is_tel (any+)>tag %parse_user_name :> uri_parameters) @100 { };
							
							( (IPv6reference >is_ipv6)@89 | (IPv4address >is_ipv4)@88 | (hostname >is_hostname)@87 ) @90
							{
								uri.Host = TSK_RagelState.Scanner.GetString(data, ts, te);
								if(uri.HostType == tsip_host_type_t.IPv6){
									uri.Host = TSK_String.UnQuote(uri.Host, '[', ']');
								}
							};							

							(":" port)@80
							{
								ts++;
								uri.Port = (ushort)TSK_RagelState.Scanner.GetInt32(data, ts, te);
							};
							
							( uri_parameters ) @70	{  };
							(any)+ @0				{  };

						

					*|;

	#main := ({ fcall SIP_URI; });

}%%

public static class TSIP_ParserUri
{
	%%write data;

	public static TSIP_Uri Parse (String data)
	{
		if(String.IsNullOrEmpty(data))
		{
			return null;
		}

		int cs = 0;
		int p = 0;
		int pe = data.Length;
		int eof = pe;

		int ts = 0, te = 0;
		int act = 0;

		TSIP_Uri uri = TSIP_Uri.Create(tsip_uri_type_t.Unknown);
		
		int tag_start = 0;
		
		%%write init;
		%%write exec;
		
		if( cs < %%{ write first_final; }%% ){
			TSK_Debug.Error("Failed to parse SIP/SIPS/TEL URI");
			uri.Dispose();
			return null;
		}
		
		return uri;
	}
}