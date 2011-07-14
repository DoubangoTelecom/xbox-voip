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
using Doubango.tinyNET;
using Doubango.tinySIP.Headers;
using Doubango.tinySIP.Dialogs;
using Doubango.tinySIP.Transactions;
using Doubango.tinySIP.Transports;
using Doubango.tinySIP.Events;

namespace Doubango.tinySIP
{
    public class TSIP_Stack : IDisposable
    {
        private String mDisplayName;
        private TSIP_Uri mPublicIdentity;
        private TSIP_Uri mPreferredIdentity;
        private String mPrivateIdentity;
        private String mPassword;

        private Boolean mEarlyIMS;
        private TSIP_Uri mRealm;
        private String mLocalIP;
        private ushort mLocalPort;
        private String mProxyHost;
        private ushort mProxyPort;
        private TNET_Socket.tnet_socket_type_t mProxyType;

        private String mAoRIP;
        private ushort mAoRPort;

        private readonly List<TSK_Param> mHeaders;

        private readonly IDictionary<Int64,TSip_Session> mSipSessions;
        private readonly TSIP_DialogLayer mLayerDialog;
        private readonly TSIP_TransacLayer mLayerTransac;
        private readonly TSIP_TransportLayer mLayerTransport;

        private Boolean mValid;
        private Boolean mRunning;

        public TSIP_Event.onEvent Callback;

        public TSIP_Stack(TSIP_Uri domainName, String privateIdentity, TSIP_Uri publicIdentity, String proxyHost, ushort proxyPort)
        {
            mSipSessions = new Dictionary<Int64, TSip_Session>();
            mHeaders = new List<TSK_Param>();

            if (domainName == null || String.IsNullOrEmpty(privateIdentity) || publicIdentity == null)
            {
                TSK_Debug.Error("Invalid parameter");
                goto bail;
            }

            mRealm = domainName;
            mPrivateIdentity = privateIdentity;
            mPublicIdentity = publicIdentity;
            mPreferredIdentity = mPublicIdentity;
            mProxyHost = String.IsNullOrEmpty(proxyHost) ? domainName.Host : proxyHost;
            mProxyPort = proxyPort == 0 ? (ushort)5060 : proxyPort;

            /* === Default values === */
            mLocalIP = TNET_Socket.TNET_SOCKET_HOST_ANY;
            mLocalPort = TNET_Socket.TNET_SOCKET_PORT_ANY;
            mProxyType = TNET_Socket.tnet_socket_type_t.tnet_socket_type_udp_ipv4;

            mLayerDialog = new TSIP_DialogLayer(this);
            mLayerTransac = new TSIP_TransacLayer(this);
            mLayerTransport = new TSIP_TransportLayer(this);


            mValid = true;

            

        bail: ;
        }

        ~TSIP_Stack()
        {
            this.Dispose();
        }

        public void Dispose()
        {
        }

        public Boolean IsValid
        {
            get { return mValid; }
        }

        public Boolean IsRunning
        {
            get { return mRunning; }
        }

        internal String LocalIP
        {
            get { return mLocalIP; }
            set { mLocalIP = value; }
        }

        internal TSIP_Uri Realm
        {
            get { return mRealm; }
        }

        internal String PrivateIdentity
        {
            get { return mPrivateIdentity; }
        }

        internal TSIP_Uri PublicIdentity
        {
            get { return mPublicIdentity; }
        }

        internal TSIP_Uri PreferredIdentity
        {
            get { return mPreferredIdentity; }
        }

        internal String Password
        {
            get { return mPassword; }
        }

        internal Boolean EarlyIMS
        {
            get { return mEarlyIMS; }
        }

        public List<TSK_Param> Headers
        {
            get {  return mHeaders; }
        }

        internal TNET_Socket.tnet_socket_type_t ProxyType
        {
            get { return mProxyType; }
        }

        internal String ProxyHost
        {
            get { return mProxyHost; }
        }

        internal ushort ProxyPort
        {
            get { return mProxyPort; }
        }

        internal TSIP_DialogLayer LayerDialog
        {
            get { return mLayerDialog; }
        }

        internal TSIP_TransacLayer LayerTransac
        {
            get { return mLayerTransac; }
        }

        internal TSIP_TransportLayer LayerTransport
        {
            get { return mLayerTransport; }
        }

        public String AoRIP
        {
            get { return mAoRIP; }
            set { mAoRIP = value; }
        }

        public ushort AoRPort
        {
            get { return mAoRPort; }
            set { mAoRPort = value; }
        }

        internal void AddSession(TSip_Session session)
        {
            if (session != null && !mSipSessions.ContainsKey(session.Id))
            {
                mSipSessions.Add(session.Id, session);
            }
        }

        internal void RemoveSession(Int64 sessionId)
        {
            mSipSessions.Remove(sessionId);
        }

        public Boolean Start()
        {
            Boolean ok = false;
            if (mRunning)
            {
                TSK_Debug.Warn("Stack already running");
                ok = true;
                goto bail;
            }

            /* === Transport Layer === */
            /* Adds the default transport to the transport Layer */
            if (!mLayerTransport.AddTransport(mLocalIP, mLocalPort, mProxyType, "SIP Transport"))
            {
                TSK_Debug.Error("Failed to add new transport");
                goto bail;
            }
            /* Starts the transport Layer */
            if (!mLayerTransport.Start())
            {
                TSK_Debug.Error("Failed to start SIP transport layer");
                goto bail;
            }
            // FIXME: Update local IP

            ok = true;
            mRunning = true;
            TSIP_EventStack.Signal(TSIP_EventStack.tsip_stack_event_type_t.Started, "Stack started");
bail:
            if (!ok)
            {
                mLayerTransport.Stop();
            }
            return ok;
        }

        public Boolean Stop()
        {
            Boolean ok = false;
            Boolean oneFailed = false;

            if (!mRunning)
            {
                TSK_Debug.Warn("Stack not running");
                ok = true;
                goto bail;
            }
             /* Hangup all dialogs starting by REGISTER */
            if (!mLayerDialog.ShutdownAll())
            {
                TSK_Debug.Warn("Failed to hang-up all dialogs");
                oneFailed = true;
            }

            /* Stop the transport layer */
            if (!mLayerTransport.Stop())
            {
                TSK_Debug.Warn("Failed to stop the transport layer");
                oneFailed = true;
            }

            /* Signal to the end-user that the stack has been stopped 
		    * should be done before tsk_runnable_stop() which will stop the thread
		    * responsible for the callbacks. The enqueued data have been marked as "important".
		    * As both the timer manager and the transport layer have been stoped there is no
		    * chance to got additional events */
            if (oneFailed)
            {
                TSIP_EventStack.Signal(TSIP_EventStack.tsip_stack_event_type_t.FailedToStop, "Stack failed to stop");
            }
            else
            {
                TSIP_EventStack.Signal(TSIP_EventStack.tsip_stack_event_type_t.Stopped, "Stack stopped");
            }

            /* reset AoR */
            mAoRIP = null;
            mAoRPort = 0;

            ok = !oneFailed;
            if (ok)
            {
                mRunning = false;
            }
bail:
            return ok;
        }

#if !WINDOWS_PHONE
        private void AsyncCallbackForDelegate(IAsyncResult result)
        {

        }
#endif

        internal void HandleEventAsynchronously(TSIP_Event @event)
        {
            if (Callback != null)
            {
#if WINDOWS_PHONE // FIXME: .NET Compact Framework does not support invoking delegates asynchronously.
                new System.Threading.Thread(delegate()
                {
                    Callback(@event);
                }).Start();
#else
                Callback.BeginInvoke(@event, AsyncCallbackForDelegate, null);
#endif
            }
        }

        internal TSIP_Uri GetProxyCSCFUri(Boolean lr)
        {
            TSIP_Uri uri = null;
            if (mLayerTransport != null && mLayerTransport.Transports.Count > 0)
            {
                TSIP_Transport transport = mLayerTransport.Transports[0];
                if (transport != null)
                {
                    Boolean ipv6 = TNET_Socket.IsIPv6Type(transport.Type);
                    Boolean quote_ip = (ipv6 && !String.IsNullOrEmpty(mProxyHost) && mProxyHost.Contains(":"))/* IPv6 IP string?*/;
                    String uriString = String.Format("{0}:{1}{2}{3}:{4};{5};transport={6}",
                            transport.Scheme,
                            quote_ip ? "[" : "",
                            mProxyHost,
                            quote_ip ? "]" : "",
                            mProxyPort,
                            lr ? "lr" : "",
                            transport.Protocol
                        );
                    uri = TSIP_ParserUri.Parse(uriString);
                }
            }
            return uri;
        }

        internal TSIP_Uri GetContactUri(String protocol)
        {
            TSIP_Uri uri = null;
            foreach (TSIP_Transport transport in mLayerTransport.Transports)
            {
                if (String.Equals(transport.Protocol, protocol, StringComparison.InvariantCultureIgnoreCase))
                {
                    if ((uri = transport.GetUri(false)) != null)
                    {
                        uri.UserName = mPublicIdentity.UserName;
                        break;
                    }
                }
            }
            return uri;
        }
    }
}
