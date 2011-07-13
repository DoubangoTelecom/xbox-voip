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
using System.Security.Cryptography;

namespace Doubango.tinySAK
{
    public static class TSK_MD5
    {
        public const String EMPTY = "d41d8cd98f00b204e9800998ecf8427e";

        public static String Compute(String input)
        {
            return TSK_MD5.Compute(Encoding.UTF8.GetBytes(input));
        }

        public static String Compute(byte[] input)
        {
#if WINDOWS_PHONE
            byte[] bs = MD5Core.GetHash(input);
#else
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bs = md5Provider.ComputeHash(input);
#endif

            // convert to hexa-strings
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in bs)
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString().ToLower();
        }
    }
}
