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
* IMPORTANT: The INVITE Client Transaction (ICT) implements corrections defined in 'draft-sparks-sip-invfix-02.txt'.
* which fixes RFC 3261. This will alow us to easily suppoort forking.

                                     |INVITE from TU
                   Timer A fires     |INVITE sent      Timer B fired
                   Reset A,          V                 or Transport Err.
                   INVITE sent +-----------+           inform TU
                     +---------|           |--------------------------+
                     |         |  Calling  |                          |
                     +-------->|           |-----------+              |
    300-699                    +-----------+ 2xx       |              |
    ACK sent                      |  |       2xx to TU |              |
    resp. to TU                   |  |1xx              |              |
    +-----------------------------+  |1xx to TU        |              |
    |                                |                 |              |
    |                1xx             V                 |              |
    |                1xx to TU +-----------+           |              |
    |                +---------|           |           |              |
    |                |         |Proceeding |           |              |
    |                +-------->|           |           |              |
    |                          +-----------+ 2xx       |              |
    |         300-699             |    |     2xx to TU |              |
    |         ACK sent,  +--------+    +---------------+              |
    |         resp. to TU|                             |              |
    |                    |                             |              |
    |                    V                             V              |
    |              +-----------+                   +----------+       |
    +------------->|           |Transport Err.     |          |       |
                   | Completed |Inform TU          | Accepted |       |
                +--|           |-------+           |          |-+     |
        300-699 |  +-----------+       |           +----------+ |     |
        ACK sent|    ^  |              |               |  ^     |     |
                |    |  |              |               |  |     |     |
                +----+  |              |               |  +-----+     |
                        |Timer D fires |  Timer M fires|    2xx       |
                        |-             |             - |    2xx to TU |
                        +--------+     |   +-----------+              |
       NOTE:                     V     V   V                          |
    transitions                 +------------+                        |
    labeled with                |            |                        |
    the event                   | Terminated |<-----------------------+
    over the action             |            |
    to take                     +------------+


                   	draft-sparks-sip-invfix-03.txt - Figure 3: INVITE client transaction

=============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySAK;
using Doubango.tinySIP.Dialogs;

namespace Doubango.tinySIP.Transactions
{
    internal class TSIP_TransacICT : TSIP_Transac
    {
        private readonly TSK_StateMachine mFSM;

        /* ======================== actions ======================== */
        private enum FSMAction
        {
	        Cancel = TSIP_Action.tsip_action_type_t.tsip_atype_cancel,

	        Send = 0xFF,
	        TimerA,
	        TimerB,
	        TimerD,
	        TimerM,
	        _1xx,
	        _2xx,
	        _300_to_699,
	        TransportError,
	        Error,
        }

        /* ======================== states ======================== */
        private enum FSMState
        {
	        Started,
	        Calling,
	        Proceeding,
	        Completed,
	        Accepted,
	        Terminated
        }

        internal TSIP_TransacICT(Boolean reliable, Int32 cseq_value, String cseq_method, String callid, TSIP_Dialog dialog)
            :base(tsip_transac_type_t.ICT, reliable, cseq_value, cseq_method, callid, dialog)
        {
            /* Initialize the state machine. */
            mFSM = new TSK_StateMachine((Int32)FSMState.Started, (Int32)FSMState.Terminated, OnTerminated, this);

            /*=======================
			* === Started === 
			*/
			// Started -> (Send) -> Calling
            mFSM.AddAlwaysEntry((Int32)FSMState.Started, (Int32)FSMAction.Send, (Int32)FSMState.Calling, Started_2_Calling_X_send, "Started_2_Calling_X_send")
			// Started -> (Any) -> Started
            .AddAlwaysNothingEntry((Int32)FSMState.Started, "Started_2_Started_X_any")
			
			/*=======================
			* === Calling === 
			*/
			// Calling -> (timerA) -> Calling
            .AddAlwaysEntry((Int32)FSMState.Calling, (Int32)FSMAction.TimerA, (Int32)FSMState.Calling, Calling_2_Calling_X_timerA, "Calling_2_Calling_X_timerA")
			// Calling -> (timerB) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Calling, (Int32)FSMAction.TimerB, (Int32)FSMState.Terminated, Calling_2_Terminated_X_timerB, "Calling_2_Terminated_X_timerB")
			// Calling -> (300-699) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Calling, (Int32)FSMAction._300_to_699, (Int32)FSMState.Completed, Calling_2_Completed_X_300_to_699, "Calling_2_Completed_X_300_to_699")
			// Calling  -> (1xx) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Calling, (Int32)FSMAction._1xx, (Int32)FSMState.Proceeding, Calling_2_Proceeding_X_1xx, "Calling_2_Proceeding_X_1xx")
			// Calling  -> (2xx) -> Accepted
            .AddAlwaysEntry((Int32)FSMState.Calling, (Int32)FSMAction._2xx, (Int32)FSMState.Accepted, Calling_2_Accepted_X_2xx, "Calling_2_Accepted_X_2xx")
			
			/*=======================
			* === Proceeding === 
			*/
			// Proceeding -> (1xx) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction._1xx, (Int32)FSMState.Proceeding, Proceeding_2_Proceeding_X_1xx, "Proceeding_2_Proceeding_X_1xx")
			// Proceeding -> (300-699) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction._300_to_699, (Int32)FSMState.Completed, Proceeding_2_Completed_X_300_to_699, "Proceeding_2_Completed_X_300_to_699")
			// Proceeding -> (2xx) -> Accepted
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction._2xx, (Int32)FSMState.Accepted, Proceeding_2_Accepted_X_2xx, "Proceeding_2_Accepted_X_2xx")
			
			/*=======================
			* === Completed === 
			*/
			// Completed -> (300-699) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Completed, (Int32)FSMAction._300_to_699, (Int32)FSMState.Completed, Completed_2_Completed_X_300_to_699, "Completed_2_Completed_X_300_to_699")
			// Completed -> (timerD) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Completed, (Int32)FSMAction.TimerD, (Int32)FSMState.Terminated, Completed_2_Terminated_X_timerD, "Completed_2_Terminated_X_timerD")
				
			/*=======================
			* === Accepted === 
			*/
			// Accepted -> (2xx) -> Accepted
            .AddAlwaysEntry((Int32)FSMState.Accepted, (Int32)FSMAction._2xx, (Int32)FSMState.Accepted, Accepted_2_Accepted_X_2xx, "Accepted_2_Accepted_X_2xx")
			// Accepted -> (timerM) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Accepted, (Int32)FSMAction.TimerM, (Int32)FSMState.Terminated, Accepted_2_Terminated_X_timerM, "Accepted_2_Terminated_X_timerM")
		
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
        /// Started -> (send) -> Calling
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Started_2_Calling_X_send(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Calling -> (timerA) -> Calling
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Calling_2_Calling_X_timerA(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Calling -> (timerB) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Calling_2_Terminated_X_timerB(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Calling -> (300-699) -> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Calling_2_Completed_X_300_to_699(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Calling -> (1xx) -> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Calling_2_Proceeding_X_1xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Calling -> (2xx) -> Accepted
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Calling_2_Accepted_X_2xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Proceeding -> (1xx) -> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Proceeding_X_1xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Proceeding -> (300-699) -> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Completed_X_300_to_699(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Proceeding -> (2xx) -> Accepted
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Accepted_X_2xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Completed -> (300-699) -> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Completed_2_Completed_X_300_to_699(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Completed -> (timerD) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Completed_2_Terminated_X_timerD(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Accepted -> (2xx) -> Accepted
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Accepted_2_Accepted_X_2xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Accepted -> (timerM) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Accepted_2_Terminated_X_timerM(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Any -> (Transport Error) -> Terminate
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
            return true;
        }


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //				== STATE MACHINE END ==
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        private Boolean OnTerminated()
        {
            return true;
        }
    }
}
