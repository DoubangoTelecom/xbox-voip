using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doubango.tinySIP.Events
{
    public class TSIP_EventDialog : TSIP_Event
    {
        public enum tsip_dialog_event_type_t
        {
            // 7xx ==> errors
            TransportError,
            GlobalError,
            MessageError,

            // 8xx ==> success
            IncomingRequest,
            RequestCancelled,
            RequestSent,

            // 9xx ==> Informational
            Connecting,
            Connected,
            Terminating,
            Terminated
        };

         private readonly tsip_dialog_event_type_t mEventType;

         internal TSIP_EventDialog(tsip_dialog_event_type_t eventType, TSip_Session sipSession, String phrase, TSIP_Message sipMessage)
            :base(sipSession, 0, phrase, sipMessage, tsip_event_type_t.DIALOG)
        {
            mEventType = eventType;
        }

         internal static Boolean Signal(tsip_dialog_event_type_t eventType, TSip_Session sipSession, String phrase, TSIP_Message sipMessage)
        {
            TSIP_EventDialog @event = new TSIP_EventDialog(eventType, sipSession, phrase, sipMessage);
            return @event.Signal();
        }

         public tsip_dialog_event_type_t EventType
        {
            get { return mEventType; }
        }
    }
}
