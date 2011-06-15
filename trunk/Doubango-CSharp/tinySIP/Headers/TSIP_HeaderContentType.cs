
/* #line 1 "./ragel/tsip_parser_header_Content_Type.rl" */
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

/* #line 65 "./ragel/tsip_parser_header_Content_Type.rl" */


using System;
using Doubango_CSharp.tinySAK;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderContentType : TSIP_Header
	{
		private String mCType;

		public TSIP_HeaderContentType()
            : this(null)
        {
        }
		
		public TSIP_HeaderContentType(String cType)
            : base(tsip_header_type_t.Content_Type)
        {
			mCType = cType;
        }

		public override String Value
        {
            get 
            { 
               return this.CType;
            }
            set
            {
                this.CType = value;
            }
        }

		public String CType
        {
            get 
            { 
               return mCType;
            }
            set
            {
                mCType = value;
            }
        }

		
/* #line 78 "../Headers/TSIP_HeaderContentType.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Content_Type_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2, 1, 
	3
};

static readonly short[] _tsip_machine_parser_header_Content_Type_key_offsets =  new short [] {
	0, 0, 2, 7, 10, 27, 28, 30, 
	46, 62, 66, 67, 69, 72, 89, 90, 
	92, 108, 126, 130, 131, 133, 136, 153, 
	154, 156, 172, 190, 194, 195, 197, 200, 
	218, 219, 221, 239, 240, 242, 245, 253, 
	254, 256, 260, 261, 267, 285, 287, 289, 
	291, 293, 295, 296, 298, 300, 302, 304
};

static readonly char[] _tsip_machine_parser_header_Content_Type_trans_keys =  new char [] {
	'\u0043', '\u0063', '\u0009', '\u0020', '\u003a', '\u004f', '\u006f', '\u0009', 
	'\u0020', '\u003a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', 
	'\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u002f', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', 
	'\u0020', '\u002f', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u002f', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', 
	'\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u003b', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', 
	'\u0020', '\u003b', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u003b', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', 
	'\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u003d', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', 
	'\u0020', '\u003d', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u003d', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', '\u0027', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0022', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u0020', '\u0022', '\u0009', '\u000d', '\u0022', 
	'\u005c', '\u0020', '\u007e', '\u0080', '\u00ff', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u000d', '\u0020', '\u003b', '\u000a', '\u0000', '\u0009', '\u000b', 
	'\u000c', '\u000e', '\u007f', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u003b', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u004e', '\u006e', '\u0054', 
	'\u0074', '\u0045', '\u0065', '\u004e', '\u006e', '\u0054', '\u0074', '\u002d', 
	'\u0054', '\u0074', '\u0059', '\u0079', '\u0050', '\u0070', '\u0045', '\u0065', 
	(char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Type_single_lengths =  new sbyte [] {
	0, 2, 5, 3, 7, 1, 2, 6, 
	8, 4, 1, 2, 3, 7, 1, 2, 
	6, 8, 4, 1, 2, 3, 7, 1, 
	2, 6, 8, 4, 1, 2, 3, 8, 
	1, 2, 8, 1, 2, 3, 4, 1, 
	2, 4, 1, 0, 8, 2, 2, 2, 
	2, 2, 1, 2, 2, 2, 2, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Type_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 5, 0, 0, 5, 
	4, 0, 0, 0, 0, 5, 0, 0, 
	5, 5, 0, 0, 0, 0, 5, 0, 
	0, 5, 5, 0, 0, 0, 0, 5, 
	0, 0, 5, 0, 0, 0, 2, 0, 
	0, 0, 0, 3, 5, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0
};

static readonly short[] _tsip_machine_parser_header_Content_Type_index_offsets =  new short [] {
	0, 0, 3, 9, 13, 26, 28, 31, 
	43, 56, 61, 63, 66, 70, 83, 85, 
	88, 100, 114, 119, 121, 124, 128, 141, 
	143, 146, 158, 172, 177, 179, 182, 186, 
	200, 202, 205, 219, 221, 224, 228, 235, 
	237, 240, 245, 247, 251, 265, 268, 271, 
	274, 277, 280, 282, 285, 288, 291, 294
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Type_indicies =  new sbyte [] {
	0, 0, 1, 2, 2, 3, 4, 4, 
	1, 2, 2, 3, 1, 3, 5, 3, 
	6, 6, 6, 6, 6, 6, 6, 6, 
	6, 1, 7, 1, 8, 8, 1, 8, 
	8, 6, 6, 6, 6, 6, 6, 6, 
	6, 6, 1, 9, 10, 9, 11, 11, 
	11, 12, 11, 11, 11, 11, 11, 1, 
	9, 10, 9, 12, 1, 13, 1, 14, 
	14, 1, 14, 14, 12, 1, 12, 15, 
	12, 16, 16, 16, 16, 16, 16, 16, 
	16, 16, 1, 17, 1, 18, 18, 1, 
	18, 18, 16, 16, 16, 16, 16, 16, 
	16, 16, 16, 1, 19, 20, 19, 16, 
	16, 16, 21, 16, 16, 16, 16, 16, 
	16, 1, 22, 23, 22, 24, 1, 25, 
	1, 26, 26, 1, 26, 26, 24, 1, 
	24, 27, 24, 28, 28, 28, 28, 28, 
	28, 28, 28, 28, 1, 29, 1, 30, 
	30, 1, 30, 30, 28, 28, 28, 28, 
	28, 28, 28, 28, 28, 1, 31, 32, 
	31, 33, 33, 33, 34, 33, 33, 33, 
	33, 33, 33, 1, 31, 32, 31, 34, 
	1, 35, 1, 36, 36, 1, 36, 36, 
	34, 1, 34, 37, 34, 38, 39, 38, 
	38, 38, 38, 38, 38, 38, 38, 1, 
	40, 1, 41, 41, 1, 41, 42, 41, 
	38, 39, 38, 38, 38, 38, 38, 38, 
	38, 38, 1, 43, 1, 44, 44, 1, 
	44, 44, 39, 1, 39, 45, 46, 47, 
	39, 39, 1, 48, 1, 39, 39, 1, 
	49, 50, 49, 51, 1, 52, 1, 39, 
	39, 39, 1, 49, 50, 49, 38, 38, 
	38, 51, 38, 38, 38, 38, 38, 38, 
	1, 53, 53, 1, 54, 54, 1, 55, 
	55, 1, 56, 56, 1, 57, 57, 1, 
	58, 1, 59, 59, 1, 60, 60, 1, 
	61, 61, 1, 2, 2, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Type_trans_targs =  new sbyte [] {
	2, 0, 3, 4, 45, 5, 8, 6, 
	7, 9, 10, 8, 13, 11, 12, 14, 
	17, 15, 16, 18, 42, 22, 18, 19, 
	22, 20, 21, 23, 26, 24, 25, 27, 
	28, 26, 31, 29, 30, 32, 44, 38, 
	33, 34, 35, 36, 37, 39, 41, 43, 
	40, 18, 42, 22, 55, 46, 47, 48, 
	49, 50, 51, 52, 53, 54
};

static readonly sbyte[] _tsip_machine_parser_header_Content_Type_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 1, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 3, 3, 3, 0, 0, 
	0, 0, 0, 0, 1, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 5, 5, 5, 7, 0, 0, 0, 
	0, 0, 0, 0, 0, 0
};

const int tsip_machine_parser_header_Content_Type_start = 1;
const int tsip_machine_parser_header_Content_Type_first_final = 55;
const int tsip_machine_parser_header_Content_Type_error = 0;

const int tsip_machine_parser_header_Content_Type_en_main = 1;


/* #line 112 "./ragel/tsip_parser_header_Content_Type.rl" */

		public static TSIP_HeaderContentType Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderContentType hdr_ctype = new TSIP_HeaderContentType();
			
			int tag_start = 0;

			
			
/* #line 249 "../Headers/TSIP_HeaderContentType.cs" */
	{
	cs = tsip_machine_parser_header_Content_Type_start;
	}

/* #line 125 "./ragel/tsip_parser_header_Content_Type.rl" */
			
/* #line 256 "../Headers/TSIP_HeaderContentType.cs" */
	{
	sbyte _klen;
	short _trans;
	sbyte _acts;
	sbyte _nacts;
	short _keys;

	if ( p == pe )
		goto _test_eof;
	if ( cs == 0 )
		goto _out;
_resume:
	_keys = _tsip_machine_parser_header_Content_Type_key_offsets[cs];
	_trans = (short)_tsip_machine_parser_header_Content_Type_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Content_Type_single_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Content_Type_trans_keys[_mid] )
				_upper = (short) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Content_Type_trans_keys[_mid] )
				_lower = (short) (_mid + 1);
			else {
				_trans += (short) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (short) _klen;
		_trans += (short) _klen;
	}

	_klen = _tsip_machine_parser_header_Content_Type_range_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Content_Type_trans_keys[_mid] )
				_upper = (short) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Content_Type_trans_keys[_mid+1] )
				_lower = (short) (_mid + 2);
			else {
				_trans += (short)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (short) _klen;
	}

_match:
	_trans = (short)_tsip_machine_parser_header_Content_Type_indicies[_trans];
	cs = _tsip_machine_parser_header_Content_Type_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Content_Type_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Content_Type_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Content_Type_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Content_Type_actions[_acts++] )
		{
	case 0:
/* #line 32 "./ragel/tsip_parser_header_Content_Type.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 36 "./ragel/tsip_parser_header_Content_Type.rl" */
	{
		hdr_ctype.CType = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	break;
	case 2:
/* #line 40 "./ragel/tsip_parser_header_Content_Type.rl" */
	{		
		 hdr_ctype.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, hdr_ctype.Params);
	}
	break;
	case 3:
/* #line 44 "./ragel/tsip_parser_header_Content_Type.rl" */
	{
	}
	break;
/* #line 353 "../Headers/TSIP_HeaderContentType.cs" */
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

/* #line 126 "./ragel/tsip_parser_header_Content_Type.rl" */
			
			if( cs < 
/* #line 370 "../Headers/TSIP_HeaderContentType.cs" */
55
/* #line 127 "./ragel/tsip_parser_header_Content_Type.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Content-Type' header.");
				hdr_ctype.Dispose();
				hdr_ctype = null;
			}
			
			return hdr_ctype;
		}
	}
}
