
/* #line 1 "./ragel/tsip_parser_header_Content_Length.rl" */
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

/* #line 48 "./ragel/tsip_parser_header_Content_Length.rl" */



using System;
using Doubango_CSharp.tinySAK;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderContentLength : TSIP_Header
	{
		private UInt32 mLength;

		public TSIP_HeaderContentLength()
            : this(0)
        {
        }
		
		public TSIP_HeaderContentLength(UInt32 length)
            : base(tsip_header_type_t.Content_Length)
        {
			mLength = length;
        }

		public override String Value
        {
            get 
            { 
               return this.Length.ToString();
            }
            set
            {
                UInt32 length;
                if (UInt32.TryParse(value, out length))
                {
                    this.Length = length;
                }
            }
        }

		public UInt32 Length
        {
            get 
            { 
               return mLength;
            }
            set
            {
                mLength = value;
            }
        }

		
/* #line 83 "../Headers/TSIP_HeaderContentLength.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Content_Length_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Length_key_offsets =  new sbyte [] {
	0, 0, 4, 6, 8, 10, 12, 14, 
	16, 17, 19, 21, 23, 25, 27, 29, 
	32, 37, 38, 40, 44, 47, 48
};

static readonly char[] _tsip_machine_parser_header_Content_Length_trans_keys =  new char [] {
	'\u0043', '\u004c', '\u0063', '\u006c', '\u004f', '\u006f', '\u004e', '\u006e', 
	'\u0054', '\u0074', '\u0045', '\u0065', '\u004e', '\u006e', '\u0054', '\u0074', 
	'\u002d', '\u004c', '\u006c', '\u0045', '\u0065', '\u004e', '\u006e', '\u0047', 
	'\u0067', '\u0054', '\u0074', '\u0048', '\u0068', '\u0009', '\u0020', '\u003a', 
	'\u0009', '\u000d', '\u0020', '\u0030', '\u0039', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u0020', '\u0030', '\u0039', '\u000d', '\u0030', '\u0039', '\u000a', 
	(char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Length_single_lengths =  new sbyte [] {
	0, 4, 2, 2, 2, 2, 2, 2, 
	1, 2, 2, 2, 2, 2, 2, 3, 
	3, 1, 2, 2, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Length_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	1, 0, 0, 1, 1, 0, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Length_index_offsets =  new sbyte [] {
	0, 0, 5, 8, 11, 14, 17, 20, 
	23, 25, 28, 31, 34, 37, 40, 43, 
	47, 52, 54, 57, 61, 64, 66
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Length_indicies =  new sbyte [] {
	0, 2, 0, 2, 1, 3, 3, 1, 
	4, 4, 1, 5, 5, 1, 6, 6, 
	1, 7, 7, 1, 8, 8, 1, 9, 
	1, 10, 10, 1, 11, 11, 1, 12, 
	12, 1, 13, 13, 1, 14, 14, 1, 
	2, 2, 1, 2, 2, 15, 1, 15, 
	16, 15, 17, 1, 18, 1, 19, 19, 
	1, 19, 19, 17, 1, 20, 21, 1, 
	22, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Length_trans_targs =  new sbyte [] {
	2, 0, 15, 3, 4, 5, 6, 7, 
	8, 9, 10, 11, 12, 13, 14, 16, 
	17, 20, 18, 19, 21, 20, 22
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Length_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 1, 0, 0, 3, 0, 5
};

const int tsip_machine_parser_header_Content_Length_start = 1;
const int tsip_machine_parser_header_Content_Length_first_final = 22;
const int tsip_machine_parser_header_Content_Length_error = 0;

const int tsip_machine_parser_header_Content_Length_en_main = 1;


/* #line 100 "./ragel/tsip_parser_header_Content_Length.rl" */

		public static TSIP_HeaderContentLength Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderContentLength hdr_clength = new TSIP_HeaderContentLength();
			
			int tag_start = 0;

			
			
/* #line 167 "../Headers/TSIP_HeaderContentLength.cs" */
	{
	cs = tsip_machine_parser_header_Content_Length_start;
	}

/* #line 113 "./ragel/tsip_parser_header_Content_Length.rl" */
			
/* #line 174 "../Headers/TSIP_HeaderContentLength.cs" */
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
	_keys = _tsip_machine_parser_header_Content_Length_key_offsets[cs];
	_trans = (sbyte)_tsip_machine_parser_header_Content_Length_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Content_Length_single_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Content_Length_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Content_Length_trans_keys[_mid] )
				_lower = (sbyte) (_mid + 1);
			else {
				_trans += (sbyte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (sbyte) _klen;
		_trans += (sbyte) _klen;
	}

	_klen = _tsip_machine_parser_header_Content_Length_range_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Content_Length_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Content_Length_trans_keys[_mid+1] )
				_lower = (sbyte) (_mid + 2);
			else {
				_trans += (sbyte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (sbyte) _klen;
	}

_match:
	_trans = (sbyte)_tsip_machine_parser_header_Content_Length_indicies[_trans];
	cs = _tsip_machine_parser_header_Content_Length_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Content_Length_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Content_Length_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Content_Length_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Content_Length_actions[_acts++] )
		{
	case 0:
/* #line 32 "./ragel/tsip_parser_header_Content_Length.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 36 "./ragel/tsip_parser_header_Content_Length.rl" */
	{
		hdr_clength.Length = TSK_RagelState.Parser.GetUInt32(data, p, tag_start);
	}
	break;
	case 2:
/* #line 40 "./ragel/tsip_parser_header_Content_Length.rl" */
	{
	}
	break;
/* #line 265 "../Headers/TSIP_HeaderContentLength.cs" */
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

/* #line 114 "./ragel/tsip_parser_header_Content_Length.rl" */
			
			if( cs < 
/* #line 282 "../Headers/TSIP_HeaderContentLength.cs" */
22
/* #line 115 "./ragel/tsip_parser_header_Content_Length.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Content-Length' header.");
				hdr_clength.Dispose();
				hdr_clength = null;
			}
			
			return hdr_clength;
		}
	}
}