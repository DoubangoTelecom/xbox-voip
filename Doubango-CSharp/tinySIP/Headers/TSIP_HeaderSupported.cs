
/* #line 1 "./ragel/tsip_parser_header_Supported.rl" */
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

/* #line 47 "./ragel/tsip_parser_header_Supported.rl" */


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

		
/* #line 103 "../Headers/TSIP_HeaderSupported.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Supported_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2
};

static readonly sbyte[] _tsip_machine_parser_header_Supported_key_offsets =  new sbyte [] {
	0, 0, 4, 7, 24, 25, 41, 45, 
	46, 48, 51, 68, 69, 71, 87, 89, 
	91, 93, 95, 97, 99, 101, 103
};

static readonly char[] _tsip_machine_parser_header_Supported_trans_keys =  new char [] {
	'\u004b', '\u0053', '\u006b', '\u0073', '\u0009', '\u0020', '\u003a', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u000a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u002c', 
	'\u007e', '\u002a', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u0009', '\u000d', '\u0020', '\u002c', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u0020', '\u002c', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0055', 
	'\u0075', '\u0050', '\u0070', '\u0050', '\u0070', '\u004f', '\u006f', '\u0052', 
	'\u0072', '\u0054', '\u0074', '\u0045', '\u0065', '\u0044', '\u0064', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Supported_single_lengths =  new sbyte [] {
	0, 4, 3, 7, 1, 8, 4, 1, 
	2, 3, 7, 1, 2, 6, 2, 2, 
	2, 2, 2, 2, 2, 2, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Supported_range_lengths =  new sbyte [] {
	0, 0, 0, 5, 0, 4, 0, 0, 
	0, 0, 5, 0, 0, 5, 0, 0, 
	0, 0, 0, 0, 0, 0, 0
};

static readonly byte[] _tsip_machine_parser_header_Supported_index_offsets =  new byte [] {
	0, 0, 5, 9, 22, 24, 37, 42, 
	44, 47, 51, 64, 66, 69, 81, 84, 
	87, 90, 93, 96, 99, 102, 105
};

static readonly sbyte[] _tsip_machine_parser_header_Supported_indicies =  new sbyte [] {
	0, 2, 0, 2, 1, 0, 0, 3, 
	1, 3, 4, 3, 5, 5, 5, 5, 
	5, 5, 5, 5, 5, 1, 6, 1, 
	7, 8, 7, 9, 9, 9, 10, 9, 
	9, 9, 9, 9, 1, 11, 12, 11, 
	13, 1, 14, 1, 15, 15, 1, 15, 
	15, 13, 1, 13, 16, 13, 5, 5, 
	5, 5, 5, 5, 5, 5, 5, 1, 
	17, 1, 18, 18, 1, 18, 18, 5, 
	5, 5, 5, 5, 5, 5, 5, 5, 
	1, 19, 19, 1, 20, 20, 1, 21, 
	21, 1, 22, 22, 1, 23, 23, 1, 
	24, 24, 1, 25, 25, 1, 0, 0, 
	1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Supported_trans_targs =  new sbyte [] {
	2, 0, 14, 3, 4, 5, 22, 6, 
	4, 5, 10, 6, 7, 10, 8, 9, 
	11, 12, 13, 15, 16, 17, 18, 19, 
	20, 21
};

static readonly sbyte[] _tsip_machine_parser_header_Supported_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 1, 5, 3, 
	3, 0, 3, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0
};

const int tsip_machine_parser_header_Supported_start = 1;
const int tsip_machine_parser_header_Supported_first_final = 22;
const int tsip_machine_parser_header_Supported_error = 0;

const int tsip_machine_parser_header_Supported_en_main = 1;


/* #line 120 "./ragel/tsip_parser_header_Supported.rl" */

		public static TSIP_HeaderSupported Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderSupported hdr_supported = new TSIP_HeaderSupported();

			int tag_start = 0;
			
			
/* #line 199 "../Headers/TSIP_HeaderSupported.cs" */
	{
	cs = tsip_machine_parser_header_Supported_start;
	}

/* #line 132 "./ragel/tsip_parser_header_Supported.rl" */
			
/* #line 206 "../Headers/TSIP_HeaderSupported.cs" */
	{
	sbyte _klen;
	byte _trans;
	sbyte _acts;
	sbyte _nacts;
	sbyte _keys;

	if ( p == pe )
		goto _test_eof;
	if ( cs == 0 )
		goto _out;
_resume:
	_keys = _tsip_machine_parser_header_Supported_key_offsets[cs];
	_trans = (byte)_tsip_machine_parser_header_Supported_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Supported_single_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Supported_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Supported_trans_keys[_mid] )
				_lower = (sbyte) (_mid + 1);
			else {
				_trans += (byte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (sbyte) _klen;
		_trans += (byte) _klen;
	}

	_klen = _tsip_machine_parser_header_Supported_range_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Supported_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Supported_trans_keys[_mid+1] )
				_lower = (sbyte) (_mid + 2);
			else {
				_trans += (byte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (byte) _klen;
	}

_match:
	_trans = (byte)_tsip_machine_parser_header_Supported_indicies[_trans];
	cs = _tsip_machine_parser_header_Supported_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Supported_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Supported_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Supported_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Supported_actions[_acts++] )
		{
	case 0:
/* #line 31 "./ragel/tsip_parser_header_Supported.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 35 "./ragel/tsip_parser_header_Supported.rl" */
	{
		hdr_supported.Options = TSK_RagelState.Parser.AddString(data, p, tag_start, hdr_supported.Options);
	}
	break;
	case 2:
/* #line 39 "./ragel/tsip_parser_header_Supported.rl" */
	{
	}
	break;
/* #line 297 "../Headers/TSIP_HeaderSupported.cs" */
		default: break;
		}
	}

_again:
	if ( cs == 0 )
		goto _out;
	if ( ++p != pe )
		goto _resume;
	_test_eof: {}
	_out: {}
	}

/* #line 133 "./ragel/tsip_parser_header_Supported.rl" */
			
			if( cs < 
/* #line 314 "../Headers/TSIP_HeaderSupported.cs" */
22
/* #line 134 "./ragel/tsip_parser_header_Supported.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Supported' header.");
				hdr_supported.Dispose();
				hdr_supported = null;
			}
			
			return hdr_supported;
		}
	}
}