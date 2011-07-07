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
    public class TSIP_Action
    {
        /// <summary>
        /// List of all supported actions
        /// </summary>
        public enum tsip_action_type_t
        {
	        //! Used as configuration action
	        tsip_atype_config = 0,
	        tsip_atype_dtmf_send = 1,

	        /* === REGISTER == */
	        tsip_atype_register = 20, /**< Sends SIP REGISTER request */
	        //! Unregister by sending SIP REGISTER request with expires value equals to zero
            tsip_atype_unregister  = tsip_atype_hangup,
        	
	        /* === SUBSCRIBE === */
	        tsip_atype_subscribe = 30, /**< Sends SIP SUBSCRIBE request */
	        //! Unsubsribe by sending SIP SUBSCRIBE request with expires value equals to zero
            tsip_atype_unsubscribe = tsip_atype_hangup,

	        /* === MESSAGE === */
	        tsip_atype_message_send = 40, /**< Sends SIP MESSAGE request */

	        /* === PUBLISH === */
	        tsip_atype_publish = 50, /**< Sends SIP PUBLISH request */
	        //! Unpublish by sending SIP PUBLISH request with expires value equals to zero
            tsip_atype_unpublish = tsip_atype_hangup,
        	
	        /* === OPTIONS === */
	        tsip_atype_options_send = 60, /**< Sends SIP OPTIONS request */

	        /* === INVITE === */
	        tsip_atype_invite = 70, /**< Sends SIP INVITE/reINVITE request */
	        tsip_atype_hold = 71, /**< Puts the session on hold state */
	        tsip_atype_resume = 72, /**< Resumes a previously held session */
	        tsip_atype_ect = 73, /**< Transfer the call */
	        tsip_atype_lmessage = 74, /**< Large message (MSRP). The session must be connected */
            tsip_atype_bye = tsip_atype_hangup,


	        /* === common === */
	        //! Accept incoming call (INVITE) or message (SIP MESSAGE)
	        tsip_atype_accept = 80,
	        //! Reject incoming call (INVITE) or message (SIP MESSAGE)
            tsip_atype_reject = tsip_atype_hangup,
	        //! Cancel an outgoing request
	        tsip_atype_cancel = 81,
	        //! Hangup any SIP dialog (BYE, unREGISTER, unSUBSCRIBE ...). If the dialog is in early state, then it will be canceled.
	        tsip_atype_hangup = 82,
	        //! Shutdown a SIP dialog. Should only be called by the stack.
	        tsip_atype_shutdown = 83,
        };

        private readonly tsip_action_type_t mType;

        internal TSIP_Action(tsip_action_type_t type, params Object[] parameters)
        {
            mType = type;
        }

        internal void AddConfig(TSIP_ActionConfig actionConfig)
        {
            if (actionConfig == null)
            {
                return;
            }
        }

        public tsip_action_type_t Type
        {
            get { return mType; }
        }



        /// <summary>
        /// TSIP_ActionConfig
        /// </summary>
        public class TSIP_ActionConfig : TSIP_Action
        {
            public TSIP_ActionConfig()
                :base(tsip_action_type_t.tsip_atype_config)
            {
            }
        }
    }
}
