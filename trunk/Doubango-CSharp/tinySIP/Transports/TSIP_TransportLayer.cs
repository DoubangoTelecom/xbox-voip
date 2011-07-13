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
            mMutex = new Mutex(true, null);
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
            TSIP_Transport transport = TNET_Socket.IsStreamType(type) ? (TSIP_Transport)new TSIP_TransportTCP(localHost, localPort, TNET_Socket.IsIPv6Type(type), description)
                : (TSIP_Transport)new TSIP_TransportUDP(localHost, localPort, TNET_Socket.IsIPv6Type(type), description);

            if (transport != null)
            {
                mTransports.Add(transport);
            }

            return (transport != null);
        }

        internal Boolean SendMessage(String branch, TSIP_Message message)
        {
            return false;
        }

        internal Boolean Start()
        {
            return true;
        }

        internal Boolean Stop()
        {
            return true;
        }
    }
}
