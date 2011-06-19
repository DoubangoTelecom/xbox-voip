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

namespace Doubango.tinySAK
{
    /// <summary>
    /// Helper class to handle event triggering.
    /// </summary>
    public static class TSK_EventHandlerTrigger
    {
        /// <summary>
        /// Check that the event handler is not null, and trigger this event with the given
        /// source and an <see cref="EventArgs.Empty"/>.
        /// </summary>
        /// <param name="handler">The event handler to trigger</param>
        /// <param name="source">The source to use</param>
        public static void TriggerEvent(EventHandler handler, Object source)
        {
            if (handler != null)
            {
                handler(source, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Check that the event handler is not null, and trigger this event with the given source
        /// and event data. This method has been made generic to handle all the <see cref="EventArgs"/>.
        /// </summary>
        /// <param name="handler">The event handler to trigger</param>
        /// <param name="source">The source to use</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public static void TriggerEvent<T>(EventHandler<T> handler, Object source, T args) where T : EventArgs
        {
            if (handler != null)
            {
                handler(source, args);
            }
        }
    }
}
