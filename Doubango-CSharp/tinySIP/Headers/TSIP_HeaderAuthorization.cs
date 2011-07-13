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
    public class TSIP_HeaderAuthorization : TSIP_Header
    {
        THTTP_HeaderAuthorization mEmbeddedHeader;

        public TSIP_HeaderAuthorization()
            :this(null)
        {

        }

        private TSIP_HeaderAuthorization(THTTP_HeaderAuthorization embeddedHeader)
            : base(tsip_header_type_t.Authorization)
        {
            mEmbeddedHeader = embeddedHeader;
        }

        private THTTP_HeaderAuthorization EmbeddedHeader
        {
            get
            {
                if (mEmbeddedHeader == null)
                {
                    mEmbeddedHeader = new THTTP_HeaderAuthorization();
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

        public String UserName
        {
            get { return this.EmbeddedHeader.UserName; }
            set { this.EmbeddedHeader.UserName = value; }
        }

        public String Realm
        {
            get { return this.EmbeddedHeader.Realm; }
            set { this.EmbeddedHeader.Realm = value; }
        }

        public String Nonce
        {
            get { return this.EmbeddedHeader.Nonce; }
            set { this.EmbeddedHeader.Nonce = value; }
        }

        public String Uri
        {
            get { return this.EmbeddedHeader.Uri; }
            set { this.EmbeddedHeader.Uri = value; }
        }

        public String Response
        {
            get { return this.EmbeddedHeader.Response; }
            set { this.EmbeddedHeader.Response = value; }
        }

        public String Algorithm
        {
            get { return this.EmbeddedHeader.Algorithm; }
            set { this.EmbeddedHeader.Algorithm = value; }
        }

        public String Cnonce
        {
            get { return this.EmbeddedHeader.Cnonce; }
            set { this.EmbeddedHeader.Cnonce = value; }
        }

        public String Opaque
        {
            get { return this.EmbeddedHeader.Opaque; }
            set { this.EmbeddedHeader.Opaque = value; }
        }

        public String Qop
        {
            get { return this.EmbeddedHeader.Qop; }
            set { this.EmbeddedHeader.Qop = value; }
        }

        public String Nc
        {
            get { return this.EmbeddedHeader.Nc; }
            set { this.EmbeddedHeader.Nc = value; }
        }

        public static TSIP_HeaderAuthorization Parse(String data)
        {
            THTTP_HeaderAuthorization embeddedHeader = THTTP_HeaderAuthorization.Parse(data);
            if (embeddedHeader != null)
            {
                return new TSIP_HeaderAuthorization(embeddedHeader);
            }
            return null;
        }

    }
}
