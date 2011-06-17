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
using Doubango_CSharp.tinySIP.Headers;
using Doubango_CSharp.tinySAK;
using tsip_header_type_t = Doubango_CSharp.tinySIP.Headers.TSIP_Header.tsip_header_type_t;

namespace Doubango_CSharp.tinySIP
{
    /// <summary>
    /// Abstract class representing a SIP Message (Request or Response)
    /// </summary>
    public abstract class TSIP_Message : IDisposable
    {
        public enum tsip_message_type_t
        {
            Unknown,
            Request,
            Response
        }

        public enum tsip_request_type_t
        {
            NONE = 0,

            ACK,
            BYE,
            CANCEL,
            INVITE,
            OPTIONS,
            REGISTER,
            SUBSCRIBE,
            NOTIFY,
            REFER,
            INFO,
            UPDATE,
            MESSAGE,
            PUBLISH,
            PRACK
        }

        private String mVersion;
        private tsip_message_type_t mType;

        // Most common headers
        private TSIP_HeaderVia mFirstVia;
        private TSIP_HeaderFrom mFrom;
        private TSIP_HeaderTo mTo;
        private TSIP_HeaderContact mContact;
        private TSIP_HeaderCallId mCallId;
        private TSIP_HeaderCSeq mCSeq;
        private TSIP_HeaderExpires mExpires;
        private TSIP_HeaderContentType mContentType;
        private TSIP_HeaderContentLength mContentLength;

        // Other headers
        private List<TSIP_Header> mHeaders;

        // To hack the message
        private String mSigCompId;
        private Boolean mShouldUpdate;

        private byte[] mContent;

        ~TSIP_Message()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (mFirstVia != null)
            {
                mFirstVia.Dispose();
            }
            if (mFrom != null)
            {
                mFrom.Dispose();
            }
            if (mTo != null)
            {
                mTo.Dispose();
            }
            if (mContact != null)
            {
                mContact.Dispose();
            }
            if (mCallId != null)
            {
                mCallId.Dispose();
            }
            if (mCSeq != null)
            {
                mCSeq.Dispose();
            }
            if (mExpires != null)
            {
                mExpires.Dispose();
            }
            if (mContentType != null)
            {
                mContentType.Dispose();
            }
            if (mContentLength != null)
            {
                mContentLength.Dispose();
            }
        }

        public String Version
        {
            get { return mVersion; }
            set { mVersion = value; }
        }

        public tsip_message_type_t Type
        {
            get { return mType; }
            set { mType = value; }
        }


        public TSIP_HeaderVia FirstVia
        {
            get { return mFirstVia; }
            set { mFirstVia = value; }
        }

        public TSIP_HeaderFrom From
        {
            get { return mFrom; }
            set { mFrom = value; }
        }

        public TSIP_HeaderTo To
        {
            get { return mTo; }
            set { mTo = value; }
        }

        public TSIP_HeaderContact Contact
        {
            get { return mContact; }
            set { mContact = value; }
        }

        public TSIP_HeaderCallId CallId
        {
            get { return mCallId; }
            set { mCallId = value; }
        }

        public TSIP_HeaderCSeq CSeq
        {
            get { return mCSeq; }
            set { mCSeq = value; }
        }

        public TSIP_HeaderExpires Expires
        {
            get { return mExpires; }
            set { mExpires = value; }
        }

        public TSIP_HeaderContentType ContentType
        {
            get { return mContentType; }
            set { mContentType = value; }
        }

        public TSIP_HeaderContentLength ContentLength
        {
            get { return mContentLength; }
            set { mContentLength = value; }
        }

        // Other headers
        public List<TSIP_Header> Headers
        {
            get
            {
                if (mHeaders == null)
                {
                    mHeaders = new List<TSIP_Header>();
                }
                return mHeaders;
            }
            
        }

        // To hack the message
        public String SigCompId
        {
            get { return mSigCompId; }
            set { mSigCompId = value; }
        }

        public Boolean ShouldUpdate
        {
            get { return mShouldUpdate; }
            set { mShouldUpdate = value; }
        }

        public Int64 ExpiresValue
        {
            get
            {
                return this.GetExpiresValue();
            }
        }

        public UInt32 ContentLengthValue
        {
            get
            {
                return this.GetContentLengthValue();
            }
        }

        public byte[] Content
        {
            get
            {
                return mContent;
            }
        }

        public Boolean AddHeaders(TSIP_Header[] headers)
        {
            if (headers == null)
            {
                TSK_Debug.Error("Invalid paramerer");
                return false;
            }

            foreach(TSIP_Header header in headers)
            {
                switch(header.Type)
                {
                    case TSIP_Header.tsip_header_type_t.Via:
                        {
                            if(this.FirstVia == null)
                            {
                                this.FirstVia = (header as TSIP_HeaderVia);
                                continue;
                            }
                            break;
                        }

                        case TSIP_Header.tsip_header_type_t.From:
                        {
                            if(this.From == null)
                            {
                                this.From = (header as TSIP_HeaderFrom);
                                continue;
                            }
                            break;
                        }

                        case TSIP_Header.tsip_header_type_t.To:
                        {
                            if(this.To == null)
                            {
                                this.To = (header as TSIP_HeaderTo);
                                continue;
                            }
                            break;
                        }

                        case TSIP_Header.tsip_header_type_t.Contact:
                        {
                            if(this.Contact == null)
                            {
                                this.Contact = (header as TSIP_HeaderContact);
                                continue;
                            }
                            break;
                        }

                        case TSIP_Header.tsip_header_type_t.Call_ID:
                        {
                            if(this.CallId == null)
                            {
                                this.CallId = (header as TSIP_HeaderCallId);
                                continue;
                            }
                            break;
                        }

                        case TSIP_Header.tsip_header_type_t.CSeq:
                        {
                            if(this.CSeq == null)
                            {
                                this.CSeq = (header as TSIP_HeaderCSeq);
                                continue;
                            }
                            break;
                        }

                        case TSIP_Header.tsip_header_type_t.Content_Type:
                        {
                            if(this.ContentType == null)
                            {
                                this.ContentType = (header as TSIP_HeaderContentType);
                                continue;
                            }
                            break;
                        }

                        case TSIP_Header.tsip_header_type_t.Content_Length:
                        {
                            if(this.ContentLength == null)
                            {
                                this.ContentLength = (header as TSIP_HeaderContentLength);
                                continue;
                            }
                            break;
                        }
                }

                this.Headers.Add(header);
            }

            return true;
        }

        public Boolean AddHeaders(List<TSIP_Header> headers)
        {
            if(headers == null)
            {
                TSK_Debug.Error("Invalid paramerer");
                return false;
            }

            return this.AddHeaders(headers.ToArray());
        }

        public Boolean AddContent(String contentType, byte[]content)
        {
            if (!String.IsNullOrEmpty(contentType))
            {
                this.ContentType = new TSIP_HeaderContentType(contentType);
            }
            if (content != null)
            {
                mContent = content;
                this.ContentLength = new TSIP_HeaderContentLength((uint)mContent.Length);
            }

            return true;
        }

        public TSIP_Header GetHeaderAtIndex(tsip_header_type_t type, int index)
        {
	        /* Do not forget to update tinyWRAP::SipMessage::getHeaderAt() */
	        int pos = 0;
	        TSIP_Header hdr = null;
            		
	        switch(type)
	        {
	        case tsip_header_type_t.Via:
		        if(index == 0){
			        hdr = this.FirstVia;
			        goto bail;
		        }else pos++; break;
	        case tsip_header_type_t.From:
		        if(index == 0){
			        hdr = this.From;
			        goto bail;
		        }else pos++; break;
	        case tsip_header_type_t.To:
		        if(index == 0){
			        hdr = this.To;
			        goto bail;
		        }else pos++; break;
	        case tsip_header_type_t.Contact:
		        if(index == 0){
			        hdr = this.Contact;
			        goto bail;
		        }else pos++; break;
	        case tsip_header_type_t.Call_ID:
		        if(index == 0){
			        hdr = this.CallId;
			        goto bail;
		        }else pos++; break;
	        case tsip_header_type_t.CSeq:
		        if(index == 0){
			        hdr = this.CSeq;
			        goto bail;
		        }else pos++; break;
	        case tsip_header_type_t.Expires:
		        if(index == 0){
			        hdr = this.Expires;
			        goto bail;
		        }else pos++; break;
	        case tsip_header_type_t.Content_Type:
		        if(index == 0){
			        hdr = this.ContentType;
			        goto bail;
		        }else pos++; break;
	        case tsip_header_type_t.Content_Length:
		        if(index == 0){
			        hdr = this.ContentLength;
			        goto bail;
		        }else pos++; break;
	        default:
		        break;
	        }

            foreach(TSIP_Header h in this.Headers)
            {
                if(h.Type == type)
                {
                    if(pos++ >= index)
                    {
                        hdr = h;
                        break;
                    }
                }
            }	        

        bail:
	        return hdr;
        }

        public TSIP_Header GetHeader(tsip_header_type_t type)
        {
            return this.GetHeaderAtIndex(type, 0);
        }

        public Boolean IsAllowed(String method)
        {
            int index = 0;
            TSIP_HeaderAllow hdr_allow;

            while ((hdr_allow = this.GetHeaderAtIndex(tsip_header_type_t.Allow, index++) as TSIP_HeaderAllow) != null)
            {
                if (hdr_allow.IsAllowed(method))
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean IsSupported(String option)
        {
            int index = 0;
            TSIP_HeaderSupported hdr_supported;

            while ((hdr_supported = this.GetHeaderAtIndex(tsip_header_type_t.Supported, index++) as TSIP_HeaderSupported) != null)
            {
                if (hdr_supported.IsSupported(option))
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean IsRequired(String option)
        {
            int index = 0;
            TSIP_HeaderRequire hdr_require;

            while ((hdr_require = this.GetHeaderAtIndex(tsip_header_type_t.Require, index++) as TSIP_HeaderRequire) != null)
            {
                if (hdr_require.IsRequired(option))
                {
                    return true;
                }
            }
            return false;
        }

        public Int64 GetExpiresValue()
        {
            if (this.Expires != null)
            {
                return this.Expires.DeltaSeconds;
            }

            // FIXME: You MUST choose the right contact
            if (this.Contact != null)
            {
                return this.Contact.Expires;
            }
            return -1;
        }

        public UInt32 GetContentLengthValue()
        {
            if (this.Content != null)
            {
                return (UInt32)this.Content.Length;
            }
            return 0;
        }
       
        public static tsip_request_type_t GetRequestType(String method)
        {
            if (String.IsNullOrEmpty(method))
            {
                return tsip_request_type_t.NONE;
            }

            if (method.Equals("ACK", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.ACK;
            }
            else if (method.Equals("BYE", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.BYE;
            }
            else if (method.Equals("CANCEL", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.CANCEL;
            }
            else if (method.Equals("INVITE", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.INVITE;
            }
            else if (method.Equals("OPTIONS", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.OPTIONS;
            }
            else if (method.Equals("REGISTER", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.REGISTER;
            }
            else if (method.Equals("SUBSCRIBE", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.SUBSCRIBE;
            }
            else if (method.Equals("NOTIFY", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.NOTIFY;
            }
            else if (method.Equals("REFER", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.REFER;
            }
            else if (method.Equals("INFO", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.INFO;
            }
            else if (method.Equals("UPDATE", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.UPDATE;
            }
            else if (method.Equals("MESSAGE", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.MESSAGE;
            }
            else if (method.Equals("PUBLISH", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.PUBLISH;
            }
            else if (method.Equals("PRACK", StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.PRACK;
            }

            return tsip_request_type_t.NONE;
        }
    }

    #region Request

    /// <summary>
    /// SIP Request
    /// </summary>
    public class TSIP_Request : TSIP_Message
    {
        private String mMethod;
        private TSIP_Uri mUri;
        private tsip_request_type_t mRequestType;

        public String Method
        {
            get { return mMethod; }
            set { mMethod = value; }
        }

        public TSIP_Uri Uri
        {
            get { return mUri; }
            set { mUri = value; }
        }

        public tsip_request_type_t RequestType
        {
            get { return mRequestType; }
            set { mRequestType = value; }
        }
    }

    #endregion


    #region Response

    /// <summary>
    /// SIP Response
    /// </summary>
    public class TSIP_Response : TSIP_Message
    {
        private ushort mStatusCode;
        private String mReasonPhrase;

        public ushort StatusCode
        {
            get { return mStatusCode; }
            set { mStatusCode = value; }
        }

        public String ReasonPhrase
        {
            get { return mReasonPhrase; }
            set { mReasonPhrase = value; }
        }
    }

    #endregion
}
