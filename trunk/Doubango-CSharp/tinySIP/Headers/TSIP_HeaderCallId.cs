
/* #line 1 "./ragel/tsip_parser_header_Call_ID.rl" */
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

/* #line 48 "./ragel/tsip_parser_header_Call_ID.rl" */


using System;
using Doubango_CSharp.tinySAK;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderCallId : TSIP_Header
	{
		private String mId;

		public TSIP_HeaderCallId()
            : this(null)
        {
        }

        public TSIP_HeaderCallId(String id)
            : base(tsip_header_type_t.Call_ID)
        {
           mId = id;
        }

		public override String Value
        {
            get 
            { 
               return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }

		public String Id
        {
            get 
            { 
               return mId;
            }
            set
            {
                mId = value;
            }
        }

		
/* #line 77 "../Headers/TSIP_HeaderCallId.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Call_ID_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2
};

static readonly sbyte[] _tsip_machine_parser_header_Call_ID_key_offsets =  new sbyte [] {
	0, 0, 4, 6, 8, 10, 11, 13, 
	15, 18, 37, 38, 40, 58, 74, 75, 
	91, 108
};

static readonly char[] _tsip_machine_parser_header_Call_ID_trans_keys =  new char [] {
	'\u0043', '\u0049', '\u0063', '\u0069', '\u0041', '\u0061', '\u004c', '\u006c', 
	'\u004c', '\u006c', '\u002d', '\u0049', '\u0069', '\u0044', '\u0064', '\u0009', 
	'\u0020', '\u003a', '\u0009', '\u000d', '\u0020', '\u0025', '\u003c', '\u0021', 
	'\u0022', '\u0027', '\u002b', '\u002d', '\u003a', '\u003e', '\u003f', '\u0041', 
	'\u005d', '\u005f', '\u007b', '\u007d', '\u007e', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u0020', '\u0025', '\u003c', '\u0021', '\u0022', '\u0027', '\u002b', 
	'\u002d', '\u003a', '\u003e', '\u003f', '\u0041', '\u005d', '\u005f', '\u007b', 
	'\u007d', '\u007e', '\u000d', '\u0025', '\u003c', '\u0040', '\u0021', '\u0022', 
	'\u0027', '\u002b', '\u002d', '\u003a', '\u003e', '\u005d', '\u005f', '\u007b', 
	'\u007d', '\u007e', '\u000a', '\u0025', '\u003c', '\u0021', '\u0022', '\u0027', 
	'\u002b', '\u002d', '\u003a', '\u003e', '\u003f', '\u0041', '\u005d', '\u005f', 
	'\u007b', '\u007d', '\u007e', '\u000d', '\u0025', '\u003c', '\u0021', '\u0022', 
	'\u0027', '\u002b', '\u002d', '\u003a', '\u003e', '\u003f', '\u0041', '\u005d', 
	'\u005f', '\u007b', '\u007d', '\u007e', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Call_ID_single_lengths =  new sbyte [] {
	0, 4, 2, 2, 2, 1, 2, 2, 
	3, 5, 1, 2, 4, 4, 1, 2, 
	3, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Call_ID_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 7, 0, 0, 7, 6, 0, 7, 
	7, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Call_ID_index_offsets =  new sbyte [] {
	0, 0, 5, 8, 11, 14, 16, 19, 
	22, 26, 39, 41, 44, 56, 67, 69, 
	79, 90
};

static readonly sbyte[] _tsip_machine_parser_header_Call_ID_indicies =  new sbyte [] {
	0, 2, 0, 2, 1, 3, 3, 1, 
	4, 4, 1, 5, 5, 1, 6, 1, 
	7, 7, 1, 2, 2, 1, 2, 2, 
	8, 1, 8, 9, 8, 10, 10, 10, 
	10, 10, 10, 10, 10, 10, 1, 11, 
	1, 12, 12, 1, 12, 12, 10, 10, 
	10, 10, 10, 10, 10, 10, 10, 1, 
	13, 14, 14, 15, 14, 14, 14, 14, 
	14, 14, 1, 16, 1, 17, 17, 17, 
	17, 17, 17, 17, 17, 17, 1, 13, 
	17, 17, 17, 17, 17, 17, 17, 17, 
	17, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Call_ID_trans_targs =  new sbyte [] {
	2, 0, 8, 3, 4, 5, 6, 7, 
	9, 10, 13, 11, 12, 14, 13, 15, 
	17, 16
};

static readonly sbyte[] _tsip_machine_parser_header_Call_ID_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 1, 0, 0, 3, 0, 0, 
	5, 0
};

const int tsip_machine_parser_header_Call_ID_start = 1;
const int tsip_machine_parser_header_Call_ID_first_final = 17;
const int tsip_machine_parser_header_Call_ID_error = 0;

const int tsip_machine_parser_header_Call_ID_en_main = 1;


/* #line 95 "./ragel/tsip_parser_header_Call_ID.rl" */

		public static TSIP_HeaderCallId Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderCallId hdr_call_id = new TSIP_HeaderCallId();
			
			int tag_start = 0;

			
			
/* #line 171 "../Headers/TSIP_HeaderCallId.cs" */
	{
	cs = tsip_machine_parser_header_Call_ID_start;
	}

/* #line 108 "./ragel/tsip_parser_header_Call_ID.rl" */
			
/* #line 178 "../Headers/TSIP_HeaderCallId.cs" */
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
	_keys = _tsip_machine_parser_header_Call_ID_key_offsets[cs];
	_trans = (sbyte)_tsip_machine_parser_header_Call_ID_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Call_ID_single_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Call_ID_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Call_ID_trans_keys[_mid] )
				_lower = (sbyte) (_mid + 1);
			else {
				_trans += (sbyte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (sbyte) _klen;
		_trans += (sbyte) _klen;
	}

	_klen = _tsip_machine_parser_header_Call_ID_range_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Call_ID_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Call_ID_trans_keys[_mid+1] )
				_lower = (sbyte) (_mid + 2);
			else {
				_trans += (sbyte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (sbyte) _klen;
	}

_match:
	_trans = (sbyte)_tsip_machine_parser_header_Call_ID_indicies[_trans];
	cs = _tsip_machine_parser_header_Call_ID_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Call_ID_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Call_ID_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Call_ID_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Call_ID_actions[_acts++] )
		{
	case 0:
/* #line 31 "./ragel/tsip_parser_header_Call_ID.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 35 "./ragel/tsip_parser_header_Call_ID.rl" */
	{
		hdr_call_id.Id = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	break;
	case 2:
/* #line 39 "./ragel/tsip_parser_header_Call_ID.rl" */
	{
	}
	break;
/* #line 269 "../Headers/TSIP_HeaderCallId.cs" */
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

/* #line 109 "./ragel/tsip_parser_header_Call_ID.rl" */
			
			if( cs < 
/* #line 286 "../Headers/TSIP_HeaderCallId.cs" */
17
/* #line 110 "./ragel/tsip_parser_header_Call_ID.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Call-ID' header.");
				hdr_call_id.Dispose();
				hdr_call_id = null;
			}
			
			return hdr_call_id;
		}

		public static String RandomCallId()
		{
			return System.Guid.NewGuid().ToString();
		}
	}
}