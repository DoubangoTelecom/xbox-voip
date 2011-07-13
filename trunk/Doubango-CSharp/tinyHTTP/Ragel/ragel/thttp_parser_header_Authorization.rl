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
	machine thttp_machine_parser_header_Authorization;

	# Includes
	include thttp_machine_utils "./ragel/thttp_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}
	
	action is_digest{
		hdr_Authorization.Scheme = "Digest";
	}

	action is_basic{
		hdr_Authorization.Scheme = "Basic";
	}

	action is_auth{
		hdr_Authorization.mType = thttp_header_type_t.Authorization;
	}

	action is_proxy{
		hdr_Authorization.mType = thttp_header_type_t.Proxy_Authorization;
	}

	action parse_username{
		hdr_Authorization.UserName = TSK_String.UnQuote(TSK_RagelState.Parser.GetString(data, p, tag_start));
	}

	action parse_realm{
		hdr_Authorization.Realm = TSK_String.UnQuote(TSK_RagelState.Parser.GetString(data, p, tag_start));
	}

	action parse_nonce{
		hdr_Authorization.Nonce = TSK_String.UnQuote(TSK_RagelState.Parser.GetString(data, p, tag_start));
	}

	action parse_uri{
		hdr_Authorization.Uri = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_response{
		hdr_Authorization.Response = TSK_String.UnQuote(TSK_RagelState.Parser.GetString(data, p, tag_start));
	}

	action parse_algorithm{
		hdr_Authorization.Algorithm = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_cnonce{
		hdr_Authorization.Cnonce = TSK_String.UnQuote(TSK_RagelState.Parser.GetString(data, p, tag_start));
	}

	action parse_opaque{
		hdr_Authorization.Opaque = TSK_String.UnQuote(TSK_RagelState.Parser.GetString(data, p, tag_start));
	}

	action parse_qop{
		hdr_Authorization.Qop = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_nc{
		hdr_Authorization.Nc = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_param{
		hdr_Authorization.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, hdr_Authorization.Params);
	}

	action eob{
	}
	
	#FIXME: Only Digest (MD5, AKAv1-MD5 and AKAv2-MD5) is supported
	qop_value  = "auth" | "auth-int" | token;
	other_response = (any+);
	auth_param = generic_param>tag %parse_param;
	
	username = "username"i EQUAL quoted_string>tag %parse_username;
	realm = "realm"i EQUAL quoted_string>tag %parse_realm;
	nonce = "nonce"i EQUAL quoted_string>tag %parse_nonce;
	digest_uri = "uri"i EQUAL LDQUOT <: (any*)>tag %parse_uri :> RDQUOT;
	#dresponse = "response"i EQUAL LDQUOT <: (LHEX{32})>tag %parse_response :> RDQUOT;
	dresponse = "response"i EQUAL quoted_string>tag %parse_response;
	algorithm = "algorithm"i EQUAL <:token>tag %parse_algorithm;
	cnonce = "cnonce"i EQUAL quoted_string>tag %parse_cnonce;
	opaque = "opaque"i EQUAL quoted_string>tag %parse_opaque;
	message_qop = "qop"i EQUAL qop_value>tag %parse_qop;
	nonce_count = "nc"i EQUAL (LHEX{8})>tag %parse_nc;
	
	dig_resp = (username | realm | nonce | digest_uri | dresponse | algorithm | cnonce | opaque | message_qop | nonce_count)@1 | auth_param@0;
	digest_response = dig_resp ( COMMA <:dig_resp )*;
	credentials = ( ("Digest"i%is_digest | "Basic"i%is_basic) LWS digest_response ) | other_response;
	Authorization = ("Authorization"i>is_auth | "Proxy-Authorization"i>is_proxy) HCOLON credentials;

	# Entry point
	main := Authorization :>CRLF @eob;

}%%

using System;
using Doubango.tinySAK;

namespace Doubango.tinyHTTP.Headers
{
	public class THTTP_HeaderAuthorization : THTTP_Header
	{
		private String mScheme;
		private String mUserName;
		private String mRealm;
		private String mNonce;
		private String mUri;
		private String mResponse;
		private String mAlgorithm;
		private String mCnonce;
		private String mOpaque;
		private String mQop;
		private String mNc;

		public THTTP_HeaderAuthorization()
			:base (thttp_header_type_t.Authorization)
		{
		}


		public String Scheme
		{
			get { return mScheme; }
			set{ mScheme = value; }
		}

		public String UserName
		{
			get { return mUserName; }
			set{ mUserName = value; }
		}

		public String Realm
		{
			get { return mRealm; }
			set{ mRealm = value; }
		}

		public String Nonce
		{
			get { return mNonce; }
			set{ mNonce = value; }
		}

		public String Uri
		{
			get { return mUri; }
			set{ mUri = value; }
		}

		public String Response
		{
			get { return mResponse; }
			set{ mResponse = value; }
		}

		public String Algorithm
		{
			get { return mAlgorithm; }
			set{ mAlgorithm = value; }
		}

		public String Cnonce
		{
			get { return mCnonce; }
			set{ mCnonce = value; }
		}

		public String Opaque
		{
			get { return mOpaque; }
			set{ mOpaque = value; }
		}

		public String Qop
		{
			get { return mQop; }
			set{ mQop = value; }
		}

		public String Nc
		{
			get { return mNc; }
			set{ mNc = value; }
		}

		public override String Value
		{
			get 
			{ 
				String ret = String.Empty;
				if(!String.IsNullOrEmpty(mScheme))
				{
					if(String.Equals(mScheme, "Basic", StringComparison.InvariantCultureIgnoreCase))
					{
						ret += String.Format("{0} {1}", mScheme, mResponse); 
					}
					else
					{
						ret+= String.Format("{0} {1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}",
							mScheme,

							!String.IsNullOrEmpty(mUserName) ? "username=\"" : String.Empty,
							!String.IsNullOrEmpty(mUserName) ? mUserName : String.Empty,
							!String.IsNullOrEmpty(mUserName) ? "\"" : String.Empty,

							!String.IsNullOrEmpty(mRealm) ? ",realm=\"" : String.Empty,
							!String.IsNullOrEmpty(mRealm) ? mRealm : String.Empty,
							!String.IsNullOrEmpty(mRealm) ? "\"" : "",

							!String.IsNullOrEmpty(mNonce) ? ",nonce=\"" : String.Empty,
							!String.IsNullOrEmpty(mNonce) ? mNonce : String.Empty,
							!String.IsNullOrEmpty(mNonce) ? "\"" : "",

							!String.IsNullOrEmpty(mUri) ? ",uri=\"" : String.Empty,
							!String.IsNullOrEmpty(mUri) ? mUri : String.Empty,
							!String.IsNullOrEmpty(mUri) ? "\"" : "",

							!String.IsNullOrEmpty(mResponse) ? ",response=\"" : String.Empty,
							!String.IsNullOrEmpty(mResponse) ? mResponse : String.Empty,
							!String.IsNullOrEmpty(mResponse) ? "\"" : "",

							!String.IsNullOrEmpty(mAlgorithm) ? ",algorithm=" : String.Empty,
							!String.IsNullOrEmpty(mAlgorithm) ? mAlgorithm : String.Empty,

							!String.IsNullOrEmpty(mCnonce) ? ",cnonce=\"" : String.Empty,
							!String.IsNullOrEmpty(mCnonce) ? mCnonce : String.Empty,
							!String.IsNullOrEmpty(mCnonce) ? "\"" : "",

							!String.IsNullOrEmpty(mOpaque) ? ",opaque=\"" : String.Empty,
							!String.IsNullOrEmpty(mOpaque) ? mOpaque : String.Empty,
							!String.IsNullOrEmpty(mOpaque) ? "\"" : "",

							!String.IsNullOrEmpty(mQop) ? ",qop=" : "",
							!String.IsNullOrEmpty(mQop) ? mQop : String.Empty,

							!String.IsNullOrEmpty(mNc) ? ",nc=" : "",
							!String.IsNullOrEmpty(mNc) ? mNc : String.Empty
							);
					}
				}
				return ret; 
			}
			set
			{
				TSK_Debug.Error("Not implemented");
			}            
		}

		%%write data;

		public static THTTP_HeaderAuthorization Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			THTTP_HeaderAuthorization hdr_Authorization = new THTTP_HeaderAuthorization();

			int tag_start = 0;


			%%write init;
			%%write exec;

			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Authorization' header.");
				hdr_Authorization.Dispose();
				hdr_Authorization = null;
			}

			return hdr_Authorization;
		}
	}
}