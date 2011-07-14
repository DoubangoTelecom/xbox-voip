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
using System.Threading;

namespace Doubango.tinySIP.Transactions
{
    internal class TSIP_TransacNICT : TSIP_Transac
    {
        private readonly TSK_StateMachine mFSM;
        private TSIP_Request mRequest;
        private TSK_Timer mTimerE;
        private TSK_Timer mTimerF;
        private TSK_Timer mTimerK;

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
            mFSM.IsDebugEnabled = true;
            /*=======================
			* === Started === 
			*/
                // Started -> (Send) -> Trying
            mFSM.AddAlwaysEntry((Int32)FSMState.Started, (Int32)FSMAction.Send, (Int32)FSMState.Trying, Started_2_Trying_X_send, "NICT_Started_2_Trying_X_send")
                // Started -> (Any) -> Started
            .AddAlwaysNothingEntry((Int32)FSMState.Started, "Started_2_Started_any")

            /*=======================
			* === Trying === 
			*/
                // Trying -> (timerE) -> Trying
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction.TimerE, (Int32)FSMState.Trying, Trying_2_Trying_X_timerE, "NICT_Trying_2_Trying_X_timerE")
                // Trying -> (timerF) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction.TimerF, (Int32)FSMState.Terminated, Trying_2_Terminated_X_timerF, "NICT_Trying_2_Terminated_X_timerF")
                // Trying -> (transport error) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction.TransportError, (Int32)FSMState.Terminated, Trying_2_Terminated_X_transportError, "NICT_Trying_2_Terminated_X_transportError")
                // Trying  -> (1xx) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction._1xx, (Int32)FSMState.Proceeding, Trying_2_Proceedding_X_1xx, "NICT_Trying_2_Proceedding_X_1xx")
                // Trying  -> (200 to 699) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Trying, (Int32)FSMAction._200_to_699, (Int32)FSMState.Completed, Trying_2_Completed_X_200_to_699, "NICT_Trying_2_Completed_X_200_to_699")

            /*=======================
            * === Proceeding === 
            */
                // Proceeding -> (timerE) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.TimerE, (Int32)FSMState.Proceeding, Proceeding_2_Proceeding_X_timerE, "NICT_Proceeding_2_Proceeding_X_timerE")
                // Proceeding -> (timerF) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.TimerF, (Int32)FSMState.Terminated, Proceeding_2_Terminated_X_timerF, "NICT_Proceeding_2_Terminated_X_timerF")
                // Proceeding -> (transport error) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction.TransportError, (Int32)FSMState.Terminated, Proceeding_2_Terminated_X_transportError, "NICT_Proceeding_2_Terminated_X_transportError")
                // Proceeding -> (1xx) -> Proceeding
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction._1xx, (Int32)FSMState.Proceeding, Proceeding_2_Proceeding_X_1xx, "NICT_Proceeding_2_Proceeding_X_1xx")
                // Proceeding -> (200 to 699) -> Completed
            .AddAlwaysEntry((Int32)FSMState.Proceeding, (Int32)FSMAction._200_to_699, (Int32)FSMState.Completed, Proceeding_2_Completed_X_200_to_699, "NICT_Proceeding_2_Completed_X_200_to_699")

            /*=======================
            * === Completed === 
            */
                // Completed -> (timer K) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.Completed, (Int32)FSMAction.TimerK, (Int32)FSMState.Terminated, Completed_2_Terminated_X_timerK, "NICT_Completed_2_Terminated_X_timerK")

            /*=======================
            * === Any === 
            */
                // Any -> (transport error) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.TransportError, (Int32)FSMState.Terminated, Any_2_Terminated_X_transportError, "NICT_Any_2_Terminated_X_transportError")
                // Any -> (error) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.Error, (Int32)FSMState.Terminated, Any_2_Terminated_X_Error, "NICT_Any_2_Terminated_X_Error")
                // Any -> (cancel) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.Cancel, (Int32)FSMState.Terminated, Any_2_Terminated_X_cancel, "NICT_Any_2_Terminated_X_cancel");

            base.Callback = this.OnCallbackEvent;
            
        }

        private TSK_Timer TimerE
        {
            get
            {
                if (mTimerE == null)
                {
                    mTimerE = new TSK_Timer(TSIP_Timers.E, 
                        new TimerCallback(delegate(object state) 
                            { 
                                base.ExecuteAction((Int32)FSMAction.TimerE, null); 
                            }));
                }
                return mTimerE;
            }
        }

        private TSK_Timer TimerF
        {
            get
            {
                if (mTimerF == null)
                {
                    mTimerF = new TSK_Timer(TSIP_Timers.F,
                         new TimerCallback(delegate(object state)
                         {
                             base.ExecuteAction((Int32)FSMAction.TimerF, null);
                         }));
                }
                return mTimerF;
            }
        }

        private TSK_Timer TimerK
        {
            get
            {
                if (mTimerK == null)
                {
                    mTimerK = new TSK_Timer(base.Reliable ? 0 : TSIP_Timers.K,
                        new TimerCallback(delegate(object state)
                        {
                            base.ExecuteAction((Int32)FSMAction.TimerK, null);
                        }));
                }
                return mTimerK;
            }
        }

        protected override TSK_StateMachine StateMachine 
        {
            get { return mFSM; }
        }

        internal override Boolean Start(TSIP_Request request)
        {
            if (request != null && !base.Running)
            {
                /* Add branch to the new client transaction
		        * IMPORTANT: CANCEL will have the same Via and Contact headers as the request it cancel
		        */
                if (request.IsCANCEL)
                {
                    base.Branch = request.FirstVia != null ? request.FirstVia.Branch : "doubango";
                }
                else
                {
                    base.Branch = String.Format("{0}_{1}", TSIP_Transac.MAGIC_COOKIE, TSK_String.Random());
                }

                base.Running = true;
                mRequest = request;

                return base.ExecuteAction((Int32)FSMAction.Send, request);
            }
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
            //tsip_transac_nict_t *self = va_arg(*app, tsip_transac_nict_t *);
	        //const tsip_message_t *message = va_arg(*app, const tsip_message_t *);

	        //== Send the request
            if (!base.Send(base.Branch, mRequest))
            {
                return false;
            }

	        /*	RFC 3261 - 17.1.2.2
		        The "Trying" state is entered when the TU initiates a new client
		        transaction with a request.  When entering this state, the client
		        transaction SHOULD set timer F to fire in 64*T1 seconds.
	        */
            this.TimerF.Start();
        		
	        /*	RFC 3261 - 17.1.2.2
		        If an  unreliable transport is in use, the client transaction MUST set timer
		        E to fire in T1 seconds.
	        */
	        if(!base.Reliable){
                this.TimerE.Start();
	        }

            return true;
        }

        /// <summary>
        /// Trying -> (Timer E) -> Trying
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Trying_X_timerE(params Object[] parameters)
        {
            //== Send the request
            if (!base.Send(base.Branch, mRequest))
            {
                return false;
            }

            /*	RFC 3261 - 17.1.2.2
                If timer E fires while still in this (Trying) state, the timer is reset, but this time with a value of MIN(2*T1, T2).
                When the timer fires again, it is reset to a MIN(4*T1, T2).  This process continues so that retransmissions occur with an exponentially
                increasing interval that caps at T2.  The default value of T2 is 4s, and it represents the amount of time a non-INVITE server transaction
                will take to respond to a request, if it does not respond immediately.  For the default values of T1 and T2, this results in
                intervals of 500 ms, 1 s, 2 s, 4 s, 4 s, 4 s, etc.
            */
            this.TimerE.Period = Math.Min(this.TimerE.Period * 2, TSIP_Timers.T2);
            return this.TimerE.Start();
        }

        /// <summary>
        /// Trying -> (Timer F) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Terminated_X_timerF(params Object[] parameters)
        {
	        /*	RFC 3261 - 17.1.2.2
		        If Timer F fires while the client transaction is still in the
		        "Trying" state, the client transaction SHOULD inform the TU about the
		        timeout, and then it SHOULD enter the "Terminated" state.
	        */

	        /* Timers will be canceled by "tsip_transac_nict_OnTerminated" */

            return base.Dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.TIMEDOUT);
        }

        /// <summary>
        /// Trying -> (Transport Error) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Terminated_X_transportError(params Object[] parameters)
        {
            // tsip_transac_nict_t *self = va_arg(*app, tsip_transac_nict_t *);
	        /*const tsip_message_t *message = va_arg(*app, const tsip_message_t *);*/

	        /* Timers will be canceled by "tsip_transac_nict_OnTerminated" */
            
            return base.Dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.TRANSPORT_ERROR);
        }

        /// <summary>
        /// Trying -> (1xx) -> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Proceedding_X_1xx(params Object[] parameters)
        {
            TSIP_Message message = parameters[1] as TSIP_Message;
	        /*	RFC 3261 - 17.1.2.2
		        If a provisional response is received while in the "Trying" state, the
		        response MUST be passed to the TU, and then the client transaction
		        SHOULD move to the "Proceeding" state.
	        */

	        /* Cancel timers */
	        if(!base.Reliable){
                this.TimerE.Stop();
	        }
            this.TimerF.Stop(); /* Now it's up to the UAS to update the FSM. */
        	
	        /* Pass the provisional response to the dialog. */
            return base.Dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.I_MSG, message);
        }

        /// <summary>
        /// Trying -> (200-699) -> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Trying_2_Completed_X_200_to_699(params Object[] parameters)
        {
            TSIP_Message message = parameters[1] as TSIP_Message;

	        /*	RFC 3261 - 17.1.2.2
		        If a final response (status codes 200-699) is received while in the "Trying" state, the response
		        MUST be passed to the TU, and the client transaction MUST transition
		        to the "Completed" state.

		        If Timer K fires while in this state (Completed), the client transaction MUST transition to the "Terminated" state.
	        */

            if (!base.Reliable)
            {
                this.TimerE.Stop();
	        }
            this.TimerF.Stop();

            Boolean ret = base.Dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.I_MSG, message);

	        /* SCHEDULE timer K */
            this.TimerK.Start();

	        return ret;
        }

        /// <summary>
        /// Proceeding -> (TimerE) -> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Proceeding_X_timerE(params Object[] parameters)
        {
            //== Send the request
            Boolean ret = base.Send(base.Branch, mRequest);

            /*	RFC 3261 - 17.1.2.2
                If Timer E fires while in the "Proceeding" state, the request MUST be
                passed to the transport layer for retransmission, and Timer E MUST be
                reset with a value of T2 seconds.
            */
            this.TimerE.Period = Math.Min(this.TimerE.Period * 2, TSIP_Timers.T2);
            return this.TimerE.Start();
        }

        /// <summary>
        /// Proceeding -> (Timer F) -> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Terminated_X_timerF(params Object[] parameters)
        {
            /*	RFC 3261 - 17.1.2.2
		        If timer F fires while in the "Proceeding" state, the TU MUST be informed of a timeout, and the
		        client transaction MUST transition to the terminated state.
	        */

            /* Timers will be canceled by "tsip_transac_nict_OnTerminated" */
            return base.Dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.TRANSPORT_ERROR, null);
        }

        /// <summary>
        /// Proceeding -> (Transport error) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Terminated_X_transportError(params Object[] parameters)
        {
            /* Timers will be canceles by On */
            return base.Dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.TRANSPORT_ERROR, null);
        }

        /// <summary>
        /// Proceeding -> (1xx) -> Proceeding
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Proceeding_X_1xx(params Object[] parameters)
        {
            TSIP_Message message = parameters[1] as TSIP_Message;

            if (!base.Reliable)
            {
                this.TimerE.Stop();
	        }
            return base.Dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.I_MSG, message);
        }

        /// <summary>
        /// Proceeding -> (200-699) -> Completed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Proceeding_2_Completed_X_200_to_699(params Object[] parameters)
        {
            TSIP_Message message = parameters[1] as TSIP_Message;

	        /*	RFC 3261 - 17.1.2.2
		        If a final response (status codes 200-699) is received while in the
		        "Proceeding" state, the response MUST be passed to the TU, and the
		        client transaction MUST transition to the "Completed" state.
	        */

	        /*	RFC 3261 - 17.1.2.2
		        Once the client transaction enters the "Completed" state, it MUST set
		        Timer K to fire in T4 seconds for unreliable transports, and zero
		        seconds for reliable transports.  The "Completed" state exists to
		        buffer any additional response retransmissions that may be received
		        (which is why the client transaction remains there only for

		        unreliable transports).  T4 represents the amount of time the network
		        will take to clear messages between client and server transactions.
		        The default value of T4 is 5s.
	        */

            if (!base.Reliable)
            {
                this.TimerE.Stop();
	        }

            if (base.Dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.I_MSG, message))
            {
                /* SCHEDULE timer K */
                return this.TimerK.Start();
            }
            return false;
        }

        /// <summary>
        /// Completed -> (Timer K) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Completed_2_Terminated_X_timerK(params Object[] parameters)
        {
            //tsip_transac_nict_t *self = va_arg(*app, tsip_transac_nict_t *);
            //const tsip_message_t *message = va_arg(*app, const tsip_message_t *);

            /*	RFC 3261 - 17.1.2.2
                If Timer K fires while in this state (Completed), the client transaction
                MUST transition to the "Terminated" state.
            */

            /*	RFC 3261 - 17.1.2.2
                ONCE THE TRANSACTION IS IN THE TERMINATED STATE, IT MUST BE DESTROYED IMMEDIATELY.
            */

            /* Timers will be canceled by "tsip_transac_nict_OnTerminated" */

            //TSIP_TRANSAC(self)->dialog->callback(TSIP_TRANSAC(self)->dialog, tsip_dialog_transac_ok, 0);

            return true;
        }
        
        /// <summary>
        /// Any -> (Transport Error) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Any_2_Terminated_X_transportError(params Object[] parameters)
        {
	        /* Timers will be canceled by "tsip_transac_nict_OnTerminated" */

            return base.Dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.TRANSPORT_ERROR, null);
        }
        
        /// <summary>
        /// Any -> (Error) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Any_2_Terminated_X_Error(params Object[] parameters)
        {
            /* Timers will be canceled by "tsip_transac_nict_OnTerminated" */
            return base.Dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.ERROR, null);
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
            TSK_Debug.Info("=== NICT terminated ===");

            this.TimerE.Stop();
            this.TimerF.Stop();
            this.TimerK.Stop();
            base.Running = false;

            return base.RemoveFromLayer();
        }

        private Boolean OnCallbackEvent(tsip_transac_event_type_t type, TSIP_Message message)
        {
            Boolean ret = true;

            switch (type)
            {
                case tsip_transac_event_type_t.IncomingMessage:
                    {
                        if (message != null && message.IsResponse)
                        {
                            if ((message as TSIP_Response).Is1xx)
                            {
                                ret = base.ExecuteAction((Int32)FSMAction._1xx, message);
                            }
                            else if ((message as TSIP_Response).Is23456)
                            {
                                ret = base.ExecuteAction((Int32)FSMAction._200_to_699, message);
                            }
                            else
                            {
                                TSK_Debug.Error("Not supported status code: {0}", (message as TSIP_Response).StatusCode);
                            }
                        }
                        break;
                    }

                case tsip_transac_event_type_t.Canceled:
                case tsip_transac_event_type_t.Terminated:
                case tsip_transac_event_type_t.TimedOut:
                default:
                    {
                        break;
                    }

                case tsip_transac_event_type_t.Error:
                    {
                        ret = base.ExecuteAction((Int32)FSMAction.Error, message);
                        break;
                    }

                case tsip_transac_event_type_t.TransportError:
                    {
                        ret = base.ExecuteAction((Int32)FSMAction.TransportError, message);
                        break;
                    }
            }

            return ret;
        }
    }
}
