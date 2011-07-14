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
using System.Collections.ObjectModel;
using Doubango.tinySIP.Headers;

namespace Doubango.tinySIP
{
    public abstract class TSip_Session : IDisposable, IEquatable<TSip_Session>
    {
        private readonly TSIP_Stack mStack;
        private readonly Int64 mId;
        private readonly List<TSK_Param> mCaps;
        private readonly List<TSK_Param> mHeaders;

        private Boolean mNoContact;
        private TSIP_Uri mUriFrom;
        private TSIP_Uri mUriTo;
        private Int64 mExpires;
        private Boolean mSilentHangUp;

        private static Int64 sUniqueId = 0;

#if DEBUG || _DEBUG
        internal const Int64 DEFAULT_EXPIRES = 10000;//3600000; /* miliseconds. */
#else
        internal const Int64 DEFAULT_EXPIRES = 600000000;
#endif
        protected TSip_Session(TSIP_Stack stack)
        {
            mId = sUniqueId++;
            mStack = stack;
            mCaps = new List<TSK_Param>();
            mHeaders = new List<TSK_Param>();
            mExpires = TSip_Session.DEFAULT_EXPIRES;

            /* From */
            mUriFrom = mStack.PublicIdentity;

            /* to */
            /* To value will be set by the dialog (whether to use as Request-URI). */

            if (mStack != null)
            {
                mStack.AddSession(this);
            }
        }

        // internal construction used to create server-side session
        protected TSip_Session(TSIP_Stack stack, TSIP_Message message)
            :this(stack)
        {
            if (message != null)
            {
                /* From: */
                if (message.From != null && message.From.Uri != null)
                { /* MUST be not null */
                    mUriFrom = message.From.Uri;
                }
                /* To: */
                if (message.To != null && message.To.Uri != null)
                { /* MUST be not null */
                    mUriTo = message.To.Uri;
                }
            }
        }

        public void Dispose()
        {
            
        }

        public bool Equals(TSip_Session other)
        {
            if (other != null)
            {
                return this.Id == other.Id;
            }
            return false;
        }

        internal Int64 Id
        {
            get { return mId; }
        }

        internal TSIP_Stack Stack
        {
            get { return mStack; }
        }

        public TSIP_Uri UriFrom
        {
            get { return mUriFrom; }
            set { mUriFrom = value; }
        }

        public TSIP_Uri UriTo
        {
            get { return mUriTo; }
            set { mUriTo = value; }
        }

        public Int64 Expires
        {
            get { return mExpires; }
            set { mExpires = value; }
        }

        public Boolean SilentHangUp
        {
            get { return mSilentHangUp; }
            set { mSilentHangUp = value; }
        }

        public ReadOnlyCollection<TSK_Param> Caps
        {
            get { return mCaps.AsReadOnly(); }
        }

        public ReadOnlyCollection<TSK_Param> Headers
        {
            get { return mHeaders.AsReadOnly(); }
        }

        public Boolean Reject(params Object[] parameters)
        {
            return false;
        }

        public Boolean HangUp(params Object[] parameters)
        {
            return false;
        }

        public Boolean Accept(params Object[] parameters)
        {
            return false;
        }
    }
}
