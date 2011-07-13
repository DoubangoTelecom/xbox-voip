﻿/* Copyright (C) 2010-2011 Mamadou Diop. 
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

namespace Doubango.tinySIP.Transports
{
    internal abstract class TSIP_Transport : TNET_Transport
    {
        private readonly String mScheme;
        private readonly String mProtocol;
        private readonly String mViaProtocol;
        private readonly String mService;

        protected TSIP_Transport(String host, ushort port, TNET_Socket.tnet_socket_type_t type, String description)
            :base(host, port, type, description)
        {
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

        protected TSIP_Transport(TNET_Socket.tnet_socket_type_t type, String description)
            : base(TNET_Socket.TNET_SOCKET_HOST_ANY, TNET_Socket.TNET_SOCKET_PORT_ANY, type, description)
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
    }
}
