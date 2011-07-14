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
using System.Threading;
using Doubango.tinyNET;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Transactions
{
    internal class TSIP_TransacLayer : IDisposable
    {
        private readonly TSIP_Stack mSipStack;
        private readonly IDictionary<Int64,TSIP_Transac> mTransactions;
        private readonly Boolean mReliable;
        private readonly Mutex mMutex;

        internal TSIP_TransacLayer(TSIP_Stack stack)
        {
            mSipStack = stack;
            mTransactions = new Dictionary<Int64,TSIP_Transac>();
            mReliable = TNET_Socket.IsStreamType(mSipStack.ProxyType);
#if WINDOWS_PHONE
            mMutex = new Mutex(false, TSK_String.Random());
#else
            mMutex = new Mutex();
#endif
        }

        ~TSIP_TransacLayer()
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

        internal Boolean IsReliable
        {
            get { return mReliable; }
        }

        internal TSIP_Transac CreateTransac(Boolean isClient, TSIP_Message message, TSIP_Dialog dialog)
        {
            TSIP_Transac transac = null;

            if (message != null && message.IsRequest)
            {
                if (isClient)/* Client transaction */
                {
                    if (message.IsINVITE)
                    {
                        // INVITE Client transaction (ICT)
                        TSK_Debug.Error("CreateTransac - not implemented");
                    }
                    else
                    {
                        // NON-INVITE Client transaction (NICT)
                        transac = new TSIP_TransacNICT(mReliable, (Int32)message.CSeq.CSeq, message.CSeq.Method, message.CallId.Value, dialog);
                    }
                }
                else/* Server transaction */
                {
                    if (message.IsINVITE)
                    {
                        // INVITE Server transaction (IST)
                        TSK_Debug.Error("CreateTransac - not implemented");
                    }
                    else
                    {
                        // NON-INVITE Server transaction (NIST)
                        TSK_Debug.Error("CreateTransac - not implemented");
                    }

                    if (transac != null)
                    {
                        /* Copy branch from the message */
                        transac.Branch = message.FirstVia.Branch;
                    }
                }
            }


            mMutex.WaitOne();

            if (transac != null)
            {
                /* Add new transaction */
                mTransactions.Add(transac.Id, transac);
            }

            mMutex.ReleaseMutex();

            return transac;
        }

        internal Boolean RemoveTransacById(Int64 transacId)
        {
            mMutex.WaitOne();

            mTransactions.Remove(transacId);

            mMutex.ReleaseMutex();

            return true;
        }

        /// <summary>
        /// cancel all transactions related to this dialog
        /// </summary>
        /// <param name="dialogId"></param>
        /// <returns></returns>
        internal Boolean CancelTransactionByDialogId(Int64 dialogId)
        {
            TSIP_Dialog dialog = mSipStack.LayerDialog.FindDialogById(dialogId);
            if(dialog == null)
            {
                return false;
            }

            mMutex.WaitOne();
again:
            foreach (TSIP_Transac transac in mTransactions.Values)
            {
                if (TSIP_Dialog.Compare(dialog, transac.Dialog) == 0)
                {
                    if (!(transac.ExecuteAction((Int32)TSIP_Action.tsip_action_type_t.tsip_atype_cancel, null)))
                    {
                        /* break */
                    }
                    else
                    {
                        /* we cannot continue because an item has been removed from the list while we are looping through  */
                        goto again;
                    }
                }
            }

            mMutex.ReleaseMutex();

            return true;
        }

        internal TSIP_Transac FindClientTransacByMsg(TSIP_Response response)
        {
             /* RFC 3261 - 17.1.3 Matching Responses to Client Transactions
                
               When the transport layer in the client receives a response, it has to
               determine which client transaction will handle the response, so that
               the processing of Sections 17.1.1 and 17.1.2 can take place.  The
               branch parameter in the top Via header field is used for this
               purpose.  A response matches a client transaction under two
               conditions:

                  1.  If the response has the same value of the branch parameter in
                      the top Via header field as the branch parameter in the top
                      Via header field of the request that created the transaction.

                  2.  If the method parameter in the CSeq header field matches the
                      method of the request that created the transaction.  The
                      method is needed since a CANCEL request constitutes a
                      different transaction, but shares the same value of the branch
                      parameter.
	            */
            if (response == null || response.FirstVia == null || response.CSeq == null)
            {
                return null;
            }

            TSIP_Transac transac = null;

            mMutex.WaitOne();

            foreach (TSIP_Transac t in mTransactions.Values)
            {
                if (String.Equals(t.Branch, response.FirstVia.Branch) && String.Equals(t.CSeqMethod, response.CSeq.Method))
                {
                    transac = t;
                    break;
                }
            }

            mMutex.ReleaseMutex();

            return transac;
        }

        internal TSIP_Transac FindServerTransacByMsg(TSIP_Message message)
        {
            /*
	           RFC 3261 - 17.2.3 Matching Requests to Server Transactions

	           When a request is received from the network by the server, it has to
	           be matched to an existing transaction.  This is accomplished in the
	           following manner.

	           The branch parameter in the topmost Via header field of the request
	           is examined.  If it is present and begins with the magic cookie
	           "z9hG4bK", the request was generated by a client transaction
	           compliant to this specification.  Therefore, the branch parameter
	           will be unique across all transactions sent by that client.  The
	           request matches a transaction if:

		          1. the branch parameter in the request is equal to the one in the
			         top Via header field of the request that created the
			         transaction, and

		          2. the sent-by value in the top Via of the request is equal to the
			         one in the request that created the transaction, and

		          3. the method of the request matches the one that created the
			         transaction, except for ACK, where the method of the request
			         that created the transaction is INVITE.
	        */
            if (message == null || message.FirstVia == null || message.CSeq == null)
            {
                return null;
            }

            TSIP_Transac transac = null;

            mMutex.WaitOne();

            foreach (TSIP_Transac t in mTransactions.Values)
            {
                /* 1. ACK branch won't match INVITE's but they MUST have the same CSeq/CallId values */
                if (message.IsACK && String.Equals(t.CallId, message.CallId.Value))
                {
                    if (String.Equals(t.CSeqMethod, TSIP_Request.METHOD_INVITE) && message.CSeq.CSeq == t.CSeqValue)
                    {
                        transac = t;
                        break;
                    }
                }
                else if (String.Equals(t.Branch, message.FirstVia.Branch) && (1 == 1))/* FIXME: compare host:ip */
                {
                    if (String.Equals(t.CSeqMethod, message.CSeq.Method))
                    {
                        transac = t;
                        break;
                    }
                    else if (message.IsCANCEL || (message.IsResponse && message.CSeq.RequestType == TSIP_Message.tsip_request_type_t.CANCEL))
                    {
                        transac = t;
                        break;
                    }
                }
            }

            mMutex.ReleaseMutex();

            return transac;
        }

        internal Boolean HandleIncomingMsg(TSIP_Message message)
        {
            TSIP_Transac transac = null;

            if (message.IsRequest)
            {
                transac = this.FindServerTransacByMsg(message);
            }
            else
            {
                transac = this.FindClientTransacByMsg((message as TSIP_Response));
            }

            if (transac != null)
            {
                return transac.RaiseCallback(TSIP_Transac.tsip_transac_event_type_t.IncomingMessage, message);
            }

            return false;
        }
    }
}
