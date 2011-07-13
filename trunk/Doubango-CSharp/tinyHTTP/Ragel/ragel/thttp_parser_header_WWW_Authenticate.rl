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
	machine thttp_machine_parser_header_WWW_Authenticate;

	# Includes
	include thttp_machine_utils "./ragel/thttp_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}
	
	action is_digest{
		hdr_WWW_Authenticate.Scheme = "Digest";
	}

	action is_basic{
		hdr_WWW_Authenticate.Scheme = "Basic";
	}

	action is_auth{
		hdr_WWW_Authenticate.mType = thttp_header_type_t.WWW_Authenticate;
	}

	action is_proxy{
		hdr_WWW_Authenticate.mType = thttp_header_type_t.Proxy_Authenticate;
	}

	action parse_realm{
		hdr_WWW_Authenticate.Realm = TSK_String.UnQuote(TSK_RagelState.Parser.GetString(data, p, tag_start));
	}

	action parse_domain{
		hdr_WWW_Authenticate.Domain = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_nonce{
		hdr_WWW_Authenticate.Nonce = TSK_String.UnQuote(TSK_RagelState.Parser.GetString(data, p, tag_start));
	}

	action parse_opaque{
		hdr_WWW_Authenticate.Opaque = TSK_String.UnQuote(TSK_RagelState.Parser.GetString(data, p, tag_start));
	}

	action parse_stale{
		String stale = TSK_RagelState.Parser.GetString(data, p, tag_start);
		hdr_WWW_Authenticate.Stale = String.Equals(stale, "true", StringComparison.InvariantCultureIgnoreCase);
	}

	action parse_algorithm{
		hdr_WWW_Authenticate.Algorithm = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_qop{
		hdr_WWW_Authenticate.Qop = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_param{
		hdr_WWW_Authenticate.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, hdr_WWW_Authenticate.Params);
	}

	action prev_not_comma{
		PrevNotComma(data, p, pe)
	}

	action eob{
	}

	#FIXME: Only Digest (MD5, AKAv1-MD5 and AKAv2-MD5) is supported
	other_challenge = (any+);
	auth_param = generic_param>tag %parse_param;

	realm = "realm"i EQUAL quoted_string>tag %parse_realm;
	domain = "domain"i EQUAL LDQUOT <: (any*)>tag %parse_domain :> RDQUOT;
	nonce = "nonce"i EQUAL quoted_string>tag %parse_nonce;
	opaque = "opaque"i EQUAL quoted_string>tag %parse_opaque;
	stale = "stale"i EQUAL ( "true"i | "false"i )>tag %parse_stale;
	algorithm = "algorithm"i EQUAL <:token>tag %parse_algorithm;
	qop_options = "qop"i EQUAL LDQUOT <: (any*)>tag %parse_qop :> RDQUOT;
	
	digest_cln = (realm | domain | nonce | opaque | stale | algorithm | qop_options)@1 | auth_param@0;
	challenge = ( ("Digest"i%is_digest | "Basic"i%is_basic) LWS digest_cln ( (COMMA | CRLF) <:digest_cln )* ) | other_challenge;
	WWW_Authenticate = ("WWW-Authenticate"i>is_auth | "Proxy-Authenticate"i>is_proxy) HCOLON challenge;

	# Entry point
	main := WWW_Authenticate CRLF @eob;

}%%


using System;
using Doubango.tinySAK;

namespace Doubango.tinyHTTP.Headers
{
	public class THTTP_HeaderWWWAuthenticate : THTTP_Header
	{
		private String mScheme;
		private String mRealm;
		private String mDomain;
		private String mNonce;
		private String mOpaque;
		private Boolean mStale;
		private String mAlgorithm;
		private String mQop;

		public THTTP_HeaderWWWAuthenticate()
            :base (thttp_header_type_t.WWW_Authenticate)
        {
        }

		public String Scheme
		{
			get { return mScheme; }
			set { mScheme = value; }
		}

		public String Realm
		{
			get { return mRealm; }
			set { mRealm = value; }
		}

		public String Domain
		{
			get { return mDomain; }
			set { mDomain = value; }
		}

		public String Nonce
		{
			get { return mNonce; }
			set { mNonce = value; }
		}

		public String Opaque
		{
			get { return mOpaque; }
			set { mOpaque = value; }
		}

		public Boolean Stale
		{
			get { return mStale; }
			set { mStale = value; }
		}

		public String Algorithm
		{
			get { return mAlgorithm; }
			set { mAlgorithm = value; }
		}

		public String Qop	
		{
			get { return mQop; }
			set { mQop = value; }
		}

		public override String Value
		{
			get 
			{
				if(!String.IsNullOrEmpty(mScheme))
				{
					return String.Format("{0} realm=\"{1}\"{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13},stale={14}{15}{16}", 
							mScheme,
							!String.IsNullOrEmpty(mRealm) ? mRealm : String.Empty,
							
							!String.IsNullOrEmpty(mDomain) ? ",domain=\"" : String.Empty,
							!String.IsNullOrEmpty(mDomain) ? mDomain : String.Empty,
							!String.IsNullOrEmpty(mDomain) ? "\"" : String.Empty,
							
							
							!String.IsNullOrEmpty(mQop) ? ",qop=\"" : String.Empty,
							!String.IsNullOrEmpty(mQop) ? mQop : String.Empty,
							!String.IsNullOrEmpty(mQop) ? "\"" : String.Empty,


							!String.IsNullOrEmpty(mNonce) ? ",nonce=\"" : String.Empty,
							!String.IsNullOrEmpty(mNonce) ? mNonce : String.Empty,
							!String.IsNullOrEmpty(mNonce) ? "\"" : String.Empty,

							!String.IsNullOrEmpty(mOpaque) ? ",opaque=\"" : String.Empty,
							!String.IsNullOrEmpty(mOpaque) ? mOpaque : String.Empty,
							!String.IsNullOrEmpty(mOpaque) ? "\"" : String.Empty,

							mStale ? "TRUE" : "FALSE",

							!String.IsNullOrEmpty(mAlgorithm) ? ",algorithm=" : String.Empty,
							!String.IsNullOrEmpty(mAlgorithm) ? mAlgorithm : String.Empty
							);
				}
				return String.Empty;
			}
			set { TSK_Debug.Error("Not implemented"); }
		}

		%%write data;

		public static THTTP_HeaderWWWAuthenticate Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			THTTP_HeaderWWWAuthenticate hdr_WWW_Authenticate = new THTTP_HeaderWWWAuthenticate();

			int tag_start = 0;


			%%write init;
			%%write exec;

			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'WWW-Authenticate' header.");
				hdr_WWW_Authenticate.Dispose();
				hdr_WWW_Authenticate = null;
			}

			return hdr_WWW_Authenticate;
		}


		// Check if we have ",CRLF" ==> See WWW-Authenticate header
		// As :>CRLF is preceded by any+ ==> p will be at least (start + 1)
		// p point to CR
		private static Boolean PrevNotComma(String data, int p, int pe)
		{
			return (pe <= p) || ((char)data[p-1] != ',');
		}
	}
}