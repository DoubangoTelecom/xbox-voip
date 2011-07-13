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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySAK;

namespace Doubango.tinyHTTP.Headers
{
    public abstract class THTTP_Header : IDisposable
    {
        public enum thttp_header_type_t
        {
	        Authorization,
	        Content_Length,
	        Content_Type,
	        Dummy,
	        ETag,
	        Proxy_Authenticate,
	        Proxy_Authorization,
	        Transfer_Encoding,
	        WWW_Authenticate,
        };

        protected thttp_header_type_t mType;
        protected List<TSK_Param> mParams;

        public THTTP_Header(thttp_header_type_t type)
        {
            mType = type;
        }

        ~THTTP_Header()
        {
            this.Dispose();
        }

        public void Dispose()
        {
        }

        public thttp_header_type_t Type
        {
            get { return mType; }
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
            set { mParams = value; }
        }

        public virtual String Name
        {
            get { return THTTP_Header.GetName(this); }
            set { }
        }

        public virtual char ParamSeparator
        {
            get { return THTTP_Header.GetParamSeparator(this); }
        }

        public abstract String Value
        {
            get;
            set;
        }

        internal static String GetName(thttp_header_type_t type)
        {
            switch (type)
            {
                case thttp_header_type_t.Authorization: return "Authorization";
		        case thttp_header_type_t.Content_Length: return "Content-Length";
		        case thttp_header_type_t.Content_Type: return "Content-Type";
		        case thttp_header_type_t.ETag: return "ETag";
		        case thttp_header_type_t.Proxy_Authenticate: return "Proxy-Authenticate";
		        case thttp_header_type_t.Proxy_Authorization: return "Proxy-Authorization";
		        case thttp_header_type_t.Transfer_Encoding: return "Transfer-Encoding";
		        case thttp_header_type_t.WWW_Authenticate: return "WWW-Authenticate";

		        default: return "unknown-header";
            }
        }

        internal static String GetName(THTTP_Header header)
        {
            if (header != null)
            {
                if (header.Type == thttp_header_type_t.Dummy)
                {
                    // return (header as THTTP_HeaderDummy).Name;
                }
                else
                {
                    return THTTP_Header.GetName(header.Type);
                }
            }
            return "unknown-header";
        }

        internal static char GetParamSeparator(THTTP_Header header)
        {
            if (header != null)
            {
                switch (header.Type)
                {
                    case thttp_header_type_t.Authorization:
                    case thttp_header_type_t.Proxy_Authorization:
                    case thttp_header_type_t.Proxy_Authenticate:
                    case thttp_header_type_t.WWW_Authenticate:
                        return ',';
                    default:
                        return ';';
                }
            }
            return '\0';
        }

        public override String ToString()
        {
            return this.ToString(true, true, true);
        }

        public String ToString(Boolean with_name, Boolean with_crlf, Boolean with_params)
        {
            return THTTP_Header.ToString(this, with_name, with_crlf, with_params);
        }

        public static String ToString(THTTP_Header header, Boolean with_name, Boolean with_crlf, Boolean with_params)
        {
            if (header != null)
            {
                String @params = String.Empty;
                if (with_params)
                {
                    foreach (TSK_Param param in header.Params)
                    {
                        @params += String.Format(!String.IsNullOrEmpty(param.Value) ? "{0}{1}={2}" : "{0}{1}", header.ParamSeparator, param.Name, param.Value);
                    }
                }
                return String.Format("{0}{1}{2}{3}{4}",
                    with_name ? header.Name : String.Empty,
                    with_name ? ": " : String.Empty,
                    header.Value,
                    @params,
                    with_crlf ? "\r\n" : String.Empty);
            }
            return String.Empty;
        }
    }
}
