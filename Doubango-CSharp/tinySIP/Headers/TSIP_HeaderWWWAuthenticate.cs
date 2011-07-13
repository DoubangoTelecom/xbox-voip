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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinyHTTP.Headers;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderWWWAuthenticate : TSIP_Header
    {
        THTTP_HeaderWWWAuthenticate mEmbeddedHeader;

        public TSIP_HeaderWWWAuthenticate()
            :this(null)
        {

        }

        private TSIP_HeaderWWWAuthenticate(THTTP_HeaderWWWAuthenticate embeddedHeader)
            : base(tsip_header_type_t.WWW_Authenticate)
        {
            mEmbeddedHeader = embeddedHeader;
        }

        private THTTP_HeaderWWWAuthenticate EmbeddedHeader
        {
            get
            {
                if (mEmbeddedHeader == null)
                {
                    mEmbeddedHeader = new THTTP_HeaderWWWAuthenticate();
                }
                return mEmbeddedHeader;
            }
        }

        public override String Value
        {
            get
            {
                return this.EmbeddedHeader.Value;
            }
            set
            {
                this.EmbeddedHeader.Value = value;
            }
        }

        public String Scheme
        {
            get { return this.EmbeddedHeader.Scheme; }
            set { this.EmbeddedHeader.Scheme = value; }
        }

        public String Realm
        {
            get { return this.EmbeddedHeader.Realm; }
            set { this.EmbeddedHeader.Realm = value; }
        }

        public String Domain
        {
            get { return this.EmbeddedHeader.Domain; }
            set { this.EmbeddedHeader.Domain = value; }
        }

        public String Nonce
        {
            get { return this.EmbeddedHeader.Nonce; }
            set { this.EmbeddedHeader.Nonce = value; }
        }

        public String Opaque
        {
            get { return this.EmbeddedHeader.Opaque; }
            set { this.EmbeddedHeader.Opaque = value; }
        }

        public Boolean Stale
        {
            get { return this.EmbeddedHeader.Stale; }
            set { this.EmbeddedHeader.Stale = value; }
        }

        public String Algorithm
        {
            get { return this.EmbeddedHeader.Algorithm; }
            set { this.EmbeddedHeader.Algorithm = value; }
        }

        public String Qop
        {
            get { return this.EmbeddedHeader.Qop; }
            set { this.EmbeddedHeader.Qop = value; }
        }


        public static TSIP_HeaderWWWAuthenticate Parse(String data)
        {
            THTTP_HeaderWWWAuthenticate embeddedHeader = THTTP_HeaderWWWAuthenticate.Parse(data);
            if (embeddedHeader != null)
            {
                return new TSIP_HeaderWWWAuthenticate(embeddedHeader);
            }
            return null;
        }
    }
}
