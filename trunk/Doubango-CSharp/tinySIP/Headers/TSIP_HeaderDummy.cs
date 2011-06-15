
/* #line 1 "./ragel/tsip_parser_header_Dummy.rl" */
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

/* #line 51 "./ragel/tsip_parser_header_Dummy.rl" */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango_CSharp.tinySAK;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderDummy : TSIP_Header
    {
        private String mName;
        private String mValue;

        public TSIP_HeaderDummy() 
            : this(null, null)
        {
        }

        public TSIP_HeaderDummy(String name, String value)
            : base(tsip_header_type_t.Dummy)
        {
            mName = name;
            mValue = value;
        }

       public override string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public override String Value
        {
            get { return mValue; }
            set { mValue = value; }
        }

		
/* #line 71 "../Headers/TSIP_HeaderDummy.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Dummy_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2, 1, 
	3, 2, 0, 2
};

static readonly sbyte[] _tsip_machine_parser_header_Dummy_key_offsets =  new sbyte [] {
	0, 0, 14, 31, 34, 37, 38, 39, 
	40, 42, 45
};

static readonly char[] _tsip_machine_parser_header_Dummy_trans_keys =  new char [] {
	'\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u003a', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', 
	'\u0020', '\u003a', '\u0009', '\u000d', '\u0020', '\u000d', '\u000a', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u000d', '\u0020', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Dummy_single_lengths =  new sbyte [] {
	0, 4, 7, 3, 3, 1, 1, 1, 
	2, 3, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Dummy_range_lengths =  new sbyte [] {
	0, 5, 5, 0, 0, 0, 0, 0, 
	0, 0, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Dummy_index_offsets =  new sbyte [] {
	0, 0, 10, 23, 27, 31, 33, 35, 
	37, 40, 44
};

static readonly sbyte[] _tsip_machine_parser_header_Dummy_indicies =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 1, 2, 2, 3, 3, 3, 4, 
	3, 3, 3, 3, 3, 3, 1, 5, 
	5, 6, 1, 6, 8, 6, 7, 10, 
	9, 11, 1, 12, 1, 13, 13, 1, 
	13, 14, 13, 7, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Dummy_trans_targs =  new sbyte [] {
	2, 0, 3, 2, 4, 3, 4, 5, 
	7, 5, 6, 10, 8, 9, 6
};

static readonly sbyte[] _tsip_machine_parser_header_Dummy_trans_actions =  new sbyte [] {
	1, 0, 3, 0, 3, 0, 0, 1, 
	0, 0, 5, 7, 0, 0, 9
};

const int tsip_machine_parser_header_Dummy_start = 1;
const int tsip_machine_parser_header_Dummy_first_final = 10;
const int tsip_machine_parser_header_Dummy_error = 0;

const int tsip_machine_parser_header_Dummy_en_main = 1;


/* #line 92 "./ragel/tsip_parser_header_Dummy.rl" */

		public static TSIP_HeaderDummy Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderDummy hdr_Dummy = new TSIP_HeaderDummy();
			
			int tag_start = 0;
			
			
/* #line 145 "../Headers/TSIP_HeaderDummy.cs" */
	{
	cs = tsip_machine_parser_header_Dummy_start;
	}

/* #line 104 "./ragel/tsip_parser_header_Dummy.rl" */
			
/* #line 152 "../Headers/TSIP_HeaderDummy.cs" */
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
	_keys = _tsip_machine_parser_header_Dummy_key_offsets[cs];
	_trans = (sbyte)_tsip_machine_parser_header_Dummy_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Dummy_single_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Dummy_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Dummy_trans_keys[_mid] )
				_lower = (sbyte) (_mid + 1);
			else {
				_trans += (sbyte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (sbyte) _klen;
		_trans += (sbyte) _klen;
	}

	_klen = _tsip_machine_parser_header_Dummy_range_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Dummy_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Dummy_trans_keys[_mid+1] )
				_lower = (sbyte) (_mid + 2);
			else {
				_trans += (sbyte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (sbyte) _klen;
	}

_match:
	_trans = (sbyte)_tsip_machine_parser_header_Dummy_indicies[_trans];
	cs = _tsip_machine_parser_header_Dummy_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Dummy_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Dummy_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Dummy_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Dummy_actions[_acts++] )
		{
	case 0:
/* #line 31 "./ragel/tsip_parser_header_Dummy.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 35 "./ragel/tsip_parser_header_Dummy.rl" */
	{
		hdr_Dummy.Name = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	break;
	case 2:
/* #line 39 "./ragel/tsip_parser_header_Dummy.rl" */
	{
		hdr_Dummy.Value = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	break;
	case 3:
/* #line 43 "./ragel/tsip_parser_header_Dummy.rl" */
	{
	}
	break;
/* #line 249 "../Headers/TSIP_HeaderDummy.cs" */
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

/* #line 105 "./ragel/tsip_parser_header_Dummy.rl" */
			
			if( cs < 
/* #line 266 "../Headers/TSIP_HeaderDummy.cs" */
10
/* #line 106 "./ragel/tsip_parser_header_Dummy.rl" */
 ){
				TSK_Debug.Error("Failed to parse 'Dummy' header");
				hdr_Dummy.Dispose();
				hdr_Dummy = null;
			}
			
			return hdr_Dummy;
		}
    }
}
