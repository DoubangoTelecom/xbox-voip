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

namespace Doubango.tinySIP.Transactions
{
    internal abstract class TSIP_Transac : IDisposable, IEquatable<TSIP_Transac>
    {
        internal enum tsip_transac_type_t
        {
	        ICT,
	        IST,
	        NICT,
	        NIST,
        };

        internal enum tsip_transac_event_type_t
        {
	        IncomingMessage,
	        OutgoingMessage,
	        Canceled,
	        Terminated,
	        TimedOut,
	        Error,
	        TransportError
        };

        private readonly Int64 mId;
        private readonly tsip_transac_type_t mType;
        private readonly Boolean mReliable;
        private readonly Int32 mCSeqValue;
        private readonly String mCSeqMethod;
        private readonly String mCallId;
        private readonly TSIP_Dialog mDialog;
        private OnEvent mCallback;
        private String mBranch;
        private Boolean mRunning;

        private static Int64 sUniqueId = 0;
        internal const String MAGIC_COOKIE = "z9hG4bK";

        internal delegate Boolean OnEvent(tsip_transac_event_type_t type, TSIP_Message message);

        protected TSIP_Transac(tsip_transac_type_t type, Boolean reliable, Int32 cseq_value, String cseq_method, String callid, TSIP_Dialog dialog)
        {
            mId = sUniqueId++;
            mType = type;
            mReliable = reliable;
            mCSeqValue = cseq_value;
            mCSeqMethod = cseq_method;
            mCallId = callid;
            mDialog = dialog;
        }

        ~TSIP_Transac()
        {
            this.Dispose();
        }

        public void Dispose()
        {
        }

        internal Int64 Id
        {
            get { return mId; }
        }

        internal tsip_transac_type_t Type
        {
            get { return mType; }
        }

        protected OnEvent Callback
        {
            get { return mCallback; }
            set { mCallback = value; }
        }

        internal Boolean Reliable
        {
            get { return mReliable; }
        }

        internal Int32 CSeqValue
        {
            get { return mCSeqValue; }
        }

        internal String CSeqMethod
        {
            get { return mCSeqMethod; }
        }

        internal String CallId
        {
            get { return mCallId; }
        }

        internal String Branch
        {
            get { return mBranch; }
            set { mBranch = value; }
        }

        protected Boolean Running
        {
            get { return mRunning; }
            set { mRunning = value; }
        }

        internal TSIP_Dialog Dialog
        {
            get { return mDialog; }
        }

        protected TSIP_Stack Stack
        {
            get { return mDialog.SipSession.Stack; }
        }

        protected abstract TSK_StateMachine StateMachine { get; }
        internal abstract Boolean Start(TSIP_Request request);

        internal Boolean RaiseCallback(tsip_transac_event_type_t type, TSIP_Message message)
        {
            if (this.Callback != null)
            {
                return this.Callback(type, message);
            }
            return false;
        }

        internal Boolean RaiseCallback(tsip_transac_event_type_t type)
        {
            return this.RaiseCallback(type, null);
        }

        internal Boolean Send(String branch, TSIP_Message message)
        {
            return mDialog.Stack.LayerTransport.SendMessage(branch, message);
        }

        internal Boolean ExecuteAction(Int32 fsmActionId, TSIP_Message message)
        {
            return this.StateMachine.ExecuteAction(fsmActionId, this, message, this, message);
        }

        protected Boolean RemoveFromLayer()
        {
            return this.Stack.LayerTransac.RemoveTransacById(this.Id);
        }

        internal static int Compare(TSIP_Transac t1, TSIP_Transac t2)
        {
            if (t1 != null && t2 != null)
            {
                if (String.Equals(t1.Branch, t2.Branch) &&
                    String.Equals(t1.CSeqMethod, t2.CSeqMethod))
                {
                    return 0;
                }
            }
            return -1;
        }

        internal Boolean SipEquals(TSIP_Transac other)
        {
            return this.CompareTo(other) == 0;
        }

        public int CompareTo(TSIP_Transac other)
        {
            return TSIP_Transac.Compare(this, other);
        }
       
        public bool Equals(TSIP_Transac other)
        {
            if (other != null)
            {
                return this.Id == other.Id;
            }
            return false;
        }
    }
}
