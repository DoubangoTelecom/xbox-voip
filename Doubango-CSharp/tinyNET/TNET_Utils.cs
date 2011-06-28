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
using System.Net;
using Doubango.tinySAK;
using System.Net.Sockets;

namespace Doubango.tinyNET
{
    public static class TNET_Utils
    {
        public static IPAddress GetBestLocalIPAddress(String host, TNET_Socket.tnet_socket_type_t type)
        {
            try
            {
#if WINDOWS_PHONE
                String localHostNameOrAddress = (host == TNET_Socket.TNET_SOCKET_HOST_ANY) ? "localhost" : host; // Not use yet
                IPAddress[] ipAddresses = new IPAddress[] { TNET_Socket.IsIPv6Type(type) ? IPAddress.IPv6Any : IPAddress.Any }; // FIXME: didn't found how to get local IP on WP7
#else
                String localHostNameOrAddress = (host == TNET_Socket.TNET_SOCKET_HOST_ANY) ? Dns.GetHostName() : host;
                IPAddress[] ipAddresses = Dns.GetHostAddresses(localHostNameOrAddress);
#endif
                IPAddress ipAddress = null;
                Boolean useIPv6 = TNET_Socket.IsIPv6Type(type);
                if (ipAddresses != null && ipAddresses.Length > 0)
                {
                    ipAddress = ipAddresses[0];
                    foreach (IPAddress ia in ipAddresses)
                    {
                        if ((ia.AddressFamily == AddressFamily.InterNetwork && (IPAddress.Loopback.ToString().Equals(ia.ToString())))
                            || (ia.AddressFamily == AddressFamily.InterNetworkV6 && (IPAddress.IPv6Loopback.ToString().Equals(ia.ToString()))))
                        {
                            continue;
                        }
                        if ((ia.AddressFamily == AddressFamily.InterNetwork && !useIPv6) || (ia.AddressFamily == AddressFamily.InterNetworkV6 && !ia.IsIPv6LinkLocal && useIPv6))
                        {
                            ipAddress = ia;
                            break;
                        }
                    }
                }

                if (ipAddress == null)
                {
                    TSK_Debug.Error("{0} is an invalid hostname or address", localHostNameOrAddress);
                    ipAddress = TNET_Socket.IsIPv6Type(type) ? IPAddress.IPv6Any : IPAddress.Any;
                }

                return ipAddress;
            }
            catch (Exception e)
            {
                TSK_Debug.Error("GetBestLocalIPAddress(host={0}) failed: ", host, e);
            }
            return TNET_Socket.IsIPv6Type(type) ? IPAddress.IPv6Any : IPAddress.Any;
        }
    }
}
