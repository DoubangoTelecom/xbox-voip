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
using System.Threading;
using Doubango.tinyNET;
using System.Collections.ObjectModel;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Transports
{
    internal class TSIP_TransportLayer : IDisposable
    {
        private readonly TSIP_Stack mStack;
        private readonly Mutex mMutex;
        private readonly List<TSIP_Transport> mTransports;
        private Boolean mRunning;

        internal TSIP_TransportLayer(TSIP_Stack stack)
        {
            mStack = stack;
            mTransports = new List<TSIP_Transport>();
#if WINDOWS_PHONE
            mMutex = new Mutex(false, TSK_String.Random());
#else
            mMutex = new Mutex();
#endif
        }

        ~TSIP_TransportLayer()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (mMutex != null)
            {
                mMutex.Close();
            }
        }

        internal Boolean IsRunning
        {
            get { return mRunning; }
        }

        internal ReadOnlyCollection<TSIP_Transport> Transports
        {
            get { return mTransports.AsReadOnly(); }
        }

        internal Boolean AddTransport(String localHost, ushort localPort, TNET_Socket.tnet_socket_type_t type, String description)
        {
            TSIP_Transport transport = TNET_Socket.IsStreamType(type) ? (TSIP_Transport)new TSIP_TransportTCP(mStack, localHost, localPort, TNET_Socket.IsIPv6Type(type), description)
                : (TSIP_Transport)new TSIP_TransportUDP(mStack, localHost, localPort, TNET_Socket.IsIPv6Type(type), description);

            if (transport != null)
            {
                // FIXME:DO not forget to call "transport.NetworkEvent -= this.TSIP_Transport_NetworkEvent;" before removing the transport
                transport.NetworkEvent += this.TSIP_Transport_NetworkEvent;
                mTransports.Add(transport);
            }

            return (transport != null);
        }

        internal Boolean SendMessage(String branch, TSIP_Message message)
        {
            String destIP = null;
            ushort destPort = 5060;

            TSIP_Transport transport = this.FindTransport(message, out destIP, out destPort);
            if (transport != null)
            {
                return transport.Send(branch, message, destIP, destPort);
            }

            return false;
        }

        internal Boolean Start()
        {
            foreach (TSIP_Transport transport in mTransports)
            {
                if (!transport.Start())
                {
                    TSK_Debug.Error("Failed to start transport [{0}]", transport.Description);
                    return false;
                }
            }
            mRunning = true;
            return true;
        }

        internal Boolean Stop()
        {
            Boolean ok = true;

            foreach (TSIP_Transport transport in mTransports)
            {
                if (!transport.Start())
                {
                    TSK_Debug.Error("Failed to start transport [{0}]", transport.Description);
                    ok = false;
                }
            }
            mRunning = false;
            return ok; ;
        }

        private void TSIP_Transport_NetworkEvent(object sender, TNET_Transport.TransportEventArgs e)
        {
            if (e.Type != Doubango.tinyNET.TNET_Transport.TransportEventArgs.TransportEventTypes.Data)
            {
                return;
            }

            /* === SigComp === */

            TSIP_Message message = TSIP_ParserMessage.Parse(e.Data, true);

            if (message != null && message.FirstVia != null && message.CSeq != null && message.From != null && message.To != null)
            {
                this.HandleIncomingMessage(message);
            }
            else
            {
                TSK_Debug.Error("Failed to parse message from network");
            }
        }

        private TSIP_Transport FindTransport(TSIP_Message msg, out String destIP, out ushort destPort)
        {
            TSIP_Transport transport = null;

            destIP = mStack.ProxyHost;
            destPort = mStack.ProxyPort;

            /* =========== Sending Request ========= */
            if (msg.IsRequest)
            {
                /* Request are always sent to the Proxy-CSCF */
                foreach (TSIP_Transport t in mTransports)
                {
                    if (t.Type == mStack.ProxyType)
                    {
                        transport = t;
                        break;
                    }
                }
            }

            /* =========== Sending Response ========= */
            else if (msg.FirstVia != null)
            {
                if (!String.Equals(msg.FirstVia.Transport, "UDP", StringComparison.InvariantCultureIgnoreCase)) // Reliable
                {
                    /*	RFC 3261 - 18.2.2 Sending Responses
				        If the "sent-protocol" is a reliable transport protocol such as
				        TCP or SCTP, or TLS over those, the response MUST be sent using
				        the existing connection to the source of the original request
				        that created the transaction, if that connection is still open.
				        This requires the server transport to maintain an association
				        between server transactions and transport connections.  If that
				        connection is no longer open, the server SHOULD open a
				        connection to the IP address in the "received" parameter, if
				        present, using the port in the "sent-by" value, or the default
				        port for that transport, if no port is specified.  If that
				        connection attempt fails, the server SHOULD use the procedures
				        in [4] for servers in order to determine the IP address and
				        port to open the connection and send the response to.
			        */
                }
                else
                {
                    if (!String.IsNullOrEmpty(msg.FirstVia.Maddr))/*== UNRELIABLE MULTICAST ===*/
                    {
                        /*	RFC 3261 - 18.2.2 Sending Responses 
					        Otherwise, if the Via header field value contains a "maddr" parameter, the 
					        response MUST be forwarded to the address listed there, using 
					        the port indicated in "sent-by", or port 5060 if none is present.  
					        If the address is a multicast address, the response SHOULD be 
					        sent using the TTL indicated in the "ttl" parameter, or with a 
					        TTL of 1 if that parameter is not present.
				        */
                    }
                    else /*=== UNRELIABLE UNICAST ===*/
                    {
                        if (!String.IsNullOrEmpty(msg.FirstVia.Received))
                        {
                            if (msg.FirstVia.RPort > 0)
                            {
                                /*	RFC 3581 - 4.  Server Behavior
							        When a server attempts to send a response, it examines the topmost
							        Via header field value of that response.  If the "sent-protocol"
							        component indicates an unreliable unicast transport protocol, such as
							        UDP, and there is no "maddr" parameter, but there is both a
							        "received" parameter and an "rport" parameter, the response MUST be
							        sent to the IP address listed in the "received" parameter, and the
							        port in the "rport" parameter.  The response MUST be sent from the
							        same address and port that the corresponding request was received on.
							        This effectively adds a new processing step between bullets two and
							        three in Section 18.2.2 of SIP [1].
						        */
                                destIP = msg.FirstVia.Received;
                                destPort = (ushort)msg.FirstVia.RPort;
                            }
                            else
                            {
                                /*	RFC 3261 - 18.2.2 Sending Responses
							        Otherwise (for unreliable unicast transports), if the top Via
							        has a "received" parameter, the response MUST be sent to the
							        address in the "received" parameter, using the port indicated
							        in the "sent-by" value, or using port 5060 if none is specified
							        explicitly.  If this fails, for example, elicits an ICMP "port
							        unreachable" response, the procedures of Section 5 of [4]
							        SHOULD be used to determine where to send the response.
						        */
                                destIP = msg.FirstVia.Received;
                                destPort = (ushort)(msg.FirstVia.Port > 0 ? msg.FirstVia.Port : 5060);
                            }
                        }
                        else if (String.IsNullOrEmpty(msg.FirstVia.Received))
                        {
                            /*	RFC 3261 - 18.2.2 Sending Responses
						        Otherwise, if it is not receiver-tagged, the response MUST be
						        sent to the address indicated by the "sent-by" value, using the
						        procedures in Section 5 of [4].
					        */
                            destIP = msg.FirstVia.Host;
                            if (msg.FirstVia.Port > 0)
                            {
                                destPort = (ushort)msg.FirstVia.Port;
                            }
                        }
                    }
                }
            }

            //tsk_list_item_t *item;
            //tsip_transport_t *curr;
            //tsk_list_foreach(item, self->transports)
            //{
            //    curr = item->data;
            //    if(tsip_transport_have_socket(curr, msg->local_fd))
            //    {
            //        transport = curr;
            //        break;
            //    }
            //}


            return transport;
        }

        private Boolean HandleIncomingMessage(TSIP_Message message)
        {
            Boolean ret = false;

            if (message != null)
            {
                if (!(ret = mStack.LayerTransac.HandleIncomingMsg(message)))
                {
                    /* NO MATCHING TRANSACTION FOUND ==> LOOK INTO DIALOG LAYER */
                    ret = mStack.LayerDialog.HandleIncomingMessage(message);
                }
            }
            return ret;
        }
    }
}
