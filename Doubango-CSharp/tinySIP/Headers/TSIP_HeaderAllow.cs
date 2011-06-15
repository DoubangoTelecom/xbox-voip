
/* #line 1 "./ragel/tsip_parser_header_Allow.rl" */
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

/* #line 55 "./ragel/tsip_parser_header_Allow.rl" */



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
		
		
/* #line 104 "../Headers/TSIP_HeaderAllow.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Allow_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_key_offsets =  new sbyte [] {
	0, 0, 2, 4, 6, 8, 10, 13, 
	30, 31, 47, 51, 52, 54, 57, 74, 
	75, 77, 93
};

static readonly char[] _tsip_machine_parser_header_Allow_trans_keys =  new char [] {
	'\u0041', '\u0061', '\u004c', '\u006c', '\u004c', '\u006c', '\u004f', '\u006f', 
	'\u0057', '\u0077', '\u0009', '\u0020', '\u003a', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u002c', '\u007e', '\u002a', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', 
	'\u000d', '\u0020', '\u002c', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', 
	'\u002c', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_single_lengths =  new sbyte [] {
	0, 2, 2, 2, 2, 2, 3, 7, 
	1, 8, 4, 1, 2, 3, 7, 1, 
	2, 6, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 5, 
	0, 4, 0, 0, 0, 0, 5, 0, 
	0, 5, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_index_offsets =  new sbyte [] {
	0, 0, 3, 6, 9, 12, 15, 19, 
	32, 34, 47, 52, 54, 57, 61, 74, 
	76, 79, 91
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_indicies =  new sbyte [] {
	0, 0, 1, 2, 2, 1, 3, 3, 
	1, 4, 4, 1, 5, 5, 1, 5, 
	5, 6, 1, 6, 7, 6, 8, 8, 
	8, 8, 8, 8, 8, 8, 8, 1, 
	9, 1, 10, 11, 10, 12, 12, 12, 
	13, 12, 12, 12, 12, 12, 1, 14, 
	15, 14, 16, 1, 17, 1, 18, 18, 
	1, 18, 18, 16, 1, 16, 19, 16, 
	8, 8, 8, 8, 8, 8, 8, 8, 
	8, 1, 20, 1, 21, 21, 1, 21, 
	21, 8, 8, 8, 8, 8, 8, 8, 
	8, 8, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_trans_targs =  new sbyte [] {
	2, 0, 3, 4, 5, 6, 7, 8, 
	9, 18, 10, 8, 9, 14, 10, 11, 
	14, 12, 13, 15, 16, 17
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	1, 5, 3, 3, 0, 3, 0, 0, 
	0, 0, 0, 0, 0, 0
};

const int tsip_machine_parser_header_Allow_start = 1;
const int tsip_machine_parser_header_Allow_first_final = 18;
const int tsip_machine_parser_header_Allow_error = 0;

const int tsip_machine_parser_header_Allow_en_main = 1;


/* #line 128 "./ragel/tsip_parser_header_Allow.rl" */

		public static TSIP_HeaderAllow Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderAllow hdr_allow = new TSIP_HeaderAllow();
			
			int tag_start = 0;

			
			
/* #line 196 "../Headers/TSIP_HeaderAllow.cs" */
	{
	cs = tsip_machine_parser_header_Allow_start;
	}

/* #line 141 "./ragel/tsip_parser_header_Allow.rl" */
			
/* #line 203 "../Headers/TSIP_HeaderAllow.cs" */
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
	_keys = _tsip_machine_parser_header_Allow_key_offsets[cs];
	_trans = (sbyte)_tsip_machine_parser_header_Allow_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Allow_single_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Allow_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Allow_trans_keys[_mid] )
				_lower = (sbyte) (_mid + 1);
			else {
				_trans += (sbyte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (sbyte) _klen;
		_trans += (sbyte) _klen;
	}

	_klen = _tsip_machine_parser_header_Allow_range_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Allow_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Allow_trans_keys[_mid+1] )
				_lower = (sbyte) (_mid + 2);
			else {
				_trans += (sbyte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (sbyte) _klen;
	}

_match:
	_trans = (sbyte)_tsip_machine_parser_header_Allow_indicies[_trans];
	cs = _tsip_machine_parser_header_Allow_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Allow_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Allow_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Allow_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Allow_actions[_acts++] )
		{
	case 0:
/* #line 33 "./ragel/tsip_parser_header_Allow.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 38 "./ragel/tsip_parser_header_Allow.rl" */
	{
		String method = TSK_RagelState.Parser.GetString(data, p, tag_start);
        if (!String.IsNullOrEmpty(method))
        {
            hdr_allow.Methods.Add(method);
        }
	}
	break;
	case 2:
/* #line 47 "./ragel/tsip_parser_header_Allow.rl" */
	{
	}
	break;
/* #line 298 "../Headers/TSIP_HeaderAllow.cs" */
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

/* #line 142 "./ragel/tsip_parser_header_Allow.rl" */
			
			if( cs < 
/* #line 315 "../Headers/TSIP_HeaderAllow.cs" */
18
/* #line 143 "./ragel/tsip_parser_header_Allow.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Allow' header.");
				hdr_allow.Dispose();
				hdr_allow = null;
			}
			
			return hdr_allow;
		}
	}
}