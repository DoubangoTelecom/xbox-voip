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
using Doubango.tinySIP.Headers;
using Doubango.tinyHTTP;

namespace Doubango.tinySIP.Authentication
{
    internal class TSIP_Challenge
    {
        private readonly TSIP_Stack mStack;
        private readonly Boolean mProxy;
        private String mScheme;
        private String mRealm;
        private String mNonce;
        private String mOpaque;
        private String mAlgorithm;
        private String mQop;
        private String mCnonce;
        private UInt32 mNc;

        internal TSIP_Challenge(TSIP_Stack stack, Boolean isproxy, String scheme, String realm, String nonce, String opaque, String algorithm, String qop)
        {
            mStack = stack;
            mProxy = isproxy;
            Update(scheme, realm, nonce, opaque, algorithm, qop);
        }

        internal TSIP_Challenge(TSIP_Stack stack)
            :this(stack, false, null, null, null, null, null, null)
        {
        }

        internal String GetResponse(String method, String uristring, byte[] entity_body)
        {
            String response = String.Empty;

            if (IsDigest)
            {
                String ha1 = null, ha2;
                String nc = String.Empty;
                /* ===
			        Calculate HA1 = MD5(A1) = M5(username:realm:secret)
			        In case of AKAv1-MD5 and AKAv2-MD5 the secret must be computed as per RFC 3310 + 3GPP TS 206/7/8/9.
			        The resulting AKA RES parameter is treated as a "password"/"secret" when calculating the response directive of RFC 2617.
		        */
                if (IsAKAv1 || IsAKAv2)
                {
                    TSK_Debug.Error("AKAv1 and AKAv2 are not supported yet");
                    return null;
                }
                else
                {
                    ha1 = THTTP_Auth.GetDigestHA1(ChallengeUserName, mRealm, ChallengePassword);
                }

                /* ===
			        HA2 
		        */
                ha2 = THTTP_Auth.GetDigestHA2(method, uristring, entity_body, mQop);

                /* RESPONSE */
                if (mNc > 0)
                {
                    nc = THTTP_Auth.GetNonceString(mNc).ToString();
                }
                response = THTTP_Auth.GetDigestResponse(ha1, mNonce, mNc, mCnonce, mQop, ha2);

                if (!String.IsNullOrEmpty(mQop))
                {
                    ++mNc;
                }
            }

            return response;
        }

        internal TSIP_Header CreateHeaderAuthorization(TSIP_Request request)
        {
            if (request == null)
            {
                TSK_Debug.Error("Invalid parameter");
                return null;
            }

             String response, nc = null, uristring;

            if (String.IsNullOrEmpty(uristring = TSIP_Uri.ToString(request.Uri, true, false)))
            {
                TSK_Debug.Error("Failed to parse URI: {0}", request.Uri);
                return null;
            }

            /* We compute the nc here because @ref tsip_challenge_get_response function will increment it's value. */
            if (mNc > 0)
            {
                nc = THTTP_Auth.GetNonceString(mNc).ToString();
            }
            /* entity_body ==> request-content */
            response = GetResponse(request.Method, uristring, request.Content);

            if (mProxy)
            {
                // FIXME
                TSK_Debug.Error("Proxy-Authorization header not supported yet");
            }
            else
            {
                TSIP_HeaderAuthorization header = new TSIP_HeaderAuthorization();
                header.UserName = ChallengeUserName;
                header.Scheme = mScheme;
                header.Realm = mRealm;
                header.Nonce = mNonce;
                header.Qop = mQop;
                header.Algorithm = String.IsNullOrEmpty(mAlgorithm) ? "MD5" : mAlgorithm;
                header.Cnonce = mNc > 0 ? mCnonce : null;
                header.Uri = uristring;
                header.Nc = mNc > 0 ? nc : null;
                header.Response = response;

                return header;
            }

            return null;
        }

        internal void Update(String scheme, String realm, String nonce, String opaque, String algorithm, String qop)
        {
            Boolean nonceHasChanged = String.Equals(mNonce, nonce, StringComparison.InvariantCultureIgnoreCase);

            mScheme = scheme;
            mRealm = realm;
            mNonce = nonce;
            mOpaque = opaque;
            mAlgorithm = algorithm;
            if (!String.IsNullOrEmpty(qop))
            {
                mQop = qop.Contains("auth-int") ? "auth-int" : (qop.Contains("auth") ? "auth" : null);
            }

            if (nonceHasChanged && !String.IsNullOrEmpty(mQop))
            {
                ResetCNonce();
            }
        }

        internal static TSIP_Header CreateEmptyAuthorization(String username, String realm, String uristring)
        {
            TSIP_HeaderAuthorization header = new TSIP_HeaderAuthorization();
            header.Scheme = "Digest";
            header.UserName = username;
            header.Realm = realm;
            header.Nonce = String.Empty;
            header.Response = String.Empty;
            header.Uri = uristring;

            return header;
        }

        private Boolean IsDigest
        {
            get { return String.Equals(mScheme, "Digest", StringComparison.InvariantCultureIgnoreCase); }
        }

        private Boolean IsAKAv1
        {
            get { return String.Equals(mScheme, "AKAv1-MD5", StringComparison.InvariantCultureIgnoreCase); }
        }

        private Boolean IsAKAv2
        {
            get { return String.Equals(mScheme, "AKAv2-MD5", StringComparison.InvariantCultureIgnoreCase); }
        }

        private String ChallengeUserName
        {
            get { return mStack.PrivateIdentity; }
        }

        private String ChallengePassword
        {
            get { return mStack.Password; }
        }

        private void ResetCNonce()
        {
            if (!String.IsNullOrEmpty(mQop))
            {
                mCnonce = TSK_MD5.Compute(TSK_String.Random());
                mNc = 1;
            }
        }
    }
}
