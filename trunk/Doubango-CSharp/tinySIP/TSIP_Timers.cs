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

namespace Doubango.tinySIP
{
    internal static class TSIP_Timers
    {
        /*
	        Timer    Value            Section               Meaning
	        ----------------------------------------------------------------------
	        T1       500ms default    Section 17.1.1.1     RTT Estimate
	        T2       4s               Section 17.1.2.2     The maximum retransmit
												           interval for non-INVITE
												           requests and INVITE
												           responses
	        T4       5s               Section 17.1.2.2     Maximum duration a
												           message will
												           remain in the network
	        Timer A  initially T1     Section 17.1.1.2     INVITE request retransmit
												           interval, for UDP only
	        Timer B  64*T1            Section 17.1.1.2     INVITE transaction
												           timeout timer
	        Timer C  > 3min           Section 16.6         proxy INVITE transaction
							           bullet 11            timeout
	        Timer D  > 32s for UDP    Section 17.1.1.2     Wait time for response
			         0s for TCP/SCTP                       retransmits
	        Timer E  initially T1     Section 17.1.2.2     non-INVITE request
												           retransmit interval,
												           UDP only
	        Timer F  64*T1            Section 17.1.2.2     non-INVITE transaction
												           timeout timer
	        Timer G  initially T1     Section 17.2.1       INVITE response
												           retransmit interval
	        Timer H  64*T1            Section 17.2.1       Wait time for
												           ACK receipt
	        Timer I  T4 for UDP       Section 17.2.1       Wait time for
			         0s for TCP/SCTP                       ACK retransmits
	        Timer J  64*T1 for UDP    Section 17.2.2       Wait time for
			         0s for TCP/SCTP                       non-INVITE request
												           retransmits
	        Timer K  T4 for UDP       Section 17.1.2.2     Wait time for
			         0s for TCP/SCTP                       response retransmits
        	
            Timer L  64*T1             Section 17.2.1         Wait time for
                                                             accepted INVITE
                                                             request retransmits

           Timer M  64*T1             Section 17.1.1         Wait time for
                                                             retransmission of
                                                             2xx to INVITE or
                                                             additional 2xx from
                                                             other branches of
                                                             a forked INVITE
        */

        private const UInt32 TIMER_T1 = 500;
        private const UInt32 TIMER_T4 = 5000;

        private static UInt32 _T1 = TIMER_T1;
        private static UInt32 _T2 = 4000;
        private static UInt32 _T4 = TIMER_T4;
        private static UInt32 _A = TIMER_T1;
        private static UInt32 _B = 64 * TIMER_T1;
        private static UInt32 _C = 5 * 60000; /* >3min */
        private static UInt32 _D = 50000; /*> 32s*/
        private static UInt32 _E = TIMER_T1;
        private static UInt32 _F = 64 * TIMER_T1;
        private static UInt32 _G = TIMER_T1;
        private static UInt32 _H = 64 * TIMER_T1;
        private static UInt32 _I = TIMER_T4;
        private static UInt32 _J = 64 * TIMER_T1;
        private static UInt32 _K = TIMER_T4;
        private static UInt32 _L = 64 * TIMER_T1; // draft-sparks-sip-invfix
        private static UInt32 _M = 64 * TIMER_T1; // draft-sparks-sip-invfix

        internal static UInt32 T1
        {
            get { return _T1; }
            set
            {
                _T1 = value;
                _A = _E = _G = _T1;
                _B = _F = _H = _J = (_T1 * 64);
            }
        }

        internal static UInt32 T2
        {
            get { return _T2; }
            set
            {
                _T2 = value;
            }
        }

        internal static UInt32 T4
        {
            get { return _T4; }
            set
            {
                _T4 = value;
                _I = _K = _T4;
            }
        }

        internal static UInt32 A
        {
            get { return _A; }
            set { _A = value; }
        }

        internal static UInt32 B
        {
            get { return _B; }
            set { _B = value; }
        }

        internal static UInt32 C
        {
            get { return _C; }
            set { _C = value; }
        }

        internal static UInt32 D
        {
            get { return _D; }
            set { _D = value; }
        }

        internal static UInt32 E
        {
            get { return _E; }
            set { _E = value; }
        }

        internal static UInt32 F
        {
            get { return _F; }
            set { _F = value; }
        }

        internal static UInt32 G
        {
            get { return _G; }
            set { _G = value; }
        }

        internal static UInt32 H
        {
            get { return _H; }
            set { _H = value; }
        }

        internal static UInt32 I
        {
            get { return _I; }
            set { _I = value; }
        }

        internal static UInt32 J
        {
            get { return _J; }
            set { _J = value; }
        }

        internal static UInt32 K
        {
            get { return _K; }
            set { _K = value; }
        }

        internal static UInt32 L
        {
            get { return _L; }
            set { _L = value; }
        }

        internal static UInt32 M
        {
            get { return _M; }
            set { _M = value; }
        }
    }
}
