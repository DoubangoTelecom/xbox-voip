
/* #line 1 "./ragel/tsip_parser_header_Max_Forwards.rl" */
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

/* #line 47 "./ragel/tsip_parser_header_Max_Forwards.rl" */


using System;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderMaxForwards : TSIP_Header
	{
		private Int32 mMaxForw;

		public const Int32 TSIP_HEADER_MAX_FORWARDS_NONE = -1;
		public const Int32 TSIP_HEADER_MAX_FORWARDS_DEFAULT = 70;

		public TSIP_HeaderMaxForwards()
            : this(TSIP_HEADER_MAX_FORWARDS_NONE)
        {
        }
		
		public TSIP_HeaderMaxForwards(Int32 maxForw)
            : base(tsip_header_type_t.Max_Forwards)
        {
			mMaxForw = maxForw;
        }

		public override String Value
        {
            get 
            { 
               return this.MaxForward.ToString();
            }
            set
            {
                Int32 maxForw;
                if (Int32.TryParse(value, out maxForw))
                {
                    this.MaxForward = maxForw;
                }
            }
        }

		public Int32 MaxForward
        {
            get 
            { 
               return mMaxForw;
            }
            set
            {
                mMaxForw = value;
            }
        }

		
/* #line 84 "../Headers/TSIP_HeaderMaxForwards.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Max_Forwards_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2
};

static readonly sbyte[] _tsip_machine_parser_header_Max_Forwards_key_offsets =  new sbyte [] {
	0, 0, 2, 4, 6, 7, 9, 11, 
	13, 15, 17, 19, 21, 23, 26, 31, 
	32, 34, 38, 41, 42
};

static readonly char[] _tsip_machine_parser_header_Max_Forwards_trans_keys =  new char [] {
	'\u004d', '\u006d', '\u0041', '\u0061', '\u0058', '\u0078', '\u002d', '\u0046', 
	'\u0066', '\u004f', '\u006f', '\u0052', '\u0072', '\u0057', '\u0077', '\u0041', 
	'\u0061', '\u0052', '\u0072', '\u0044', '\u0064', '\u0053', '\u0073', '\u0009', 
	'\u0020', '\u003a', '\u0009', '\u000d', '\u0020', '\u0030', '\u0039', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u0020', '\u0030', '\u0039', '\u000d', '\u0030', 
	'\u0039', '\u000a', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Max_Forwards_single_lengths =  new sbyte [] {
	0, 2, 2, 2, 1, 2, 2, 2, 
	2, 2, 2, 2, 2, 3, 3, 1, 
	2, 2, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Max_Forwards_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 1, 0, 
	0, 1, 1, 0, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Max_Forwards_index_offsets =  new sbyte [] {
	0, 0, 3, 6, 9, 11, 14, 17, 
	20, 23, 26, 29, 32, 35, 39, 44, 
	46, 49, 53, 56, 58
};

static readonly sbyte[] _tsip_machine_parser_header_Max_Forwards_indicies =  new sbyte [] {
	0, 0, 1, 2, 2, 1, 3, 3, 
	1, 4, 1, 5, 5, 1, 6, 6, 
	1, 7, 7, 1, 8, 8, 1, 9, 
	9, 1, 10, 10, 1, 11, 11, 1, 
	12, 12, 1, 12, 12, 13, 1, 13, 
	14, 13, 15, 1, 16, 1, 17, 17, 
	1, 17, 17, 15, 1, 18, 19, 1, 
	20, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Max_Forwards_trans_targs =  new sbyte [] {
	2, 0, 3, 4, 5, 6, 7, 8, 
	9, 10, 11, 12, 13, 14, 15, 18, 
	16, 17, 19, 18, 20
};

static readonly sbyte[] _tsip_machine_parser_header_Max_Forwards_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 1, 
	0, 0, 3, 0, 5
};

const int tsip_machine_parser_header_Max_Forwards_start = 1;
const int tsip_machine_parser_header_Max_Forwards_first_final = 20;
const int tsip_machine_parser_header_Max_Forwards_error = 0;

const int tsip_machine_parser_header_Max_Forwards_en_main = 1;


/* #line 101 "./ragel/tsip_parser_header_Max_Forwards.rl" */

		public static TSIP_HeaderMaxForwards Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderMaxForwards hdr_maxf = new TSIP_HeaderMaxForwards();
			
			int tag_start = 0;

			
/* #line 165 "../Headers/TSIP_HeaderMaxForwards.cs" */
	{
	cs = tsip_machine_parser_header_Max_Forwards_start;
	}

/* #line 113 "./ragel/tsip_parser_header_Max_Forwards.rl" */
			
/* #line 172 "../Headers/TSIP_HeaderMaxForwards.cs" */
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
	_keys = _tsip_machine_parser_header_Max_Forwards_key_offsets[cs];
	_trans = (sbyte)_tsip_machine_parser_header_Max_Forwards_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Max_Forwards_single_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Max_Forwards_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Max_Forwards_trans_keys[_mid] )
				_lower = (sbyte) (_mid + 1);
			else {
				_trans += (sbyte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (sbyte) _klen;
		_trans += (sbyte) _klen;
	}

	_klen = _tsip_machine_parser_header_Max_Forwards_range_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Max_Forwards_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Max_Forwards_trans_keys[_mid+1] )
				_lower = (sbyte) (_mid + 2);
			else {
				_trans += (sbyte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (sbyte) _klen;
	}

_match:
	_trans = (sbyte)_tsip_machine_parser_header_Max_Forwards_indicies[_trans];
	cs = _tsip_machine_parser_header_Max_Forwards_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Max_Forwards_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Max_Forwards_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Max_Forwards_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Max_Forwards_actions[_acts++] )
		{
	case 0:
/* #line 31 "./ragel/tsip_parser_header_Max_Forwards.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 35 "./ragel/tsip_parser_header_Max_Forwards.rl" */
	{
		hdr_maxf.MaxForward = TSK_RagelState.Parser.GetInt32(data, p, tag_start);
	}
	break;
	case 2:
/* #line 39 "./ragel/tsip_parser_header_Max_Forwards.rl" */
	{
	}
	break;
/* #line 263 "../Headers/TSIP_HeaderMaxForwards.cs" */
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

/* #line 114 "./ragel/tsip_parser_header_Max_Forwards.rl" */
			
			if( cs < 
/* #line 280 "../Headers/TSIP_HeaderMaxForwards.cs" */
20
/* #line 115 "./ragel/tsip_parser_header_Max_Forwards.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Content-Length' header.");
				hdr_maxf.Dispose();
				hdr_maxf = null;
			}
			
			return hdr_maxf;
		}
	}
}