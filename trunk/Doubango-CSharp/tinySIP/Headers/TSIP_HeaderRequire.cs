
/* #line 1 "./ragel/tsip_parser_header_Require.rl" */
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

/* #line 47 "./ragel/tsip_parser_header_Require.rl" */



using System;
using Doubango_CSharp.tinySAK;
using System.Collections.Generic;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderRequire : TSIP_Header
	{
		private List<String> mOptions;

		public TSIP_HeaderRequire()
			: this((String)null)
		{
		}

        public TSIP_HeaderRequire(String option)
            : this(new List<String>(new String[]{option}))
        {
        }

        public TSIP_HeaderRequire(List<String> options)
			: base(tsip_header_type_t.Require)
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

		public Boolean IsRequired(String option)
		{
			return this.Options.Exists(
                (x) => { return x.Equals(option, StringComparison.InvariantCultureIgnoreCase); }
            );
		}

		
/* #line 104 "../Headers/TSIP_HeaderRequire.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Require_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2
};

static readonly sbyte[] _tsip_machine_parser_header_Require_key_offsets =  new sbyte [] {
	0, 0, 2, 4, 6, 8, 10, 12, 
	14, 17, 34, 35, 37, 53, 69, 73, 
	74, 76, 79, 80
};

static readonly char[] _tsip_machine_parser_header_Require_trans_keys =  new char [] {
	'\u0052', '\u0072', '\u0045', '\u0065', '\u0051', '\u0071', '\u0055', '\u0075', 
	'\u0049', '\u0069', '\u0052', '\u0072', '\u0045', '\u0065', '\u0009', '\u0020', 
	'\u003a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u002c', '\u007e', '\u002a', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u002c', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u002c', '\u000a', 
	(char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Require_single_lengths =  new sbyte [] {
	0, 2, 2, 2, 2, 2, 2, 2, 
	3, 7, 1, 2, 6, 8, 4, 1, 
	2, 3, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Require_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 5, 0, 0, 5, 4, 0, 0, 
	0, 0, 0, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Require_index_offsets =  new sbyte [] {
	0, 0, 3, 6, 9, 12, 15, 18, 
	21, 25, 38, 40, 43, 55, 68, 73, 
	75, 78, 82, 84
};

static readonly sbyte[] _tsip_machine_parser_header_Require_indicies =  new sbyte [] {
	0, 0, 1, 2, 2, 1, 3, 3, 
	1, 4, 4, 1, 5, 5, 1, 6, 
	6, 1, 7, 7, 1, 7, 7, 8, 
	1, 8, 9, 8, 10, 10, 10, 10, 
	10, 10, 10, 10, 10, 1, 11, 1, 
	12, 12, 1, 12, 12, 10, 10, 10, 
	10, 10, 10, 10, 10, 10, 1, 13, 
	14, 13, 15, 15, 15, 16, 15, 15, 
	15, 15, 15, 1, 17, 18, 17, 8, 
	1, 19, 1, 20, 20, 1, 20, 20, 
	8, 1, 21, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Require_trans_targs =  new sbyte [] {
	2, 0, 3, 4, 5, 6, 7, 8, 
	9, 10, 13, 11, 12, 14, 18, 13, 
	9, 14, 15, 16, 17, 19
};

static readonly sbyte[] _tsip_machine_parser_header_Require_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 1, 0, 0, 3, 3, 0, 
	3, 0, 0, 0, 0, 5
};

const int tsip_machine_parser_header_Require_start = 1;
const int tsip_machine_parser_header_Require_first_final = 19;
const int tsip_machine_parser_header_Require_error = 0;

const int tsip_machine_parser_header_Require_en_main = 1;


/* #line 121 "./ragel/tsip_parser_header_Require.rl" */

		public static TSIP_HeaderRequire Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderRequire hdr_require = new TSIP_HeaderRequire();

			int tag_start = 0;
			
			
/* #line 193 "../Headers/TSIP_HeaderRequire.cs" */
	{
	cs = tsip_machine_parser_header_Require_start;
	}

/* #line 133 "./ragel/tsip_parser_header_Require.rl" */
			
/* #line 200 "../Headers/TSIP_HeaderRequire.cs" */
	{
	sbyte _klen;
	sbyte _trans;
	sbyte _acts;
	sbyte _nacts;
	sbyte _keys;

	if ( p == pe )
		goto _test_eof;
	if ( cs == 0 )
		goto _out;
_resume:
	_keys = _tsip_machine_parser_header_Require_key_offsets[cs];
	_trans = (sbyte)_tsip_machine_parser_header_Require_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Require_single_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Require_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Require_trans_keys[_mid] )
				_lower = (sbyte) (_mid + 1);
			else {
				_trans += (sbyte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (sbyte) _klen;
		_trans += (sbyte) _klen;
	}

	_klen = _tsip_machine_parser_header_Require_range_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Require_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Require_trans_keys[_mid+1] )
				_lower = (sbyte) (_mid + 2);
			else {
				_trans += (sbyte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (sbyte) _klen;
	}

_match:
	_trans = (sbyte)_tsip_machine_parser_header_Require_indicies[_trans];
	cs = _tsip_machine_parser_header_Require_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Require_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Require_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Require_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Require_actions[_acts++] )
		{
	case 0:
/* #line 31 "./ragel/tsip_parser_header_Require.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 35 "./ragel/tsip_parser_header_Require.rl" */
	{
		hdr_require.Options = TSK_RagelState.Parser.AddString(data, p, tag_start, hdr_require.Options);
	}
	break;
	case 2:
/* #line 39 "./ragel/tsip_parser_header_Require.rl" */
	{
	}
	break;
/* #line 291 "../Headers/TSIP_HeaderRequire.cs" */
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

/* #line 134 "./ragel/tsip_parser_header_Require.rl" */
			
			if( cs < 
/* #line 308 "../Headers/TSIP_HeaderRequire.cs" */
19
/* #line 135 "./ragel/tsip_parser_header_Require.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Require' header.");
				hdr_require.Dispose();
				hdr_require = null;
			}
			
			return hdr_require;
		}
	}
}