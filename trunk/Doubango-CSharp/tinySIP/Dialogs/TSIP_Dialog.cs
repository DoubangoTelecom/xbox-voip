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
using Doubango.tinySIP.Transactions;
using Doubango.tinySIP.Events;

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

        protected Boolean mRunning;

        private String mLastErrorPhrase;
        private short mLastErrorCode;
        private TSIP_Message mLastErrorMessage;

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

        private readonly List<TSIP_Uri> mPaths;
        private readonly List<TSIP_Uri> mServiceRoutes;
        private readonly List<TSIP_Uri> mAssociatedUris;

        private OnEvent mCallback;

        private static Int64 sUniqueId = 0;

        internal const Int64 SHUTDOWN_TIMEOUT = 2000; /* miliseconds. */

        internal TSIP_Dialog(tsip_dialog_type_t type, String callId, TSip_Session session)
        {
            mId = sUniqueId++;
            mRecordRoutes = new List<TSIP_HeaderRecordRoute>();
            mChallenges = new List<TSIP_Challenge>();

            mPaths = new List<TSIP_Uri>();
            mServiceRoutes = new List<TSIP_Uri>();
            mAssociatedUris = new List<TSIP_Uri>();

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
                mUriLocal = !String.IsNullOrEmpty(callId) /* Server Side */ ? mSipSession.UriTo : mSipSession.UriFrom;
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

        internal TSIP_Stack Stack
        {
            get { return SipSession.Stack; }
        }

        protected TSIP_Action CurrentAction
        {
            get { return mCurrentAction; }
            set { mCurrentAction = value; }
        }

        protected String LastErrorPhrase
        {
            get { return mLastErrorPhrase; }
        }

        protected short LastErrorCode
        {
            get { return mLastErrorCode; }
        }

        protected TSIP_Message LastErrorMessage
        {
            get { return mLastErrorMessage; }
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

        internal Boolean Running
        {
            get { return mRunning; }
        }

        protected tsip_dialog_state_t State
        {
            get { return mState; }
            set { mState = value; }
        }

        protected OnEvent Callback
        {
            get { return mCallback; }
            set { mCallback = value; }
        }

        protected Int64 Expires
        {
            get { return mExpires; }
            set { mExpires = value; }
        }

        internal abstract TSK_StateMachine StateMachine { get; }
        
        protected void Signal(TSIP_EventDialog.tsip_dialog_event_type_t eventType, String phrase, TSIP_Message sipMessage)
        {
            TSIP_EventDialog.Signal(eventType, mSipSession, phrase, sipMessage);
        }

        protected void SetLastError(short code, String phrase, TSIP_Message message)
        {
            mLastErrorCode = code;
            mLastErrorPhrase = phrase;
            mLastErrorMessage = message;
        }

        protected Boolean RemoveFromLayer()
        {
            return this.Stack.LayerDialog.RemoveDialogById(this.Id);
        }

        protected void SetLastError(short code, String phrase)
        {
            this.SetLastError(code, phrase, null);
        }

        internal Boolean RaiseCallback(tsip_dialog_event_type_t type, TSIP_Message message)
        {
            if (mCallback != null)
            {
               return mCallback(type, message);
            }
            return false;
        }

        internal Boolean RaiseCallback(tsip_dialog_event_type_t type)
        {
            return this.RaiseCallback(type, null);
        }

        protected void Signal(TSIP_EventDialog.tsip_dialog_event_type_t eventType, String phrase)
        {
            TSIP_EventDialog.Signal(eventType, mSipSession, phrase, null);
        }

        protected TSIP_Request CreateRequest(String method)
        {
            TSIP_Request request = null;
            TSIP_Uri to_uri, from_uri, request_uri;
	        String call_id;
	        int copy_routes_start = -1; /* NONE */

            /*
	        RFC 3261 - 12.2.1.1 Generating the Request

	        The Call-ID of the request MUST be set to the Call-ID of the dialog.
	        */
            call_id = mCallId;

            /*
	        RFC 3261 - 12.2.1.1 Generating the Request

	        Requests within a dialog MUST contain strictly monotonically
	        increasing and contiguous CSeq sequence numbers (increasing-by-one)
	        in each direction (excepting ACK and CANCEL of course, whose numbers
	        equal the requests being acknowledged or cancelled).  Therefore, if
	        the local sequence number is not empty, the value of the local
	        sequence number MUST be incremented by one, and this value MUST be
	        placed into the CSeq header field.
	        */
            /*if(!tsk_striequals(method, "ACK") && !tsk_striequals(method, "CANCEL"))
            {
                TSIP_DIALOG(self)->cseq_value +=1;
            }
            ===> See send method (cseq will be incremented before sending the request)
            */


            /*
            RFC 3261 - 12.2.1.1 Generating the Request

            The URI in the To field of the request MUST be set to the remote URI
            from the dialog state.  The tag in the To header field of the request
            MUST be set to the remote tag of the dialog ID.  The From URI of the
            request MUST be set to the local URI from the dialog state.  The tag
            in the From header field of the request MUST be set to the local tag
            of the dialog ID.  If the value of the remote or local tags is null,
            the tag parameter MUST be omitted from the To or From header fields,
            respectively.
            */
            to_uri = mUriRemote;
            from_uri = mUriLocal;

            /*
	        RFC 3261 - 12.2.1.1 Generating the Request

	        If the route set is empty, the UAC MUST place the remote target URI
	        into the Request-URI.  The UAC MUST NOT add a Route header field to
	        the request.
	        */
            if (mRecordRoutes == null || mRecordRoutes.Count == 0)
            {
                request_uri = mUriRemoteTarget;
            }

            /*
	        RFC 3261 - 12.2.1.1 Generating the Request

	        If the route set is not empty, and the first URI in the route set
	        contains the lr parameter (see Section 19.1.1), the UAC MUST place
	        the remote target URI into the Request-URI and MUST include a Route
	        header field containing the route set values in order, including all
	        parameters.

	        If the route set is not empty, and its first URI does not contain the
	        lr parameter, the UAC MUST place the first URI from the route set
	        into the Request-URI, stripping any parameters that are not allowed
	        in a Request-URI.  The UAC MUST add a Route header field containing
	        the remainder of the route set values in order, including all
	        parameters.  The UAC MUST then place the remote target URI into the
	        Route header field as the last value.

	        For example, if the remote target is sip:user@remoteua and the route
	        set contains:

	        <sip:proxy1>,<sip:proxy2>,<sip:proxy3;lr>,<sip:proxy4>
	        */
            else
            {
                TSIP_Uri first_route = mRecordRoutes[0].Uri;
                if (TSK_Param.HasParam(first_route.Params, "lr"))
                {
			        request_uri = mUriRemoteTarget;
			        copy_routes_start = 0; /* Copy all */
		        }
		        else{
                    request_uri = first_route;
			        copy_routes_start = 1; /* Copy starting at index 1. */
		        }
            }

            /*=====================================================================
	        */
            request = new TSIP_Request(method, request_uri, from_uri, to_uri, call_id, (Int32)mCSeqValue);
            request.To.Tag = mTagRemote;
            request.From.Tag = mTagLocal;
            request.ShouldUpdate = true; /* Now signal that the message should be updated by the transport layer (Contact, SigComp, IPSec, ...) */

            /*
                RFC 3261 - 12.2.1.1 Generating the Request

                A UAC SHOULD include a Contact header field in any target refresh
                requests within a dialog, and unless there is a need to change it,
                the URI SHOULD be the same as used in previous requests within the
                dialog.  If the "secure" flag is true, that URI MUST be a SIPS URI.
                As discussed in Section 12.2.2, a Contact header field in a target
                refresh request updates the remote target URI.  This allows a UA to
                provide a new contact address, should its address change during the
                duration of the dialog.
                */
            switch (request.RequestType)
            {
                case TSIP_Message.tsip_request_type_t.MESSAGE:
                case TSIP_Message.tsip_request_type_t.PUBLISH:
                case TSIP_Message.tsip_request_type_t.BYE:
                    {
                        if (request.RequestType == TSIP_Message.tsip_request_type_t.PUBLISH)
                        {
                            request.AddHeader(new TSIP_HeaderExpires(mExpires));
                        }
                        /* add caps in Accept-Contact headers */
                        foreach (TSK_Param param in mSipSession.Caps)
                        {
                            request.AddHeader(new TSIP_HeaderDummy("Accept-Contact",
                                String.Format("*;{0}{1}{2}", 
                                    param.Name,
                                    !String.IsNullOrEmpty(param.Value) ? "=" : String.Empty,
                                    !String.IsNullOrEmpty(param.Value) ? param.Value : String.Empty)
                                ));
                        }
                        break;
                    }

                default:
                    {
                        String contact = null;
                        List<TSIP_HeaderContact> hdr_contacts = null;
                        if (request.RequestType == TSIP_Message.tsip_request_type_t.OPTIONS ||
                            request.RequestType == TSIP_Message.tsip_request_type_t.PUBLISH ||
                            request.RequestType == TSIP_Message.tsip_request_type_t.REGISTER)
                        {
                            /**** with expires */
                            contact = String.Format("m: <{0}:{1}@{2}:{3}>;expires={4}\r\n",
                                "sip",
                                from_uri.UserName,
                                "127.0.0.1",
                                5060,
                                TSK_Time.Milliseconds2Seconds(mExpires));
                        }
                        else
                        {
                            /**** without expires */
                            if (request.RequestType == TSIP_Message.tsip_request_type_t.SUBSCRIBE)
                            {
                                /* RFC 3265 - 3.1.1. Subscription Duration
							        An "expires" parameter on the "Contact" header has no semantics for SUBSCRIBE and is explicitly 
							        not equivalent to an "Expires" header in a SUBSCRIBE request or response.
						        */
                                request.AddHeader(new TSIP_HeaderExpires(TSK_Time.Milliseconds2Seconds(mExpires)));
                            }
                            contact = String.Format("m: <{0}:{1}@{2}:{3}>\r\n",
                                "sip",
                                from_uri.UserName,
                                "127.0.0.1",
                                5060);
                        }

                        hdr_contacts = TSIP_HeaderContact.Parse(contact);
                        if (hdr_contacts != null && hdr_contacts.Count > 0)
                        {
                            request.Contact = hdr_contacts[0];
                        }

                        /* Add capabilities as per RFC 3840 */
                        if (request.Contact != null)
                        {
                            foreach (TSK_Param param in mSipSession.Caps)
                            {
                                request.Contact.Params = TSK_Param.AddParam(request.Contact.Params, param.Name, param.Value);
                            }
                        }

                        break;
                    }
            }

            /* Update authorizations */
            if (mState == tsip_dialog_state_t.Initial && mChallenges == null || mChallenges.Count == 0)
            {
                /* 3GPP TS 33.978 6.2.3.1 Procedures at the UE
			        On sending a REGISTER request in order to indicate support for early IMS security procedures, the UE shall not
			        include an Authorization header field and not include header fields or header field values as required by RFC3329.
		        */
                if (request.IsREGISTER && !this.Stack.EarlyIMS)
                {
                    /*	3GPP TS 24.229 - 5.1.1.2.2 Initial registration using IMS AKA
				        On sending a REGISTER request, the UE shall populate the header fields as follows:
					        a) an Authorization header field, with:
					        - the "username" header field parameter, set to the value of the private user identity;
					        - the "realm" header field parameter, set to the domain name of the home network;
					        - the "uri" header field parameter, set to the SIP URI of the domain name of the home network;
					        - the "nonce" header field parameter, set to an empty value; and
					        - the "response" header field parameter, set to an empty value;
			        */
                    String realm = this.Stack.Realm != null ? this.Stack.Realm.Host : "(null)";
                    String request_uri_ = TSIP_Uri.ToString(request.Uri, false, false);
                    TSIP_Header auth_hdr = TSIP_Challenge.CreateEmptyAuthorization(this.Stack.PrivateIdentity, realm, request_uri_);
                    if (auth_hdr != null)
                    {
                        request.AddHeader(auth_hdr);
                    }
                }
            }
            else if (mChallenges != null && mChallenges.Count > 0)
            {
                TSIP_Header auth_hdr;
                foreach (TSIP_Challenge challenge in mChallenges)
                {
                    auth_hdr = challenge.CreateHeaderAuthorization(request);
                    if (auth_hdr != null)
                    {
                        request.AddHeader(auth_hdr);
                    }
                }
            }

            /* Update CSeq */
            /*	RFC 3261 - 13.2.2.4 2xx Responses
               Generating ACK: The sequence number of the CSeq header field MUST be
               the same as the INVITE being acknowledged, but the CSeq method MUST
               be ACK.  The ACK MUST contain the same credentials as the INVITE.  If
               the 2xx contains an offer (based on the rules above), the ACK MUST
               carry an answer in its body.
               ==> CSeq number will be added/updated by the caller of this function,
               credentials were added above.
            */
            if (!request.IsACK && !request.IsCANCEL)
            {
                request.CSeq.CSeq = ++mCSeqValue;
            }

            /* Route generation 
		        *	==> http://betelco.blogspot.com/2008/11/proxy-and-service-route-discovery-in.html
		        * The dialog Routes have been copied above.

		        3GPP TS 24.229 - 5.1.2A.1 UE-originating case

		        The UE shall build a proper preloaded Route header field value for all new dialogs and standalone transactions. The UE
		        shall build a list of Route header field values made out of the following, in this order:
		        a) the P-CSCF URI containing the IP address or the FQDN learnt through the P-CSCF discovery procedures; and
		        b) the P-CSCF port based on the security mechanism in use:

		        - if IMS AKA or SIP digest with TLS is in use as a security mechanism, the protected server port learnt during
		        the registration procedure;
		        - if SIP digest without TLS, NASS-IMS bundled authentciation or GPRS-IMS-Bundled authentication is in
		        use as a security mechanism, the unprotected server port used during the registration procedure;
		        c) and the values received in the Service-Route header field saved from the 200 (OK) response to the last
		        registration or re-registration of the public user identity with associated contact address.
	        */
            if (!request.IsREGISTER)
            {// According to the above link ==> Initial/Re/De registration do not have routes.
                if (copy_routes_start != -1)
                { /* The dialog already have routes ==> copy them. */
                    if (mState == tsip_dialog_state_t.Early || mState == tsip_dialog_state_t.Established)
                    {
                        Int32 index = -1;
                        foreach (TSIP_HeaderRecordRoute record_route in mRecordRoutes)
                        {
                            TSIP_Uri uri = record_route.Uri;
                            if (++index < copy_routes_start || uri == null)
                            {
                                continue;
                            }
                            TSIP_HeaderRoute route = new TSIP_HeaderRoute(uri);
                            // copy parameters: see http://code.google.com/p/imsdroid/issues/detail?id=52
                            route.Params.AddRange(record_route.Params);
                            request.AddHeader(route);
                        }
                    }
                }
            }
            else
            {/* No routes associated to this dialog. */
                if (mState == tsip_dialog_state_t.Initial || mState == tsip_dialog_state_t.Early)
                {
                    /*	GPP TS 24.229 section 5.1.2A [Generic procedures applicable to all methods excluding the REGISTER method]:
					    The UE shall build a proper preloaded Route header field value for all new dialogs and standalone transactions. The UE
					    shall build a list of Route header field values made out of the following, in this order:
					    a) the P-CSCF URI containing the IP address or the FQDN learnt through the P-CSCF discovery procedures; and
					    b) the P-CSCF port based on the security mechanism in use:
						    - if IMS AKA or SIP digest with TLS is in use as a security mechanism, the protected server port learnt during
						    the registration procedure;
						    - if SIP digest without TLS, NASS-IMS bundled authentciation or GPRS-IMS-Bundled authentication is in
						    use as a security mechanism, the unprotected server port used during the registration procedure;
					    c) and the values received in the Service-Route header field saved from the 200 (OK) response to the last
					    registration or re-registration of the public user identity with associated contact address.
				    */
#if _DEBUG && SDS_HACK
                    /* Ericsson SDS hack (INVITE with Proxy-CSCF as First route fail) */
#else
                    TSIP_Uri uri = this.Stack.GetProxyCSCFUri(true);
                    // Proxy-CSCF as first route
                    if (uri != null)
                    {
                        request.AddHeader(new TSIP_HeaderRoute(uri));
                    }
#endif
                    // Service routes
                    foreach (TSIP_Uri uriServiceRoute in mServiceRoutes)
                    {
                        request.AddHeader(new TSIP_HeaderRoute(uriServiceRoute));
                    }
                }
            }

            /* Add headers associated to the dialog's session */
            foreach (TSK_Param param in mSipSession.Headers)
            {
                request.AddHeader(new TSIP_HeaderDummy(param.Name, param.Value));
            }

            /* Add headers associated to the dialog's stack */
            foreach (TSK_Param param in this.Stack.Headers)
            {
                request.AddHeader(new TSIP_HeaderDummy(param.Name, param.Value));
            }

            /* Add common headers */
            this.AddCommonHeaders(request);

            /* SigComp */
            

            return request;
        }

        protected Boolean SenRequest(TSIP_Request request)
        {
            TSIP_Transac transact = this.Stack.LayerTransac.CreateTransac(true, request, this);
            switch (transact.Type)
            {
                case TSIP_Transac.tsip_transac_type_t.ICT:
                case TSIP_Transac.tsip_transac_type_t.NICT:
                    {
                        /* Start the newly create IC/NIC transaction */
                        return transact.Start(request);
                    }
            }
            return false;
        }

        /// <summary>
        /// Updates the dialog state:
        /// - Authorizations (using challenges from the @a response message)
        /// - State (early, established, disconnected, ...)
        /// - Routes (and Service-Route)
        /// - Target (remote)
        /// - ...
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected Boolean UpdateWithResponse(TSIP_Response response)
        {
            if (response == null || response.To == null || response.CSeq == null)
            {
                return false;
            }
            String tag = response.To.Tag;

            /* 
		    *	1xx (!100) or 2xx 
		    */
            /*
            *	401 or 407 or 421 or 494
            */
            if (response.StatusCode == 401 || response.StatusCode == 407 || response.StatusCode == 421 || response.StatusCode == 494)
            {
                Boolean acceptNewVector;

                /* 3GPP IMS - Each authentication vector is used only once.
                *	==> Re-registration/De-registration ==> Allow 401/407 challenge.
                */
                acceptNewVector = (response.CSeq.RequestType == TSIP_Message.tsip_request_type_t.REGISTER && this.State == tsip_dialog_state_t.Established);
                return this.UpdateChallenges(response, acceptNewVector);
            }
            else if (100 < response.StatusCode && response.StatusCode < 300)
            {
                tsip_dialog_state_t state = this.State;

                /* 1xx */
                if (response.StatusCode <= 199)
                {
                    if (String.IsNullOrEmpty(response.To.Tag))
                    {
                        TSK_Debug.Error("Invalid tag  parameter");
                        return false;
                    }
                    state = tsip_dialog_state_t.Early;
                }
                /* 2xx */
                else
                {
                    state = tsip_dialog_state_t.Established;
                }

                /* Remote target */
                {
                    /*	RFC 3261 12.2.1.2 Processing the Responses
                        When a UAC receives a 2xx response to a target refresh request, it
                        MUST replace the dialog's remote target URI with the URI from the
                        Contact header field in that response, if present.

                        FIXME: Because PRACK/UPDATE sent before the session is established MUST have
                        the rigth target URI to be delivered to the UAS ==> Do not not check that we are connected
                    */
                    if (response.CSeq.RequestType != TSIP_Message.tsip_request_type_t.REGISTER && response.Contact != null && response.Contact.Uri != null)
                    {
                        mUriRemoteTarget = response.Contact.Uri.Clone(true, false);
                    }
                }

                /* Route sets */
			    {
				    int index;
				    TSIP_HeaderRecordRoute recordRoute;

                    mRecordRoutes.Clear();

                    for (index = 0; (recordRoute = response.GetHeaderAtIndex(TSIP_Header.tsip_header_type_t.Record_Route, index) as TSIP_HeaderRecordRoute) != null; index++)
                    {
                        mRecordRoutes.Insert(0, recordRoute); /* Copy reversed. */
				    }
			    }

                /* cseq + tags + ... */
                if (this.State == tsip_dialog_state_t.Established && String.Equals(mTagRemote, tag, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
                else
                {
                    if (response.CSeq.RequestType != TSIP_Message.tsip_request_type_t.REGISTER && response.CSeq.RequestType != TSIP_Message.tsip_request_type_t.PUBLISH)
                    { /* REGISTER and PUBLISH don't establish dialog */
                        mTagRemote = tag;
                    }
#if NEVER_EXECUTE_00	// PRACK and BYE will have same CSeq value ==> Let CSeq value to be incremented by "tsip_dialog_request_new()"
				self->cseq_value = response->CSeq ? response->CSeq->seq : self->cseq_value;
#endif
                }

                this.State = state;
                return true;
            }

            return true;
        }

        protected Boolean UpdateWithINVITE(TSIP_Request invite)
        {
            if (invite == null)
            {
                return false;
            }

            /* Remote target */
            if (invite.Contact != null && invite.Contact.Uri != null)
            {
                mUriRemoteTarget = invite.Contact.Uri.Clone(true, false);
            }

            /* cseq + tags + remote-uri */
            mTagRemote = invite.From != null ? invite.From.Tag : "doubango";

            /* self->cseq_value = invite->CSeq ? invite->CSeq->seq : self->cseq_value; */
            if (invite.From != null && invite.From.Uri != null)
            {
                mUriRemote = invite.From.Uri;
            }

            /* Route sets */
	        {
		        int index;
		        TSIP_HeaderRecordRoute recordRoute;

                mRecordRoutes.Clear();

		        for(index = 0; (recordRoute = (invite.GetHeaderAtIndex(TSIP_Header.tsip_header_type_t.Record_Route, index)) as TSIP_HeaderRecordRoute) != null; index++)
                {
			        mRecordRoutes.Insert(0, recordRoute);/* Copy non-reversed. */
		        }
	        }

            this.State = tsip_dialog_state_t.Established;

            return true;
        }

        private Boolean UpdateChallenges(TSIP_Response response, Boolean acceptNewVector)
        {
            Boolean ret = true;
            TSIP_HeaderWWWAuthenticate WWW_Authenticate;
            // TSIP_HeaderProxyAuthenticate Proxy_Authenticate;

            /* RFC 2617 - HTTP Digest Session

	        *	(A) The client response to a WWW-Authenticate challenge for a protection
		        space starts an authentication session with that protection space.
		        The authentication session lasts until the client receives another
		        WWW-Authenticate challenge from any server in the protection space.

		        (B) The server may return a 401 response with a new nonce value, causing the client
		        to retry the request; by specifying stale=TRUE with this response,
		        the server tells the client to retry with the new nonce, but without
		        prompting for a new username and password.
	        */
            /* RFC 2617 - 1.2 Access Authentication Framework
                The realm directive (case-insensitive) is required for all authentication schemes that issue a challenge.
            */

            /* FIXME: As we perform the same task ==> Use only one loop.
            */

            for(int i =0; (WWW_Authenticate = (TSIP_HeaderWWWAuthenticate)response.GetHeaderAtIndex(TSIP_Header.tsip_header_type_t.WWW_Authenticate, i)) != null; i++)
            {
		        Boolean isnew = true;

		        foreach(TSIP_Challenge challenge in mChallenges)
                {
			        //if(challenge.IProxy)
                    //{
                    //    continue;
                    //}
        			
			        if(String.Equals(challenge.Realm, WWW_Authenticate.Realm, StringComparison.InvariantCultureIgnoreCase) && (WWW_Authenticate.Stale || acceptNewVector))
                    {
				        /*== (B) ==*/
				        if(!(ret = challenge.Update(WWW_Authenticate.Scheme, 
					        WWW_Authenticate.Realm,
					        WWW_Authenticate.Nonce, 
					        WWW_Authenticate.Opaque, 
					        WWW_Authenticate.Algorithm, 
					        WWW_Authenticate.Qop)))
				        {
					        return ret;
				        }
				        else
                        {
					        isnew = false;
					        continue;
				        }
			        }
			        else
                    {
				        TSK_Debug.Error("Failed to handle new challenge");
				        return false;
			        }
		        }

		        if(isnew)
                {
                    TSIP_Challenge challenge;
			        if((challenge = new TSIP_Challenge(this.Stack,
					        false, 
					        WWW_Authenticate.Scheme, 
					        WWW_Authenticate.Realm, 
					        WWW_Authenticate.Nonce, 
					        WWW_Authenticate.Opaque, 
					        WWW_Authenticate.Algorithm, 
					        WWW_Authenticate.Qop)) != null)
			        {
                        mChallenges.Add(challenge);
			        }
			        else
                    {
				        TSK_Debug.Error("Failed to handle new challenge");
				        return false;
			        }
		        }
	        }

            return ret;
        }

        protected Int64 GetNewDelay(TSIP_Response response)
        {
            Int64 expires = mExpires;
            Int64 newdelay = expires;	/* default value */
            TSIP_Header hdr;
            int i;

            /*== NOTIFY with subscription-state header with expires parameter */
            if (response.CSeq.RequestType == TSIP_Message.tsip_request_type_t.NOTIFY)
            {
                TSIP_HeaderSubscriptionState hdr_state = response.GetHeader(TSIP_Header.tsip_header_type_t.Subscription_State) as TSIP_HeaderSubscriptionState;
                if (hdr_state != null && hdr_state.Expires > 0)
                {
                    expires = TSK_Time.Seconds2Milliseconds(hdr_state.Expires);
                    goto compute;
                }
            }

            /*== Expires header */
            if((hdr = response.GetHeader(TSIP_Header.tsip_header_type_t.Expires)) != null)
            {
                expires = TSK_Time.Seconds2Milliseconds((hdr as TSIP_HeaderExpires).DeltaSeconds);
                goto compute;
            }

            /*== Contact header */
            for(i=0; (hdr = response.GetHeaderAtIndex(TSIP_Header.tsip_header_type_t.Contact, i)) != null; i++)
            {
                TSIP_HeaderContact contact = hdr as TSIP_HeaderContact;

                if(contact != null && contact.Uri != null)
                {
                    String transport = TSK_Param.GetValueByName(contact.Uri.Params, "transport");
                    TSIP_Uri contactUri = this.Stack.GetContactUri(String.IsNullOrEmpty(transport) ? "udp" : transport);
                    if(contactUri != null)
                    {
                    if(String.Equals(contact.Uri.UserName, contactUri.UserName)
					        && String.Equals(contact.Uri.Host, contactUri.Host)
					        && contact.Uri.Port == contactUri.Port)
				        {
                            if(contact.Expires >= 0){ /* No expires parameter ==> -1*/
						        expires = TSK_Time.Seconds2Milliseconds(contact.Expires);
						        goto compute;
					        }
                        }
                    }
                }
            }

        /*
	    *	3GPP TS 24.229 - 
	    *
	    *	The UE shall reregister the public user identity either 600 seconds before the expiration time if the initial 
	    *	registration was for greater than 1200 seconds, or when half of the time has expired if the initial registration 
	    *	was for 1200 seconds or less.
	    */
        compute:
            expires = TSK_Time.Milliseconds2Seconds(expires);
            newdelay = (expires > 1200) ? (expires - 600) : (expires / 2);

            return TSK_Time.Seconds2Milliseconds(newdelay);
        }

        public Boolean ExecuteAction(Int32 fsmActionId, TSIP_Message message, TSIP_Action action)
        {
            if (this.StateMachine != null)
            {
                return this.StateMachine.ExecuteAction(fsmActionId, this, message, this, message, action);
            }

            TSK_Debug.Error("Invalid FSM");

            return false;
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

        private void AddCommonHeaders(TSIP_Request request)
        {
            //
	        //	P-Preferred-Identity
	        //
            if (this.Stack.PreferredIdentity != null)
            {
                /*	3GPP TS 33.978 6.2.3.1 Procedures at the UE
			        The UE shall use the temporary public user identity (IMSI-derived IMPU, cf. section 6.1.2) only in registration
			        messages (i.e. initial registration, re-registration or de-registration), but not in any other type of SIP requests.
		        */
                switch (request.RequestType)
                {
                    case TSIP_Message.tsip_request_type_t.BYE:
                    case TSIP_Message.tsip_request_type_t.INVITE:
                    case TSIP_Message.tsip_request_type_t.OPTIONS:
                    case TSIP_Message.tsip_request_type_t.SUBSCRIBE:
                    case TSIP_Message.tsip_request_type_t.NOTIFY:
                    case TSIP_Message.tsip_request_type_t.REFER:
                    case TSIP_Message.tsip_request_type_t.MESSAGE:
                    case TSIP_Message.tsip_request_type_t.PUBLISH:
                    case TSIP_Message.tsip_request_type_t.REGISTER:
                        {
                            if (!this.Stack.EarlyIMS || (this.Stack.EarlyIMS && request.IsREGISTER))
                            {
                                // FIXME
                                // TSIP_MESSAGE_ADD_HEADER(request, TSIP_HEADER_P_PREFERRED_IDENTITY_VA_ARGS(preferred_identity));
                            }
                            break;
                        }
                }

                //
                //	P-Access-Network-Info
                //
            }
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
