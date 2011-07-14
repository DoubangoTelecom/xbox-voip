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
using Doubango.tinySAK;
using Doubango.tinySIP.Sessions;
using Doubango.tinySIP.Headers;
using Doubango.tinySIP.Events;
using System.Threading;

namespace Doubango.tinySIP.Dialogs
{
    internal partial class TSIP_DialogRegister : TSIP_Dialog
    {
        /* ======================== actions ======================== */
        private enum FSMAction
        {
	        Accept = TSIP_Action.tsip_action_type_t.tsip_atype_accept,
	        Reject = TSIP_Action.tsip_action_type_t.tsip_atype_hangup,
	        Hangup = TSIP_Action.tsip_action_type_t.tsip_atype_hangup,
	        oREGISTER = TSIP_Action.tsip_action_type_t.tsip_atype_register,
	        Cancel = TSIP_Action.tsip_action_type_t.tsip_atype_cancel,
	        Shutdown = TSIP_Action.tsip_action_type_t.tsip_atype_shutdown,

	        _1xx = 0xFF,
	        _2xx,
	        _401_407_421_494,
	        _423,
	        _300_to_699,

	        iREGISTER,

	        ShutdownTimedout, /* Any -> Terminated */
	        TransportError,
	        Error,
        }

        /* ======================== states ======================== */
        private enum FSMState
        {
	        Started,
	        InProgress, // Outgoing (Client)
	        Incoming, // Incoming (Server)
	        Connected,
	        Terminated
        }

        private readonly TSK_StateMachine mFSM;
        private Boolean mUnRegsitering;
        private TSK_Timer mTimerRefresh;
        private TSK_Timer mTimerShutdown;

        internal TSIP_DialogRegister(TSIP_SessionRegister session, String callId)
            :base(tsip_dialog_type_t.REGISTER, callId, session)
        {
            /* Initialize the state machine. */
            mFSM = new TSK_StateMachine((Int32)FSMState.Started, (Int32)FSMState.Terminated, OnTerminated, this);
            mFSM.IsDebugEnabled = true;

            // Initialize client side
            InitClientFSM();
            // initialize server side
            InitServerFSM();

            // initialize common transitions side

            /*=======================
            * === Any === 
            */
                // Any -> (hangup) -> InProgress
            mFSM.AddEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.Hangup, IsNotSilentHangup, (Int32)FSMState.InProgress, Any_2_InProgress_X_hangup, "REGISTER_Any_2_InProgress_X_hangup")
                // Any -> (silenthangup) -> Terminated
            .AddEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.Hangup, IsSilentHangup, (Int32)FSMState.Terminated, null, "REGISTER_Any_2_InProgress_X_silenthangup")
                // Any -> (shutdown) -> InProgress
            .AddEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.Shutdown, IsNotSilentHangup, (Int32)FSMState.InProgress, Any_2_InProgress_X_shutdown, "REGISTER_Any_2_InProgress_X_shutdown")
                // Any -> (silentshutdown) -> Terminated
            .AddEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.Shutdown, IsSilentHangup, (Int32)FSMState.Terminated, null, "REGISTER_Any_2_InProgress_X_silentshutdow")
                // Any -> (shutdown timedout) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.ShutdownTimedout, (Int32)FSMState.Terminated, null, "REGISTER_shutdown_timedout")
                // Any -> (transport error) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.TransportError, (Int32)FSMState.Terminated, Any_2_Terminated_X_transportError, "REGISTER_Any_2_Terminated_X_transportError")
                // Any -> (error) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)FSMAction.Error, (Int32)FSMState.Terminated, Any_2_Terminated_X_Error, "REGISTER_Any_2_Terminated_X_Error");

            // hook callback
            base.Callback = this.OnCallbackEvent;
        }

        internal TSIP_DialogRegister(TSIP_SessionRegister session)
            : this(session, null)
        {

        }

        internal override TSK_StateMachine StateMachine
        {
            get { return mFSM; }
        }

        private TSK_Timer TimerRefresh
        {
            get
            {
                if (mTimerRefresh == null)
                {
                    mTimerRefresh = new TSK_Timer((UInt64)base.Expires,
                         new TimerCallback(delegate(object state)
                         {
                             base.ExecuteAction((Int32)FSMAction.oREGISTER, null, null);
                         }));
                }
                return mTimerRefresh;
            }
        }

        private TSK_Timer TimerShutdown
        {
            get
            {
                if (mTimerShutdown == null)
                {
                    mTimerShutdown = new TSK_Timer(TSIP_Dialog.SHUTDOWN_TIMEOUT,
                         new TimerCallback(delegate(object state)
                         {
                             base.ExecuteAction((Int32)FSMAction.ShutdownTimedout, null, null);
                         }));
                }
                return mTimerShutdown;
            }
        }

        //--------------------------------------------------------
        //				== STATE MACHINE BEGIN ==
        //--------------------------------------------------------

        /// <summary>
        /// Any -> (hangup) -> InProgress
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Any_2_InProgress_X_hangup(params Object[] parameters)
        {
            /* Set  current action */
            base.CurrentAction = parameters[2] as TSIP_Action;

            /* Alert the user */
            TSIP_EventDialog.Signal(TSIP_EventDialog.tsip_dialog_event_type_t.Terminating, base.SipSession, "Terminating dialog", null);

            mUnRegsitering = true;
            return this.SendRegister(true);
        }

        /// <summary>
        /// Any -> (shutdown) -> InProgress
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Any_2_InProgress_X_shutdown(params Object[] parameters)
        {
            /* schedule shutdow timeout */
            this.TimerShutdown.Start();

            /* alert user */
            TSIP_EventDialog.Signal(TSIP_EventDialog.tsip_dialog_event_type_t.Terminating, base.SipSession, "Terminating dialog", null);

            mUnRegsitering = true;
            return this.SendRegister(true);
        }

        /// <summary>
        /// Any -> (transport error) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Any_2_Terminated_X_transportError(params Object[] parameters)
        {
            TSIP_EventDialog.Signal(TSIP_EventDialog.tsip_dialog_event_type_t.TransportError, base.SipSession, "Transport error", null);
            return true;
        }

        /// <summary>
        /// Any -> (error) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Any_2_Terminated_X_Error(params Object[] parameters)
        {
            TSIP_Response response = parameters[2] as TSIP_Response;
            if (response != null)
            {
                base.SetLastError((short)response.StatusCode, response.ReasonPhrase, response);
                TSIP_EventRegister.Signal(mUnRegsitering ? TSIP_EventRegister.tsip_register_event_type_t.AO_UNREGISTER : TSIP_EventRegister.tsip_register_event_type_t.AO_REGISTER,
                    base.SipSession, response.StatusCode, response.ReasonPhrase, response);
            }
            else
            {
                TSIP_EventDialog.Signal(TSIP_EventDialog.tsip_dialog_event_type_t.GlobalError, base.SipSession, "Global error", null);
            }

            return false;
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //				== STATE MACHINE END ==
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /* ======================== conds ======================== */
        private static Boolean IsSilentHangup(Object self, Object message)
        {
            return (self as TSIP_Dialog).SipSession.SilentHangUp;
        }

        private static Boolean IsNotSilentHangup(Object self, Object message)
        {
            return !TSIP_DialogRegister.IsSilentHangup(self, message);
        }

        private Boolean OnTerminated()
        {
            TSK_Debug.Info("=== REGISTER Dialog terminated ===");

            /* Cleanup IPSec SAs */
            //if (TSIP_DIALOG_GET_STACK(self)->security.secagree_mech && tsk_striequals(TSIP_DIALOG_GET_STACK(self)->security.secagree_mech, "ipsec-3gpp"))
            //{
            //    tsip_transport_cleanupSAs(TSIP_DIALOG_GET_STACK(self)->layer_transport);
            //}

            /* alert the user */
            TSIP_EventDialog.Signal(TSIP_EventDialog.tsip_dialog_event_type_t.Terminated,
                base.SipSession, String.IsNullOrEmpty(base.LastErrorPhrase) ? "Dialog terminated" : base.LastErrorPhrase, base.LastErrorMessage);

            /* Remove from the dialog layer. */
            base.RemoveFromLayer();
            return true;
        }

        /// <summary>
        /// Callback function called to alert the dialog for new events from the transaction/transport layers
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private Boolean OnCallbackEvent(tsip_dialog_event_type_t type, TSIP_Message message)
        {
            Boolean ret = false;

            switch (type)
            {
                case tsip_dialog_event_type_t.I_MSG:
                    {
                        if (message != null)
                        {
                            if (message.IsResponse)
                            {
                                TSIP_Response response = message as TSIP_Response;
                                //
                                //	RESPONSE
                                //
                                if (response.Is1xx)
                                {
                                    ret = base.ExecuteAction((Int32)FSMAction._1xx, message, null);
                                }
                                else if (response.Is2xx)
                                {
                                    ret = base.ExecuteAction((Int32)FSMAction._2xx, message, null);
                                }
                                else if (response.StatusCode == 401 || response.StatusCode == 407 || response.StatusCode == 421 || response.StatusCode == 494)
                                {
                                    ret = base.ExecuteAction((Int32)FSMAction._401_407_421_494, message, null);
                                }
                                else if (response.StatusCode == 423)
                                {
                                    ret = base.ExecuteAction((Int32)FSMAction._423, message, null);
                                }
                                else
                                {
                                    // Alert User
                                    ret = base.ExecuteAction((Int32)FSMAction.Error, message, null);
                                    /* TSK_DEBUG_WARN("Not supported status code: %d", TSIP_RESPONSE_CODE(msg)); */
                                }
                            }
                            else
                            {
                                //
                                //	REQUEST
                                //
                                if (message.IsREGISTER)
                                {
                                    ret = base.ExecuteAction((Int32)FSMAction.iREGISTER, message, null);
                                }
                            }
                        }
                        break;
                    }

                case tsip_dialog_event_type_t.CANCELED:
                    {
                        ret = base.ExecuteAction((Int32)FSMAction.Cancel, message, null);
                        break;
                    }

                case tsip_dialog_event_type_t.TERMINATED:
                case tsip_dialog_event_type_t.TIMEDOUT:
                case tsip_dialog_event_type_t.ERROR:
                case tsip_dialog_event_type_t.TRANSPORT_ERROR:
                    {
                        ret = base.ExecuteAction((Int32)FSMAction.TransportError, message, null);
                        break;
                    }
            }

            return true;
        }

        private Boolean SendRegister(Boolean initial)
        {
            TSIP_Request request = base.CreateRequest(TSIP_Request.METHOD_REGISTER);
            Boolean ret = false;

            /* 3GPP TS 24.229 - 5.1.1.2 Initial registration */
            if (base.State == tsip_dialog_state_t.Initial)
            {
                /*
				    g) the Supported header field containing the option-tag "path", and
				    1) if GRUU is supported, the option-tag "gruu"; and
				    2) if multiple registrations is supported, the option-tag "outbound".
			    */
                request.AddHeader(new TSIP_HeaderSupported("path"));
            }

            /* action parameters and payload */
            if (base.CurrentAction != null)
            {
                foreach (TSK_Param param in base.CurrentAction.Headers)
                {
                    request.AddHeader(new TSIP_HeaderDummy(param.Name, param.Value));
                }
                if (base.CurrentAction.Payload != null)
                {
                    request.AddContent(null, base.CurrentAction.Payload);
                }
            }

            /* create temorary IPSec SAs if initial register. */
		    
            // send the request
            ret = base.SenRequest(request);
            if (ret)
            {
                base.Signal(TSIP_EventDialog.tsip_dialog_event_type_t.RequestSent, "(un)REGISTER request successfully sent");
            }
            else
            {
                base.Signal(TSIP_EventDialog.tsip_dialog_event_type_t.TransportError, "Transport error");
            }

            return ret;
        }
    }
}
