/* Copyright (C) 2010-2011 Mamadou Diop. 
* Copyright (C) 2011 Doubango Telecom <http://www.doubango.org>
*
* Contact: Mamadou Diop <diopmamadou(at)doubango.org>
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
	machine tsip_machine_parser_header_Subscription_State;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}

	action parse_state{
		hdr_Subscription_State.State = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_reason{
		hdr_Subscription_State.Reason = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}

	action parse_expires{
		hdr_Subscription_State.Expires = TSK_RagelState.Parser.GetInt32(data, p, tag_start);
	}

	action parse_retry_after{
		hdr_Subscription_State.RetryAfter = TSK_RagelState.Parser.GetInt32(data, p, tag_start);
	}

	action parse_param{
		hdr_Subscription_State.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, hdr_Subscription_State.Params);
	}

	action eob{
	}
	
	subexp_params = (( "reason"i EQUAL token>tag %parse_reason ) | ( "expires"i EQUAL delta_seconds>tag %parse_expires ) | ( "retry-after"i EQUAL delta_seconds>tag %parse_retry_after ))@1 | generic_param>tag %parse_param @0;
	Subscription_State = ( "Subscription-State"i ) HCOLON token>tag %parse_state ( SEMI subexp_params )*;
	
	# Entry point
	main := Subscription_State :>CRLF @eob;

}%%

using System;
using Doubango.tinySAK;
using System.Collections.Generic;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderSubscriptionState : TSIP_Header
	{
		private String mState;
		private String mReason;
		private Int32 mExpires;
		private Int32 mRetryAfter;

		public TSIP_HeaderSubscriptionState()
            : base(tsip_header_type_t.Subscription_State)
        {
            mExpires = -1;
			mRetryAfter = -1;
        }


		public String State
        {
            get { return mState; }
            set{ mState = value; }
        }

		public String Reason
        {
            get { return mReason; }
            set{ mReason = value; }
        }

		public Int32 Expires
        {
            get { return mExpires; }
            set{ mExpires = value; }
        }

		public Int32 RetryAfter 
        {
            get { return mRetryAfter; }
            set{ mRetryAfter = value; }
        }

		public override String Value
        {
            get 
            {                 
                String ret = String.Format("{0}{1}{1}", 
			        this.State,
        			
                    !String.IsNullOrEmpty(this.Reason) ? ";reason=" : String.Empty,
			        !String.IsNullOrEmpty(this.Reason) ? this.Reason : String.Empty				
			        );

		        if(this.Expires >= 0)
                {
                    ret+= String.Format(";expires={0}", this.Expires);
		        }
		        if(this.RetryAfter >= 0)
                {
                    ret+= String.Format(";retry-after={0}", this.RetryAfter);
		        }
                return ret; 
            }
            set
            {
                TSK_Debug.Error("Not implemented");
            }
        }

		%%write data;

		public static TSIP_HeaderSubscriptionState Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderSubscriptionState hdr_Subscription_State = new TSIP_HeaderSubscriptionState();
			
			int tag_start = 0;
			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Subscription-State' header.");
				hdr_Subscription_State.Dispose();
				hdr_Subscription_State = null;
			}
			
			return hdr_Subscription_State;
		}
	}
}