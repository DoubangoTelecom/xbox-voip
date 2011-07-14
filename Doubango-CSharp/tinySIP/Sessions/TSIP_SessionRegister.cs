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
using Doubango.tinySIP.Dialogs;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Sessions
{
    public class TSIP_SessionRegister : TSip_Session
    {
        public TSIP_SessionRegister(TSIP_Stack stack)
            :this(stack,  null)
        {
        }

        // internal construction used to create server-side session
        internal TSIP_SessionRegister(TSIP_Stack stack, TSIP_Message message)
            :base(stack, message)
        {
            
        }

        public Boolean Register(TSIP_Action.TSIP_ActionConfig actionConfig)
        {
            if (this.Stack == null || !this.Stack.IsValid)
            {
                TSK_Debug.Error("Invalid stack");
                return false;
            }

            if (!this.Stack.IsRunning)
            {
                TSK_Debug.Error("Stack not running");
                return false;
            }

            TSIP_Action action = new TSIP_Action(TSIP_Action.tsip_action_type_t.tsip_atype_register);
            action.AddConfig(actionConfig);
            TSIP_Dialog dialog;

            if ((dialog = this.Stack.LayerDialog.FindDialogBySessionId(this.Id)) == null)
            {
                dialog = this.Stack.LayerDialog.CreateDialog(TSIP_Dialog.tsip_dialog_type_t.REGISTER, this);
            }

            if (dialog == null)
            {
                TSK_Debug.Error("Failed to create new dialog");
                return false;
            }

            return dialog.ExecuteAction((Int32)action.Type, null, action);
        }

        public Boolean Register()
        {
            return Register(null);
        }

        public Boolean UnRegister(params Object[] parameters)
        {
            return false;
        }
    }
}
