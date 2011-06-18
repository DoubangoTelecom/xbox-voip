
/* #line 1 "./ragel/tsip_parser_header_From.rl" */
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

/* #line 73 "./ragel/tsip_parser_header_From.rl" */


using System;
using Doubango.tinySAK;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderFrom : TSIP_Header
	{
		private String mDisplayName;
		private TSIP_Uri mUri;
		private String mTag;

		public TSIP_HeaderFrom()
			: this(null,null,null)
		{
		}

		public TSIP_HeaderFrom(String displayName, TSIP_Uri uri, String tag)
			: base(tsip_header_type_t.From)
		{
			this.DisplayName = displayName;
			this.Uri = uri;
			this.Tag = tag;
		}

		public override String Value
        {
            get 
            { 
               // Uri with hacked display-name
                String ret = TSIP_Uri.Serialize(this.Uri, true, true);
                if (ret != null && this.Tag != null)
                {
                    ret += String.Format(";tag={0}", this.Tag);
                }
                return ret;
            }
            set { TSK_Debug.Error("Not implemented"); }
        }

		public String DisplayName
		{
			get { return mDisplayName; }
			set { mDisplayName = value;}
		}

		public TSIP_Uri Uri
			{
			get { return mUri; }
			set { mUri = value;}
		}

		public String Tag
		{
			get { return mTag; }
			set { mTag = value;}
		}

		
/* #line 91 "../Headers/TSIP_HeaderFrom.cs" */
static readonly sbyte[] _tsip_machine_parser_header_From_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2, 1, 
	3, 1, 4, 1, 5
};

static readonly short[] _tsip_machine_parser_header_From_key_offsets =  new short [] {
	0, 0, 2, 7, 10, 31, 32, 34, 
	55, 56, 58, 61, 65, 77, 80, 80, 
	81, 85, 89, 90, 92, 95, 114, 115, 
	117, 135, 154, 159, 160, 162, 166, 185, 
	186, 188, 207, 208, 210, 213, 221, 222, 
	224, 228, 229, 235, 253, 260, 268, 276, 
	284, 286, 293, 302, 304, 307, 309, 312, 
	314, 317, 320, 321, 324, 325, 328, 329, 
	338, 347, 355, 363, 371, 379, 381, 387, 
	396, 405, 414, 416, 419, 422, 423, 424, 
	445, 466, 485, 490, 491, 493, 497, 516, 
	517, 519, 538, 556, 573, 591, 595, 596, 
	598, 606, 607, 609, 613, 619, 639, 658, 
	663, 663, 667, 669, 671
};

static readonly char[] _tsip_machine_parser_header_From_trans_keys =  new char [] {
	'\u0046', '\u0066', '\u0009', '\u0020', '\u003a', '\u0052', '\u0072', '\u0009', 
	'\u0020', '\u003a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', 
	'\u0027', '\u003c', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u0060', '\u0061', '\u007a', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', 
	'\u0027', '\u003c', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u0060', '\u0061', '\u007a', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u0020', '\u003c', '\u0041', '\u005a', '\u0061', 
	'\u007a', '\u0009', '\u0020', '\u002b', '\u003a', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u0009', '\u0020', '\u003a', 
	'\u003e', '\u0009', '\u000d', '\u0020', '\u003b', '\u0009', '\u000d', '\u0020', 
	'\u003b', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u003b', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u0054', '\u0074', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u0054', '\u0074', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003b', '\u003d', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u003b', '\u003d', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u0020', '\u003b', '\u003d', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0022', '\u0025', '\u0027', '\u005b', '\u007e', '\u002a', 
	'\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0022', '\u0025', '\u0027', '\u005b', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u0020', '\u0022', '\u0009', '\u000d', '\u0022', 
	'\u005c', '\u0020', '\u007e', '\u0080', '\u00ff', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u000d', '\u0020', '\u003b', '\u000a', '\u0000', '\u0009', '\u000b', 
	'\u000c', '\u000e', '\u007f', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u003b', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u003a', '\u0030', '\u0039', 
	'\u0041', '\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', 
	'\u0041', '\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', 
	'\u0041', '\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', 
	'\u0041', '\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u003a', '\u0030', 
	'\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u002e', '\u003a', '\u005d', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u0030', '\u0039', 
	'\u002e', '\u0030', '\u0039', '\u0030', '\u0039', '\u002e', '\u0030', '\u0039', 
	'\u0030', '\u0039', '\u005d', '\u0030', '\u0039', '\u005d', '\u0030', '\u0039', 
	'\u005d', '\u002e', '\u0030', '\u0039', '\u002e', '\u002e', '\u0030', '\u0039', 
	'\u002e', '\u002e', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u002e', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', 
	'\u0046', '\u0061', '\u0066', '\u002e', '\u003a', '\u005d', '\u0030', '\u0039', 
	'\u0041', '\u0046', '\u0061', '\u0066', '\u002e', '\u003a', '\u005d', '\u0030', 
	'\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u002e', '\u003a', '\u005d', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u0030', '\u0039', 
	'\u002e', '\u0030', '\u0039', '\u002e', '\u0030', '\u0039', '\u002e', '\u003a', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003b', '\u003d', 
	'\u0041', '\u0061', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0042', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u003b', '\u003d', '\u0047', '\u0067', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', 
	'\u003b', '\u003d', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u003b', '\u003d', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u003b', 
	'\u003d', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', '\u0027', 
	'\u005b', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0022', '\u0025', '\u0027', '\u005b', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', 
	'\u003b', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u003c', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', 
	'\u000d', '\u0020', '\u003c', '\u000a', '\u0009', '\u0020', '\u0009', '\u000d', 
	'\u0022', '\u005c', '\u0020', '\u007e', '\u0080', '\u00ff', '\u000a', '\u0009', 
	'\u0020', '\u0009', '\u000d', '\u0020', '\u003c', '\u0000', '\u0009', '\u000b', 
	'\u000c', '\u000e', '\u007f', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u002a', '\u002b', '\u003a', '\u007e', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u0060', '\u0061', '\u007a', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003a', '\u003c', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u003a', '\u003c', '\u0009', 
	'\u000d', '\u0020', '\u003b', '\u004f', '\u006f', '\u004d', '\u006d', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_From_single_lengths =  new sbyte [] {
	0, 2, 5, 3, 9, 1, 2, 9, 
	1, 2, 3, 0, 4, 3, 0, 1, 
	4, 4, 1, 2, 3, 9, 1, 2, 
	8, 9, 5, 1, 2, 4, 9, 1, 
	2, 9, 1, 2, 3, 4, 1, 2, 
	4, 1, 0, 8, 1, 2, 2, 2, 
	2, 1, 3, 0, 1, 0, 1, 0, 
	1, 1, 1, 1, 1, 1, 1, 3, 
	3, 2, 2, 2, 2, 2, 0, 3, 
	3, 3, 0, 1, 1, 1, 1, 11, 
	11, 9, 5, 1, 2, 4, 9, 1, 
	2, 9, 8, 7, 8, 4, 1, 2, 
	4, 1, 2, 4, 0, 10, 9, 5, 
	0, 4, 2, 2, 0
};

static readonly sbyte[] _tsip_machine_parser_header_From_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 6, 0, 0, 6, 
	0, 0, 0, 2, 4, 0, 0, 0, 
	0, 0, 0, 0, 0, 5, 0, 0, 
	5, 5, 0, 0, 0, 0, 5, 0, 
	0, 5, 0, 0, 0, 2, 0, 0, 
	0, 0, 3, 5, 3, 3, 3, 3, 
	0, 3, 3, 1, 1, 1, 1, 1, 
	1, 1, 0, 1, 0, 1, 0, 3, 
	3, 3, 3, 3, 3, 0, 3, 3, 
	3, 3, 1, 1, 1, 0, 0, 5, 
	5, 5, 0, 0, 0, 0, 5, 0, 
	0, 5, 5, 5, 5, 0, 0, 0, 
	2, 0, 0, 0, 3, 5, 5, 0, 
	0, 0, 0, 0, 0
};

static readonly short[] _tsip_machine_parser_header_From_index_offsets =  new short [] {
	0, 0, 3, 9, 13, 29, 31, 34, 
	50, 52, 55, 59, 62, 71, 75, 76, 
	78, 83, 88, 90, 93, 97, 112, 114, 
	117, 131, 146, 152, 154, 157, 162, 177, 
	179, 182, 197, 199, 202, 206, 213, 215, 
	218, 223, 225, 229, 243, 248, 254, 260, 
	266, 269, 274, 281, 283, 286, 288, 291, 
	293, 296, 299, 301, 304, 306, 309, 311, 
	318, 325, 331, 337, 343, 349, 352, 356, 
	363, 370, 377, 379, 382, 385, 387, 389, 
	406, 423, 438, 444, 446, 449, 454, 469, 
	471, 474, 489, 503, 516, 530, 535, 537, 
	540, 547, 549, 552, 557, 561, 577, 592, 
	598, 599, 604, 607, 610
};

static readonly byte[] _tsip_machine_parser_header_From_indicies =  new byte [] {
	0, 0, 1, 2, 2, 3, 4, 4, 
	1, 2, 2, 3, 1, 3, 5, 3, 
	6, 7, 6, 6, 8, 6, 6, 6, 
	6, 9, 6, 9, 1, 10, 1, 11, 
	11, 1, 11, 12, 11, 6, 7, 6, 
	6, 8, 6, 6, 6, 6, 9, 6, 
	9, 1, 13, 1, 14, 14, 1, 14, 
	14, 8, 1, 15, 15, 1, 16, 16, 
	17, 18, 17, 17, 17, 17, 1, 16, 
	16, 18, 1, 19, 20, 19, 21, 22, 
	21, 23, 1, 21, 24, 21, 23, 1, 
	25, 1, 26, 26, 1, 26, 26, 23, 
	1, 23, 27, 23, 28, 28, 28, 29, 
	29, 28, 28, 28, 28, 28, 28, 1, 
	30, 1, 31, 31, 1, 31, 31, 28, 
	28, 28, 29, 29, 28, 28, 28, 28, 
	28, 28, 1, 32, 33, 32, 34, 34, 
	34, 35, 36, 34, 34, 34, 34, 34, 
	34, 1, 37, 38, 37, 23, 36, 1, 
	39, 1, 40, 40, 1, 40, 40, 23, 
	36, 1, 36, 41, 36, 42, 43, 42, 
	42, 44, 42, 42, 42, 42, 42, 42, 
	1, 45, 1, 46, 46, 1, 46, 47, 
	46, 42, 43, 42, 42, 44, 42, 42, 
	42, 42, 42, 42, 1, 48, 1, 49, 
	49, 1, 49, 49, 43, 1, 43, 50, 
	51, 52, 43, 43, 1, 53, 1, 43, 
	43, 1, 54, 33, 54, 35, 1, 55, 
	1, 43, 43, 43, 1, 54, 33, 54, 
	42, 42, 42, 35, 42, 42, 42, 42, 
	42, 42, 1, 57, 56, 56, 56, 1, 
	59, 51, 58, 58, 58, 1, 59, 51, 
	60, 60, 60, 1, 59, 51, 61, 61, 
	61, 1, 59, 51, 1, 63, 62, 56, 
	56, 1, 64, 59, 51, 65, 58, 58, 
	1, 66, 1, 67, 68, 1, 69, 1, 
	70, 71, 1, 72, 1, 51, 73, 1, 
	51, 74, 1, 51, 1, 70, 75, 1, 
	70, 1, 67, 76, 1, 67, 1, 64, 
	59, 51, 77, 60, 60, 1, 64, 59, 
	51, 61, 61, 61, 1, 79, 51, 78, 
	78, 78, 1, 81, 51, 80, 80, 80, 
	1, 81, 51, 82, 82, 82, 1, 81, 
	51, 83, 83, 83, 1, 81, 51, 1, 
	84, 78, 78, 1, 64, 81, 51, 85, 
	80, 80, 1, 64, 81, 51, 86, 82, 
	82, 1, 64, 81, 51, 83, 83, 83, 
	1, 87, 1, 64, 88, 1, 64, 89, 
	1, 64, 1, 63, 1, 32, 33, 32, 
	34, 34, 34, 35, 36, 90, 90, 34, 
	34, 34, 34, 34, 34, 1, 32, 33, 
	32, 34, 34, 34, 35, 36, 91, 91, 
	34, 34, 34, 34, 34, 34, 1, 92, 
	33, 92, 34, 34, 34, 35, 93, 34, 
	34, 34, 34, 34, 34, 1, 94, 95, 
	94, 23, 93, 1, 96, 1, 97, 97, 
	1, 97, 97, 23, 93, 1, 93, 98, 
	93, 99, 43, 99, 99, 44, 99, 99, 
	99, 99, 99, 99, 1, 100, 1, 101, 
	101, 1, 101, 47, 101, 99, 43, 99, 
	99, 44, 99, 99, 99, 99, 99, 99, 
	1, 102, 103, 102, 104, 104, 104, 105, 
	104, 104, 104, 104, 104, 104, 1, 106, 
	107, 106, 108, 108, 108, 108, 108, 108, 
	108, 108, 108, 1, 109, 110, 109, 108, 
	108, 108, 111, 108, 108, 108, 108, 108, 
	108, 1, 112, 12, 112, 8, 1, 113, 
	1, 106, 106, 1, 114, 115, 116, 117, 
	114, 114, 1, 118, 1, 114, 114, 1, 
	109, 110, 109, 111, 1, 114, 114, 114, 
	1, 119, 107, 119, 108, 108, 108, 108, 
	120, 121, 108, 120, 120, 120, 108, 120, 
	1, 122, 110, 122, 108, 108, 108, 121, 
	111, 108, 108, 108, 108, 108, 108, 1, 
	123, 12, 123, 121, 8, 1, 124, 125, 
	126, 125, 127, 124, 128, 128, 1, 2, 
	2, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_From_trans_targs =  new sbyte [] {
	2, 0, 3, 4, 106, 5, 91, 96, 
	11, 101, 6, 7, 8, 9, 10, 12, 
	13, 12, 14, 15, 16, 17, 41, 21, 
	18, 19, 20, 22, 25, 79, 23, 24, 
	26, 41, 25, 21, 30, 26, 27, 28, 
	29, 31, 43, 37, 44, 32, 33, 34, 
	35, 36, 38, 40, 42, 39, 17, 108, 
	45, 78, 46, 49, 47, 48, 50, 65, 
	51, 63, 52, 53, 61, 54, 55, 59, 
	56, 57, 58, 60, 62, 64, 66, 74, 
	67, 70, 68, 69, 71, 72, 73, 75, 
	76, 77, 80, 81, 82, 86, 82, 83, 
	84, 85, 87, 90, 88, 89, 17, 41, 
	90, 21, 92, 94, 91, 93, 8, 11, 
	93, 95, 96, 97, 99, 100, 98, 102, 
	101, 104, 103, 103, 105, 17, 41, 21, 
	107
};

static readonly sbyte[] _tsip_machine_parser_header_From_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 1, 1, 
	0, 1, 0, 0, 0, 0, 0, 1, 
	0, 0, 0, 0, 3, 0, 0, 0, 
	0, 0, 0, 0, 1, 1, 0, 0, 
	9, 9, 0, 9, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 9, 11, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 9, 0, 0, 0, 
	0, 0, 0, 1, 0, 0, 7, 7, 
	0, 7, 0, 0, 0, 5, 5, 5, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 5, 0, 0, 3, 3, 3, 
	0
};

const int tsip_machine_parser_header_From_start = 1;
const int tsip_machine_parser_header_From_first_final = 108;
const int tsip_machine_parser_header_From_error = 0;

const int tsip_machine_parser_header_From_en_main = 1;


/* #line 133 "./ragel/tsip_parser_header_From.rl" */

		public static TSIP_HeaderFrom Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderFrom hdr_from = new TSIP_HeaderFrom();

			int tag_start = 0;
			
			
/* #line 392 "../Headers/TSIP_HeaderFrom.cs" */
	{
	cs = tsip_machine_parser_header_From_start;
	}

/* #line 145 "./ragel/tsip_parser_header_From.rl" */
			
/* #line 399 "../Headers/TSIP_HeaderFrom.cs" */
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
	_keys = _tsip_machine_parser_header_From_key_offsets[cs];
	_trans = (short)_tsip_machine_parser_header_From_index_offsets[cs];

	_klen = _tsip_machine_parser_header_From_single_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_From_trans_keys[_mid] )
				_upper = (short) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_From_trans_keys[_mid] )
				_lower = (short) (_mid + 1);
			else {
				_trans += (short) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (short) _klen;
		_trans += (short) _klen;
	}

	_klen = _tsip_machine_parser_header_From_range_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_From_trans_keys[_mid] )
				_upper = (short) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_From_trans_keys[_mid+1] )
				_lower = (short) (_mid + 2);
			else {
				_trans += (short)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (short) _klen;
	}

_match:
	_trans = (short)_tsip_machine_parser_header_From_indicies[_trans];
	cs = _tsip_machine_parser_header_From_trans_targs[_trans];

	if ( _tsip_machine_parser_header_From_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_From_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_From_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_From_actions[_acts++] )
		{
	case 0:
/* #line 32 "./ragel/tsip_parser_header_From.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 36 "./ragel/tsip_parser_header_From.rl" */
	{
		int len = (int)(p  - tag_start);
		if(hdr_from != null && hdr_from.Uri == null){
			if((hdr_from.Uri = TSIP_ParserUri.Parse(data.Substring(tag_start, len))) != null && !String.IsNullOrEmpty(hdr_from.DisplayName)){
				hdr_from.Uri.DisplayName = hdr_from.DisplayName;
			}
		}
	}
	break;
	case 2:
/* #line 45 "./ragel/tsip_parser_header_From.rl" */
	{
		hdr_from.DisplayName = TSK_RagelState.Parser.GetString(data, p, tag_start);
		hdr_from.DisplayName = TSK_String.UnQuote(hdr_from.DisplayName);
	}
	break;
	case 3:
/* #line 50 "./ragel/tsip_parser_header_From.rl" */
	{
		hdr_from.Tag = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	break;
	case 4:
/* #line 54 "./ragel/tsip_parser_header_From.rl" */
	{
		hdr_from.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, hdr_from.Params);
	}
	break;
	case 5:
/* #line 58 "./ragel/tsip_parser_header_From.rl" */
	{
	}
	break;
/* #line 514 "../Headers/TSIP_HeaderFrom.cs" */
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

/* #line 146 "./ragel/tsip_parser_header_From.rl" */
			
			if( cs < 
/* #line 531 "../Headers/TSIP_HeaderFrom.cs" */
108
/* #line 147 "./ragel/tsip_parser_header_From.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'From' header.");
				hdr_from.Dispose();
				hdr_from = null;
			}
			
			return hdr_from;
		}
	}
}