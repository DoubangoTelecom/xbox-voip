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
								  |Request received
                                  |pass to TU
                                  V
                            +-----------+
                            |           |
                            | Trying    |-------------+
                            |           |             |
                            +-----------+             |200-699 from TU
                                  |                   |send response
                                  |1xx from TU        |
                                  |send response      |
                                  |                   |
               Request            V      1xx from TU  |
               send response+-----------+send response|
                   +--------|           |--------+    |
                   |        | Proceeding|        |    |
                   +------->|           |<-------+    |
            +<--------------|           |             |
            |Trnsprt Err    +-----------+             |
            |Inform TU            |                   |
            |                     |                   |
            |                     |200-699 from TU    |
            |                     |send response      |
            |  Request            V                   |
            |  send response+-----------+             |
            |      +--------|           |             |
            |      |        | Completed |<------------+
            |      +------->|           |
            +<--------------|           |
            |Trnsprt Err    +-----------+
            |Inform TU            |
            |                     |Timer J fires
            |                     |-
            |                     |
            |                     V
            |               +-----------+
            |               |           |
            +-------------->| Terminated|
                            |           |
                            +-----------+ 

=============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySAK;
using Doubango.tinySIP.Dialogs;

namespace Doubango.tinySIP.Transactions
{
    internal class TSIP_TransacNIST : TSIP_Transac
    {
        private readonly TSK_StateMachine mFSM;

        /* ======================== actions ======================== */
        private enum FSMAction
        {
	        Cancel = TSIP_Action.tsip_action_type_t.tsip_atype_cancel,

	        Request = 0xFF,
	        Send_1xx,
	        Send_200_to_699,
	        TimerJ,
	        TransportError,
	        Error,
        }

        /* ======================== states ======================== */
        private enum FSMState
        {
	        Started,
	        Trying,
	        Proceeding,
	        Completed,
	        Terminated
        }

        internal TSIP_TransacNIST(Boolean reliable, Int32 cseq_value, String cseq_method, String callid, TSIP_Dialog dialog)
            :base(tsip_transac_type_t.NIST, reliable, cseq_value, cseq_method, callid, dialog)
        {
            /* Initialize the state machine. */
            mFSM = new TSK_StateMachine((Int32)FSMState.Started, (Int32)FSMState.Terminated, OnTerminated, this);

            /*=======================
			* === Started === 
			*/
			// Started -> (receive request) -> Trying
            mFSM.AddAlwaysEntry((Int32)FSMState.Started, (Int32)FSMAction.Request, (Int32)FSMState.Trying, Started_2_Trying_X_request, "Started_2_Trying_X_request")
			// Started -> (Any other) -> Started
            .AddAlwaysNothingEntry((Int32)FSMState.Started, "Started_2_Started_X_any")

			/*=======================
			* === Trying === 
			*/
			// Trying -> (send 1xx) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction.Send_1xx, (Int32)FSMState.Proceeding, Trying_2_Proceeding_X_send_1xx, "Trying_2_Proceeding_X_send_1xx")
			// Trying -> (send 200 to 699) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction.Send_200_to_699, (Int32)FSMState.Completed, Trying_2_Completed_X_send_200_to_699, "Trying_2_Completed_X_send_200_to_699")
			
			/*=======================
			* === Proceeding === 
			*/
			// Proceeding -> (send 1xx) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.Send_1xx, (Int32)FSMState.Proceeding, Proceeding_2_Proceeding_X_send_1xx, "Proceeding_2_Proceeding_X_send_1xx")
			// Proceeding -> (send 200 to 699) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.Send_200_to_699, (Int32)FSMState.Completed, Proceeding_2_Completed_X_send_200_to_699, "Proceeding_2_Completed_X_send_200_to_699")
			// Proceeding -> (receive request) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.Request, (Int32)FSMState.Proceeding, Proceeding_2_Proceeding_X_request, "Proceeding_2_Proceeding_X_request")
			
			/*=======================
			* === Completed === 
			*/
			// Completed -> (receive request) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Completed, (Int32)FSMAction.Request, (Int32)FSMState.Completed, Completed_2_Completed_X_request, "Completed_2_Completed_X_request")
			// Completed -> (timer J) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Completed, (Int32)FSMAction.TimerJ, (Int32)FSMState.Terminated, Completed_2_Terminated_X_tirmerJ, "Completed_2_Terminated_X_tirmerJ")

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
        /// Started --> (INCOMING REQUEST) --> Trying
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Started_2_Trying_X_request(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Trying --> (1xx) --> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Proceeding_X_send_1xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Trying --> (200-699) --> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Completed_X_send_200_to_699(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Proceeding --> (1xx) --> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Proceeding_X_send_1xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Proceeding -> (INCOMING REQUEST) -> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Proceeding_X_request(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Proceeding --> (200-699) --> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Completed_X_send_200_to_699(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Completed --> (INCOMING REQUEST) --> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Completed_2_Completed_X_request(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }
        
        /// <summary>
        /// Complete --> (Timer J) --> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Completed_2_Terminated_X_tirmerJ(params Object[] parameters)
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
