
/* #line 1 "./ragel/tsip_parser_header_Record_Route.rl" */
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

/* #line 86 "./ragel/tsip_parser_header_Record_Route.rl" */


using System;
using Doubango.tinySAK;
using System.Collections.Generic;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderRecordRoute : TSIP_Header
	{
		private String mDisplayName;
		private TSIP_Uri mUri;

		public TSIP_HeaderRecordRoute()
            : this(null)
        {
        }

		public TSIP_HeaderRecordRoute(TSIP_Uri uri)
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

		
/* #line 78 "../Headers/TSIP_HeaderRecordRoute.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Record_Route_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2, 1, 
	3, 1, 4, 1, 5, 1, 6, 2, 
	1, 0, 2, 4, 5
};

static readonly short[] _tsip_machine_parser_header_Record_Route_key_offsets =  new short [] {
	0, 0, 2, 4, 6, 8, 10, 12, 
	13, 15, 17, 19, 21, 23, 26, 45, 
	46, 48, 67, 68, 70, 73, 77, 89, 
	92, 92, 93, 98, 99, 116, 117, 119, 
	135, 153, 159, 160, 162, 167, 186, 187, 
	189, 208, 209, 211, 214, 222, 223, 225, 
	230, 235, 236, 238, 242, 248, 265, 272, 
	280, 288, 296, 298, 305, 314, 316, 319, 
	321, 324, 326, 329, 332, 333, 336, 337, 
	340, 341, 350, 359, 367, 375, 383, 391, 
	393, 399, 408, 417, 426, 428, 431, 434, 
	435, 436, 453, 471, 475, 476, 478, 486, 
	487, 489, 493, 499
};

static readonly char[] _tsip_machine_parser_header_Record_Route_trans_keys =  new char [] {
	'\u0052', '\u0072', '\u0045', '\u0065', '\u0043', '\u0063', '\u004f', '\u006f', 
	'\u0052', '\u0072', '\u0044', '\u0064', '\u002d', '\u0052', '\u0072', '\u004f', 
	'\u006f', '\u0055', '\u0075', '\u0054', '\u0074', '\u0045', '\u0065', '\u0009', 
	'\u0020', '\u003a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', 
	'\u0027', '\u003c', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', '\u0027', '\u003c', 
	'\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', 
	'\u003c', '\u0041', '\u005a', '\u0061', '\u007a', '\u0009', '\u0020', '\u002b', 
	'\u003a', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', 
	'\u007a', '\u0009', '\u0020', '\u003a', '\u003e', '\u0009', '\u000d', '\u0020', 
	'\u002c', '\u003b', '\u000a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u002c', '\u003b', '\u003d', 
	'\u007e', '\u002a', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u0009', '\u000d', '\u0020', '\u002c', '\u003b', '\u003d', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u0020', '\u002c', '\u003b', '\u003d', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0022', '\u0025', '\u0027', '\u005b', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0022', '\u0025', '\u0027', '\u005b', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u0022', '\u0009', '\u000d', 
	'\u0022', '\u005c', '\u0020', '\u007e', '\u0080', '\u00ff', '\u000a', '\u0009', 
	'\u0020', '\u0009', '\u000d', '\u0020', '\u002c', '\u003b', '\u0009', '\u000d', 
	'\u0020', '\u002c', '\u003b', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', 
	'\u002c', '\u003b', '\u0000', '\u0009', '\u000b', '\u000c', '\u000e', '\u007f', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u002c', '\u003b', 
	'\u007e', '\u002a', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u003a', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u003a', '\u005d', '\u003a', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', 
	'\u0066', '\u002e', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u0030', '\u0039', '\u002e', '\u0030', '\u0039', '\u0030', 
	'\u0039', '\u002e', '\u0030', '\u0039', '\u0030', '\u0039', '\u005d', '\u0030', 
	'\u0039', '\u005d', '\u0030', '\u0039', '\u005d', '\u002e', '\u0030', '\u0039', 
	'\u002e', '\u002e', '\u0030', '\u0039', '\u002e', '\u002e', '\u003a', '\u005d', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u002e', '\u003a', 
	'\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', 
	'\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', 
	'\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', 
	'\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', 
	'\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', 
	'\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u002e', 
	'\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u002e', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', 
	'\u0066', '\u002e', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u0030', '\u0039', '\u002e', '\u0030', '\u0039', '\u002e', 
	'\u0030', '\u0039', '\u002e', '\u003a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u003c', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', 
	'\u000d', '\u0020', '\u003c', '\u000a', '\u0009', '\u0020', '\u0009', '\u000d', 
	'\u0022', '\u005c', '\u0020', '\u007e', '\u0080', '\u00ff', '\u000a', '\u0009', 
	'\u0020', '\u0009', '\u000d', '\u0020', '\u003c', '\u0000', '\u0009', '\u000b', 
	'\u000c', '\u000e', '\u007f', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Record_Route_single_lengths =  new sbyte [] {
	0, 2, 2, 2, 2, 2, 2, 1, 
	2, 2, 2, 2, 2, 3, 9, 1, 
	2, 9, 1, 2, 3, 0, 4, 3, 
	0, 1, 5, 1, 7, 1, 2, 6, 
	10, 6, 1, 2, 5, 9, 1, 2, 
	9, 1, 2, 3, 4, 1, 2, 5, 
	5, 1, 2, 4, 0, 9, 1, 2, 
	2, 2, 2, 1, 3, 0, 1, 0, 
	1, 0, 1, 1, 1, 1, 1, 1, 
	1, 3, 3, 2, 2, 2, 2, 2, 
	0, 3, 3, 3, 0, 1, 1, 1, 
	1, 7, 8, 4, 1, 2, 4, 1, 
	2, 4, 0, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Record_Route_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 5, 0, 
	0, 5, 0, 0, 0, 2, 4, 0, 
	0, 0, 0, 0, 5, 0, 0, 5, 
	4, 0, 0, 0, 0, 5, 0, 0, 
	5, 0, 0, 0, 2, 0, 0, 0, 
	0, 0, 0, 0, 3, 4, 3, 3, 
	3, 3, 0, 3, 3, 1, 1, 1, 
	1, 1, 1, 1, 0, 1, 0, 1, 
	0, 3, 3, 3, 3, 3, 3, 0, 
	3, 3, 3, 3, 1, 1, 1, 0, 
	0, 5, 5, 0, 0, 0, 2, 0, 
	0, 0, 3, 0
};

static readonly short[] _tsip_machine_parser_header_Record_Route_index_offsets =  new short [] {
	0, 0, 3, 6, 9, 12, 15, 18, 
	20, 23, 26, 29, 32, 35, 39, 54, 
	56, 59, 74, 76, 79, 83, 86, 95, 
	99, 100, 102, 108, 110, 123, 125, 128, 
	140, 155, 162, 164, 167, 173, 188, 190, 
	193, 208, 210, 213, 217, 224, 226, 229, 
	235, 241, 243, 246, 251, 255, 269, 274, 
	280, 286, 292, 295, 300, 307, 309, 312, 
	314, 317, 319, 322, 325, 327, 330, 332, 
	335, 337, 344, 351, 357, 363, 369, 375, 
	378, 382, 389, 396, 403, 405, 408, 411, 
	413, 415, 428, 442, 447, 449, 452, 459, 
	461, 464, 469, 473
};

static readonly sbyte[] _tsip_machine_parser_header_Record_Route_indicies =  new sbyte [] {
	0, 0, 1, 2, 2, 1, 3, 3, 
	1, 4, 4, 1, 5, 5, 1, 6, 
	6, 1, 7, 1, 8, 8, 1, 9, 
	9, 1, 10, 10, 1, 11, 11, 1, 
	12, 12, 1, 12, 12, 13, 1, 14, 
	15, 14, 16, 17, 16, 16, 18, 16, 
	16, 16, 16, 16, 16, 1, 19, 1, 
	20, 20, 1, 21, 22, 21, 16, 17, 
	16, 16, 18, 16, 16, 16, 16, 16, 
	16, 1, 23, 1, 24, 24, 1, 24, 
	24, 25, 1, 26, 26, 1, 27, 27, 
	28, 29, 28, 28, 28, 28, 1, 27, 
	27, 29, 1, 30, 31, 30, 32, 33, 
	32, 34, 35, 1, 36, 1, 35, 37, 
	35, 38, 38, 38, 38, 38, 38, 38, 
	38, 38, 1, 39, 1, 40, 40, 1, 
	40, 40, 38, 38, 38, 38, 38, 38, 
	38, 38, 38, 1, 41, 42, 41, 43, 
	43, 43, 44, 45, 46, 43, 43, 43, 
	43, 43, 1, 47, 48, 47, 13, 35, 
	46, 1, 49, 1, 50, 50, 1, 50, 
	50, 13, 35, 46, 1, 46, 51, 46, 
	52, 53, 52, 52, 54, 52, 52, 52, 
	52, 52, 52, 1, 55, 1, 56, 56, 
	1, 56, 57, 56, 52, 53, 52, 52, 
	54, 52, 52, 52, 52, 52, 52, 1, 
	58, 1, 59, 59, 1, 59, 59, 53, 
	1, 53, 60, 61, 62, 53, 53, 1, 
	63, 1, 53, 53, 1, 64, 42, 64, 
	44, 45, 1, 65, 66, 65, 13, 35, 
	1, 67, 1, 68, 68, 1, 68, 68, 
	13, 35, 1, 53, 53, 53, 1, 64, 
	42, 64, 52, 52, 52, 44, 45, 52, 
	52, 52, 52, 52, 1, 70, 69, 69, 
	69, 1, 72, 61, 71, 71, 71, 1, 
	72, 61, 73, 73, 73, 1, 72, 61, 
	74, 74, 74, 1, 72, 61, 1, 76, 
	75, 69, 69, 1, 77, 72, 61, 78, 
	71, 71, 1, 79, 1, 80, 81, 1, 
	82, 1, 83, 84, 1, 85, 1, 61, 
	86, 1, 61, 87, 1, 61, 1, 83, 
	88, 1, 83, 1, 80, 89, 1, 80, 
	1, 77, 72, 61, 90, 73, 73, 1, 
	77, 72, 61, 74, 74, 74, 1, 92, 
	61, 91, 91, 91, 1, 94, 61, 93, 
	93, 93, 1, 94, 61, 95, 95, 95, 
	1, 94, 61, 96, 96, 96, 1, 94, 
	61, 1, 97, 91, 91, 1, 77, 94, 
	61, 98, 93, 93, 1, 77, 94, 61, 
	99, 95, 95, 1, 77, 94, 61, 96, 
	96, 96, 1, 100, 1, 77, 101, 1, 
	77, 102, 1, 77, 1, 76, 1, 103, 
	104, 103, 105, 105, 105, 105, 105, 105, 
	105, 105, 105, 1, 106, 107, 106, 105, 
	105, 105, 108, 105, 105, 105, 105, 105, 
	105, 1, 109, 110, 109, 25, 1, 111, 
	1, 103, 103, 1, 112, 113, 114, 115, 
	112, 112, 1, 116, 1, 112, 112, 1, 
	106, 107, 106, 108, 1, 112, 112, 112, 
	1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Record_Route_trans_targs =  new sbyte [] {
	2, 0, 3, 4, 5, 6, 7, 8, 
	9, 10, 11, 12, 13, 14, 14, 15, 
	89, 94, 21, 16, 17, 17, 18, 19, 
	20, 21, 22, 23, 22, 24, 25, 26, 
	26, 27, 14, 28, 99, 29, 32, 30, 
	31, 33, 27, 32, 14, 28, 37, 33, 
	34, 35, 36, 38, 53, 44, 54, 39, 
	40, 41, 42, 43, 45, 47, 52, 46, 
	48, 48, 49, 50, 51, 55, 88, 56, 
	59, 57, 58, 60, 75, 61, 73, 62, 
	63, 71, 64, 65, 69, 66, 67, 68, 
	70, 72, 74, 76, 84, 77, 80, 78, 
	79, 81, 82, 83, 85, 86, 87, 90, 
	92, 89, 91, 18, 21, 91, 18, 93, 
	94, 95, 97, 98, 96
};

static readonly sbyte[] _tsip_machine_parser_header_Record_Route_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 3, 3, 
	15, 15, 3, 0, 0, 3, 3, 0, 
	0, 0, 1, 0, 0, 0, 0, 7, 
	11, 11, 11, 0, 13, 0, 1, 0, 
	0, 18, 18, 0, 18, 9, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	18, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 5, 5, 5, 0, 0, 0, 
	0, 0, 0, 0, 0
};

const int tsip_machine_parser_header_Record_Route_start = 1;
const int tsip_machine_parser_header_Record_Route_first_final = 99;
const int tsip_machine_parser_header_Record_Route_error = 0;

const int tsip_machine_parser_header_Record_Route_en_main = 1;


/* #line 133 "./ragel/tsip_parser_header_Record_Route.rl" */

		public static List<TSIP_HeaderRecordRoute> Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			List<TSIP_HeaderRecordRoute> hdr_record_routes = new List<TSIP_HeaderRecordRoute>();
			TSIP_HeaderRecordRoute curr_route = null;

			int tag_start = 0;

			
			
/* #line 336 "../Headers/TSIP_HeaderRecordRoute.cs" */
	{
	cs = tsip_machine_parser_header_Record_Route_start;
	}

/* #line 147 "./ragel/tsip_parser_header_Record_Route.rl" */
			
/* #line 343 "../Headers/TSIP_HeaderRecordRoute.cs" */
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
	_keys = _tsip_machine_parser_header_Record_Route_key_offsets[cs];
	_trans = (short)_tsip_machine_parser_header_Record_Route_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Record_Route_single_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Record_Route_trans_keys[_mid] )
				_upper = (short) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Record_Route_trans_keys[_mid] )
				_lower = (short) (_mid + 1);
			else {
				_trans += (short) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (short) _klen;
		_trans += (short) _klen;
	}

	_klen = _tsip_machine_parser_header_Record_Route_range_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Record_Route_trans_keys[_mid] )
				_upper = (short) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Record_Route_trans_keys[_mid+1] )
				_lower = (short) (_mid + 2);
			else {
				_trans += (short)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (short) _klen;
	}

_match:
	_trans = (short)_tsip_machine_parser_header_Record_Route_indicies[_trans];
	cs = _tsip_machine_parser_header_Record_Route_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Record_Route_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Record_Route_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Record_Route_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Record_Route_actions[_acts++] )
		{
	case 0:
/* #line 32 "./ragel/tsip_parser_header_Record_Route.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 36 "./ragel/tsip_parser_header_Record_Route.rl" */
	{
		if(curr_route == null){
			curr_route = new TSIP_HeaderRecordRoute();
		}
	}
	break;
	case 2:
/* #line 42 "./ragel/tsip_parser_header_Record_Route.rl" */
	{
		if(curr_route != null){
			curr_route.DisplayName = TSK_RagelState.Parser.GetString(data, p, tag_start);
			curr_route.DisplayName = TSK_String.UnQuote(curr_route.DisplayName);
		}
	}
	break;
	case 3:
/* #line 49 "./ragel/tsip_parser_header_Record_Route.rl" */
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
/* #line 59 "./ragel/tsip_parser_header_Record_Route.rl" */
	{
		if(curr_route != null){
			curr_route.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, curr_route.Params);
		}
	}
	break;
	case 5:
/* #line 65 "./ragel/tsip_parser_header_Record_Route.rl" */
	{
		if(curr_route != null){
			hdr_record_routes.Add(curr_route);
            curr_route = null;
		}
	}
	break;
	case 6:
/* #line 72 "./ragel/tsip_parser_header_Record_Route.rl" */
	{
	}
	break;
/* #line 474 "../Headers/TSIP_HeaderRecordRoute.cs" */
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

/* #line 148 "./ragel/tsip_parser_header_Record_Route.rl" */
			
			if( cs < 
/* #line 491 "../Headers/TSIP_HeaderRecordRoute.cs" */
99
/* #line 149 "./ragel/tsip_parser_header_Record_Route.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Record-Route' header.");
				hdr_record_routes.Clear();
				hdr_record_routes = null;
				curr_route.Dispose();
				curr_route = null;
			}
			
			return hdr_record_routes;
		}
	}
}