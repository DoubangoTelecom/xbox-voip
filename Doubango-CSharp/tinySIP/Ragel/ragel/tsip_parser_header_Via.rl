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

/***********************************
*	Ragel state machine.
*/
%%{
	machine tsip_machine_parser_header_Via;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";


	action tag{
		tag_start = p;
	}

	action create_via{
		if(curr_via == null){
			curr_via = new TSIP_HeaderVia();
		}
	}

	action parse_protocol_name{
		curr_via.ProtoName = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_protocol_version{
		curr_via.ProtoVersion = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_host{
		curr_via.Host = TSK_RagelState.Parser.GetString(data, p, tag_start);
		if(!String.IsNullOrEmpty(curr_via.Host) && curr_via.Host[0] == '['){
			curr_via.Host = TSK_String.UnQuote(curr_via.Host, '[', ']');
		}
	}

	action parse_port{
		curr_via.Port = (UInt16)TSK_RagelState.Parser.GetUInt32(data, p, tag_start);
	}

	action parse_transport{
		curr_via.Transport = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_ttl{
		curr_via.TTL = TSK_RagelState.Parser.GetInt32(data, p, tag_start);
	}

	action parse_maddr{
		curr_via.Maddr = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	
	action parse_received{
		curr_via.Received = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_branch{
		curr_via.Branch = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_comp{
		curr_via.Comp = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_rport{
		curr_via.RPort = TSK_RagelState.Parser.GetInt32(data, p, tag_start);
	}

	action has_rport{
		if(curr_via.RPort < 0){
			curr_via.RPort = 0;
		}
	}

	action parse_param{
		if(curr_via != null){
			curr_via.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, curr_via.Params);
		}
	}
	
	action add_via{
		if(curr_via != null){
			hdr_vias.Add(curr_via);
			curr_via = null;
		}
	}

	action eob{
		
	}

	protocol_name = "SIP"i | token >tag %parse_protocol_name;
	protocol_version = token >tag %parse_protocol_version;
	transport = ("UDP"i | "TCP"i | "TLS"i | "SCTP"i | "TLS-SCTP"i | other_transport) >tag %parse_transport;
	sent_protocol = protocol_name SLASH protocol_version SLASH transport;
	sent_by = host>tag %parse_host ( COLON port >tag %parse_port )?;
	via_ttl = "ttl"i EQUAL ttl >tag %parse_ttl;
	via_maddr = "maddr"i EQUAL host >tag %parse_maddr;
	via_received = "received"i EQUAL ( IPv4address | IPv6address )>tag %parse_received;
	via_branch = "branch"i EQUAL token >tag %parse_branch;
	via_compression = "comp"i EQUAL ( "sigcomp"i | other_compression )>tag %parse_comp;
	response_port = "rport"i ( EQUAL DIGIT+ >tag %parse_rport )? %has_rport;
	via_extension = (generic_param) >tag %parse_param;
	via_params = (via_ttl | via_maddr | via_received | via_branch | via_compression | response_port)@1 | (via_extension)@0;
	via_parm = (sent_protocol LWS sent_by ( SEMI via_params )*) >create_via %add_via;
	Via = ( "Via"i | "v"i ) HCOLON via_parm ( COMMA via_parm )*;
	
	# Entry point
	main := Via :>CRLF @eob;
}%%


using System;
using Doubango.tinySAK;
using System.Collections.Generic;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderVia : TSIP_Header
	{
		private String mBranch;
		private String mHost;
		private UInt16 mPort;
		private String mComp;
		private String mSigcompId;
		private String mReceived;
		private String mMaddr;
		private String mProtoName;
		private String mProtoVersion;
		private String mTransport;
	
		private Int32 mRPort;
		private Int32 mTTL;

		internal const String PROTO_NAME_DEFAULT = "SIP";
        internal const String PROTO_VERSION_DEFAULT = "2.0";

        public TSIP_HeaderVia()
            :this(null,null,null,null,0)
        {
        }

		public TSIP_HeaderVia(String protoName, String protoVersion, String transport, String host, UInt16 port)
			: base(tsip_header_type_t.Via)
		{
			this.ProtoName = protoName;
            this.ProtoVersion = protoVersion;
            this.Transport = transport;
            this.Host = host;
            this.Port = port;

			this.RPort = -1;
			this.TTL = -1;
		}

        public override String Value
        {
            /* SIP/2.0/UDP [::]:1988;test=1234;comp=sigcomp;rport=254;ttl=457;received=192.0.2.101;branch=z9hG4bK1245420841406\r\n" */
            get 
            { 
                Boolean isIPv6 = !String.IsNullOrEmpty(this.Host) && this.Host.Contains(":");
				return String.Format("{0}/{1}/{2} {3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}",
                        !String.IsNullOrEmpty(this.ProtoName) ? this.ProtoName : "SIP",

                        !String.IsNullOrEmpty(this.ProtoVersion) ? this.ProtoVersion : "2.0",

                        !String.IsNullOrEmpty(this.Transport) ? this.Transport : "UDP",

			            isIPv6 ? "[" : String.Empty,
                        !String.IsNullOrEmpty(this.Host) ? this.Host : "127.0.0.1",
			            isIPv6 ? "]" : String.Empty,

			            this.Port>0 ? ":" : String.Empty,
			            this.Port>0 ? this.Port.ToString() : String.Empty,

                        !String.IsNullOrEmpty(this.mMaddr) ? ";maddr=" : String.Empty,
                        !String.IsNullOrEmpty(this.mMaddr) ? this.mMaddr : String.Empty,

                        !String.IsNullOrEmpty(this.SigcompId) ? ";sigcomp-id=" : String.Empty,
                        !String.IsNullOrEmpty(this.SigcompId) ? this.SigcompId : String.Empty,

                        !String.IsNullOrEmpty(this.Comp) ? ";comp=" : String.Empty,
                        !String.IsNullOrEmpty(this.Comp) ? this.Comp : String.Empty,

			            this.RPort>=0 ? (this.RPort>0? ";rport=" : ";rport") : String.Empty,
                        this.RPort>0 ? this.RPort.ToString() : String.Empty,

                        this.TTL>=0 ? (this.TTL>0? ";ttl=" : ";ttl") : String.Empty,
                        this.TTL>0 ? this.TTL.ToString() : String.Empty,

			            !String.IsNullOrEmpty(this.Received) ? ";received=" : String.Empty,
			            !String.IsNullOrEmpty(this.Received) ? this.Received : String.Empty,

                        !String.IsNullOrEmpty(this.Branch) ? ";branch=" : String.Empty,
			            !String.IsNullOrEmpty(this.Branch) ? this.Branch : String.Empty
                    );
            }
            set { TSK_Debug.Error("Not implemented"); }
        }

		public String Branch
		{
			get{ return mBranch; }
			set{ mBranch = value; }
		}

		public String Host
		{
			get{ return mHost; }
			set{ mHost = value; }
		}

		public UInt16 Port
		{
			get{ return mPort; }
			set{ mPort = value; }
		}
		
		public String Comp
		{
			get{ return mComp; }
			set{ mComp = value; }
		}
		
		public String SigcompId
		{
			get{ return mSigcompId; }
			set{ mSigcompId = value; }
		}

		public String Received
		{
			get{ return mReceived; }
			set{ mReceived = value; }
		}

		public String Maddr
		{
			get{ return mMaddr; }
			set{ mMaddr = value; }
		}
		
		public String ProtoName
		{
			get{ return mProtoName; }
			set{ mProtoName = value; }
		}

		public String ProtoVersion
		{
			get{ return mProtoVersion; }
			set{ mProtoVersion = value; }
		}

		public String Transport
		{
			get{ return mTransport; }
			set{ mTransport = value; }
		}

		public Int32 RPort
		{
			get{ return mRPort; }
			set{ mRPort = value; }
		}

		public Int32 TTL
		{
			get{ return mTTL; }
			set{ mTTL = value; }
		}

		%%write data;

		public static List<TSIP_HeaderVia> Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			List<TSIP_HeaderVia> hdr_vias = new List<TSIP_HeaderVia>();
			TSIP_HeaderVia curr_via = null;

			int tag_start = 0;

			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Via' header.");
				hdr_vias.Clear();
				hdr_vias = null;
				curr_via.Dispose();
				curr_via = null;
			}
			
			return hdr_vias;
		}
	}
}