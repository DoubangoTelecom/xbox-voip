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

namespace Doubango.tinySIP
{
    public abstract class TSIP_Event
    {
        /// <summary>
        /// Base event type
        /// </summary>
        public enum tsip_event_type_t
        {
            INVITE,
            MESSAGE,
            OPTIONS,
            PUBLISH,
            REGISTER,
            SUBSCRIBE,

            DIALOG,
            STACK,
        };

        public delegate Boolean onEvent(TSIP_Event @event);

        private readonly TSip_Session mSipSession;
        private readonly ushort mCode;
        private readonly String mPhrase;
        private readonly tsip_event_type_t mType;
        private readonly TSIP_Message mSipMessage;
        private Object mUserData;


        protected TSIP_Event(TSip_Session sipSession, ushort code, String phrase, TSIP_Message sipMessage, tsip_event_type_t type)
        {
            mSipSession = sipSession;
            mCode = code;
            mPhrase = phrase;
            mSipMessage = sipMessage;
            mType = type;
        }

        public TSip_Session SipSession
        {
            get { return mSipSession; }
        }

        public Object UserData
        {
            get { return mUserData; }
            set { mUserData = value; }
        }

        protected Boolean Signal()
        {
            if (this.SipSession != null && this.SipSession.Stack != null)
            {
                this.SipSession.Stack.HandleEventAsynchronously(this);
                return true;
            }

            return false;
        }
    }
}
