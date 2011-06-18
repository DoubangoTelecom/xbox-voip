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



/***********************************
*	Ragel state machine.
*/
%%{
	machine tsip_machine_parser_header_Allow_events;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}

	action parse_event{
		String @event = TSK_RagelState.Parser.GetString(data, p, tag_start);
        if (!String.IsNullOrEmpty(@event))
        {
            Allow_Events.Events.Add(@event);
        }
	}

	action eob{
	}
	
	event_package = token_nodot;
	event_template = token_nodot;
	event_type = event_package ( "." event_template )*;

	Allow_Events = ( "Allow-Events"i | "u"i ) HCOLON event_type>tag %parse_event ( COMMA event_type>tag %parse_event )*;
	
	# Entry point
	main := Allow_Events :>CRLF @eob;

}%%

using System;
using Doubango.tinySAK;
using System.Collections.Generic;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderAllowEvents : TSIP_Header
	{
		private List<String> mEvents;

		public TSIP_HeaderAllowEvents()
            : this(null)
        {
        }

        public TSIP_HeaderAllowEvents(String events)
            : base(tsip_header_type_t.Allow_Events)
        {
            if (events != null)
            {
                this.Events.AddRange(events.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            }
        }

		public List<String> Events
		{
			get 
			{ 
				if(mEvents == null)
				{
					mEvents = new List<String>();
				}
				return mEvents; 
			}
		}

		public override String Value
        {
            get 
            { 
                String ret = String.Empty;
                foreach(String @event in this.Events)
                {
                    if(String.IsNullOrEmpty(ret))
                    {
                        ret = @event;
                    }
                    else
                    {
                        ret += String.Format(",{0}", @event);
                    }
                }
                return ret; 
            }
            set
            {
                TSK_Debug.Error("Not implemented");
            }
        }
		
		public Boolean IsEventAllowed(String @event)
		{
 
#if WINDOWS_PHONE
            foreach (String _event in this.Events)
            {
                if (String.Equals(_event, @event, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
#else
			return this.Events.Exists(
                (x) => { return x.Equals(@event, StringComparison.InvariantCultureIgnoreCase); }
            );
#endif
		}

		%%write data;

		public static TSIP_HeaderAllowEvents Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderAllowEvents Allow_Events = new TSIP_HeaderAllowEvents();
			
			int tag_start = 0;

			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Allow' header.");
				Allow_Events.Dispose();
				Allow_Events = null;
			}
			
			return Allow_Events;
		}
	}
}