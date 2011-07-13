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

namespace Doubango.tinyHTTP
{
    public static class THTTP_Auth
    {
        /// <summary>
        /// Generates HTTP-basic response as per RFC 2617.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static String GetBasicResponse(String userid, String password)
        {
            /* RFC 2617 - 2 Basic Authentication Scheme
	
	            To receive authorization, the client sends the userid and password,
	            separated by a single colon (":") character, within a base64 [7]
	            encoded string in the credentials.
	            */
            return TSK_Base64.Encode(String.Format("{0}:{1}", userid, password));
        }

        /// <summary>
        /// Generates digest HA1 value as per RFC 2617 subclause 3.2.2.2. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="realm"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static String GetDigestHA1(String username, String realm, String password)
        {
            /* RFC 2617 - 3.2.2.2 A1
		        A1       = unq(username-value) ":" unq(realm-value) ":" passwd
	        */
            String A1 = String.Format("{0}:{1}:{2}", username, realm, password);
            return TSK_MD5.Compute(A1);
        }

        /// <summary>
        /// Generates digest HA1 value for 'MD5-sess' algo as per RFC 2617 subclause 3.2.2.2.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="realm"></param>
        /// <param name="password"></param>
        /// <param name="nonce"></param>
        /// <param name="cnonce"></param>
        /// <returns></returns>
        public static String GetDigestHA1Sess(String username, String realm, String password, String nonce, String cnonce)
        {
            String A1Sess = String.Format("{0}:{1}:{2}:{3}:{4}", username, realm, password, nonce, cnonce);
            return TSK_MD5.Compute(A1Sess);
        }

        /// <summary>
        /// Generates digest HA2 value as per RFC 2617 subclause 3.2.2.3.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="entity_body"></param>
        /// <returns></returns>
        public static String GetDigestHA2(String method, String url, byte[] entity_body, String qop)
        {
            /* RFC 2617 - 3.2.2.3 A2

	        If the "qop" directive's value is "auth" or is unspecified, then A2
	        is:
	        A2       = Method ":" digest-url-value

	        If the "qop" value is "auth-int", then A2 is:
	        A2       = Method ":" digest-url-value ":" H(entity-body)
	        */
            String A2 = String.Empty;
            if (String.IsNullOrEmpty(qop) || String.Equals(qop, "auth", StringComparison.InvariantCultureIgnoreCase))
            {
                A2 = String.Format("{0}:{1}", method, url);
            }
            else if (String.Equals(qop, "auth-int", StringComparison.InvariantCultureIgnoreCase))
            {
                if (entity_body != null && entity_body.Length > 0)
                {
                    String hEntity = TSK_MD5.Compute(entity_body);
                    A2 = String.Format("{0}:{1}:{2}", method, url, hEntity);
                }
                else
                {
                    A2 = String.Format("{0}:{1}:{2}", method, url, TSK_MD5.EMPTY);
                }
            }

            return TSK_MD5.Compute(A2);
        }

        /// <summary>
        /// Generates HTTP digest response as per RFC 2617 subclause 3.2.2.1.
        /// </summary>
        /// <param name="ha1">HA1 string generated using  @ref thttp_auth_digest_HA1 or @ref thttp_auth_digest_HA1sess.</param>
        /// <param name="nonce">The nonce value</param>
        /// <param name="noncecount">The nonce count</param>
        /// <param name="cnonce">The client nounce (unquoted)</param>
        /// <param name="qop">The Quality Of Protection (unquoted)</param>
        /// <param name="ha2">HA2 string generated using @ref thttp_auth_digest_HA2</param>
        /// <returns></returns>
        public static String GetDigestResponse(String ha1, String nonce, UInt32 noncecount, String cnonce, String qop, String ha2)
        {
            /* RFC 2617 3.2.2.1 Request-Digest

	            ============ CASE 1 ============
	            If the "qop" value is "auth" or "auth-int":
	            request-digest  = <"> < KD ( H(A1),     unq(nonce-value)
	            ":" nc-value
	            ":" unq(cnonce-value)
	            ":" unq(qop-value)
	            ":" H(A2)
	            ) <">
	            ============ CASE 2 ============
	            If the "qop" directive is not present (this construction is for
	            compatibility with RFC 2069):
	            request-digest  =
	            <"> < KD ( H(A1), unq(nonce-value) ":" H(A2) ) >
	            <">
	            */

            String res;
            if (String.Equals(qop, "auth", StringComparison.InvariantCultureIgnoreCase) || String.Equals(qop, "auth-int", StringComparison.InvariantCultureIgnoreCase))
            {
                /* CASE 1 */
                res = String.Format("{0}:{1}:{2}:{3}:{4}:{5}", ha1, nonce, noncecount, cnonce, qop, ha2);
            }
            else
            {
                /* CASE 2 */
                res = String.Format("{0}:{1}:{2}", ha1, nonce, ha2);
            }

            return TSK_MD5.Compute(res);
        }

        public static char[] GetNonceString(UInt32 nc_int32)
        {
            char[] nc_string = new char[9];
            int i = 7;
		    do{
			    nc_string[7-i]= "0123456789abcdef"[(int)(nc_int32 >> i*4) & 0xF];
		    }
		    while(i-->0);

            nc_string[8] = '\0';

            return nc_string;
        }
    }
}
