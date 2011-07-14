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
using System.Threading;
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

        public static Int64 sUniqueId = 0;

        private readonly Int64 mId;
        private readonly ManualResetEvent mSocketEvent;
        private readonly Socket mSocket;
        private readonly SocketAsyncEventArgs mSendSocketEventArgs;
        private SocketAsyncEventArgs mRecvSocketEventArgs;
        public event EventHandler<SocketAsyncEventArgs> RecvSocketCompleted;
        private readonly String mHost;
        private readonly ushort mPort;
        private readonly tnet_socket_type_t mType;

        private const Int32 MAX_RCV_BUFFER_SIZE = 0xFFFF;

        // Define a timeout in milliseconds for each asynchronous call. If a response is not received within this 
        // timeout period, the call is aborted.
        private const int TIMEOUT_MILLISECONDS = 5000;

        byte[] buffers = new byte[MAX_RCV_BUFFER_SIZE];

        public TNET_Socket(String host, ushort port, tnet_socket_type_t type, Boolean nonblocking, Boolean bindsocket)
        {
            mId = ++sUniqueId;
            mType = type;
            mHost = host;
            mPort = port;
            mSocketEvent = new ManualResetEvent(false);

            mSendSocketEventArgs = new SocketAsyncEventArgs();
            mSendSocketEventArgs.Completed += delegate(object sender, SocketAsyncEventArgs e)
            {

                if (e.SocketError != SocketError.Success)
                {
                    mSocketEvent.Set();
                    return;
                }

                // On WP7 Socket.ReceiveFromAsync() will only works after at least ONE SendTo()
                if (e.LastOperation == SocketAsyncOperation.SendTo)
                {
                    if (mRecvSocketEventArgs == null && RecvSocketCompleted != null)
                    {
                        mRecvSocketEventArgs = new SocketAsyncEventArgs();
                        
                        // setting the remote endpoint will not filter on the remote host:port
                        // => will continue to work even if the remote port is different than 5060
                        // IPEndPoint is not serializable => create new one
                        mRecvSocketEventArgs.RemoteEndPoint = new IPEndPoint(IPAddress.Any, 5060); // e.RemoteEndPoint
                        mRecvSocketEventArgs.Completed += this.RecvSocketCompleted;
                        
                        mRecvSocketEventArgs.SetBuffer(buffers, 0, buffers.Length);
                        mRecvSocketEventArgs.UserToken = e.UserToken;
                        (mRecvSocketEventArgs.UserToken as TNET_Socket).Socket.ReceiveFromAsync(mRecvSocketEventArgs);
                    }
                }

                mSocketEvent.Set();
            };

            AddressFamily addressFamily = TNET_Socket.IsIPv6Type(type) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork;
            SocketType socketType = TNET_Socket.IsStreamType(type) ? SocketType.Stream : SocketType.Dgram;
#if WINDOWS_PHONE
            ProtocolType protocolType = TNET_Socket.IsStreamType(type) ? ProtocolType.Tcp : ProtocolType.Udp; 
#else
            ProtocolType protocolType = TNET_Socket.IsIPv6Type(type) ? ProtocolType.IP : ProtocolType.IP; // Leave it like this
#endif
            mSocket = new Socket(addressFamily, socketType, protocolType);
#if !WINDOWS_PHONE
            mSocket.Blocking = !nonblocking;
            mSocket.UseOnlyOverlappedIO = true;
#endif

            mSendSocketEventArgs.UserToken = this;

            if (bindsocket)
            {
                IPAddress ipAddress = TNET_Utils.GetBestLocalIPAddress(host, type);
                TSK_Debug.Info("Local address={0}", ipAddress);
                mHost = ipAddress.ToString();
                if (bindsocket)
                {
                    ushort localPort = (port == TNET_SOCKET_PORT_ANY) ? (ushort)0 : port;
                    IPEndPoint localIEP = new IPEndPoint(ipAddress, localPort);
#if !WINDOWS_PHONE
                    mSocket.Bind(localIEP);
#endif
                    if (TNET_Socket.IsDatagramType(type))
                    {
 #if !WINDOWS_PHONE 
                        mPort = (ushort)(mSocket.LocalEndPoint as IPEndPoint).Port;
#endif
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
                mSocket.Shutdown(SocketShutdown.Both);
#if !WINDOWS_PHONE
                mSocket.Disconnect(true);
#endif
            }
        }

        internal Socket Socket
        {
            get { return mSocket; }
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

        public Int64 Id
        {
            get { return mId; }
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
            get { return TNET_Socket.IsTLSType(this.Type); }
        }

        public Boolean IsSCTP
        {
            get { return (((int)this.Type & TNET_SOCKET_TYPE_SCTP) == TNET_SOCKET_TYPE_SCTP); }
        }

        public Boolean IsSecure
        {
            get { return this.IsIPSec || this.IsTLS; }
        }

        internal Boolean ScheduleReceiveFromAsync()
        {
            /*if (mRecvSocketEventArgs != null && mSocket != null)
            {
                mRecvSocketEventArgs = new SocketAsyncEventArgs();
                // setting the remote endpoint will not filter on the remote host:port
                // => will continue to work even if the remote port is different than 5060
                // IPEndPoint is not serializable => create new one
                mRecvSocketEventArgs.RemoteEndPoint = new IPEndPoint(IPAddress.Any, 5060); // e.RemoteEndPoint
                mRecvSocketEventArgs.Completed += this.RecvSocketCompleted;
                byte[] buffers = new byte[MAX_RCV_BUFFER_SIZE];
                mRecvSocketEventArgs.SetBuffer(buffers, 0, buffers.Length);
                mRecvSocketEventArgs.UserToken = this;
                return mSocket.ReceiveAsync(mRecvSocketEventArgs);
            }*/
            return false;
        }

        public Int32 SendTo(EndPoint remoteEP, byte[] buffer)
        {
            try
            {
//#if WINDOWS_PHONE
                Int32 ret;
                mSocketEvent.Reset();
                mSendSocketEventArgs.SetBuffer(buffer, 0, buffer.Length);
                mSendSocketEventArgs.RemoteEndPoint = remoteEP;
                ret = mSocket.SendToAsync(mSendSocketEventArgs) ? buffer.Length : -1;
                mSocketEvent.WaitOne(TIMEOUT_MILLISECONDS);

                return ret;
//#else
                
//               return mSocket.SendTo(buffer, remoteEP);
//#endif
            }
            catch (Exception e)
            {
                TSK_Debug.Error("SendTo() failed: {0}", e);
            }
            return -1;
        }

        /// <summary>
        /// Gets local ip and address (FIXME: add support for STUN).
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public Boolean GetLocalIpAndPort(out String ip, out ushort port)
        {
            ip = this.IsIPv6 ? "::" : "0.0.0.1";
            port = 5060;

            if (mSocket != null)
            {
                ip = this.Host;
                port = this.Port;
                return true;
            }
            return false;
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

        public static Boolean IsTLSType(tnet_socket_type_t type)
        {
            return (((int)type & TNET_SOCKET_TYPE_TLS) == TNET_SOCKET_TYPE_TLS);
        }

        public static EndPoint CreateEndPoint(String host, ushort port)
        {
            try
            {
#if WINDOWS_PHONE
                return new DnsEndPoint(host, port);
#else
                return new IPEndPoint(Dns.GetHostAddresses(host)[0], port);
#endif
            }
            catch (Exception e)
            {
                TSK_Debug.Error("CreateEndPoint failed: {0}", e);
            }
            return null;
        }
    }
}
