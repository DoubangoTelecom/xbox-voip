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

                                   |Request from TU
                                   |send request
               Timer E             V
               send request  +-----------+
                   +---------|           |-------------------+
                   |         |  Trying   |  Timer F          |
                   +-------->|           |  or Transport Err.|
                             +-----------+  inform TU        |
                200-699         |  |                         |
                resp. to TU     |  |1xx                      |
                +---------------+  |resp. to TU              |
                |                  |                         |
                |   Timer E        V       Timer F           |
                |   send req +-----------+ or Transport Err. |
                |  +---------|           | inform TU         |
                |  |         |Proceeding |------------------>|
                |  +-------->|           |-----+             |
                |            +-----------+     |1xx          |
                |              |      ^        |resp to TU   |
                | 200-699      |      +--------+             |
                | resp. to TU  |                             |
                |              |                             |
                |              V                             |
                |            +-----------+                   |
                |            |           |                   |
                |            | Completed |                   |
                |            |           |                   |
                |            +-----------+                   |
                |              ^   |                         |
                |              |   | Timer K                 |
                +--------------+   | -                       |
                                   |                         |
                                   V                         |
             NOTE:           +-----------+                   |
                             |           |                   |
         transitions         | Terminated|<------------------+
         labeled with        |           |
         the event           +-----------+
         over the action

=============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySIP.Dialogs;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Transactions
{
    internal class TSIP_TransacNICT : TSIP_Transac
    {
        private readonly TSK_StateMachine mFSM;

        /* ======================== actions ======================== */
        private enum FSMAction
        {
            Cancel = TSIP_Action.tsip_action_type_t.tsip_atype_cancel,

            Send = 0xFF,
            TimerE,
            TimerF,
            TimerK,
            _1xx,
            _200_to_699,
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

        internal TSIP_TransacNICT(Boolean reliable, Int32 cseq_value, String cseq_method, String callid, TSIP_Dialog dialog)
            :base(tsip_transac_type_t.NICT, reliable, cseq_value, cseq_method, callid, dialog)
        {
            /* Initialize the state machine. */
            mFSM = new TSK_StateMachine((Int32)FSMState.Started, (Int32)FSMState.Terminated, OnTerminated, this);
            /*=======================
			* === Started === 
			*/
                // Started -> (Send) -> Trying
            mFSM.AddAlwaysEntry((Int32)FSMState.Started, (Int32)FSMAction.Send, (Int32)FSMState.Trying, Started_2_Trying_X_send, "Started_2_Trying_X_send")
                // Started -> (Any) -> Started
            .AddAlwaysNothingEntry((Int32)FSMState.Started, "Started_2_Started_any")

            /*=======================
			* === Trying === 
			*/
                // Trying -> (timerE) -> Trying
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction.TimerE, (Int32)FSMState.Trying, Trying_2_Trying_X_timerE, "Trying_2_Trying_X_timerE")
                // Trying -> (timerF) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction.TimerF, (Int32)FSMState.Terminated, Trying_2_Terminated_X_timerF, "Trying_2_Terminated_X_timerF")
                // Trying -> (transport error) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction.TransportError, (Int32)FSMState.Terminated, Trying_2_Terminated_X_transportError, "Trying_2_Terminated_X_transportError")
                // Trying  -> (1xx) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction._1xx, (Int32)FSMState.Proceeding, Trying_2_Proceedding_X_1xx, "Trying_2_Proceedding_X_1xx")
                // Trying  -> (200 to 699) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction._200_to_699, (Int32)FSMState.Completed, Trying_2_Completed_X_200_to_699, "Trying_2_Completed_X_200_to_699")

            /*=======================
            * === Proceeding === 
            */
                // Proceeding -> (timerE) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.TimerE, (Int32)FSMState.Proceeding, Proceeding_2_Proceeding_X_timerE, "Proceeding_2_Proceeding_X_timerE")
                // Proceeding -> (timerF) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.TimerF, (Int32)FSMState.Terminated, Proceeding_2_Terminated_X_timerF, "Proceeding_2_Terminated_X_timerF")
                // Proceeding -> (transport error) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.TransportError, (Int32)FSMState.Terminated, Proceeding_2_Terminated_X_transportError, "Proceeding_2_Terminated_X_transportError")
                // Proceeding -> (1xx) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction._1xx, (Int32)FSMState.Proceeding, Proceeding_2_Proceeding_X_1xx, "Proceeding_2_Proceeding_X_1xx")
                // Proceeding -> (200 to 699) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction._200_to_699, (Int32)FSMState.Completed, Proceeding_2_Completed_X_200_to_699, "Proceeding_2_Completed_X_200_to_699")

            /*=======================
            * === Completed === 
            */
                // Completed -> (timer K) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Completed, (Int32)FSMAction.TimerK, (Int32)FSMState.Terminated, Completed_2_Terminated_X_timerK, "Completed_2_Terminated_X_timerK")

            /*=======================
            * === Any === 
            */
                // Any -> (transport error) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.TransportError, (Int32)FSMState.Terminated, Any_2_Terminated_X_transportError, "Any_2_Terminated_X_transportError")
                // Any -> (error) -> Terminated
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
        /// Started -> (send) -> Trying
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Started_2_Trying_X_send(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Trying -> (Timer E) -> Trying
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Trying_X_timerE(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Trying -> (Timer F) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Terminated_X_timerF(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Trying -> (Transport Error) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Terminated_X_transportError(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Trying -> (1xx) -> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Proceedding_X_1xx(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Trying -> (200-699) -> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Completed_X_200_to_699(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Proceeding -> (TimerE) -> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Proceeding_X_timerE(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Proceeding -> (Timer F) -> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Terminated_X_timerF(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Proceeding -> (Transport error) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Terminated_X_transportError(params Object[] parameters)
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
        /// Proceeding -> (200-699) -> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Completed_X_200_to_699(params Object[] parameters)
        {
            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// Completed -> (Timer K) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Completed_2_Terminated_X_timerK(params Object[] parameters)
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
        {   /* doubango-specific */
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
