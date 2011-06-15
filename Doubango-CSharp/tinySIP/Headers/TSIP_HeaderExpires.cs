
/* #line 1 "./ragel/tsip_parser_header_Expires.rl" */
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

/* #line 47 "./ragel/tsip_parser_header_Expires.rl" */


using System;
using Doubango_CSharp.tinySAK;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderExpires : TSIP_Header
    {
		private Int64 mDeltaSeconds;
		private const Int64 TSIP_HEADER_EXPIRES_NONE = -1;
		private const Int64 TSIP_HEADER_EXPIRES_DEFAULT = 600000;

        public TSIP_HeaderExpires()
            : this(TSIP_HEADER_EXPIRES_NONE)
        {
        }

        public TSIP_HeaderExpires(Int64 deltaSeconds)
            : base(tsip_header_type_t.Expires)
        {
            mDeltaSeconds = deltaSeconds;
        }

		public Int64 DeltaSeconds
		{
			get{ return  mDeltaSeconds; }
			set{  mDeltaSeconds = value; }
		}

		public override String Value
        {
            get { return mDeltaSeconds >= 0 ? mDeltaSeconds.ToString() : String.Empty; }
            set 
            { 
                Int64 seconds;
                if (Int64.TryParse(value, out seconds))
                {
                    mDeltaSeconds = seconds;
                }
            }
        }

		
/* #line 74 "../Headers/TSIP_HeaderExpires.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Expires_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2
};

static readonly sbyte[] _tsip_machine_parser_header_Expires_key_offsets =  new sbyte [] {
	0, 0, 2, 4, 6, 8, 10, 12, 
	14, 17, 22, 23, 25, 29, 32, 33
};

static readonly char[] _tsip_machine_parser_header_Expires_trans_keys =  new char [] {
	'\u0045', '\u0065', '\u0058', '\u0078', '\u0050', '\u0070', '\u0049', '\u0069', 
	'\u0052', '\u0072', '\u0045', '\u0065', '\u0053', '\u0073', '\u0009', '\u0020', 
	'\u003a', '\u0009', '\u000d', '\u0020', '\u0030', '\u0039', '\u000a', '\u0009', 
	'\u0020', '\u0009', '\u0020', '\u0030', '\u0039', '\u000d', '\u0030', '\u0039', 
	'\u000a', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Expires_single_lengths =  new sbyte [] {
	0, 2, 2, 2, 2, 2, 2, 2, 
	3, 3, 1, 2, 2, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Expires_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 1, 0, 0, 1, 1, 0, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Expires_index_offsets =  new sbyte [] {
	0, 0, 3, 6, 9, 12, 15, 18, 
	21, 25, 30, 32, 35, 39, 42, 44
};

static readonly sbyte[] _tsip_machine_parser_header_Expires_indicies =  new sbyte [] {
	0, 0, 1, 2, 2, 1, 3, 3, 
	1, 4, 4, 1, 5, 5, 1, 6, 
	6, 1, 7, 7, 1, 7, 7, 8, 
	1, 8, 9, 8, 10, 1, 11, 1, 
	12, 12, 1, 12, 12, 10, 1, 13, 
	14, 1, 15, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Expires_trans_targs =  new sbyte [] {
	2, 0, 3, 4, 5, 6, 7, 8, 
	9, 10, 13, 11, 12, 14, 13, 15
};

static readonly sbyte[] _tsip_machine_parser_header_Expires_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 1, 0, 0, 3, 0, 5
};

const int tsip_machine_parser_header_Expires_start = 1;
const int tsip_machine_parser_header_Expires_first_final = 15;
const int tsip_machine_parser_header_Expires_error = 0;

const int tsip_machine_parser_header_Expires_en_main = 1;


/* #line 91 "./ragel/tsip_parser_header_Expires.rl" */

		public static TSIP_HeaderExpires Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderExpires hdr_expires = new TSIP_HeaderExpires();
			
			int tag_start = 0;
			
			
/* #line 146 "../Headers/TSIP_HeaderExpires.cs" */
	{
	cs = tsip_machine_parser_header_Expires_start;
	}

/* #line 103 "./ragel/tsip_parser_header_Expires.rl" */
			
/* #line 153 "../Headers/TSIP_HeaderExpires.cs" */
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
	_keys = _tsip_machine_parser_header_Expires_key_offsets[cs];
	_trans = (sbyte)_tsip_machine_parser_header_Expires_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Expires_single_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Expires_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Expires_trans_keys[_mid] )
				_lower = (sbyte) (_mid + 1);
			else {
				_trans += (sbyte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (sbyte) _klen;
		_trans += (sbyte) _klen;
	}

	_klen = _tsip_machine_parser_header_Expires_range_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Expires_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Expires_trans_keys[_mid+1] )
				_lower = (sbyte) (_mid + 2);
			else {
				_trans += (sbyte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (sbyte) _klen;
	}

_match:
	_trans = (sbyte)_tsip_machine_parser_header_Expires_indicies[_trans];
	cs = _tsip_machine_parser_header_Expires_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Expires_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Expires_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Expires_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Expires_actions[_acts++] )
		{
	case 0:
/* #line 31 "./ragel/tsip_parser_header_Expires.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 35 "./ragel/tsip_parser_header_Expires.rl" */
	{
		hdr_expires.DeltaSeconds = TSK_RagelState.Parser.GetInt64(data, p, tag_start);
	}
	break;
	case 2:
/* #line 39 "./ragel/tsip_parser_header_Expires.rl" */
	{
	}
	break;
/* #line 244 "../Headers/TSIP_HeaderExpires.cs" */
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

/* #line 104 "./ragel/tsip_parser_header_Expires.rl" */
			
			if( cs < 
/* #line 261 "../Headers/TSIP_HeaderExpires.cs" */
15
/* #line 105 "./ragel/tsip_parser_header_Expires.rl" */
 ){
				TSK_Debug.Error("Failed to parse 'Expires' header.");
				hdr_expires.Dispose();
				hdr_expires = null;
			}
			
			return hdr_expires;
		}
    }
}