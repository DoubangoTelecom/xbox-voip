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

namespace Doubango.tinySIP.Events
{
    public class TSIP_EventStack : TSIP_Event
    {
        public enum tsip_stack_event_type_t
        {
            Started,
            Stopped,
            FailedToStart,
            FailedToStop
        };

        private readonly tsip_stack_event_type_t mEventType;

        internal TSIP_EventStack(tsip_stack_event_type_t eventType, String phrase)
            :base(null, 0, phrase, null, tsip_event_type_t.STACK)
        {
            mEventType = eventType;
        }

        internal static Boolean Signal(tsip_stack_event_type_t eventType, String phrase)
        {
            TSIP_EventStack @event = new TSIP_EventStack(eventType, phrase);
            return @event.Signal();
        }

        public tsip_stack_event_type_t EventType
        {
            get { return mEventType; }
        }
    }
}
