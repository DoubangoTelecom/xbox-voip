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
using Doubango.tinySIP.Sessions;
using Doubango.tinySIP.Transactions;

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
            mMutex = new Mutex(false, TSK_String.Random());
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

        private void AddDialog(TSIP_Dialog dialog)
        {
            if (dialog != null)
            {
                mMutex.WaitOne();

                mDialogs.Add(dialog.Id, dialog);

                mMutex.ReleaseMutex();
            }
        }

        private TSIP_Dialog FindDialog(String callid, String to_tag, String from_tag, TSIP_Message.tsip_request_type_t type, out Boolean cid_matched)
        {
            TSIP_Dialog dialog = null;
            cid_matched = false;

            mMutex.WaitOne();

            foreach (TSIP_Dialog d in mDialogs.Values)
            {
                if (String.Equals(d.CallId, callid))
                {
                    Boolean is_cancel = (type == TSIP_Message.tsip_request_type_t.CANCEL); // Incoming CANCEL
                    Boolean is_register = (type == TSIP_Message.tsip_request_type_t.REGISTER); // Incoming REGISTER
                    Boolean is_notify = (type == TSIP_Message.tsip_request_type_t.NOTIFY); // Incoming NOTIFY
                    cid_matched = true;

                    /* CANCEL Request will have the same local tag than the INVITE request -> do not compare tags */
                    if ((is_cancel || String.Equals(d.TagLocal, from_tag)) && String.Equals(d.TagRemote, to_tag))
                    {
                        dialog = d;
                        break;
                    }

                    /* REGISTER is dialogless which means that each reREGISTER or unREGISTER will have empty to tag  */
                    if (is_register /* Do not check tags */)
                    {
                        dialog = d;
                        break;
                    }

                    /*	NOTIFY could arrive before the 200 SUBSCRIBE => This is why we don't try to match both tags
        			 
				        RFC 3265 - 3.1.4.4. Confirmation of Subscription Creation
				        Due to the potential for both out-of-order messages and forking, the
				        subscriber MUST be prepared to receive NOTIFY messages before the
				        SUBSCRIBE transaction has completed.
			         */
                    if (is_notify /* Do not check tags */)
                    {
                        dialog = d;
                        break;
                    }
                }
            }

            mMutex.ReleaseMutex();

            return dialog;
        }

        internal TSIP_Dialog FindDialogBySessionId(Int64 sessionId)
        {
            TSIP_Dialog dialog = null;

            mMutex.WaitOne();

            mMutex.ReleaseMutex();

            return dialog;
        }

        internal TSIP_Dialog FindDialogById(Int64 dialogId)
        {
            TSIP_Dialog dialog = null;

            mMutex.WaitOne();
            if (mDialogs.ContainsKey(dialogId))
            {
                dialog = mDialogs[dialogId];
            }
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
                        dialog = new TSIP_DialogRegister(sipSession as TSIP_SessionRegister);
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

        // this function is only called if no transaction match
        // for responses, the transaction will always match
        internal Boolean HandleIncomingMessage(TSIP_Message message)
        {
            TSIP_Dialog dialog = null;
            TSIP_Transac transac = null;
            Boolean cid_matched;
            Boolean ret = false;

            dialog = this.FindDialog(message.CallId.Value,
                message.IsResponse ? message.To.Tag : message.From.Tag,
                message.IsResponse ? message.From.Tag : message.To.Tag,
                message.IsRequest ? (message as TSIP_Request).RequestType : TSIP_Message.tsip_request_type_t.NONE,
                out cid_matched);

            if (dialog != null)
            {
                if (message.IsCANCEL || message.IsACK)
                {
                    ret = dialog.RaiseCallback(TSIP_Dialog.tsip_dialog_event_type_t.I_MSG, message);
                    goto bail;
                }
                else
                {
                    transac = mStack.LayerTransac.CreateTransac(false, message, dialog);
                }
            }
            else
            {
                if (message.IsRequest)
                {
                    TSIP_Dialog newdialog = null;

                    switch ((message as TSIP_Request).RequestType)
                    {
                        case TSIP_Message.tsip_request_type_t.REGISTER:
                            {
                                /* incoming REGISTER */
                                TSIP_SessionRegister session = new TSIP_SessionRegister(mStack, message);
                                newdialog = new TSIP_DialogRegister(session, message.CallId == null ? null : message.CallId.Value);
                                break;
                            }
                    }

                    // for new dialog, create a new transac and start it later
                    if (newdialog != null)
                    {
                        transac = mStack.LayerTransac.CreateTransac(false, message, newdialog);
                        this.AddDialog(newdialog);
                    }
                }
            }

            if (transac != null)
            {
                ret = transac.Start((message as TSIP_Request));
            }
            else if (message.IsRequest) /* No transaction match for the SIP request */
            {
                TSIP_Response response = null;

                if (cid_matched) /* We are receiving our own message. */
                {
                    response = new TSIP_Response(482, "Loop Detected (Check your iFCs)", (message as TSIP_Request));
                    if (String.IsNullOrEmpty(response.To.Tag))/* Early dialog? */
                    {
                        response.To.Tag = "doubango";
                    }
                }
                else
                {
                    switch ((message as TSIP_Request).RequestType)
                    {
                        case TSIP_Message.tsip_request_type_t.OPTIONS: // Hacked to work on Tiscali IMS networks
                        case TSIP_Message.tsip_request_type_t.INFO:
                            {
                                response = new TSIP_Response(405, "Method Not Allowed", (message as TSIP_Request));
                                break;
                            }
                        default:
                            {
                                response = new TSIP_Response(481, "Dialog/Transaction Does Not Exist", (message as TSIP_Request));
                                break;
                            }
                    }
                }

                if (response != null)
                {
                    // send the response
                    ret = mStack.LayerTransport.SendMessage(response.FirstVia != null ? response.FirstVia.Branch : "no-branch", response);
                }
            }

bail:
            return ret;
        }
    }
}
