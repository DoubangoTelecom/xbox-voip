
/* #line 1 "./ragel/tsip_parser_header_Contact.rl" */
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

/* #line 93 "./ragel/tsip_parser_header_Contact.rl" */


using System;
using Doubango.tinySAK;
using System.Collections.Generic;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderContact : TSIP_Header
	{
		private String mDisplayName;
		private TSIP_Uri mUri;
		private Int64 mExpires;

		public TSIP_HeaderContact()
            : this(null, null, 0)
        {
        }

		public TSIP_HeaderContact(String displayName, TSIP_Uri uri, Int64 expires)
            : base(tsip_header_type_t.Contact)
        {
			mDisplayName = displayName;
			mUri = uri;
			mExpires = expires;
        }

		public override String Value
        {
            get 
            { 
				// Uri with hacked display-name
                String ret = TSIP_Uri.Serialize(this.Uri, true, true);
                // Expires 
                if (this.Expires >= 0)
                {
                    ret += String.Format(";expires={0}", this.Expires);
                }
                return ret;
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

		public Int64 Expires
        {
            get { return mExpires; }
            set { mExpires = value; }
        }

		
/* #line 92 "../Headers/TSIP_HeaderContact.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Contact_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2, 1, 
	3, 1, 4, 1, 5, 1, 6, 1, 
	7, 2, 1, 0, 2, 3, 6, 2, 
	4, 6, 2, 5, 6
};

static readonly short[] _tsip_machine_parser_header_Contact_key_offsets =  new short [] {
	0, 0, 4, 6, 8, 10, 12, 14, 
	16, 19, 40, 41, 43, 64, 65, 67, 
	71, 74, 75, 79, 91, 94, 94, 95, 
	100, 105, 106, 108, 112, 133, 134, 136, 
	157, 158, 160, 163, 180, 198, 202, 203, 
	205, 213, 214, 216, 220, 226, 246, 265, 
	270, 270, 275, 294, 295, 297, 315, 333, 
	339, 340, 342, 347, 366, 367, 369, 388, 
	389, 391, 394, 402, 403, 405, 410, 416, 
	433, 440, 448, 456, 464, 466, 473, 482, 
	484, 487, 489, 492, 494, 497, 500, 501, 
	504, 505, 508, 509, 518, 527, 535, 543, 
	551, 559, 561, 567, 576, 585, 594, 596, 
	599, 602, 603, 604, 624, 644, 664, 684, 
	704, 724, 742, 748, 749, 751, 756, 775, 
	776, 778, 797, 804, 821, 839, 843
};

static readonly char[] _tsip_machine_parser_header_Contact_trans_keys =  new char [] {
	'\u0043', '\u004d', '\u0063', '\u006d', '\u004f', '\u006f', '\u004e', '\u006e', 
	'\u0054', '\u0074', '\u0041', '\u0061', '\u0043', '\u0063', '\u0054', '\u0074', 
	'\u0009', '\u0020', '\u003a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', 
	'\u0025', '\u0027', '\u002a', '\u002b', '\u003c', '\u007e', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u0060', '\u0061', '\u007a', 
	'\u000a', '\u0009', '\u0020', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', 
	'\u0025', '\u0027', '\u002a', '\u002b', '\u003c', '\u007e', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u0060', '\u0061', '\u007a', 
	'\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u002a', '\u003c', '\u0009', 
	'\u000d', '\u0020', '\u000a', '\u0041', '\u005a', '\u0061', '\u007a', '\u0009', 
	'\u0020', '\u002b', '\u003a', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u0061', '\u007a', '\u0009', '\u0020', '\u003a', '\u003e', '\u0009', 
	'\u000d', '\u0020', '\u002c', '\u003b', '\u0009', '\u000d', '\u0020', '\u002c', 
	'\u003b', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u002c', '\u003b', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', '\u0027', '\u003c', 
	'\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u005f', '\u0060', '\u0061', '\u007a', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', '\u0027', '\u003c', 
	'\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u005f', '\u0060', '\u0061', '\u007a', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u0020', '\u003c', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u003c', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', 
	'\u0020', '\u003c', '\u000a', '\u0009', '\u0020', '\u0009', '\u000d', '\u0022', 
	'\u005c', '\u0020', '\u007e', '\u0080', '\u00ff', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u000d', '\u0020', '\u003c', '\u0000', '\u0009', '\u000b', '\u000c', 
	'\u000e', '\u007f', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', 
	'\u002a', '\u002b', '\u003a', '\u007e', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u0060', '\u0061', '\u007a', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u003a', '\u003c', '\u007e', '\u002a', 
	'\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u0009', '\u000d', '\u0020', '\u003a', '\u003c', '\u0009', '\u000d', 
	'\u0020', '\u002c', '\u003b', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u0045', '\u0065', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', 
	'\u0020', '\u0009', '\u0020', '\u0021', '\u0025', '\u0027', '\u0045', '\u0065', 
	'\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u002c', '\u003b', '\u003d', '\u007e', '\u002a', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u002c', '\u003b', '\u003d', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', 
	'\u002c', '\u003b', '\u003d', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', 
	'\u0025', '\u0027', '\u005b', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', 
	'\u0020', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', '\u0027', 
	'\u005b', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', 
	'\u0020', '\u0022', '\u0009', '\u000d', '\u0022', '\u005c', '\u0020', '\u007e', 
	'\u0080', '\u00ff', '\u000a', '\u0009', '\u0020', '\u0009', '\u000d', '\u0020', 
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
	'\u0025', '\u0027', '\u002c', '\u003b', '\u003d', '\u0058', '\u0078', '\u007e', 
	'\u002a', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u002c', '\u003b', 
	'\u003d', '\u0050', '\u0070', '\u007e', '\u002a', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u002c', '\u003b', '\u003d', '\u0049', '\u0069', '\u007e', 
	'\u002a', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u002c', '\u003b', 
	'\u003d', '\u0052', '\u0072', '\u007e', '\u002a', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u002c', '\u003b', '\u003d', '\u0045', '\u0065', '\u007e', 
	'\u002a', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u002c', '\u003b', 
	'\u003d', '\u0053', '\u0073', '\u007e', '\u002a', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u002c', '\u003b', '\u003d', '\u007e', '\u002a', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', 
	'\u0020', '\u002c', '\u003b', '\u003d', '\u000a', '\u0009', '\u0020', '\u0009', 
	'\u0020', '\u002c', '\u003b', '\u003d', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0022', '\u0025', '\u0027', '\u005b', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', 
	'\u0027', '\u005b', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u002c', '\u003b', '\u0030', '\u0039', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u003c', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', 
	'\u000d', '\u0020', '\u003c', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Contact_single_lengths =  new sbyte [] {
	0, 4, 2, 2, 2, 2, 2, 2, 
	3, 11, 1, 2, 11, 1, 2, 4, 
	3, 1, 0, 4, 3, 0, 1, 5, 
	5, 1, 2, 4, 9, 1, 2, 9, 
	1, 2, 3, 7, 8, 4, 1, 2, 
	4, 1, 2, 4, 0, 10, 9, 5, 
	0, 5, 9, 1, 2, 8, 10, 6, 
	1, 2, 5, 9, 1, 2, 9, 1, 
	2, 3, 4, 1, 2, 5, 0, 9, 
	1, 2, 2, 2, 2, 1, 3, 0, 
	1, 0, 1, 0, 1, 1, 1, 1, 
	1, 1, 1, 3, 3, 2, 2, 2, 
	2, 2, 0, 3, 3, 3, 0, 1, 
	1, 1, 1, 12, 12, 12, 12, 12, 
	12, 10, 6, 1, 2, 5, 9, 1, 
	2, 9, 5, 7, 8, 4, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Contact_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 5, 0, 0, 5, 0, 0, 0, 
	0, 0, 2, 4, 0, 0, 0, 0, 
	0, 0, 0, 0, 6, 0, 0, 6, 
	0, 0, 0, 5, 5, 0, 0, 0, 
	2, 0, 0, 0, 3, 5, 5, 0, 
	0, 0, 5, 0, 0, 5, 4, 0, 
	0, 0, 0, 5, 0, 0, 5, 0, 
	0, 0, 2, 0, 0, 0, 3, 4, 
	3, 3, 3, 3, 0, 3, 3, 1, 
	1, 1, 1, 1, 1, 1, 0, 1, 
	0, 1, 0, 3, 3, 3, 3, 3, 
	3, 0, 3, 3, 3, 3, 1, 1, 
	1, 0, 0, 4, 4, 4, 4, 4, 
	4, 4, 0, 0, 0, 0, 5, 0, 
	0, 5, 1, 5, 5, 0, 0
};

static readonly short[] _tsip_machine_parser_header_Contact_index_offsets =  new short [] {
	0, 0, 5, 8, 11, 14, 17, 20, 
	23, 27, 44, 46, 49, 66, 68, 71, 
	76, 80, 82, 85, 94, 98, 99, 101, 
	107, 113, 115, 118, 123, 139, 141, 144, 
	160, 162, 165, 169, 182, 196, 201, 203, 
	206, 213, 215, 218, 223, 227, 243, 258, 
	264, 265, 271, 286, 288, 291, 305, 320, 
	327, 329, 332, 338, 353, 355, 358, 373, 
	375, 378, 382, 389, 391, 394, 400, 404, 
	418, 423, 429, 435, 441, 444, 449, 456, 
	458, 461, 463, 466, 468, 471, 474, 476, 
	479, 481, 484, 486, 493, 500, 506, 512, 
	518, 524, 527, 531, 538, 545, 552, 554, 
	557, 560, 562, 564, 581, 598, 615, 632, 
	649, 666, 681, 688, 690, 693, 699, 714, 
	716, 719, 734, 741, 754, 768, 773
};

static readonly byte[] _tsip_machine_parser_header_Contact_indicies =  new byte [] {
	0, 2, 0, 2, 1, 3, 3, 1, 
	4, 4, 1, 5, 5, 1, 6, 6, 
	1, 7, 7, 1, 2, 2, 1, 2, 
	2, 8, 1, 9, 10, 9, 11, 12, 
	11, 11, 13, 11, 14, 11, 11, 11, 
	15, 11, 15, 1, 16, 1, 17, 17, 
	1, 18, 19, 18, 11, 12, 11, 11, 
	13, 11, 14, 11, 11, 11, 15, 11, 
	15, 1, 20, 1, 21, 21, 1, 21, 
	21, 22, 23, 1, 22, 24, 22, 1, 
	25, 1, 26, 26, 1, 27, 27, 28, 
	29, 28, 28, 28, 28, 1, 27, 27, 
	29, 1, 30, 31, 30, 32, 33, 32, 
	34, 35, 1, 36, 37, 36, 38, 35, 
	1, 39, 1, 40, 40, 1, 40, 40, 
	38, 35, 1, 41, 42, 41, 11, 12, 
	11, 11, 14, 11, 11, 11, 11, 15, 
	11, 15, 1, 43, 1, 44, 44, 1, 
	45, 46, 45, 11, 12, 11, 11, 14, 
	11, 11, 11, 11, 15, 11, 15, 1, 
	47, 1, 48, 48, 1, 48, 48, 23, 
	1, 49, 50, 49, 51, 51, 51, 51, 
	51, 51, 51, 51, 51, 1, 52, 53, 
	52, 51, 51, 51, 54, 51, 51, 51, 
	51, 51, 51, 1, 55, 56, 55, 23, 
	1, 57, 1, 49, 49, 1, 58, 59, 
	60, 61, 58, 58, 1, 62, 1, 58, 
	58, 1, 52, 53, 52, 54, 1, 58, 
	58, 58, 1, 63, 50, 63, 51, 51, 
	51, 51, 64, 65, 51, 64, 64, 64, 
	51, 64, 1, 66, 53, 66, 51, 51, 
	51, 65, 54, 51, 51, 51, 51, 51, 
	51, 1, 67, 56, 67, 65, 23, 1, 
	68, 69, 70, 69, 71, 72, 68, 35, 
	73, 35, 74, 74, 74, 75, 75, 74, 
	74, 74, 74, 74, 74, 1, 76, 1, 
	77, 77, 1, 77, 77, 74, 74, 74, 
	75, 75, 74, 74, 74, 74, 74, 74, 
	1, 78, 79, 78, 80, 80, 80, 81, 
	82, 83, 80, 80, 80, 80, 80, 1, 
	84, 85, 84, 38, 35, 83, 1, 86, 
	1, 87, 87, 1, 87, 87, 38, 35, 
	83, 1, 83, 88, 83, 89, 90, 89, 
	89, 91, 89, 89, 89, 89, 89, 89, 
	1, 92, 1, 93, 93, 1, 93, 94, 
	93, 89, 90, 89, 89, 91, 89, 89, 
	89, 89, 89, 89, 1, 95, 1, 96, 
	96, 1, 96, 96, 90, 1, 90, 97, 
	98, 99, 90, 90, 1, 100, 1, 90, 
	90, 1, 101, 79, 101, 81, 82, 1, 
	90, 90, 90, 1, 101, 79, 101, 89, 
	89, 89, 81, 82, 89, 89, 89, 89, 
	89, 1, 103, 102, 102, 102, 1, 105, 
	98, 104, 104, 104, 1, 105, 98, 106, 
	106, 106, 1, 105, 98, 107, 107, 107, 
	1, 105, 98, 1, 109, 108, 102, 102, 
	1, 110, 105, 98, 111, 104, 104, 1, 
	112, 1, 113, 114, 1, 115, 1, 116, 
	117, 1, 118, 1, 98, 119, 1, 98, 
	120, 1, 98, 1, 116, 121, 1, 116, 
	1, 113, 122, 1, 113, 1, 110, 105, 
	98, 123, 106, 106, 1, 110, 105, 98, 
	107, 107, 107, 1, 125, 98, 124, 124, 
	124, 1, 127, 98, 126, 126, 126, 1, 
	127, 98, 128, 128, 128, 1, 127, 98, 
	129, 129, 129, 1, 127, 98, 1, 130, 
	124, 124, 1, 110, 127, 98, 131, 126, 
	126, 1, 110, 127, 98, 132, 128, 128, 
	1, 110, 127, 98, 129, 129, 129, 1, 
	133, 1, 110, 134, 1, 110, 135, 1, 
	110, 1, 109, 1, 78, 79, 78, 80, 
	80, 80, 81, 82, 83, 136, 136, 80, 
	80, 80, 80, 80, 1, 78, 79, 78, 
	80, 80, 80, 81, 82, 83, 137, 137, 
	80, 80, 80, 80, 80, 1, 78, 79, 
	78, 80, 80, 80, 81, 82, 83, 138, 
	138, 80, 80, 80, 80, 80, 1, 78, 
	79, 78, 80, 80, 80, 81, 82, 83, 
	139, 139, 80, 80, 80, 80, 80, 1, 
	78, 79, 78, 80, 80, 80, 81, 82, 
	83, 140, 140, 80, 80, 80, 80, 80, 
	1, 78, 79, 78, 80, 80, 80, 81, 
	82, 83, 141, 141, 80, 80, 80, 80, 
	80, 1, 142, 79, 142, 80, 80, 80, 
	81, 82, 143, 80, 80, 80, 80, 80, 
	1, 144, 145, 144, 38, 35, 143, 1, 
	146, 1, 147, 147, 1, 147, 147, 38, 
	35, 143, 1, 143, 148, 143, 89, 90, 
	89, 89, 91, 89, 89, 89, 149, 89, 
	89, 1, 150, 1, 151, 151, 1, 151, 
	94, 151, 89, 90, 89, 89, 91, 89, 
	89, 89, 149, 89, 89, 1, 152, 153, 
	152, 154, 156, 155, 1, 157, 24, 157, 
	51, 51, 51, 51, 51, 51, 51, 51, 
	51, 1, 158, 24, 158, 51, 51, 51, 
	54, 51, 51, 51, 51, 51, 51, 1, 
	159, 24, 159, 23, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Contact_trans_targs =  new sbyte [] {
	2, 0, 8, 3, 4, 5, 6, 7, 
	9, 9, 10, 35, 40, 123, 18, 45, 
	11, 12, 12, 13, 14, 15, 16, 18, 
	17, 126, 19, 20, 19, 21, 22, 23, 
	24, 17, 28, 50, 24, 25, 28, 26, 
	27, 28, 29, 30, 31, 31, 32, 33, 
	34, 36, 38, 35, 37, 32, 18, 37, 
	32, 39, 40, 41, 43, 44, 42, 46, 
	45, 48, 47, 47, 49, 24, 17, 28, 
	50, 51, 54, 107, 52, 53, 55, 17, 
	54, 28, 50, 59, 55, 56, 57, 58, 
	60, 71, 66, 72, 61, 62, 63, 64, 
	65, 67, 69, 70, 68, 24, 73, 106, 
	74, 77, 75, 76, 78, 93, 79, 91, 
	80, 81, 89, 82, 83, 87, 84, 85, 
	86, 88, 90, 92, 94, 102, 95, 98, 
	96, 97, 99, 100, 101, 103, 104, 105, 
	108, 109, 110, 111, 112, 113, 114, 118, 
	114, 115, 116, 117, 119, 122, 120, 121, 
	24, 17, 28, 122, 50, 124, 125, 125
};

static readonly sbyte[] _tsip_machine_parser_header_Contact_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 3, 3, 17, 17, 17, 3, 17, 
	0, 0, 3, 3, 0, 0, 0, 0, 
	0, 15, 1, 0, 0, 0, 0, 7, 
	13, 13, 13, 0, 0, 0, 0, 0, 
	0, 3, 3, 0, 0, 3, 3, 0, 
	0, 0, 0, 0, 5, 5, 5, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 5, 0, 0, 20, 20, 20, 
	7, 0, 1, 1, 0, 0, 26, 26, 
	0, 26, 11, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 26, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 26, 0, 
	0, 0, 0, 0, 0, 1, 0, 0, 
	23, 23, 23, 0, 9, 0, 5, 0
};

const int tsip_machine_parser_header_Contact_start = 1;
const int tsip_machine_parser_header_Contact_first_final = 126;
const int tsip_machine_parser_header_Contact_error = 0;

const int tsip_machine_parser_header_Contact_en_main = 1;


/* #line 155 "./ragel/tsip_parser_header_Contact.rl" */

		public static List<TSIP_HeaderContact> Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			List<TSIP_HeaderContact> hdr_contacts = new List<TSIP_HeaderContact>();
			TSIP_HeaderContact curr_contact = null;

			int tag_start = 0;

			
			
/* #line 453 "../Headers/TSIP_HeaderContact.cs" */
	{
	cs = tsip_machine_parser_header_Contact_start;
	}

/* #line 169 "./ragel/tsip_parser_header_Contact.rl" */
			
/* #line 460 "../Headers/TSIP_HeaderContact.cs" */
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
	_keys = _tsip_machine_parser_header_Contact_key_offsets[cs];
	_trans = (short)_tsip_machine_parser_header_Contact_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Contact_single_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Contact_trans_keys[_mid] )
				_upper = (short) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Contact_trans_keys[_mid] )
				_lower = (short) (_mid + 1);
			else {
				_trans += (short) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (short) _klen;
		_trans += (short) _klen;
	}

	_klen = _tsip_machine_parser_header_Contact_range_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Contact_trans_keys[_mid] )
				_upper = (short) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Contact_trans_keys[_mid+1] )
				_lower = (short) (_mid + 2);
			else {
				_trans += (short)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (short) _klen;
	}

_match:
	_trans = (short)_tsip_machine_parser_header_Contact_indicies[_trans];
	cs = _tsip_machine_parser_header_Contact_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Contact_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Contact_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Contact_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Contact_actions[_acts++] )
		{
	case 0:
/* #line 31 "./ragel/tsip_parser_header_Contact.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 35 "./ragel/tsip_parser_header_Contact.rl" */
	{
		if(curr_contact == null){
			curr_contact = new TSIP_HeaderContact();
		}
	}
	break;
	case 2:
/* #line 41 "./ragel/tsip_parser_header_Contact.rl" */
	{
		if(curr_contact != null){
			curr_contact.DisplayName = TSK_RagelState.Parser.GetString(data, p, tag_start);
			curr_contact.DisplayName = TSK_String.UnQuote(curr_contact.DisplayName);
		}
	}
	break;
	case 3:
/* #line 48 "./ragel/tsip_parser_header_Contact.rl" */
	{
		if(curr_contact != null && curr_contact.Uri == null){
			int len = (int)(p  - tag_start);
            if ((curr_contact.Uri = TSIP_ParserUri.Parse(data.Substring(tag_start, len))) != null && !String.IsNullOrEmpty(curr_contact.DisplayName))
            {
                curr_contact.Uri.DisplayName = curr_contact.DisplayName;
			}
		}
	}
	break;
	case 4:
/* #line 58 "./ragel/tsip_parser_header_Contact.rl" */
	{
		if(curr_contact != null){
			curr_contact.Expires = TSK_RagelState.Parser.GetInt64(data, p, tag_start);
		}
	}
	break;
	case 5:
/* #line 64 "./ragel/tsip_parser_header_Contact.rl" */
	{
		if(curr_contact != null){
			curr_contact.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, curr_contact.Params);
		}
	}
	break;
	case 6:
/* #line 70 "./ragel/tsip_parser_header_Contact.rl" */
	{
		if(curr_contact != null){
			hdr_contacts.Add(curr_contact);
            curr_contact = null;
		}
	}
	break;
	case 7:
/* #line 77 "./ragel/tsip_parser_header_Contact.rl" */
	{
	}
	break;
/* #line 599 "../Headers/TSIP_HeaderContact.cs" */
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

/* #line 170 "./ragel/tsip_parser_header_Contact.rl" */
			
			if( cs < 
/* #line 616 "../Headers/TSIP_HeaderContact.cs" */
126
/* #line 171 "./ragel/tsip_parser_header_Contact.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Contact' header.");
				hdr_contacts.Clear();
				hdr_contacts = null;
				curr_contact.Dispose();
				curr_contact = null;
			}
			
			return hdr_contacts;
		}
	}
}