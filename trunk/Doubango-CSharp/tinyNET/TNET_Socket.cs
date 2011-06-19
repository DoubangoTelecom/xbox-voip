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
using System.Net.Sockets;
using System.Net;
using Doubango.tinySAK;
namespace Doubango.tinyNET
{
    public class TNET_Socket : IDisposable
    {
        #region tnet_socket_type_t
        const Int32 TNET_SOCKET_TYPE_IPV4 = (0x0001 << 0);
        const Int32 TNET_SOCKET_TYPE_UDP = (0x0001 << 1);
        const Int32 TNET_SOCKET_TYPE_TCP = (0x0001 << 2);
        const Int32 TNET_SOCKET_TYPE_TLS = (0x0001 << 3);
        const Int32 TNET_SOCKET_TYPE_SCTP = (0x0001 << 4);

        const Int32 TNET_SOCKET_TYPE_IPSEC = (0x0001 << 8);

        const Int32 TNET_SOCKET_TYPE_IPV6 = (0x0001 << 12);

        const Int32 TNET_SOCKET_TYPE_IPV46 = (TNET_SOCKET_TYPE_IPV4 | TNET_SOCKET_TYPE_IPV6);

        public enum tnet_socket_type_t
        {
	        tnet_socket_type_invalid				= 0x0000, // Invalid socket.*/

	        tnet_socket_type_udp_ipv4				= (TNET_SOCKET_TYPE_IPV4 | TNET_SOCKET_TYPE_UDP), // UDP/IPv4 socket.*/
	        tnet_socket_type_tcp_ipv4				= (TNET_SOCKET_TYPE_IPV4 | TNET_SOCKET_TYPE_TCP), // TCP/IPv4 socket.*/
	        tnet_socket_type_tls_ipv4				= (TNET_SOCKET_TYPE_IPV4 | TNET_SOCKET_TYPE_TLS), // TLS/IPv4 socket.*/
	        tnet_socket_type_sctp_ipv4				= (TNET_SOCKET_TYPE_IPV4 | TNET_SOCKET_TYPE_SCTP), // SCTP/IPv4 socket.*/

	        tnet_socket_type_udp_ipsec_ipv4			= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_udp_ipv4), // UDP/IPSec/IPv4 socket.*/
	        tnet_socket_type_tcp_ipsec_ipv4			= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_tcp_ipv4), // TCP/IPSec/IPv4 socket.*/
	        tnet_socket_type_tls_ipsec_ipv4			= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_tls_ipv4),	// TLS/IPSec /IPv4socket.*/
	        tnet_socket_type_sctp_ipsec_ipv4		= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_sctp_ipv4), // SCTP/IPSec/IPv4 socket.*/

	        tnet_socket_type_udp_ipv6				= (TNET_SOCKET_TYPE_IPV6 | (tnet_socket_type_udp_ipv4 ^ TNET_SOCKET_TYPE_IPV4)),	// UDP/IPv6 socket.*/
	        tnet_socket_type_tcp_ipv6				= (TNET_SOCKET_TYPE_IPV6 | (tnet_socket_type_tcp_ipv4 ^ TNET_SOCKET_TYPE_IPV4)),	// TCP/IPv6 socket.*/
	        tnet_socket_type_tls_ipv6				= (TNET_SOCKET_TYPE_IPV6 | (tnet_socket_type_tls_ipv4 ^ TNET_SOCKET_TYPE_IPV4)),	// TLS/IPv6 socket.*/
	        tnet_socket_type_sctp_ipv6				= (TNET_SOCKET_TYPE_IPV6 | (tnet_socket_type_sctp_ipv4 ^ TNET_SOCKET_TYPE_IPV4)),	// SCTP/IPv6 socket.*/
	        tnet_socket_type_udp_ipsec_ipv6			= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_udp_ipv6), // UDP/IPSec/IPv6 socket.*/
	        tnet_socket_type_tcp_ipsec_ipv6			= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_tcp_ipv6), // TCP/IPSec/IPv6 socket.*/
	        tnet_socket_type_tls_ipsec_ipv6			= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_tls_ipv6),	// TLS/IPSec/IPv6 socket.*/
	        tnet_socket_type_sctp_ipsec_ipv6		= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_sctp_ipv6),// SCTP/IPSec/IPv6 socket.*/

	        tnet_socket_type_udp_ipv46				= (TNET_SOCKET_TYPE_IPV46 | (tnet_socket_type_udp_ipv4 | tnet_socket_type_udp_ipv6)),	// UDP/IPv4/6 socket.*/
	        tnet_socket_type_tcp_ipv46				= (TNET_SOCKET_TYPE_IPV46 | (tnet_socket_type_tcp_ipv4 | tnet_socket_type_tcp_ipv6)),	// TCP/IPv4/6 socket.*/
	        tnet_socket_type_tls_ipv46				= (TNET_SOCKET_TYPE_IPV46 | (tnet_socket_type_tls_ipv4 | tnet_socket_type_tls_ipv6)),	// TLS/IPv4/6 socket.*/
	        tnet_socket_type_sctp_ipv46				= (TNET_SOCKET_TYPE_IPV46 | (tnet_socket_type_sctp_ipv4 | tnet_socket_type_sctp_ipv6)),	// SCTP/IPv4/6 socket.*/
	        tnet_socket_type_udp_ipsec_ipv46		= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_udp_ipv46), // UDP/IPSec/IPv4/6 socket.*/
	        tnet_socket_type_tcp_ipsec_ipv46		= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_tcp_ipv46), // TCP/IPSec/IPv4/6 socket.*/
	        tnet_socket_type_tls_ipsec_ipv46		= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_tls_ipv46),	// TLS/IPSec/IPv4/6 socket.*/
	        tnet_socket_type_sctp_ipsec_ipv46		= (TNET_SOCKET_TYPE_IPSEC | tnet_socket_type_sctp_ipv46),// SCTP/IPSec/IPv4/6 socket.*/
        };
        #endregion

        public const String TNET_SOCKET_HOST_ANY = null;
        public const ushort TNET_SOCKET_PORT_ANY = 0;


        private Socket mSocket;
        private String mHost;
        private ushort mPort;
        private tnet_socket_type_t mType; 

        public TNET_Socket(String host, ushort port, tnet_socket_type_t type, Boolean nonblocking, Boolean bindsocket)
        {
            mType = type;
            mHost = host;
            mPort = port;

            AddressFamily addressFamily = TNET_Socket.IsIPv6Type(type) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork;
            SocketType socketType = TNET_Socket.IsStreamType(type) ? SocketType.Stream : SocketType.Dgram;
            ProtocolType protocolType = TNET_Socket.IsIPv6Type(type) ? ProtocolType.IP : ProtocolType.IP; // Leave it like this
            mSocket = new Socket(addressFamily, socketType, protocolType);
            mSocket.Blocking = !nonblocking;
            mSocket.UseOnlyOverlappedIO = true;
            
            if (bindsocket)
            {
                IPAddress ipAddress = TNET_Utils.GetBestLocalIPAddress(host, type);
                TSK_Debug.Info("Local address={0}", ipAddress);
                mHost = ipAddress.ToString();
                if (bindsocket)
                {
                    ushort localPort = (port == TNET_SOCKET_PORT_ANY) ? (ushort)0 : port;
                    IPEndPoint localIEP = new IPEndPoint(ipAddress, localPort);
                    mSocket.Bind(localIEP);
                    if (TNET_Socket.IsDatagramType(type))
                    {
                        mPort = (ushort)(mSocket.LocalEndPoint as IPEndPoint).Port;
                    }
                }
            }
        }

        public TNET_Socket(String host, ushort port, tnet_socket_type_t type)
            :this(host, port, type, true, true)
        {
        }

        public TNET_Socket(tnet_socket_type_t type)
            : this(TNET_SOCKET_HOST_ANY, TNET_SOCKET_PORT_ANY, type, true, true)
        {
        }

        ~TNET_Socket()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (mSocket != null && mSocket.Connected)
            {
                mSocket.Disconnect(true);
            }
        }

        public tnet_socket_type_t Type
        {
            get { return mType; }
        }

        public String Host
        {
            get { return mHost; }
        }

        public ushort Port
        {
            get { return mPort; }
        }

        public IntPtr Handle
        {
            get { return mSocket != null ? mSocket.Handle : IntPtr.Zero; }
        }

        public Boolean IsValid
        {
            get
            {
                return mSocket != null && this.Type != tnet_socket_type_t.tnet_socket_type_invalid;
            }
        }

        public Boolean IsStream
        {
            get { return  TNET_Socket.IsStreamType(this.Type); }
        }

        public Boolean IsDatagram
        {
            get { return TNET_Socket.IsDatagramType(this.Type); }
        }
        
        public Boolean IsIPv4
        {
            get { return TNET_Socket.IsIPv4Type(this.Type); }
        }

        public Boolean IsIPv6
        {
            get { return TNET_Socket.IsIPv6Type(this.Type); }
        }

        public Boolean IsIPv46
        {
            get { return this.IsIPv4 && this.IsIPv6; }
        }

        public Boolean IsIPSec
        {
            get { return (((int)this.Type & TNET_SOCKET_TYPE_IPSEC) == TNET_SOCKET_TYPE_IPSEC); }
        }

        public Boolean IsUDP
        {
            get { return (((int)this.Type & TNET_SOCKET_TYPE_UDP) == TNET_SOCKET_TYPE_UDP); }
        }

        public Boolean IsTCP
        {
            get { return (((int)this.Type & TNET_SOCKET_TYPE_TCP) == TNET_SOCKET_TYPE_TCP); }
        }

        public Boolean IsTLS
        {
            get { return (((int)this.Type & TNET_SOCKET_TYPE_TLS) == TNET_SOCKET_TYPE_TLS); }
        }

        public Boolean IsSCTP
        {
            get { return (((int)this.Type & TNET_SOCKET_TYPE_SCTP) == TNET_SOCKET_TYPE_SCTP); }
        }

        public Boolean IsSecure
        {
            get { return this.IsIPSec || this.IsTLS; }
        }

        private static Boolean IsIPv4Type(tnet_socket_type_t type)
        {
            return (((int)type & TNET_SOCKET_TYPE_IPV4) == TNET_SOCKET_TYPE_IPV4);
        }

        public static Boolean IsIPv6Type(tnet_socket_type_t type)
        {
            return (((int)type & TNET_SOCKET_TYPE_IPV6) == TNET_SOCKET_TYPE_IPV6);
        }

        public static Boolean IsStreamType(tnet_socket_type_t type)
        {
            return (((int)type & TNET_SOCKET_TYPE_UDP) != TNET_SOCKET_TYPE_UDP);
        }

        public static Boolean IsDatagramType(tnet_socket_type_t type)
        {
            return (((int)type & TNET_SOCKET_TYPE_UDP) == TNET_SOCKET_TYPE_UDP);
        }

        
    }
}
