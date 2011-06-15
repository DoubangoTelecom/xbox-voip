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
	machine tsip_machine_parser_header_CSeq;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}
	
	action parse_method{
		hdr_cseq.Method = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_seq{
		hdr_cseq.CSeq = TSK_RagelState.Parser.GetUInt32(data, p, tag_start);
	}

	action eob{
	}
	
	CSeq = "CSeq"i HCOLON DIGIT+>tag %parse_seq LWS Method >tag %parse_method;
	
	# Entry point
	main := CSeq :>CRLF @eob;

}%%

using System;
using Doubango_CSharp.tinySAK;
using Doubango_CSharp.tinySIP;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderCSeq : TSIP_Header
	{
		private String mMethod;
		private UInt32 mCSeq;
		private TSIP_Message.tsip_request_type_t mRequestType;

		public TSIP_HeaderCSeq()
            : this(0, null)
        {
        }
		
		public TSIP_HeaderCSeq(UInt32 cseq, String method)
            : base(tsip_header_type_t.CSeq)
        {
			this.CSeq = cseq;
			this.Method = method;
        }

		public override String Value
        {
            get 
            { 
				return String.Format("{0} {1}", this.CSeq, this.Method);
            }
            set
            {
                TSK_Debug.Error("Not implemented");
            }
        }

		public String Method
        {
            get { return mMethod; }
            set 
			{ 
				if((mMethod = value) != null)
				{
					this.RequestType = TSIP_Message.GetRequestType(mMethod);
				}
			}
        }

		public UInt32 CSeq
        {
            get { return mCSeq; }
            set { mCSeq = value; }
        }

		public TSIP_Message.tsip_request_type_t RequestType
        {
            get { return mRequestType; }
            set { mRequestType = value; }
        }

		%%write data;

		public static TSIP_HeaderCSeq Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderCSeq hdr_cseq = new TSIP_HeaderCSeq();
			
			int tag_start = 0;

			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'CSeq' header.");
				hdr_cseq.Dispose();
				hdr_cseq = null;
			}
			
			return hdr_cseq;
		}
	}
}