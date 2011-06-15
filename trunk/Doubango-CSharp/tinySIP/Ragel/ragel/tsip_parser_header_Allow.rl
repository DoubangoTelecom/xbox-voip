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
	machine tsip_machine_parser_header_Allow;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag
	{
		tag_start = p;
	}
	
	action parse_method
	{
		String method = TSK_RagelState.Parser.GetString(data, p, tag_start);
        if (!String.IsNullOrEmpty(method))
        {
            hdr_allow.Methods.Add(method);
        }
	}

	action eob
	{
	}
	
	Allow = "Allow"i HCOLON ( Method>tag %parse_method ( COMMA Method>tag %parse_method )* )?;
	
	# Entry point
	main := Allow :>CRLF @eob;

}%%


using System;
using Doubango_CSharp.tinySAK;
using System.Collections.Generic;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderAllow : TSIP_Header
    {
		public const String TSIP_HEADER_ALLOW_DEFAULT = "ACK, BYE, CANCEL, INVITE, MESSAGE, NOTIFY, OPTIONS, PRACK, REFER, UPDATE";

		private List<String> mMethods;

		public TSIP_HeaderAllow()
            : this(null)
        {
        }

        public TSIP_HeaderAllow(String methods)
            : base(tsip_header_type_t.Allow)
        {
            if (methods != null)
            {
                this.Methods.AddRange(methods.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            }
        }

		public List<String> Methods
		{
			get 
			{ 
				if(mMethods == null)
				{
					mMethods = new List<String>();
				}
				return mMethods; 
			}
		}

		public Boolean IsAllowed(String method)
		{
			return this.Methods.Exists(
                (x) => { return x.Equals(method, StringComparison.InvariantCultureIgnoreCase); }
            );
		}
		
		public override String Value
        {
            get 
            { 
                String ret = String.Empty;
                foreach(String method in this.Methods)
                {
                    if(String.IsNullOrEmpty(ret))
                    {
                        ret = method;
                    }
                    else
                    {
                        ret += String.Format(",{0}", method);
                    }
                }
                return ret; 
            }
            set
            {
                TSK_Debug.Error("Not implemented");
            }
        }
		
		%%write data;

		public static TSIP_HeaderAllow Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderAllow hdr_allow = new TSIP_HeaderAllow();
			
			int tag_start = 0;

			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Allow' header.");
				hdr_allow.Dispose();
				hdr_allow = null;
			}
			
			return hdr_allow;
		}
	}
}