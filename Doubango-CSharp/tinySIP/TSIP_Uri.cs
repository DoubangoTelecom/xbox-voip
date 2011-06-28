/*Copyright (C) 2010-2011 Mamadou Diop.
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySAK;

namespace Doubango.tinySIP
{
    /// <summary>
    /// Sip Uri types
    /// </summary>
    public enum tsip_uri_type_t
    {
	    Unknown,
	    Sip,
	    Sips,
	    Tel
    };

    /// <summary>
    /// Sip Uri host types
    /// </summary>
    public enum tsip_host_type_t
    {
	    Unknown,
	    Hostname,
	    IPv4,
	    IPv6
    };

    /// <summary>
    /// Sip Uri
    /// </summary>
    public class TSIP_Uri : IDisposable
    {
        private tsip_uri_type_t mType;
	    private String mScheme;
	    private String mHost;
	    private tsip_host_type_t mHostType;
	    private ushort mPort;
	    private String mUserName;
	    private String mPassword;
	    private String mDisplayName;

	    private List<TSK_Param> mParams;

        public TSIP_Uri(tsip_uri_type_t type)
        {
            this.Type = type;
        }

        public void Dispose()
        {
        }

        public tsip_uri_type_t Type
        {
            get { return mType; }
            set { mType = value; }
        }

        public String Scheme
        {
            get { return mScheme; }
            set { mScheme = value; }
        }

        public String Host
        {
            get { return mHost; }
            set { mHost = value; }
        }

        public tsip_host_type_t HostType
        {
            get { return mHostType; }
            set { mHostType = value; }
        }

        public ushort Port
        {
            get { return mPort; }
            set { mPort = value; }
        }

        public String UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }

        public String Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public String DisplayName
        {
            get { return mDisplayName; }
            set { mDisplayName = value; }
        }

        public Boolean IsSecure
        {
            get { return (this.Type == tsip_uri_type_t.Sips); }
        }

        public List<TSK_Param> Params
        {
            get 
            {
                if (mParams == null)
                {
                    mParams = new List<TSK_Param>();
                }
                return mParams; 
            }
        }

        public String Serialize()
        {
            return TSIP_Uri.Serialize(this, true, true);
        }

        public override String ToString()
        {
            return this.ToString(true, true);
        }

        public String ToString(Boolean with_params, Boolean with_quote)
        {
            return TSIP_Uri.Serialize(this, with_params, with_quote);
        }

        public TSIP_Uri Clone()
        {
            return TSIP_Uri.Clone(this, true, true);
        }

        public static TSIP_Uri Create(tsip_uri_type_t type)
        {
            return new TSIP_Uri(type);
        }

        public static TSIP_Uri Create(String uri)
        {
            return TSIP_ParserUri.Parse(uri);
        }

        private static String _InternalSerialize(TSIP_Uri uri, Boolean with_params)
        {
            /* sip:alice:secretword@atlanta.com:65535 */
           String uriString = String.Format("{0}:{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                uri.Scheme!=null ? uri.Scheme : "sip", /* default scheme is sip: */

		        uri.UserName!=null ? uri.UserName : "",

		        uri.Password!=null ? ":" : "",
		        uri.Password!=null ? uri.Password : "",

		        uri.Host!=null ? (uri.UserName!=null ? "@" : "") : "",
		        uri.HostType == tsip_host_type_t.IPv6 ? "[" : "",
		        uri.Host!=null ? uri.Host : "",
		        uri.HostType == tsip_host_type_t.IPv6 ? "]" : "",

		        uri.Port!=0 ? ":" : "",
		        uri.Port!=0 ? uri.Port.ToString() : ""
		        );
        	
	        /* Params */
	        if(with_params && uri.Params.Count > 0){
                uriString += ";";
		        uriString += TSK_Param.ToString(uri.Params, ';');
	        }

            return uriString;
        }

        public static String Serialize(TSIP_Uri uri, Boolean with_params, Boolean with_quote)
        {
            String ret = String.Empty;
            if (uri != null)
            {
                if (with_quote)
                {
                    if (uri.DisplayName != null)
                    {
                        ret += String.Format("\"{0}\"", uri.DisplayName);
                    }

                    ret += "<";
                    ret += _InternalSerialize(uri, with_params);
                    ret += ">";
                }
                else
                {
                    ret += _InternalSerialize(uri, with_params);
                }
            }
            else
            {
                TSK_Debug.Error("Invalid parameter");
            }
            return ret;
        }

        public static String ToString(TSIP_Uri uri, Boolean with_params, Boolean with_quote)
        {
            return TSIP_Uri.Serialize(uri, with_params, with_quote);
        }

        public static TSIP_Uri Clone(TSIP_Uri uri, Boolean with_params, Boolean with_quote)
        {
            String uriString = TSIP_Uri.Serialize(uri, with_params, with_params);
            return TSIP_ParserUri.Parse(uriString);
        }
    }
}
