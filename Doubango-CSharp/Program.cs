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

// http://www.pitorque.de/MisterGoodcat/post/Windows-Phone-7-Mango-Sockets.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango_CSharp.tinySIP;
using Doubango_CSharp.Tests.SIP;
using Doubango_CSharp.tinySIP.Headers;

namespace Doubango_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_UriParser.TestParser();

            TSIP_HeaderContentType header = TSIP_HeaderContentType.Parse("Content-Type: plain/text;charset=utf-8;t=78\r\n");
            if (header != null)
            {
                Console.WriteLine(header.ToString(true, true, true));
            }

            Console.ReadLine();
        }
    }
}
