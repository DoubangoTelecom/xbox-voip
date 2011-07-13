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

/*=============================================================================

									   |INVITE
                                       |pass INV to TU
                    INVITE             V send 100 if TU won't in 200ms
                    send response+------------+
                        +--------|            |--------+ 101-199 from TU
                        |        |            |        | send response
                        +------->|            |<-------+
                                 | Proceeding |
                                 |            |--------+ Transport Err.
                                 |            |        | Inform TU
                                 |            |<-------+
                                 +------------+
                    300-699 from TU |    |2xx from TU
                    send response   |    |send response
                     +--------------+    +------------+
                     |                                |
    INVITE           V          Timer G fires         |
    send response +-----------+ send response         |
         +--------|           |--------+              |
         |        |           |        |              |
         +------->| Completed |<-------+      INVITE  |  Transport Err.
                  |           |               -       |  Inform TU
         +--------|           |----+          +-----+ |  +---+
         |        +-----------+    | ACK      |     | v  |   v
         |          ^   |          | -        |  +------------+
         |          |   |          |          |  |            |
         +----------+   |          |          +->|  Accepted  |
         Transport Err. |          |             |            |
         Inform TU      |          V             +------------+
                        |      +-----------+        |  ^     |
                        |      |           |        |  |     |
                        |      | Confirmed |        |  +-----+
                        |      |           |        |  2xx from TU
          Timer H fires |      +-----------+        |  send response
          -             |          |                |
                        |          | Timer I fires  |
                        |          | -              | Timer L fires
                        |          V                | -
                        |        +------------+     |
                        |        |            |<----+
                        +------->| Terminated |
                                 |            |
                                 +------------+



                   draft-sparks-sip-invfix-03 - Figure 5: INVITE server transaction

=============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySAK;
using Doubango.tinySIP.Dialogs;

namespace Doubango.tinySIP.Transactions
{
    internal class TSIP_TransacIST : TSIP_Transac
    {
        private readonly TSK_StateMachine mFSM;
        
        /* ======================== actions ======================== */
        private enum FSMAction
        {
            Cancel = TSIP_Action.tsip_action_type_t.tsip_atype_cancel,

	        RecvINVITE = 0xFF,
	        RecvACK,
	        Send1xx,
	        Send2xx,
	        Send300_to_699,
	        SendNon1xx,
	        TimerH,
	        TimerI,
	        TimerG,
	        TimerL,
	        TransportError,
	        Error,
        }

        /* ======================== states ======================== */
        private enum FSMState
        {
	        Started,
	        Proceeding,
	        Completed,
	        Accepted,
	        Confirmed,
	        Terminated
        }

        internal TSIP_TransacIST(Boolean reliable, Int32 cseq_value, String cseq_method, String callid, TSIP_Dialog dialog)
            :base(tsip_transac_type_t.IST, reliable, cseq_value, cseq_method, callid, dialog)
        {
            /* Initialize the state machine. */
            mFSM = new TSK_StateMachine((Int32)FSMState.Started, (Int32)FSMState.Terminated, OnTerminated, this);

            /*=======================
			* === Started === 
			*/
			// Started -> (recv INVITE) -> Proceeding
            mFSM.AddAlwaysEntry((Int32)FSMState.Started, (Int32)FSMAction.RecvINVITE, (Int32)FSMState.Proceeding, Started_2_Proceeding_X_INVITE, "Started_2_Proceeding_X_INVITE")
			// Started -> (Any other) -> Started
            .AddAlwaysNothingEntry((Int32)FSMState.Started, "Started_2_Started_X_any")

			/*=======================
			* === Proceeding === 
			*/
			// Proceeding -> (recv INVITE) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.RecvINVITE, (Int32)FSMState.Proceeding, Proceeding_2_Proceeding_X_INVITE, "Proceeding_2_Proceeding_X_INVITE")
			// Proceeding -> (send 1xx) -> Proceeding
            .AddEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.Send1xx, IsResp2INVITE, (Int32)FSMState.Proceeding, Proceeding_2_Proceeding_X_1xx, "Proceeding_2_Proceeding_X_1xx")
			// Proceeding -> (send 300to699) -> Completed
            .AddEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.Send300_to_699, IsResp2INVITE, (Int32)FSMState.Completed, Proceeding_2_Completed_X_300_to_699, "Proceeding_2_Completed_X_300_to_699")
			// Proceeding -> (send 2xx) -> Accepted
            .AddEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.Send2xx, IsResp2INVITE, (Int32)FSMState.Accepted, Proceeding_2_Accepted_X_2xx, "Proceeding_2_Accepted_X_2xx")

			/*=======================
			* === Completed === 
			*/
			// Completed -> (recv INVITE) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Completed, (Int32)FSMAction.RecvINVITE, (Int32)FSMState.Completed, Completed_2_Completed_INVITE, "Completed_2_Completed_INVITE")
			// Completed -> (timer G) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Completed, (Int32)FSMAction.TimerG, (Int32)FSMState.Completed, Completed_2_Completed_timerG, "Completed_2_Completed_timerG")
			// Completed -> (timerH) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Completed, (Int32)FSMAction.TimerH, (Int32)FSMState.Terminated, Completed_2_Terminated_timerH, "Completed_2_Terminated_timerH")
			// Completed -> (recv ACK) -> Confirmed
            .AddAlwaysEntry((Int32)FSMState.Completed, (Int32)FSMAction.RecvACK, (Int32)FSMState.Confirmed, Completed_2_Confirmed_ACK, "Completed_2_Confirmed_ACK")
			
			/*=======================
			* === Accepted === 
			*/
			// Accepted -> (recv INVITE) -> Accepted
            .AddAlwaysEntry((Int32)FSMState.Accepted, (Int32)FSMAction.RecvINVITE, (Int32)FSMState.Accepted, Accepted_2_Accepted_INVITE, "Accepted_2_Accepted_INVITE")
			// Accepted -> (send 2xx) -> Accepted
            .AddEntry((Int32)FSMState.Accepted, (Int32)FSMAction.Send2xx, IsResp2INVITE, (Int32)FSMState.Accepted, Accepted_2_Accepted_2xx, "Accepted_2_Accepted_2xx")
			// Accepted -> (recv ACK) -> Accepted
            .AddAlwaysEntry((Int32)FSMState.Accepted, (Int32)FSMAction.RecvACK, (Int32)FSMState.Accepted, Accepted_2_Accepted_iACK, "Accepted_2_Accepted_iACK")
			// Accepted -> (timerL) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Accepted, (Int32)FSMAction.TimerL, (Int32)FSMState.Terminated, Accepted_2_Terminated_timerL, "Accepted_2_Terminated_timerL")

			/*=======================
			* === Confirmed === 
			*/
			// Confirmed -> (timerI) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Confirmed, (Int32)FSMAction.TimerI, (Int32)FSMState.Terminated, Confirmed_2_Terminated_timerI, "Confirmed_2_Terminated_timerI")


			/*=======================
			* === Any === 
			*/
			// Any -> (transport error) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.TransportError, (Int32)FSMState.Terminated, Any_2_Terminated_X_transportError, "Any_2_Terminated_X_transportError")
			// Any -> (transport error) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.Error, (Int32)FSMState.Terminated, Any_2_Terminated_X_Error, "Any_2_Terminated_X_Error")
			// Any -> (cancel) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.Cancel, (Int32)FSMState.Terminated, Any_2_Terminated_X_cancel, "Any_2_Terminated_X_cancel");
        }

        protected override TSK_StateMachine StateMachine 
        {
            get { return mFSM; }
        }

        internal override Boolean Start(TSIP_Request request)
        {
            return false;
        }


        //--------------------------------------------------------
        //				== STATE MACHINE BEGIN ==
        //--------------------------------------------------------

        /// <summary>
        /// Started --> (recv INVITE) --> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Started_2_Proceeding_X_INVITE(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Proceeding --> (recv INVITE) --> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Proceeding_X_INVITE(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Proceeding --> (send 1xx) --> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Proceeding_X_1xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Proceeding --> (send 300-699) --> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Completed_X_300_to_699(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Proceeding --> (send 2xx) --> Accepted
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Accepted_X_2xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Completed --> (recv INVITE) --> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Completed_2_Completed_INVITE(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Completed --> (timerG) --> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Completed_2_Completed_timerG(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Completed --> (timerH) --> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Completed_2_Terminated_timerH(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Completed --> (recv ACK) --> Confirmed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Completed_2_Confirmed_ACK(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Accepted --> (recv INVITE) --> Accepted
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Accepted_2_Accepted_INVITE(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Accepted --> (send 2xx) --> Accepted
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Accepted_2_Accepted_2xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Accepted --> (Recv ACK) --> Accepted
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Accepted_2_Accepted_iACK(params Object[] parameters)
        {/* doubango-specific */
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Accepted --> (timerL) --> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Accepted_2_Terminated_timerL(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Confirmed --> (timerI) --> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Confirmed_2_Terminated_timerI(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Any -> (Transport Error) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Any_2_Terminated_X_transportError(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Any -> (Error) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Any_2_Terminated_X_Error(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Any -> (cancel) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Any_2_Terminated_X_cancel(params Object[] parameters)
        {/* doubango-specific */
            TSK_Debug.Error("Not implemented");
            return false;
        }


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //				== STATE MACHINE END ==
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /* ======================== conds ======================== */
        private static Boolean IsResp2INVITE(Object self, Object message)
        {
            TSIP_Response response = message != null ? (message as TSIP_Response) : null;
            if (response != null)
            {
                return response.CSeq.RequestType == TSIP_Message.tsip_request_type_t.INVITE;
            }
            return false;
        }

        private Boolean OnTerminated()
        {
            return true;
        }
    }
}
