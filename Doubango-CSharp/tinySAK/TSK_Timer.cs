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

namespace Doubango.tinySAK
{
    public class TSK_Timer
    {
        private UInt64 mPeriod;
        private readonly Boolean mRepeat;
        private readonly Timer mTimer;

        public TSK_Timer(UInt64 period, Boolean repeat, TimerCallback callback)
        {
            mPeriod = period;
            mRepeat = repeat;

            mTimer = new Timer(callback, this, Timeout.Infinite, Timeout.Infinite);
        }
        public TSK_Timer(UInt64 period, TimerCallback callback)
            :this(period, false, callback)
        {
        }

        public UInt64 Period
        {
            get { return mPeriod; }
            set { mPeriod = value; }
        }

        public Boolean Start()
        {
            return mTimer.Change((long)this.Period, mRepeat ? (long)this.Period : Timeout.Infinite); 
        }

        public Boolean Stop()
        {
            return mTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
