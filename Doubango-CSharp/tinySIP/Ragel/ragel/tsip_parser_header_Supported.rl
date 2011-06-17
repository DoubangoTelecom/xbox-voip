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
	machine tsip_machine_parser_header_Supported;

	# Includes
	include tsip_machine_utils "./ragel/tsip_machine_utils.rl";
	
	action tag{
		tag_start = p;
	}
	
	action parse_option{
		hdr_supported.Options = TSK_RagelState.Parser.AddString(data, p, tag_start, hdr_supported.Options);
	}

	action eob{
	}
	
	Supported = ( "Supported"i | "k"i ) HCOLON ( option_tag>tag %parse_option ( COMMA option_tag>tag %parse_option )* )?;
	
	# Entry point
	main := Supported :>CRLF @eob;

}%%

using System;
using Doubango_CSharp.tinySAK;
using System.Collections.Generic;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderSupported : TSIP_Header
	{
		private List<String> mOptions;

		public TSIP_HeaderSupported()
			: this((String)null)
		{
		}

        public TSIP_HeaderSupported(String option)
            : this(new List<String>(new String[]{option}))
        {
        }

        public TSIP_HeaderSupported(List<String> options)
			: base(tsip_header_type_t.Supported)
		{
            if (options != null)
            {
                this.Options.AddRange(options.FindAll((x) => { return !String.IsNullOrEmpty(x); }));
            }
		}

		public override String Value
        {
            get 
            { 
				String ret = String.Empty;
                foreach(String option in this.Options)
                {
                    if(String.IsNullOrEmpty(ret))
                    {
                        ret = option;
                    }
                    else
                    {
                        ret += String.Format(",{0}", option);
                    }
                }
                return ret;
            }
            set { TSK_Debug.Error("Not implemented"); }
        }

		public List<String> Options
		{
			get
			{
				if(mOptions == null)
				{
					mOptions = new List<String>();
				}
				return mOptions;
			}
			set{ mOptions = value; }
		}

		public Boolean IsSupported(String option)
		{
			return this.Options.Exists(
                (x) => { return x.Equals(option, StringComparison.InvariantCultureIgnoreCase); }
            );
		}

		%%write data;

		public static TSIP_HeaderSupported Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderSupported hdr_supported = new TSIP_HeaderSupported();

			int tag_start = 0;
			
			%%write init;
			%%write exec;
			
			if( cs < %%{ write first_final; }%% ){
				TSK_Debug.Error("Failed to parse SIP 'Supported' header.");
				hdr_supported.Dispose();
				hdr_supported = null;
			}
			
			return hdr_supported;
		}
	}
}