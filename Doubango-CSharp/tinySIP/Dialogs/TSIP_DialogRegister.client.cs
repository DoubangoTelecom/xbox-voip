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
using Doubango.tinySIP.Events;
using Doubango.tinySIP.Headers;

namespace Doubango.tinySIP.Dialogs
{
    internal partial class TSIP_DialogRegister
    {
        private void InitClientFSM()
        {
            /*=======================
			* === Started === 
			*/
			// Started -> (REGISTER) -> InProgress
            mFSM.AddAlwaysEntry((Int32)FSMState.Started, (Int32)FSMAction.oREGISTER, (Int32)FSMState.InProgress, Started_2_InProgress_X_oRegister, "REGISTER_Started_2_InProgress_X_oRegister")
                // Started -> (Any) -> Started
                //TSK_FSM_ADD_ALWAYS_NOTHING(_fsm_state_Started, "tsip_dialog_register_Started_2_Started_X_any"),


            /*=======================
            * === InProgress === 
            */
                // InProgress -> (1xx) -> InProgress
            .AddAlwaysEntry((Int32)FSMState.InProgress, (Int32)FSMAction._1xx, (Int32)FSMState.InProgress, InProgress_2_InProgress_X_1xx, "REGISTER_InProgress_2_InProgress_X_1xx")
                // InProgress -> (2xx) -> Terminated
            .AddEntry((Int32)FSMState.InProgress, (Int32)FSMAction._2xx, IsUnregistering, (Int32)FSMState.Terminated, InProgress_2_Terminated_X_2xx, "REGISTER_InProgress_2_Terminated_X_2xx")
                // InProgress -> (2xx) -> Connected
            .AddEntry((Int32)FSMState.InProgress, (Int32)FSMAction._2xx, IsRegistering, (Int32)FSMState.Connected, InProgress_2_Connected_X_2xx, "REGISTER_InProgress_2_Connected_X_2xx")
                // InProgress -> (401/407/421/494) -> InProgress
            .AddAlwaysEntry((Int32)FSMState.InProgress, (Int32)FSMAction._401_407_421_494, (Int32)FSMState.InProgress, InProgress_2_InProgress_X_401_407_421_494, "REGISTER_InProgress_2_InProgress_X_401_407_421_494")
                // InProgress -> (423) -> InProgress
            .AddAlwaysEntry((Int32)FSMState.InProgress, (Int32)FSMAction._423, (Int32)FSMState.InProgress, InProgress_2_InProgress_X_423, "REGISTER_InProgress_2_InProgress_X_423")
                // InProgress -> (300_to_699) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.InProgress, (Int32)FSMAction._300_to_699, (Int32)FSMState.Terminated, InProgress_2_Terminated_X_300_to_699, "REGISTER_InProgress_2_Terminated_X_300_to_699")
                // InProgress -> (cancel) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.InProgress, (Int32)FSMAction.Cancel, (Int32)FSMState.Terminated, InProgress_2_Terminated_X_cancel, "REGISTER_InProgress_2_Terminated_X_cancel")
                // InProgress -> (hangup) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.InProgress, (Int32)FSMAction.Hangup, (Int32)FSMState.Terminated, null, "REGISTER_InProgress_2_Terminated_X_hangup")
                // InProgress -> (shutdown) -> Terminated
            .AddAlwaysEntry((Int32)FSMState.InProgress, (Int32)FSMAction.Shutdown, (Int32)FSMState.Terminated, null, "REGISTER_InProgress_2_Terminated_X_shutdown")
                // InProgress -> (Any) -> InProgress
                //TSK_FSM_ADD_ALWAYS_NOTHING(_fsm_state_InProgress, "tsip_dialog_register_InProgress_2_InProgress_X_any"),


            /*=======================
            * === Connected === 
            */
                // Connected -> (register) -> InProgress [refresh case]
            .AddAlwaysEntry((Int32)FSMState.Connected, (Int32)FSMAction.oREGISTER, (Int32)FSMState.InProgress, Connected_2_InProgress_X_oRegister, "REGISTER_Connected_2_InProgress_X_oRegister");
        }


        //--------------------------------------------------------
        //				== STATE MACHINE BEGIN ==
        //--------------------------------------------------------

        /// <summary>
        /// Started -> (REGISTER) -> InProgress
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Started_2_InProgress_X_oRegister(params Object[] parameters)
        {
            TSIP_Action action = (parameters[2] as TSIP_Action);

            base.mRunning = true;
            base.CurrentAction = action;

            /* alert the user */
            base.Signal(TSIP_EventDialog.tsip_dialog_event_type_t.Connecting, "Dialog connecting");

            return this.SendRegister(true);
        }

        /// <summary>
        /// InProgress -> (1xx) -> InProgress
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean InProgress_2_InProgress_X_1xx(params Object[] parameters)
        {
            TSIP_Response response = parameters[1] as TSIP_Response;

	        /* Alert the user (session) */
            TSIP_EventRegister.Signal(TSIP_EventRegister.tsip_register_event_type_t.AO_REGISTER,
                base.SipSession, response.StatusCode, response.ReasonPhrase, response);

	        return base.UpdateWithResponse(response);
        }

        /// <summary>
        /// InProgress -> (2xx) -> Connected
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean InProgress_2_Connected_X_2xx(params Object[] parameters)
        {

            Boolean ret = true;
            TSIP_Response response = parameters[1] as TSIP_Response;

            Boolean first_time_to_connect = base.State == tsip_dialog_state_t.Initial;

            /*	- Set P-associated-uriS
            *	- Update service-routes
            *	- Update Pats
            */
            {
                int index;
                // const tsip_header_Path_t *hdr_Path;
                // const tsip_header_Service_Route_t *hdr_Service_Route;
                // const tsip_header_P_Associated_URI_t *hdr_P_Associated_URI_t;
                TSIP_Uri uri;

                /* To avoid memory leaks ==> delete all concerned objects (it worth nothing) */
                //TSK_OBJECT_SAFE_FREE(TSIP_DIALOG_GET_STACK(self)->associated_uris);
                //TSK_OBJECT_SAFE_FREE(TSIP_DIALOG_GET_STACK(self)->service_routes);
                //TSK_OBJECT_SAFE_FREE(TSIP_DIALOG_GET_STACK(self)->paths);

                /* Associated URIs */
                /*for(index = 0; (hdr_P_Associated_URI_t = (const tsip_header_P_Associated_URI_t*)tsip_message_get_headerAt(response, tsip_htype_P_Associated_URI, index)); index++){
                    if(!TSIP_DIALOG_GET_STACK(self)->associated_uris){
                        TSIP_DIALOG_GET_STACK(self)->associated_uris = tsk_list_create();
                    }
                    uri = tsk_object_ref(hdr_P_Associated_URI_t->uri);
                    tsk_list_push_back_data(TSIP_DIALOG_GET_STACK(self)->associated_uris, (void**)&uri);
                }*/

                /*	Service-Route (3GPP TS 24.229)
                    store the list of service route values contained in the Service-Route header field and bind the list to the contact
                    address used in registration, in order to build a proper preloaded Route header field value for new dialogs and
                    standalone transactions when using the respective contact address.
                */
                /*for(index = 0; (hdr_Service_Route = (const tsip_header_Service_Route_t*)tsip_message_get_headerAt(response, tsip_htype_Service_Route, index)); index++){
                    if(!TSIP_DIALOG_GET_STACK(self)->service_routes){
                        TSIP_DIALOG_GET_STACK(self)->service_routes = tsk_list_create();
                    }
                    uri = tsk_object_ref(hdr_Service_Route->uri);
                    tsk_list_push_back_data(TSIP_DIALOG_GET_STACK(self)->service_routes, (void**)&uri);
                }*/

                /* Paths */
                /*for(index = 0; (hdr_Path = (const tsip_header_Path_t*)tsip_message_get_headerAt(response, tsip_htype_Path, index)); index++){
                    if(TSIP_DIALOG_GET_STACK(self)->paths == 0){
                        TSIP_DIALOG_GET_STACK(self)->paths = tsk_list_create();
                    }
                    uri = tsk_object_ref(hdr_Path->uri);
                    tsk_list_push_back_data(TSIP_DIALOG_GET_STACK(self)->paths, (void**)&uri);
                }*/
            }

            /* 3GPP TS 24.229 - 5.1.1.2 Initial registration */
            if (first_time_to_connect)
            {
                Boolean barred = true;
                //const tsk_list_item_t *item;
                //const tsip_uri_t *uri;
                //const tsip_uri_t *uri_first = 0;

                /*	
                    b) store as the default public user identity the first URI on the list of URIs present in the P-Associated-URI header
                    field and bind it to the respective contact address of the UE and the associated set of security associations or TLS
                    session;
                    NOTE 4: When using the respective contact address and associated set of security associations or TLS session, the
                    UE can utilize additional URIs contained in the P-Associated-URI header field and bound it to the
                    respective contact address of the UE and the associated set of security associations or TLS session, e.g. for
                    application purposes.
                    c) treat the identity under registration as a barred public user identity, if it is not included in the P-Associated-URI
                    header field;
                */
                //tsk_list_foreach(item, TSIP_DIALOG_GET_STACK(self)->associated_uris){
                //    uri = item->data;
                //    if(item == TSIP_DIALOG_GET_STACK(self)->associated_uris->head){
                //        uri_first = item->data;
                //    }
                //    if(!tsk_object_cmp(TSIP_DIALOG_GET_STACK(self)->identity.preferred, uri)){
                //        barred = 0;
                //        break;
                //    }
                //}

                //if(barred && uri_first){
                //    TSK_OBJECT_SAFE_FREE(TSIP_DIALOG_GET_STACK(self)->identity.preferred);
                //    TSIP_DIALOG_GET_STACK(self)->identity.preferred = tsk_object_ref((void*)uri_first);
                //}
            }

            /* Update the dialog state */
            if (!(ret = this.UpdateWithResponse(response)))
            {
                return ret;
            }

            /* Reset current action */
            base.CurrentAction = null;

            /* Request timeout for dialog refresh (re-registration). */
            this.TimerRefresh.Period = (UInt64)base.GetNewDelay(response);
            this.TimerRefresh.Start();

            /* Alert the user (session) */
            TSIP_EventRegister.Signal(TSIP_EventRegister.tsip_register_event_type_t.AO_REGISTER, base.SipSession,
                response.StatusCode, response.ReasonPhrase, response);

            /* Alert the user (dialog) */
            if (first_time_to_connect)
            {
                TSIP_EventDialog.Signal(TSIP_EventDialog.tsip_dialog_event_type_t.Connected, base.SipSession, response.ReasonPhrase, response);
            }

            return ret;
        }

        /// <summary>
        /// InProgress -> (2xx) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean InProgress_2_Terminated_X_2xx(params Object[] parameters)
        {
            TSIP_Response response = parameters[1] as TSIP_Response;

            /* Alert the user */
            TSIP_EventRegister.Signal(TSIP_EventRegister.tsip_register_event_type_t.AO_UNREGISTER, base.SipSession,
                response.StatusCode, response.ReasonPhrase, response);

            return true;
        }

        /// <summary>
        /// InProgress --> (401/407/421/494) --> InProgress
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean InProgress_2_InProgress_X_401_407_421_494(params Object[] parameters)
        {
            TSIP_Response response = parameters[1] as TSIP_Response;
            Boolean ret;

            if (!(ret = base.UpdateWithResponse(response)))
            {
                /* alert the user */
                TSIP_EventRegister.Signal(mUnRegsitering ? TSIP_EventRegister.tsip_register_event_type_t.AO_UNREGISTER : TSIP_EventRegister.tsip_register_event_type_t.AO_REGISTER, 
                    base.SipSession,
                response.StatusCode, response.ReasonPhrase, response);

                /* set last error (or info) */
                base.SetLastError((short)response.StatusCode, response.ReasonPhrase, response);

                return ret;
            }

            /* Ensure IPSec SAs */
            //if (TSIP_DIALOG_GET_STACK(self)->security.secagree_mech && tsk_striequals(TSIP_DIALOG_GET_STACK(self)->security.secagree_mech, "ipsec-3gpp"))
            //{
            //    tsip_transport_ensureTempSAs(TSIP_DIALOG_GET_STACK(self)->layer_transport, response, TSIP_DIALOG(self)->expires);
            //}

            return this.SendRegister(false);
        }

        /// <summary>
        /// InProgress -> (423) -> InProgress
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean InProgress_2_InProgress_X_423(params Object[] parameters)
        {
            TSIP_Response response = parameters[1] as TSIP_Response;

            /* FIXME
	        RFC 3261 - 10.2.8 Error Responses

	        If a UA receives a 423 (Interval Too Brief) response, it MAY retry
	        the registration after making the expiration interval of all contact
	        addresses in the REGISTER request equal to or greater than the
	        expiration interval within the Min-Expires header field of the 423
	        (Interval Too Brief) response.
	        */
            //hdr = (tsip_header_Min_Expires_t*)tsip_message_get_header(message, tsip_htype_Min_Expires);
            //if (hdr)
            //{
            //    TSIP_DIALOG(self)->expires = TSK_TIME_S_2_MS(hdr->value);

            //    if (tsk_striequals(TSIP_DIALOG_GET_STACK(self)->security.secagree_mech, "ipsec-3gpp"))
            //    {
            //        tsip_transport_cleanupSAs(TSIP_DIALOG_GET_STACK(self)->layer_transport);
            //        ret = tsip_dialog_register_send_REGISTER(self, tsk_true);
            //    }
            //    else
            //    {
            //        ret = tsip_dialog_register_send_REGISTER(self, tsk_false);
            //    }
           //}
            //else
            //{
            //    ret = -1;
            //}
            

            TSK_Debug.Error("Not implemented");
            return false;
        }

        /// <summary>
        /// InProgress -> (300-699) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean InProgress_2_Terminated_X_300_to_699(params Object[] parameters)
        {
            TSIP_Response response = parameters[1] as TSIP_Response;

            base.SetLastError((short)response.StatusCode, response.ReasonPhrase, response);

            /* alert the user */
            TSIP_EventRegister.Signal(mUnRegsitering ? TSIP_EventRegister.tsip_register_event_type_t.AO_UNREGISTER : TSIP_EventRegister.tsip_register_event_type_t.AO_REGISTER,
                base.SipSession,
            response.StatusCode, response.ReasonPhrase, response);
            
            return true;
        }

        /// <summary>
        /// InProgress -> (cancel) -> Terminated
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean InProgress_2_Terminated_X_cancel(params Object[] parameters)
        {
            /* Cancel all transactions associated to this dialog (will also be done when the dialog is destroyed (worth nothing)) */
            if (!base.Stack.LayerTransac.CancelTransactionByDialogId(base.Id))
            {
                return false;
            }

            /* alert the user */
            TSIP_EventDialog.Signal(TSIP_EventDialog.tsip_dialog_event_type_t.RequestCancelled, base.SipSession,
                "Registration cancelled", null);

            return true;
        }

        /// <summary>
        /// Connected -> (REGISTER) -> InProgress
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Boolean Connected_2_InProgress_X_oRegister(params Object[] parameters)
        {
            base.CurrentAction = parameters[2] as TSIP_Action;

            return this.SendRegister(true);
        }
        

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //				== STATE MACHINE END ==
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        /* ======================== conds ======================== */
        private static Boolean IsUnregistering(Object self, Object message)
        {
            return (self as TSIP_DialogRegister).mUnRegsitering;
        }

        private static Boolean IsRegistering(Object self, Object message)
        {
            return !TSIP_DialogRegister.IsUnregistering(self, message);
        }
    }
}
