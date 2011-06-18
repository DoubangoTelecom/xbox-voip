using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Headers
{
    public abstract class TSIP_Header : IDisposable
    {
        public enum tsip_header_type_t
        {
	        Accept,
	        Accept_Contact,
	        Accept_Encoding,
	        Accept_Language,
	        Accept_Resource_Priority,
	        Alert_Info,
	        Allow,
	        Allow_Events,
	        Authentication_Info,
	        Authorization,
	        Call_ID,
	        Call_Info,
	        Contact,
	        Content_Disposition,
	        Content_Encoding,
	        Content_Language,
	        Content_Length,
	        Content_Type,
	        CSeq,
	        Date,
	        Dummy,
	        Error_Info,
	        Event,
	        Expires,
	        From,
	        History_Info,
	        Identity,
	        Identity_Info,
	        In_Reply_To,
	        Join,
	        Max_Forwards,
	        MIME_Version,
	        Min_Expires,
	        Min_SE,
	        Organization,
	        Path,
	        Priority,
	        Privacy,
	        Proxy_Authenticate,
	        Proxy_Authorization,
	        Proxy_Require,
	        RAck,
	        Reason,
	        Record_Route,
	        Refer_Sub,
	        Refer_To,
	        Referred_By,
	        Reject_Contact,
	        Replaces,
	        Reply_To,
	        Request_Disposition,
	        Require,
	        Resource_Priority,
	        Retry_After,
	        Route,
	        RSeq,
	        Security_Client,
	        Security_Server,
	        Security_Verify,
	        Server,
	        Service_Route,
	        Session_Expires,
	        SIP_ETag,
	        SIP_If_Match,
	        Subject,
	        Subscription_State,
	        Supported,
	        Target_Dialog,
	        Timestamp,
	        To,
	        Unsupported,
	        User_Agent,
	        Via,
	        Warning,
	        WWW_Authenticate,
	        P_Access_Network_Info,
	        P_Answer_State,
	        P_Asserted_Identity,
	        P_Associated_URI,
	        P_Called_Party_ID,
	        P_Charging_Function_Addresses,
	        P_Charging_Vector,
	        P_DCS_Billing_Info,
	        P_DCS_LAES,
	        P_DCS_OSPS,
	        P_DCS_Redirect,
	        P_DCS_Trace_Party_ID,
	        P_Early_Media,
	        P_Media_Authorization,
	        P_Preferred_Identity,
	        P_Profile_Key,
	        P_User_Database,
	        P_Visited_Network_ID
        };

        protected readonly tsip_header_type_t mType;
        protected List<TSK_Param> mParams;

        public TSIP_Header(tsip_header_type_t type)
        {
            mType = type;
        }

        public void Dispose()
        {
        }

        public List<TSK_Param> Params
        {
            get 
            {
                if (mParams == null)
                {
                    mParams = new List<TSK_Param>();
                }
                return mParams; 
            }
            set { mParams = value; }
        }

        public tsip_header_type_t Type
        {
            get { return mType; }
        }

        public virtual String Name
        {
            get { return TSIP_Header.GetName(this); }
            set { }
        }

        public virtual char ParamSeparator
        {
            get { return TSIP_Header.GetParamSeparator(this); }
        }

        public abstract String Value
        {
            get;
            set;
        }

        public override String ToString()
        {
            return this.ToString(true, true, true);
        }

        public String ToString(Boolean with_name, Boolean with_crlf, Boolean with_params)
        {
            return TSIP_Header.ToString(this, with_name, with_crlf, with_params);
        }

        public static String ToString(TSIP_Header header, Boolean with_name, Boolean with_crlf, Boolean with_params)
        {
            if (header != null)
            {
                String @params = String.Empty;
                if (with_params)
                {
                    foreach(TSK_Param param in header.Params)
                    {
                        @params += String.Format(!String.IsNullOrEmpty(param.Value) ? "{0}{1}={2}":"{0}{1}", header.ParamSeparator, param.Name, param.Value);
                    }
                }
                return String.Format("{0}{1}{2}{3}{4}",
                    with_name ? header.Name : String.Empty,
                    with_name ? ": " : String.Empty,
                    header.Value,
                    @params,
                    with_crlf ? "\r\n" : String.Empty);
            }
            return String.Empty;
        }


#if TSIP_COMPACT_HEADERS
    const String _Accept_Contact = "a";
    const String _Referred_By  = "b";
    const String _Content_Type  = "c";
    const String _Request_Disposition = "d";
    const String _Content_Encoding  = "e";
    const String _From  = "f";
    const String _Call_ID  = "i";
    const String _Reject_Contact = "j";
    const String _Supported  = "k";
    const String _Content_Length  = "l";
    const String _Contact = "m";
    const String _Identity_Info = "n";
    const String _Event  = "o";
    const String _Refer_To  = "r";
    const String _Subject  = "s";
    const String _To  = "t";
    const String _Allow_Events  = "u";
    const String _Via  = "v";
    const String _Session_Expires = "x";
    const String _Identity  = "y";
#else
    const String _Accept_Contact = "Accept-Contact";
    const String _Referred_By  = "Referred-By";
    const String _Content_Type  = "Content-Type";
    const String _Request_Disposition = "Request-Disposition";
    const String _Content_Encoding  = "Content-Encoding";
    const String _From  = "From";
    const String _Call_ID  = "Call-ID";
    const String _Reject_Contact = "Reject-Contact";
    const String _Supported  = "Supported";
    const String _Content_Length  = "Content-Length";
    const String _Contact  = "Contact";
    const String _Identity_Info  = "Identity-Info";
    const String _Event  = "Event";
    const String _Refer_To  = "Refer-To";
    const String _Subject  = "Subject";
    const String _To  = "To";
    const String _Allow_Events  = "Allow-Events";
    const String _Via  = "Via";
    const String _Session_Expires = "Session-Expires";
    const String _Identity  = "Identity";
#endif
        public static String GetName(tsip_header_type_t type)
        {
            switch (type)
            {
                case tsip_header_type_t.Accept: return "Accept";
                case tsip_header_type_t.Accept_Contact: return TSIP_Header._Accept_Contact;
                case tsip_header_type_t.Accept_Encoding: return "Accept-Encoding";
                case tsip_header_type_t.Accept_Language: return "Accept-Language";
                case tsip_header_type_t.Accept_Resource_Priority: return "Accept-Resource-Priority";
                case tsip_header_type_t.Alert_Info: return "Alert-Info";
                case tsip_header_type_t.Allow: return "Allow";
                case tsip_header_type_t.Allow_Events: return TSIP_Header._Allow_Events;
                case tsip_header_type_t.Authentication_Info: return "Authentication-Info";
                case tsip_header_type_t.Authorization: return "Authorization";
                case tsip_header_type_t.Call_ID: return _Call_ID;
                case tsip_header_type_t.Call_Info: return "Call-Info";
                case tsip_header_type_t.Contact: return _Contact;
                case tsip_header_type_t.Content_Disposition: return "Content-Disposition";
                case tsip_header_type_t.Content_Encoding: return TSIP_Header._Content_Encoding;
                case tsip_header_type_t.Content_Language: return "Content-Language";
                case tsip_header_type_t.Content_Length: return TSIP_Header._Content_Length;
                case tsip_header_type_t.Content_Type: return TSIP_Header._Content_Type;
                case tsip_header_type_t.CSeq: return "CSeq";
                case tsip_header_type_t.Date: return "Date";
                case tsip_header_type_t.Error_Info: return "Error-Info";
                case tsip_header_type_t.Event: return TSIP_Header._Event;
                case tsip_header_type_t.Expires: return "Expires";
                case tsip_header_type_t.From: return TSIP_Header._From;
                case tsip_header_type_t.History_Info: return "History-Info";
                case tsip_header_type_t.Identity: return TSIP_Header._Identity;
                case tsip_header_type_t.Identity_Info: return TSIP_Header._Identity_Info;
                case tsip_header_type_t.In_Reply_To: return "In-Reply-To";
                case tsip_header_type_t.Join: return "Join";
                case tsip_header_type_t.Max_Forwards: return "Max-Forwards";
                case tsip_header_type_t.MIME_Version: return "MIME-Version";
                case tsip_header_type_t.Min_Expires: return "Min-Expires";
                case tsip_header_type_t.Min_SE: return "Min-SE";
                case tsip_header_type_t.Organization: return "Organization";
                case tsip_header_type_t.Path: return "Path";
                case tsip_header_type_t.Priority: return "Priority";
                case tsip_header_type_t.Privacy: return "Privacy";
                case tsip_header_type_t.Proxy_Authenticate: return "Proxy-Authenticate";
                case tsip_header_type_t.Proxy_Authorization: return "Proxy-Authorization";
                case tsip_header_type_t.Proxy_Require: return "Proxy-Require";
                case tsip_header_type_t.RAck: return "RAck";
                case tsip_header_type_t.Reason: return "Reason";
                case tsip_header_type_t.Record_Route: return "Record-Route";
                case tsip_header_type_t.Refer_Sub: return "Refer-Sub";
                case tsip_header_type_t.Refer_To: return TSIP_Header._Refer_To;
                case tsip_header_type_t.Referred_By: return TSIP_Header._Referred_By;
                case tsip_header_type_t.Reject_Contact: return TSIP_Header._Reject_Contact;
                case tsip_header_type_t.Replaces: return "Replaces";
                case tsip_header_type_t.Reply_To: return "Reply-To";
                case tsip_header_type_t.Request_Disposition: return TSIP_Header._Request_Disposition;
                case tsip_header_type_t.Require: return "Require";
                case tsip_header_type_t.Resource_Priority: return "Resource-Priority";
                case tsip_header_type_t.Retry_After: return "Retry-After";
                case tsip_header_type_t.Route: return "Route";
                case tsip_header_type_t.RSeq: return "RSeq";
                case tsip_header_type_t.Security_Client: return "Security-Client";
                case tsip_header_type_t.Security_Server: return "Security-Server";
                case tsip_header_type_t.Security_Verify: return "Security-Verify";
                case tsip_header_type_t.Server: return "Server";
                case tsip_header_type_t.Service_Route: return "Service-Route";
                case tsip_header_type_t.Session_Expires: return TSIP_Header._Session_Expires;
                case tsip_header_type_t.SIP_ETag: return "SIP-ETag";
                case tsip_header_type_t.SIP_If_Match: return "SIP-If-Match";
                case tsip_header_type_t.Subject: return TSIP_Header._Subject;
                case tsip_header_type_t.Subscription_State: return "Subscription-State";
                case tsip_header_type_t.Supported: return TSIP_Header._Supported;
                case tsip_header_type_t.Target_Dialog: return "Target-Dialog";
                case tsip_header_type_t.Timestamp: return "Timestamp";
                case tsip_header_type_t.To: return TSIP_Header._To;
                case tsip_header_type_t.Unsupported: return "Unsupported";
                case tsip_header_type_t.User_Agent: return "User-Agent";
                case tsip_header_type_t.Via: return TSIP_Header._Via;
                case tsip_header_type_t.Warning: return "Warning";
                case tsip_header_type_t.WWW_Authenticate: return "WWW-Authenticate";
                case tsip_header_type_t.P_Access_Network_Info: return "P-Access-Network-Info";
                case tsip_header_type_t.P_Answer_State: return "P-Answer-State";
                case tsip_header_type_t.P_Asserted_Identity: return "P-Asserted-Identity";
                case tsip_header_type_t.P_Associated_URI: return "P-Associated-URI";
                case tsip_header_type_t.P_Called_Party_ID: return "P-Called-Party-ID";
                case tsip_header_type_t.P_Charging_Function_Addresses: return "P-Charging-Function-Addresses";
                case tsip_header_type_t.P_Charging_Vector: return "P-Charging-Vector";
                case tsip_header_type_t.P_DCS_Billing_Info: return "P-DCS-Billing-Info";
                case tsip_header_type_t.P_DCS_LAES: return "P-DCS-LAES";
                case tsip_header_type_t.P_DCS_OSPS: return "P-DCS-OSPS";
                case tsip_header_type_t.P_DCS_Redirect: return "P-DCS-Redirect";
                case tsip_header_type_t.P_DCS_Trace_Party_ID: return "P-DCS-Trace-Party-ID";
                case tsip_header_type_t.P_Early_Media: return "P-Early-Media";
                case tsip_header_type_t.P_Media_Authorization: return "P-Media-Authorization";
                case tsip_header_type_t.P_Preferred_Identity: return "P-Preferred-Identity";
                case tsip_header_type_t.P_Profile_Key: return "P-Profile-Key";
                case tsip_header_type_t.P_User_Database: return "P-User-Database";
                case tsip_header_type_t.P_Visited_Network_ID: return "P-Visited-Network-ID";

                default: return "unknown-header";
            }
        }

        public static String GetName(TSIP_Header header)
        {
            if (header != null)
            {
                if (header.Type == tsip_header_type_t.Dummy)
                {
			        return (header as TSIP_HeaderDummy).Name;
		        }
		        else{
                    return TSIP_Header.GetName(header.Type);
		        }
	        }
	        return "unknown-header";
        }

        public static char GetParamSeparator(TSIP_Header header)
        {
            if (header != null)
	        {
                switch (header.Type)
		        {
                    case tsip_header_type_t.Authorization:
                    case tsip_header_type_t.Proxy_Authorization:
                    case tsip_header_type_t.Proxy_Authenticate:
                    case tsip_header_type_t.WWW_Authenticate:
			        {
				        return ',';
			        }

		        default:
			        {
				        return ';';
			        }
		        }
	        }
	        return ';';
        }
    }
}
