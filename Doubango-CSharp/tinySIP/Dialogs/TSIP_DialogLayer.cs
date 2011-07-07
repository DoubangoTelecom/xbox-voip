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
using System.Threading;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Dialogs
{
    internal partial class TSIP_DialogLayer : IDisposable
    {
        private readonly TSIP_Stack mStack;
        private readonly IDictionary<Int64,TSIP_Dialog> mDialogs;
        private readonly Mutex mMutex;

        internal TSIP_DialogLayer(TSIP_Stack stack)
        {
            mStack = stack;
            mDialogs = new Dictionary<Int64, TSIP_Dialog>();
#if WINDOWS_PHONE
            mMutex = new Mutex(true, null);
#else
            mMutex = new Mutex();
#endif
        }

        ~TSIP_DialogLayer()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (mMutex != null)
            {
                mMutex.Close();
            }
        }

        internal TSIP_Dialog FindDialogBySessionId(Int64 sessionId)
        {
            TSIP_Dialog dialog = null;

            mMutex.WaitOne();

            mMutex.ReleaseMutex();

            return dialog;
        }

        internal TSIP_Dialog CreateDialog(TSIP_Dialog.tsip_dialog_type_t type, TSip_Session sipSession)
        {
            TSIP_Dialog dialog = null;

            switch (type)
            {
                case TSIP_Dialog.tsip_dialog_type_t.REGISTER:
                    {
                        break;
                    }

                default:
                    {
                        TSK_Debug.Error("{0} not support as dialog type", type);
                        break;
                    }
            }

            if (dialog != null && !mDialogs.ContainsKey(dialog.Id))
            {
                mMutex.WaitOne();
                mDialogs.Add(dialog.Id, dialog);
                mMutex.ReleaseMutex();
            }

            return dialog;
        }

        internal Boolean RemoveDialogById(Int64 dialogId)
        {
            mMutex.WaitOne();
            mDialogs.Remove(dialogId);
            mMutex.ReleaseMutex();

            return true;
        }

        internal Boolean ShutdownAll()
        {
            return true;
        }

        internal Boolean HandleIncomingMessage(TSIP_Message message)
        {
            return false;
        }
    }
}
