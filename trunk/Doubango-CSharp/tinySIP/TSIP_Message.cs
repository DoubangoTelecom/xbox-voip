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
using Doubango.tinySIP.Headers;
using Doubango.tinySAK;
using tsip_header_type_t = Doubango.tinySIP.Headers.TSIP_Header.tsip_header_type_t;
using System.IO;

namespace Doubango.tinySIP
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

        public const String TSIP_MESSAGE_VERSION_10 = "SIP/1.0";
        public const String TSIP_MESSAGE_VERSION_20 = "SIP/2.0";
        public const String TSIP_MESSAGE_VERSION_DEFAULT = TSIP_MESSAGE_VERSION_20;

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

        protected TSIP_Message(tsip_message_type_t type)
        {
            mType = type;
        }

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
            get { return mContent; }
        }

        public Boolean IsRequest
        {
            get{ return this.Type == tsip_message_type_t.Request; }
        }

        public Boolean IsREGISTER
        {
            get { return  IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.REGISTER; }
        }

        public Boolean IsSUBSCRIBE
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.SUBSCRIBE; }
        }

        public Boolean IsPRACK
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.PRACK; }
        }

        public Boolean IsINVITE
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.INVITE; }
        }

        public Boolean IsBYE
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.BYE; }
        }

        public Boolean IsACK
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.ACK; }
        }

        public Boolean IsCANCEL
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.CANCEL; }
        }

        public Boolean IsINFO
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.INFO; }
        }

        public Boolean IsMESSAGE
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.MESSAGE; }
        }

        public Boolean IsNOTIFY
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.NOTIFY; }
        }

        public Boolean IsOPTIONS
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.OPTIONS; }
        }

        public Boolean IsPUBLISH
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.PUBLISH; }
        }

        public Boolean IsREFER
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.REFER; }
        }

        public Boolean IsUPDATE
        {
            get { return IsRequest && (this as TSIP_Request).RequestType == tsip_request_type_t.UPDATE; }
        }

        public Boolean IsResponse
        {
            get{ return this.Type == tsip_message_type_t.Response; }
        }

        public override String  ToString()
        {
            return this.ToStringEx(true);
        }


        public byte[] ToBytes()
        {
            String headers = this.ToStringEx(false);
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(headers)))
            {
                if (this.Content != null)
                {
                    stream.Write(this.Content, 0, this.Content.Length);
                }
#if PUBLICLY_VISIBLE_BUFFER
                bytes = stream.GetBuffer();
#else
                bytes = stream.ToArray(); // Will create a copy
#endif
            }
            return bytes;
       }

        private String ToStringEx(Boolean with_content)
        {
            String ret = String.Empty;

            if (this.IsRequest)
            {
                /* Method SP Request_URI SP SIP_Version CRLF */
                // Method
                ret += String.Format("{0} ", (this as TSIP_Request).Method);
                // Request URI (without quotes but with params)
                ret += (this as TSIP_Request).Uri.ToString(true, false);
                // SIP_Version
                ret += String.Format(" {0}\r\n", TSIP_MESSAGE_VERSION_DEFAULT);
            }
            else
            {
                /*SIP_Version SP Status_Code SP Reason_Phrase CRLF*/
                ret += String.Format("{0} {1} {2}\r\n",
                    TSIP_MESSAGE_VERSION_DEFAULT,
                    (this as TSIP_Response).StatusCode,
                    (this as TSIP_Response).ReasonPhrase);
            }

            // First Via
            if (this.FirstVia != null) ret += this.FirstVia;
            // From
            if (this.From != null) ret += this.From;
            // To
            if (this.To != null) ret += this.To;
            // Contact
            if (this.Contact != null) ret += this.Contact;
            // Call-Id
            if (this.CallId != null) ret += this.CallId;
            // CSeq
            if (this.CSeq != null) ret += this.CSeq;
            // Expires
            if (this.Expires != null) ret += this.Expires;
            // ContentType
            if (this.ContentType != null) ret += this.ContentType;
            // Content-Length
            if (this.ContentLength != null) ret += this.ContentLength;
            // All other headers
            foreach (TSIP_Header header in this.Headers) ret += header;

            // Empty line before the content
            ret += "\r\n";

            // add content as string
            if (with_content && this.Content != null)
            {
#if WINDOWS_PHONE
                ret += Encoding.UTF8.GetString(this.Content, 0, this.Content.Length);
#else
                ret += Encoding.UTF8.GetString(this.Content);
#endif
            }

            return ret;
        }

        public Boolean AddHeader(TSIP_Header header)
        {
            if (header != null)
            {
                return this.AddHeaders(new TSIP_Header[] { header });
            }
            return false;
        }

        public Boolean AddHeaders(List<TSIP_Header> headers)
        {
            if (headers != null)
            {
                return this.AddHeaders(headers.ToArray());
            }
            return false;
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
                if (header == null) continue;
                
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

            if (method.Equals(TSIP_Request.METHOD_ACK, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.ACK;
            }
            else if (method.Equals(TSIP_Request.METHOD_BYE, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.BYE;
            }
            else if (method.Equals(TSIP_Request.METHOD_CANCEL, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.CANCEL;
            }
            else if (method.Equals(TSIP_Request.METHOD_INVITE, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.INVITE;
            }
            else if (method.Equals(TSIP_Request.METHOD_OPTIONS, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.OPTIONS;
            }
            else if (method.Equals(TSIP_Request.METHOD_REGISTER, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.REGISTER;
            }
            else if (method.Equals(TSIP_Request.METHOD_SUBSCRIBE, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.SUBSCRIBE;
            }
            else if (method.Equals(TSIP_Request.METHOD_NOTIFY, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.NOTIFY;
            }
            else if (method.Equals(TSIP_Request.METHOD_REFER, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.REFER;
            }
            else if (method.Equals(TSIP_Request.METHOD_INFO, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.INFO;
            }
            else if (method.Equals(TSIP_Request.METHOD_UPDATE, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.UPDATE;
            }
            else if (method.Equals(TSIP_Request.METHOD_MESSAGE, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.MESSAGE;
            }
            else if (method.Equals(TSIP_Request.METHOD_PUBLISH, StringComparison.InvariantCultureIgnoreCase))
            {
                return tsip_request_type_t.PUBLISH;
            }
            else if (method.Equals(TSIP_Request.METHOD_PRACK, StringComparison.InvariantCultureIgnoreCase))
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

        public const String METHOD_ACK = "ACK";
        public const String METHOD_BYE = "BYE";
        public const String METHOD_CANCEL = "CANCEL";
        public const String METHOD_INVITE = "INVITE";
        public const String METHOD_OPTIONS = "OPTIONS";
        public const String METHOD_REGISTER = "REGISTER";
        public const String METHOD_SUBSCRIBE = "SUBSCRIBE";
        public const String METHOD_NOTIFY = "NOTIFY";
        public const String METHOD_REFER = "REFER";
        public const String METHOD_INFO = "INFO";
        public const String METHOD_UPDATE = "UPDATE";
        public const String METHOD_MESSAGE = "MESSAGE";
        public const String METHOD_PUBLISH = "PUBLISH";
        public const String METHOD_PRACK = "PRACK";

        public TSIP_Request(String method, TSIP_Uri requestUri, TSIP_Uri fromUri, TSIP_Uri toUri, String callId, Int32 cseq)
            :base(tsip_message_type_t.Request)
        {
            /* RFC 3261 8.1.1 Generating the Request
		        A valid SIP request formulated by a UAC MUST, at a minimum, contain
		        the following header fields: To, From, CSeq, Call-ID, Max-Forwards,
		        and Via; all of these header fields are mandatory in all SIP
		        requests.  These six header fields are the fundamental building
		        blocks of a SIP message, as they jointly provide for most of the
		        critical message routing services including the addressing of
		        messages, the routing of responses, limiting message propagation,
		        ordering of messages, and the unique identification of transactions.
		        These header fields are in addition to the mandatory request line,
		        which contains the method, Request-URI, and SIP version.
	        */
            this.Method = method;
            this.Uri = requestUri;
            this.AddHeaders(new TSIP_Header[]
                {
                    toUri == null ? null : new TSIP_HeaderTo(toUri.DisplayName, toUri, null),
                    fromUri == null ? null : new TSIP_HeaderFrom(fromUri.DisplayName, toUri, null),
                    /* Via will be added by the transport layer */
                    new TSIP_HeaderCSeq((uint)cseq, method),
                    String.IsNullOrEmpty(callId) ? null : new TSIP_HeaderCallId(callId),
                    new TSIP_HeaderMaxForwards(TSIP_HeaderMaxForwards.TSIP_HEADER_MAX_FORWARDS_DEFAULT),
                    /* Content-Length is mandatory for TCP. If both from and to are not null this means that it's a valid request */
                   toUri != null && fromUri != null ? new TSIP_HeaderContentLength(0) : null,
                }
            );
        }

        public String Method
        {
            get { return mMethod; }
            set 
            { 
                mMethod = value;
                mRequestType = TSIP_Message.GetRequestType(mMethod);
            }
        }

        public TSIP_Uri Uri
        {
            get { return mUri; }
            set { mUri = value; }
        }

        public tsip_request_type_t RequestType
        {
            get { return mRequestType; }
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

        public TSIP_Response(ushort statusScode, String reasonPhrase, TSIP_Request request)
            : base(tsip_message_type_t.Response)
        {
            this.StatusCode = statusScode;
            this.ReasonPhrase = reasonPhrase;
            this.AddHeaders(
                    new TSIP_Header[]
                    {
                        /* Content-Length is mandatory for TCP */
                        new TSIP_HeaderContentLength(0),
                    }
                );
            if (request != null)
            {
                /*
				RFC 3261 - 8.2.6.2 Headers and Tags

				The From field of the response MUST equal the From header field of
				the request.  The Call-ID header field of the response MUST equal the
				Call-ID header field of the request.  The CSeq header field of the
				response MUST equal the CSeq field of the request.  The Via header
				field values in the response MUST equal the Via header field values
				in the request and MUST maintain the same ordering.

				If a request contained a To tag in the request, the To header field
				in the response MUST equal that of the request.  However, if the To
				header field in the request did not contain a tag, the URI in the To
				header field in the response MUST equal the URI in the To header
				field; additionally, the UAS MUST add a tag to the To header field in
				the response (with the exception of the 100 (Trying) response, in
				which a tag MAY be present).  This serves to identify the UAS that is
				responding, possibly resulting in a component of a dialog ID.  The
				same tag MUST be used for all responses to that request, both final
				and provisional (again excepting the 100 (Trying)).  Procedures for
				the generation of tags are defined in Section 19.3.
				*/
                this.From = request.From;
                this.To = request.To;
				this.CallId = request.CallId;
                this.CSeq = request.CSeq;
                this.FirstVia = request.FirstVia;		
				/* All other VIAs */
                if(this.FirstVia != null)
                {
                    int index = 1;
                    TSIP_HeaderVia via;
                    while((via = (request.GetHeaderAtIndex(tsip_header_type_t.Via, index++) as TSIP_HeaderVia)) != null)
                    {
                        this.AddHeaders(new TSIP_Header[]{via});
                    }
                }
				/* Record routes */
#if WINDOWS_PHONE
                foreach (TSIP_Header header in request.Headers)
                {
                    if (header.Type == tsip_header_type_t.Record_Route)
                    {
                        this.AddHeader(header);
                    }
                }
#else
                this.AddHeaders(request.Headers.FindAll((x) => { return x.Type == tsip_header_type_t.Record_Route; }));
#endif
            }
        }

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
