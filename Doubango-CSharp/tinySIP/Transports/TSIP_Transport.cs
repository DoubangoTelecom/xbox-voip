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
using Doubango.tinyNET;
using Doubango.tinySAK;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using Doubango.tinySIP.Headers;
using Doubango.tinySIP.Transactions;

namespace Doubango.tinySIP.Transports
{
    internal abstract class TSIP_Transport : TNET_Transport
    {
        private readonly String mScheme;
        private readonly String mProtocol;
        private readonly String mViaProtocol;
        private readonly String mService;
        private readonly TSIP_Stack mStack;

        protected TSIP_Transport(TSIP_Stack stack, String host, ushort port, TNET_Socket.tnet_socket_type_t type, String description)
            :base(host, port, type, description)
        {
            mStack = stack;
            mScheme = TNET_Socket.IsTLSType(type) ? "sips" : "sip";
            if (TNET_Socket.IsStreamType(type))
            {
                mProtocol = "tcp";

                if (TNET_Socket.IsTLSType(type))
                {
                    mViaProtocol = "TLS";
                    mService = "SIPS+D2T";
                }
                else
                {
                    mViaProtocol = "TCP";
                    mService = "SIP+D2T";
                }
            }
            else
            {
                mProtocol = "udp";
                mViaProtocol = "UDP";
                mService = "SIP+D2U";
            }
        }

        protected TSIP_Transport(TSIP_Stack stack, TNET_Socket.tnet_socket_type_t type, String description)
            : this(stack, TNET_Socket.TNET_SOCKET_HOST_ANY, TNET_Socket.TNET_SOCKET_PORT_ANY, type, description)
        {

        }

        ~TSIP_Transport()
        {
            this.Dispose();
        }

        public override void Dispose()
        {
            
        }

        public String Scheme
        {
            get { return mScheme; }
        }

        public String Protocol
        {
            get { return mProtocol; }
        }

        public String ViaProtocol
        {
            get { return mViaProtocol; }
        }

        public String Service
        {
            get { return mService; }
        }

        public Boolean Send(String branch, TSIP_Message msg, String destIP, ushort destPort)
        {
            /* Add Via and update AOR, IPSec headers, SigComp ...
		    * ACK sent from the transaction layer will contains a Via header and should not be updated 
		    * CANCEL will have the same Via and Contact headers as the request it cancel */
            if (msg.IsRequest && (!msg.IsACK || (msg.IsACK && msg.FirstVia == null)) && !msg.IsCANCEL)
            {
                this.AddViaToMessage(branch, msg);/* should be done before tsip_transport_msg_update() which could use the Via header */
                this.AddAoRToMessage(msg);/* AoR */
                this.UpdateMessage(msg);/* IPSec, SigComp, ... */
            }
            else if (msg.IsResponse)
            {
                /* AoR for responses which have a contact header (e.g. 183/200 INVITE) */
                if (msg.Contact != null)
                {
                    this.AddAoRToMessage(msg);
                }

                /*	RFC 3581 - 4.  Server Behavior
				    When a server compliant to this specification (which can be a proxy
				    or UAS) receives a request, it examines the topmost Via header field
				    value.  If this Via header field value contains an "rport" parameter
				    with no value, it MUST set the value of the parameter to the source
				    port of the request.
			    */
                if (msg.FirstVia.RPort == 0)
                {
                    msg.FirstVia.RPort = msg.FirstVia.Port;
                }
            }

            // serialize the message
            byte []bytes = msg.ToBytes();

            if (bytes.Length > 1300)
            {
                /*	RFC 3261 - 18.1.1 Sending Requests (FIXME)
					If a request is within 200 bytes of the path MTU, or if it is larger
					than 1300 bytes and the path MTU is unknown, the request MUST be sent
					using an RFC 2914 [43] congestion controlled transport protocol, such
					as TCP. If this causes a change in the transport protocol from the
					one indicated in the top Via, the value in the top Via MUST be
					changed.  This prevents fragmentation of messages over UDP and
					provides congestion control for larger messages.  However,
					implementations MUST be able to handle messages up to the maximum
					datagram packet size.  For UDP, this size is 65,535 bytes, including
					IP and UDP headers.
				*/
            }

            /* === SigComp === */

            /* === Send the message === */
            //if (/*TNET_Socket.IsIPSec()*/false)
            //{
            //    return false;
            //}
            //else
            //{
                return this.SendBytes(TNET_Socket.CreateEndPoint(destIP, destPort), bytes);
            //}
        }

        /// <summary>
        /// add Via header using the transport config
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="msg"></param>
        private Boolean AddViaToMessage(String branch, TSIP_Message msg)
        {
            String ip;
            ushort port;
            if (!this.GetLocalIpAndPort(out ip, out port))
            {
                return false;
            }

#if WINDOWS_PHONE
            if (port == 0 && (mStack.AoRPort != 0 && !String.IsNullOrEmpty(mStack.AoRIP)))
            {
                port = mStack.AoRPort;
                ip = mStack.AoRIP;
            }
#endif

            /* is there a Via header? */
            if (msg.FirstVia == null)
            {
                /*	RFC 3261 - 18.1.1 Sending Requests
			        Before a request is sent, the client transport MUST insert a value of
			        the "sent-by" field into the Via header field.  This field contains
			        an IP address or host name, and port.  The usage of an FQDN is
			        RECOMMENDED.  This field is used for sending responses under certain
			        conditions, described below.  If the port is absent, the default
			        value depends on the transport.  It is 5060 for UDP, TCP and SCTP,
			        5061 for TLS.
		        */
                msg.FirstVia = new TSIP_HeaderVia(TSIP_HeaderVia.PROTO_NAME_DEFAULT, TSIP_HeaderVia.PROTO_VERSION_DEFAULT,
                    this.ViaProtocol, ip, (UInt16)port);
                msg.FirstVia.Params.Add(new TSK_Param("rport"));
            }

            /* updates the branch */
            if (!String.IsNullOrEmpty(branch))
            {
                msg.FirstVia.Branch = branch;
            }
            else
            {
                msg.FirstVia.Branch = String.Format("{0}_{1}", TSIP_Transac.MAGIC_COOKIE, TSK_String.Random());
            }

            /* multicast case */
            if (false)
            {
                /*	RFC 3261 - 18.1.1 Sending Requests (FIXME)
			        A client that sends a request to a multicast address MUST add the
			        "maddr" parameter to its Via header field value containing the
			        destination multicast address, and for IPv4, SHOULD add the "ttl"
			        parameter with a value of 1.  Usage of IPv6 multicast is not defined
			        in this specification, and will be a subject of future
			        standardization when the need arises.
		        */
            }
            /*
	        * comp=sigcomp; sigcomp-id=
	        */

            return true;
        }

        private Boolean AddAoRToMessage(TSIP_Message msg)
        {
            if (!msg.ShouldUpdate)
            {
                return true;
            }

            /* retrieves the transport ip address and port */
            if (String.IsNullOrEmpty(mStack.AoRIP) && mStack.AoRPort <= 0)
            {
                String ip;
                ushort port;
                if (!GetLocalIpAndPort(out ip, out port))
                {
                    return false;
                }
                else
                {
                    mStack.AoRIP = ip;
                    mStack.AoRPort = port;
                }
            }

            /* === Host and port === */
            if (msg.Contact != null && msg.Contact.Uri != null)
            {
                msg.Contact.Uri.Scheme = this.Scheme;
                msg.Contact.Uri.Host = mStack.AoRIP;
                msg.Contact.Uri.Port = mStack.AoRPort;
            }

            return true;
        }

        internal TSIP_Uri GetUri(Boolean lr)
        {
            Boolean ipv6 = TNET_Socket.IsIPv6Type(this.Type);

            String uristring = String.Format("{0}:{1}{2}{3}:{4};{5};transport={6}",
                this.Scheme,
                ipv6 ? "[" : String.Empty,
                mStack.AoRIP,
                ipv6 ? "]" : String.Empty,
                mStack.AoRPort,
                lr ? "lr" : String.Empty,
                this.Protocol);

            TSIP_Uri uri = TSIP_ParserUri.Parse(uristring);
            uri.HostType = ipv6 ? tsip_host_type_t.IPv6 : tsip_host_type_t.IPv4;
            return uri;
        }

        private Boolean UpdateMessage(TSIP_Message msg)
        {
            if (!msg.ShouldUpdate)
            {
                return true;
            }

            /* === IPSec headers (Security-Client, Security-Verify, Sec-Agree ...) === */

            /* === SigComp === */

            msg.ShouldUpdate = false; /* To avoid to update retrans. */

            return true;
        }

        private Boolean SendBytes(EndPoint toEP, byte[] bytes)
        {
            if (TNET_Socket.IsStreamType(this.Type))
            {
                TSK_Debug.Error("Not implemented");
                return false;
            }
            else
            {
                return base.SendTo(toEP, bytes) > 0;
            }
        }
    }
}
