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
using Doubango.tinySIP.Headers;
using Doubango.tinySIP.Authentication;

namespace Doubango.tinySIP.Dialogs
{
    internal abstract class TSIP_Dialog : IDisposable, IEquatable<TSIP_Dialog>, IComparable<TSIP_Dialog>
    {
        internal enum tsip_dialog_type_t
        {
	        NONE,

	        INVITE,
	        MESSAGE,
	        OPTIONS,
	        PUBLISH,
	        REGISTER,
	        SUBSCRIBE,
        };

        internal enum tsip_dialog_state_t
        {
	        Initial,
	        Early,
	        Established,
	        Terminated
        };

        internal enum tsip_dialog_event_type_t
        {
            I_MSG,
            TO_MSG,
            TRANSAC_OK,
            CANCELED,
            TERMINATED,
            TIMEDOUT,
            ERROR,
            TRANSPORT_ERROR,
        };

        internal delegate Boolean OnEvent(tsip_dialog_event_type_t type, TSIP_Message message);

        private readonly Int64 mId;
        private readonly tsip_dialog_type_t mType;
        private readonly String mCallId;

        private readonly TSip_Session mSipSession;
        private TSIP_Action mCurrentAction;

        private tsip_dialog_state_t mState;

        private Boolean mRunning;

        private String mLastError;

        private String mTagLocal;
        private TSIP_Uri mUriLocal;
        private String mTagRemote;
        private TSIP_Uri mUriRemote;

        private TSIP_Uri mUriRemoteTarget;

        private UInt32 mCSeqValue;
        private String mCSeqMethod;

        private Int64 mExpires; // in milliseconds

        private readonly List<TSIP_HeaderRecordRoute> mRecordRoutes;

        private readonly List<TSIP_Challenge> mChallenges;

        private OnEvent mOnEvent;

        private static Int64 sUniqueId = 0;

        internal TSIP_Dialog(tsip_dialog_type_t type, String callId, TSip_Session session)
        {
            mId = sUniqueId++;
            mRecordRoutes = new List<TSIP_HeaderRecordRoute>();
            mChallenges = new List<TSIP_Challenge>();

            mState = tsip_dialog_state_t.Initial;
            mType = type;

            mCallId = String.IsNullOrEmpty(callId) ? TSIP_HeaderCallId.RandomCallId() : callId;
            mSipSession = session;

            /* Sets some default values */
            mExpires = TSip_Session.DEFAULT_EXPIRES;
            mTagLocal = TSK_String.Random();
            mCSeqValue = (UInt32)new Random().Next();

            /*=== SIP Session ===*/
            if (mSipSession != null)
            {
                mExpires = mSipSession.Expires;
                mUriLocal = !String.IsNullOrEmpty(mCallId) /* Server Side */ ? mSipSession.UriTo : mSipSession.UriFrom;
                if (mSipSession.UriTo != null)
                {
                    mUriRemote = mSipSession.UriTo;
                    mUriRemoteTarget = mSipSession.UriTo;
                }
                else
                {
                    mUriRemote = mSipSession.UriFrom;
                    mUriRemoteTarget = mSipSession.Stack.Realm;
                }
            }
            else
            {
                TSK_Debug.Error("Invalid Sip Session");
            }
        }

        ~TSIP_Dialog()
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

        internal TSip_Session SipSession 
        {
            get { return mSipSession; }
        }

        internal TSIP_Action CurrentAction
        {
            get { return mCurrentAction; }
            set { mCurrentAction = value; }
        }

        internal String LastError
        {
            get { return mLastError; }
            set { mLastError = value; }
        }

        internal String CallId
        {
            get { return mCallId; }
        }

        internal String TagLocal
        {
            get { return mTagLocal; }
        }

        internal String TagRemote
        {
            get { return mTagRemote; }
        }

        internal abstract TSK_StateMachine StateMachine { get; }

        public Boolean ExecuteAction(Int32 fsmActionId, TSIP_Message message, TSIP_Action action)
        {
            if (this.StateMachine != null)
            {
                return this.StateMachine.ExecuteAction(fsmActionId, this, message, this, message, action);
            }

            TSK_Debug.Error("Invalid FSM");

            return false;
        }

        internal TSIP_Request CreateRequest(String method)
        {
            return null;
        }

        internal Boolean HangUp(TSIP_Action action)
        {
            if (mState == tsip_dialog_state_t.Established)
            {
                return this.ExecuteAction((int)TSIP_Action.tsip_action_type_t.tsip_atype_hangup, null, action);
            }
            else
            {
                return this.ExecuteAction((int)TSIP_Action.tsip_action_type_t.tsip_atype_cancel, null, action);
            }
        }

        internal Boolean Shutdown(TSIP_Action action)
        {
            return this.ExecuteAction((int)TSIP_Action.tsip_action_type_t.tsip_atype_shutdown, null, action);
        }

        internal static int Compare(TSIP_Dialog d1, TSIP_Dialog d2)
        {
            if (d1 != null && d2 != null)
            {
                if (String.Equals(d1.CallId, d2.CallId) &&
                    String.Equals(d1.TagLocal, d2.TagLocal) &&
                    String.Equals(d1.TagRemote, d2.TagRemote))
                {

                    return 0;
                }
            }
            return -1;
        }

        internal Boolean SipEquals(TSIP_Dialog other)
        {
            return this.CompareTo(other) == 0;
        }

        public int CompareTo(TSIP_Dialog other)
        {
            return TSIP_Dialog.Compare(this, other);
        }

        public Boolean Equals(TSIP_Dialog other)
        {
            if (other != null)
            {
                return this.Id == other.Id;
            }
            return false;
        }
    }
}
