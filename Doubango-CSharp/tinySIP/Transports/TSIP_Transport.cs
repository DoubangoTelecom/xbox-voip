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

namespace Doubango.tinySIP.Transports
{
    public abstract class TSIP_Transport : IDisposable
    {
        private String mHost;
        private ushort mPort;
        private TNET_Socket.tnet_socket_type_t mType;
        private String mDescription;
        private TNET_Socket mMasterSocket;
        private Boolean mPrepared;

        public event EventHandler<TransportEventArgs> NetworkEvent;

        public TSIP_Transport(String host, ushort port, TNET_Socket.tnet_socket_type_t type, String description)
        {
            mHost = host;
            mPort = port;
            mType = type;
            mDescription = description;

            mMasterSocket = new TNET_Socket(host, port, type);
        }

        ~TSIP_Transport()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            
        }

        public String Host
        {
            get { return mHost; }
        }

        public ushort Port
        {
            get { return mPort; }
        }

        public TNET_Socket.tnet_socket_type_t Type
        {
            get { return mType; }
        }

        public String Description
        {
            get { return mDescription; }
        }

        public Boolean Isprepared
        {
            get { return mPrepared; }
        }



        /// <summary>
        /// Trasport event class
        /// </summary>
        public class TransportEventArgs : TSK_EventArgs
        {
            public enum TransportEventTypes
            {
               Data,
               Closed,
               Error,
               Connected,
               Accepted
            }

            private readonly TransportEventTypes mType;
            private readonly byte[] mData;
            private readonly Object mContext;
            private IntPtr mLocalSocketHandle;

            public TransportEventArgs(TransportEventTypes type, byte[] data, Object context, IntPtr localSocketHandle)
            {
                mType = type;
                mData = data;
                mContext = context;
                mLocalSocketHandle = localSocketHandle;
            }

            public TransportEventTypes Type
            {
                get { return mType; }
            }

            private byte[] Data
            {
                get { return mData; }
            }

            private Object Context
            {
                get { return mContext; }
            }

            private IntPtr LocalSocketHandle
            {
                get { return mLocalSocketHandle; }
            }




        }
    }
}
