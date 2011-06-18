
/* #line 1 "./ragel/tsip_parser_header_Route.rl" */
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

/* #line 90 "./ragel/tsip_parser_header_Route.rl" */


using System;
using Doubango.tinySAK;
using System.Collections.Generic;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderRoute : TSIP_Header
	{
		private String mDisplayName;
		private TSIP_Uri mUri;

		public TSIP_HeaderRoute()
            : this(null)
        {
        }

		public TSIP_HeaderRoute(TSIP_Uri uri)
            : base(tsip_header_type_t.Record_Route)
        {
			mUri = uri;
        }

		public override String Value
        {
            get 
            { 
				// Uri with hacked display-name
                return  TSIP_Uri.Serialize(this.Uri, true, true);
            }
            set { TSK_Debug.Error("Not implemented"); }
        }

		public String DisplayName
        {
            get { return mDisplayName; }
            set { mDisplayName = value; }
        }
		
		public TSIP_Uri Uri
        {
            get { return mUri; }
            set { mUri = value; }
        }

		
/* #line 77 "../Headers/TSIP_HeaderRoute.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Route_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2, 1, 
	3, 1, 4, 1, 5, 1, 6, 2, 
	1, 0, 2, 4, 5
};

static readonly short[] _tsip_machine_parser_header_Route_key_offsets =  new short [] {
	0, 0, 2, 4, 6, 8, 10, 13, 
	32, 33, 35, 54, 55, 57, 60, 64, 
	76, 79, 79, 80, 85, 86, 103, 104, 
	106, 122, 140, 146, 147, 149, 154, 173, 
	174, 176, 195, 196, 198, 201, 209, 210, 
	212, 217, 222, 223, 225, 229, 235, 252, 
	259, 267, 275, 283, 285, 292, 301, 303, 
	306, 308, 311, 313, 316, 319, 320, 323, 
	324, 327, 328, 337, 346, 354, 362, 370, 
	378, 380, 386, 395, 404, 413, 415, 418, 
	421, 422, 423, 440, 458, 462, 463, 465, 
	473, 474, 476, 480, 486
};

static readonly char[] _tsip_machine_parser_header_Route_trans_keys =  new char [] {
	'\u0052', '\u0072', '\u004f', '\u006f', '\u0055', '\u0075', '\u0054', '\u0074', 
	'\u0045', '\u0065', '\u0009', '\u0020', '\u003a', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0022', '\u0025', '\u0027', '\u003c', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u000a', '\u0009', '\u0020', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', 
	'\u0025', '\u0027', '\u003c', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', 
	'\u0020', '\u0009', '\u0020', '\u003c', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u0009', '\u0020', '\u002b', '\u003a', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u0009', '\u0020', '\u003a', '\u003e', 
	'\u0009', '\u000d', '\u0020', '\u002c', '\u003b', '\u000a', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u0020', '\u0021', '\u0025', '\u0027', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', 
	'\u002c', '\u003b', '\u003d', '\u007e', '\u002a', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u002c', 
	'\u003b', '\u003d', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u002c', 
	'\u003b', '\u003d', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', 
	'\u0027', '\u005b', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', '\u0027', '\u005b', 
	'\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', 
	'\u0022', '\u0009', '\u000d', '\u0022', '\u005c', '\u0020', '\u007e', '\u0080', 
	'\u00ff', '\u000a', '\u0009', '\u0020', '\u0009', '\u000d', '\u0020', '\u002c', 
	'\u003b', '\u0009', '\u000d', '\u0020', '\u002c', '\u003b', '\u000a', '\u0009', 
	'\u0020', '\u0009', '\u0020', '\u002c', '\u003b', '\u0000', '\u0009', '\u000b', 
	'\u000c', '\u000e', '\u007f', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u002c', '\u003b', '\u007e', '\u002a', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u003a', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u003a', '\u0030', '\u0039', 
	'\u0041', '\u0046', '\u0061', '\u0066', '\u002e', '\u003a', '\u005d', '\u0030', 
	'\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u0030', '\u0039', '\u002e', 
	'\u0030', '\u0039', '\u0030', '\u0039', '\u002e', '\u0030', '\u0039', '\u0030', 
	'\u0039', '\u005d', '\u0030', '\u0039', '\u005d', '\u0030', '\u0039', '\u005d', 
	'\u002e', '\u0030', '\u0039', '\u002e', '\u002e', '\u0030', '\u0039', '\u002e', 
	'\u002e', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', 
	'\u0066', '\u002e', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u002e', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u002e', '\u003a', '\u005d', '\u0030', '\u0039', 
	'\u0041', '\u0046', '\u0061', '\u0066', '\u002e', '\u003a', '\u005d', '\u0030', 
	'\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u0030', '\u0039', '\u002e', 
	'\u0030', '\u0039', '\u002e', '\u0030', '\u0039', '\u002e', '\u003a', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003c', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u003c', '\u000a', '\u0009', 
	'\u0020', '\u0009', '\u000d', '\u0022', '\u005c', '\u0020', '\u007e', '\u0080', 
	'\u00ff', '\u000a', '\u0009', '\u0020', '\u0009', '\u000d', '\u0020', '\u003c', 
	'\u0000', '\u0009', '\u000b', '\u000c', '\u000e', '\u007f', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Route_single_lengths =  new sbyte [] {
	0, 2, 2, 2, 2, 2, 3, 9, 
	1, 2, 9, 1, 2, 3, 0, 4, 
	3, 0, 1, 5, 1, 7, 1, 2, 
	6, 10, 6, 1, 2, 5, 9, 1, 
	2, 9, 1, 2, 3, 4, 1, 2, 
	5, 5, 1, 2, 4, 0, 9, 1, 
	2, 2, 2, 2, 1, 3, 0, 1, 
	0, 1, 0, 1, 1, 1, 1, 1, 
	1, 1, 3, 3, 2, 2, 2, 2, 
	2, 0, 3, 3, 3, 0, 1, 1, 
	1, 1, 7, 8, 4, 1, 2, 4, 
	1, 2, 4, 0, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Route_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 5, 
	0, 0, 5, 0, 0, 0, 2, 4, 
	0, 0, 0, 0, 0, 5, 0, 0, 
	5, 4, 0, 0, 0, 0, 5, 0, 
	0, 5, 0, 0, 0, 2, 0, 0, 
	0, 0, 0, 0, 0, 3, 4, 3, 
	3, 3, 3, 0, 3, 3, 1, 1, 
	1, 1, 1, 1, 1, 0, 1, 0, 
	1, 0, 3, 3, 3, 3, 3, 3, 
	0, 3, 3, 3, 3, 1, 1, 1, 
	0, 0, 5, 5, 0, 0, 0, 2, 
	0, 0, 0, 3, 0
};

static readonly short[] _tsip_machine_parser_header_Route_index_offsets =  new short [] {
	0, 0, 3, 6, 9, 12, 15, 19, 
	34, 36, 39, 54, 56, 59, 63, 66, 
	75, 79, 80, 82, 88, 90, 103, 105, 
	108, 120, 135, 142, 144, 147, 153, 168, 
	170, 173, 188, 190, 193, 197, 204, 206, 
	209, 215, 221, 223, 226, 231, 235, 249, 
	254, 260, 266, 272, 275, 280, 287, 289, 
	292, 294, 297, 299, 302, 305, 307, 310, 
	312, 315, 317, 324, 331, 337, 343, 349, 
	355, 358, 362, 369, 376, 383, 385, 388, 
	391, 393, 395, 408, 422, 427, 429, 432, 
	439, 441, 444, 449, 453
};

static readonly sbyte[] _tsip_machine_parser_header_Route_indicies =  new sbyte [] {
	0, 0, 1, 2, 2, 1, 3, 3, 
	1, 4, 4, 1, 5, 5, 1, 5, 
	5, 6, 1, 7, 8, 7, 9, 10, 
	9, 9, 11, 9, 9, 9, 9, 9, 
	9, 1, 12, 1, 13, 13, 1, 14, 
	15, 14, 9, 10, 9, 9, 11, 9, 
	9, 9, 9, 9, 9, 1, 16, 1, 
	17, 17, 1, 17, 17, 18, 1, 19, 
	19, 1, 20, 20, 21, 22, 21, 21, 
	21, 21, 1, 20, 20, 22, 1, 23, 
	24, 23, 25, 26, 25, 27, 28, 1, 
	29, 1, 28, 30, 28, 31, 31, 31, 
	31, 31, 31, 31, 31, 31, 1, 32, 
	1, 33, 33, 1, 33, 33, 31, 31, 
	31, 31, 31, 31, 31, 31, 31, 1, 
	34, 35, 34, 36, 36, 36, 37, 38, 
	39, 36, 36, 36, 36, 36, 1, 40, 
	41, 40, 6, 28, 39, 1, 42, 1, 
	43, 43, 1, 43, 43, 6, 28, 39, 
	1, 39, 44, 39, 45, 46, 45, 45, 
	47, 45, 45, 45, 45, 45, 45, 1, 
	48, 1, 49, 49, 1, 49, 50, 49, 
	45, 46, 45, 45, 47, 45, 45, 45, 
	45, 45, 45, 1, 51, 1, 52, 52, 
	1, 52, 52, 46, 1, 46, 53, 54, 
	55, 46, 46, 1, 56, 1, 46, 46, 
	1, 57, 35, 57, 37, 38, 1, 58, 
	59, 58, 6, 28, 1, 60, 1, 61, 
	61, 1, 61, 61, 6, 28, 1, 46, 
	46, 46, 1, 57, 35, 57, 45, 45, 
	45, 37, 38, 45, 45, 45, 45, 45, 
	1, 63, 62, 62, 62, 1, 65, 54, 
	64, 64, 64, 1, 65, 54, 66, 66, 
	66, 1, 65, 54, 67, 67, 67, 1, 
	65, 54, 1, 69, 68, 62, 62, 1, 
	70, 65, 54, 71, 64, 64, 1, 72, 
	1, 73, 74, 1, 75, 1, 76, 77, 
	1, 78, 1, 54, 79, 1, 54, 80, 
	1, 54, 1, 76, 81, 1, 76, 1, 
	73, 82, 1, 73, 1, 70, 65, 54, 
	83, 66, 66, 1, 70, 65, 54, 67, 
	67, 67, 1, 85, 54, 84, 84, 84, 
	1, 87, 54, 86, 86, 86, 1, 87, 
	54, 88, 88, 88, 1, 87, 54, 89, 
	89, 89, 1, 87, 54, 1, 90, 84, 
	84, 1, 70, 87, 54, 91, 86, 86, 
	1, 70, 87, 54, 92, 88, 88, 1, 
	70, 87, 54, 89, 89, 89, 1, 93, 
	1, 70, 94, 1, 70, 95, 1, 70, 
	1, 69, 1, 96, 97, 96, 98, 98, 
	98, 98, 98, 98, 98, 98, 98, 1, 
	99, 100, 99, 98, 98, 98, 101, 98, 
	98, 98, 98, 98, 98, 1, 102, 103, 
	102, 18, 1, 104, 1, 96, 96, 1, 
	105, 106, 107, 108, 105, 105, 1, 109, 
	1, 105, 105, 1, 99, 100, 99, 101, 
	1, 105, 105, 105, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Route_trans_targs =  new sbyte [] {
	2, 0, 3, 4, 5, 6, 7, 7, 
	8, 82, 87, 14, 9, 10, 10, 11, 
	12, 13, 14, 15, 16, 15, 17, 18, 
	19, 19, 20, 7, 21, 92, 22, 25, 
	23, 24, 26, 20, 25, 7, 21, 30, 
	26, 27, 28, 29, 31, 46, 37, 47, 
	32, 33, 34, 35, 36, 38, 40, 45, 
	39, 41, 41, 42, 43, 44, 48, 81, 
	49, 52, 50, 51, 53, 68, 54, 66, 
	55, 56, 64, 57, 58, 62, 59, 60, 
	61, 63, 65, 67, 69, 77, 70, 73, 
	71, 72, 74, 75, 76, 78, 79, 80, 
	83, 85, 82, 84, 11, 14, 84, 11, 
	86, 87, 88, 90, 91, 89
};

static readonly sbyte[] _tsip_machine_parser_header_Route_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 3, 
	3, 15, 15, 3, 0, 0, 3, 3, 
	0, 0, 0, 1, 0, 0, 0, 0, 
	7, 11, 11, 11, 0, 13, 0, 1, 
	0, 0, 18, 18, 0, 18, 9, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 18, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 5, 5, 5, 0, 0, 
	0, 0, 0, 0, 0, 0
};

const int tsip_machine_parser_header_Route_start = 1;
const int tsip_machine_parser_header_Route_first_final = 92;
const int tsip_machine_parser_header_Route_error = 0;

const int tsip_machine_parser_header_Route_en_main = 1;


/* #line 137 "./ragel/tsip_parser_header_Route.rl" */

		public static List<TSIP_HeaderRoute> Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			List<TSIP_HeaderRoute> hdr_routes = new List<TSIP_HeaderRoute>();
			TSIP_HeaderRoute curr_route = null;

			int tag_start = 0;

			
			
/* #line 324 "../Headers/TSIP_HeaderRoute.cs" */
	{
	cs = tsip_machine_parser_header_Route_start;
	}

/* #line 151 "./ragel/tsip_parser_header_Route.rl" */
			
/* #line 331 "../Headers/TSIP_HeaderRoute.cs" */
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
	_keys = _tsip_machine_parser_header_Route_key_offsets[cs];
	_trans = (short)_tsip_machine_parser_header_Route_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Route_single_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Route_trans_keys[_mid] )
				_upper = (short) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Route_trans_keys[_mid] )
				_lower = (short) (_mid + 1);
			else {
				_trans += (short) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (short) _klen;
		_trans += (short) _klen;
	}

	_klen = _tsip_machine_parser_header_Route_range_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Route_trans_keys[_mid] )
				_upper = (short) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Route_trans_keys[_mid+1] )
				_lower = (short) (_mid + 2);
			else {
				_trans += (short)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (short) _klen;
	}

_match:
	_trans = (short)_tsip_machine_parser_header_Route_indicies[_trans];
	cs = _tsip_machine_parser_header_Route_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Route_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Route_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Route_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Route_actions[_acts++] )
		{
	case 0:
/* #line 31 "./ragel/tsip_parser_header_Route.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 35 "./ragel/tsip_parser_header_Route.rl" */
	{
		if(curr_route == null){
			curr_route = new TSIP_HeaderRoute();
		}
	}
	break;
	case 2:
/* #line 41 "./ragel/tsip_parser_header_Route.rl" */
	{
		if(curr_route != null){
			curr_route.DisplayName = TSK_RagelState.Parser.GetString(data, p, tag_start);
			curr_route.DisplayName = TSK_String.UnQuote(curr_route.DisplayName);
		}
	}
	break;
	case 3:
/* #line 48 "./ragel/tsip_parser_header_Route.rl" */
	{
		if(curr_route != null && curr_route.Uri == null){
			int len = (int)(p  - tag_start);
            if ((curr_route.Uri = TSIP_ParserUri.Parse(data.Substring(tag_start, len))) != null && !String.IsNullOrEmpty(curr_route.DisplayName))
            {
                curr_route.Uri.DisplayName = curr_route.DisplayName;
			}
		}
	}
	break;
	case 4:
/* #line 58 "./ragel/tsip_parser_header_Route.rl" */
	{
		if(curr_route != null){
			curr_route.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, curr_route.Params);
		}
	}
	break;
	case 5:
/* #line 64 "./ragel/tsip_parser_header_Route.rl" */
	{
		if(curr_route != null){
			hdr_routes.Add(curr_route);
            curr_route = null;
		}
	}
	break;
	case 6:
/* #line 71 "./ragel/tsip_parser_header_Route.rl" */
	{
	}
	break;
/* #line 462 "../Headers/TSIP_HeaderRoute.cs" */
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

/* #line 152 "./ragel/tsip_parser_header_Route.rl" */
			
			if( cs < 
/* #line 479 "../Headers/TSIP_HeaderRoute.cs" */
92
/* #line 153 "./ragel/tsip_parser_header_Route.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Route' header.");
				hdr_routes.Clear();
				hdr_routes = null;
				curr_route.Dispose();
				curr_route = null;
			}
			
			return hdr_routes;
		}
	}
}