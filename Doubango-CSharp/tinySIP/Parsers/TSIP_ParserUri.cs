
/* #line 1 "./ragel/tsip_parser_uri.rl" */
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

using System;
using Doubango.tinySIP;
using Doubango.tinySAK;

/***********************************
*	Ragel state machine.
*/

/* #line 119 "./ragel/tsip_parser_uri.rl" */


public static class TSIP_ParserUri
{
	
/* #line 39 "../Parsers/TSIP_ParserUri.cs" */
static readonly sbyte[] _tsip_machine_parser_uri_actions =  new sbyte [] {
	0, 1, 0, 1, 5, 1, 7, 1, 
	9, 1, 11, 1, 12, 1, 13, 1, 
	14, 1, 17, 1, 18, 1, 20, 1, 
	21, 1, 22, 1, 23, 2, 1, 15, 
	2, 2, 15, 2, 4, 6, 2, 7, 
	10, 2, 7, 16, 2, 8, 10, 2, 
	9, 16, 2, 9, 19, 2, 13, 0, 
	2, 13, 6, 3, 0, 8, 10, 3, 
	13, 0, 6, 3, 13, 3, 0
};

static readonly short[] _tsip_machine_parser_uri_key_offsets =  new short [] {
	0, 0, 7, 15, 22, 28, 34, 40, 
	53, 66, 72, 78, 78, 91, 97, 103, 
	116, 122, 128, 141, 154, 160, 166, 180, 
	194, 200, 206, 219, 219, 227, 234, 242, 
	248, 256, 262, 268, 276, 282, 290, 296, 
	304, 312, 320, 328, 336, 344, 352, 360, 
	368, 370, 372, 385, 400, 414, 420, 426, 
	436, 446, 457, 457, 466, 466, 476, 486, 
	495, 496, 511, 525, 532, 540, 548, 556, 
	558, 565, 574, 576, 579, 581, 584, 586, 
	589, 592, 593, 596, 597, 600, 601, 610, 
	619, 627, 635, 643, 651, 653, 659, 668, 
	677, 686, 688, 691, 694, 695, 696
};

static readonly char[] _tsip_machine_parser_uri_trans_keys =  new char [] {
	'\u002d', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u002d', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u0021', '\u0025', '\u005d', '\u005f', '\u007e', '\u0024', '\u002b', '\u002d', 
	'\u003a', '\u0041', '\u005b', '\u0061', '\u007a', '\u0021', '\u0025', '\u005d', 
	'\u005f', '\u007e', '\u0024', '\u002b', '\u002d', '\u003a', '\u0041', '\u005b', 
	'\u0061', '\u007a', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u0021', '\u0025', 
	'\u005d', '\u005f', '\u007e', '\u0024', '\u002b', '\u002d', '\u003a', '\u0041', 
	'\u005b', '\u0061', '\u007a', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', 
	'\u0066', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u0021', 
	'\u0025', '\u005d', '\u005f', '\u007e', '\u0024', '\u002b', '\u002d', '\u003a', 
	'\u0041', '\u005b', '\u0061', '\u007a', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u0021', '\u0025', '\u003b', '\u003d', '\u003f', '\u005f', '\u007e', '\u0024', 
	'\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u0021', '\u0025', '\u003a', 
	'\u003d', '\u0040', '\u005f', '\u007e', '\u0024', '\u003b', '\u003f', '\u005a', 
	'\u0061', '\u007a', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u0021', '\u0025', 
	'\u003d', '\u0040', '\u005f', '\u007e', '\u0024', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u0021', '\u0025', '\u003d', '\u0040', 
	'\u005f', '\u007e', '\u0024', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', '\u003b', 
	'\u0053', '\u0054', '\u005b', '\u0073', '\u0074', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u0061', '\u007a', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u0061', '\u007a', '\u002d', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u0030', '\u0039', '\u0030', '\u0039', '\u0021', '\u0025', '\u005d', '\u005f', 
	'\u007e', '\u0024', '\u002b', '\u002d', '\u003a', '\u0041', '\u005b', '\u0061', 
	'\u007a', '\u0021', '\u0025', '\u003b', '\u003d', '\u005d', '\u005f', '\u007e', 
	'\u0024', '\u002b', '\u002d', '\u003a', '\u0041', '\u005b', '\u0061', '\u007a', 
	'\u0021', '\u0025', '\u003b', '\u005d', '\u005f', '\u007e', '\u0024', '\u002b', 
	'\u002d', '\u003a', '\u0041', '\u005b', '\u0061', '\u007a', '\u0030', '\u0039', 
	'\u0041', '\u0046', '\u0061', '\u0066', '\u0030', '\u0039', '\u0041', '\u0046', 
	'\u0061', '\u0066', '\u002d', '\u002e', '\u0049', '\u0069', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u002d', '\u002e', '\u0050', '\u0070', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u002d', '\u002e', 
	'\u003a', '\u0053', '\u0073', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', 
	'\u007a', '\u002d', '\u002e', '\u003a', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u002d', '\u002e', '\u0045', '\u0065', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u002d', '\u002e', '\u004c', '\u006c', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u002d', '\u002e', 
	'\u003a', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u003b', 
	'\u0021', '\u0025', '\u003b', '\u003d', '\u005d', '\u005f', '\u007e', '\u0024', 
	'\u002b', '\u002d', '\u003a', '\u0041', '\u005b', '\u0061', '\u007a', '\u0021', 
	'\u0025', '\u003b', '\u005d', '\u005f', '\u007e', '\u0024', '\u002b', '\u002d', 
	'\u003a', '\u0041', '\u005b', '\u0061', '\u007a', '\u003a', '\u0030', '\u0039', 
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
	(char) 0
};

static readonly sbyte[] _tsip_machine_parser_uri_single_lengths =  new sbyte [] {
	0, 1, 2, 1, 0, 0, 0, 5, 
	5, 0, 0, 0, 5, 0, 0, 5, 
	0, 0, 7, 7, 0, 0, 6, 6, 
	0, 0, 7, 0, 2, 1, 2, 0, 
	2, 0, 0, 2, 0, 2, 0, 2, 
	2, 2, 2, 2, 2, 2, 2, 2, 
	0, 0, 5, 7, 6, 0, 0, 4, 
	4, 5, 0, 3, 0, 4, 4, 3, 
	1, 7, 6, 1, 2, 2, 2, 2, 
	1, 3, 0, 1, 0, 1, 0, 1, 
	1, 1, 1, 1, 1, 1, 3, 3, 
	2, 2, 2, 2, 2, 0, 3, 3, 
	3, 0, 1, 1, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_uri_range_lengths =  new sbyte [] {
	0, 3, 3, 3, 3, 3, 3, 4, 
	4, 3, 3, 0, 4, 3, 3, 4, 
	3, 3, 3, 3, 3, 3, 4, 4, 
	3, 3, 3, 0, 3, 3, 3, 3, 
	3, 3, 3, 3, 3, 3, 3, 3, 
	3, 3, 3, 3, 3, 3, 3, 3, 
	1, 1, 4, 4, 4, 3, 3, 3, 
	3, 3, 0, 3, 0, 3, 3, 3, 
	0, 4, 4, 3, 3, 3, 3, 0, 
	3, 3, 1, 1, 1, 1, 1, 1, 
	1, 0, 1, 0, 1, 0, 3, 3, 
	3, 3, 3, 3, 0, 3, 3, 3, 
	3, 1, 1, 1, 0, 0, 0
};

static readonly short[] _tsip_machine_parser_uri_index_offsets =  new short [] {
	0, 0, 5, 11, 16, 20, 24, 28, 
	38, 48, 52, 56, 57, 67, 71, 75, 
	85, 89, 93, 104, 115, 119, 123, 134, 
	145, 149, 153, 164, 165, 171, 176, 182, 
	186, 192, 196, 200, 206, 210, 216, 220, 
	226, 232, 238, 244, 250, 256, 262, 268, 
	274, 276, 278, 288, 300, 311, 315, 319, 
	327, 335, 344, 345, 352, 353, 361, 369, 
	376, 378, 390, 401, 406, 412, 418, 424, 
	427, 432, 439, 441, 444, 446, 449, 451, 
	454, 457, 459, 462, 464, 467, 469, 476, 
	483, 489, 495, 501, 507, 510, 514, 521, 
	528, 535, 537, 540, 543, 545, 547
};

static readonly sbyte[] _tsip_machine_parser_uri_indicies =  new sbyte [] {
	1, 2, 2, 2, 0, 3, 4, 5, 
	5, 5, 0, 3, 5, 5, 5, 0, 
	5, 2, 2, 0, 7, 7, 7, 6, 
	8, 8, 8, 6, 9, 10, 9, 9, 
	9, 9, 9, 9, 9, 6, 11, 12, 
	11, 11, 11, 11, 11, 11, 11, 6, 
	13, 13, 13, 6, 11, 11, 11, 6, 
	14, 16, 17, 16, 16, 16, 16, 16, 
	16, 16, 15, 18, 18, 18, 15, 19, 
	19, 19, 15, 20, 21, 20, 20, 20, 
	20, 20, 20, 20, 15, 22, 22, 22, 
	15, 20, 20, 20, 15, 23, 25, 23, 
	23, 23, 23, 23, 23, 23, 23, 24, 
	26, 27, 28, 26, 29, 26, 26, 26, 
	26, 26, 24, 30, 30, 30, 24, 26, 
	26, 26, 24, 31, 32, 31, 33, 31, 
	31, 31, 31, 31, 31, 24, 34, 35, 
	34, 36, 34, 34, 34, 34, 34, 34, 
	24, 37, 37, 37, 24, 34, 34, 34, 
	24, 40, 41, 43, 44, 45, 43, 44, 
	39, 42, 42, 38, 38, 47, 48, 49, 
	50, 50, 38, 47, 50, 50, 50, 38, 
	47, 51, 50, 50, 50, 38, 50, 2, 
	2, 38, 1, 53, 2, 2, 2, 52, 
	5, 2, 2, 52, 54, 2, 2, 38, 
	47, 55, 56, 50, 50, 38, 57, 2, 
	2, 38, 47, 58, 59, 50, 50, 38, 
	60, 2, 2, 38, 3, 4, 61, 5, 
	5, 52, 3, 4, 62, 5, 5, 52, 
	3, 4, 5, 5, 5, 52, 47, 58, 
	63, 50, 50, 38, 47, 58, 50, 50, 
	50, 38, 47, 55, 64, 50, 50, 38, 
	47, 55, 50, 50, 50, 38, 47, 48, 
	65, 50, 50, 38, 47, 48, 50, 50, 
	50, 38, 66, 38, 66, 67, 9, 68, 
	9, 9, 9, 9, 9, 9, 9, 38, 
	8, 70, 71, 72, 8, 8, 8, 8, 
	8, 8, 8, 69, 11, 12, 71, 11, 
	11, 11, 11, 11, 11, 11, 69, 73, 
	73, 73, 38, 8, 8, 8, 38, 1, 
	53, 74, 74, 2, 2, 2, 52, 1, 
	53, 75, 75, 2, 2, 2, 52, 1, 
	53, 76, 77, 77, 2, 2, 2, 52, 
	78, 1, 53, 79, 2, 2, 2, 52, 
	80, 1, 53, 81, 81, 2, 2, 2, 
	52, 1, 53, 82, 82, 2, 2, 2, 
	52, 1, 53, 83, 2, 2, 2, 52, 
	86, 85, 19, 88, 89, 90, 19, 19, 
	19, 19, 19, 19, 19, 87, 20, 21, 
	89, 20, 20, 20, 20, 20, 20, 20, 
	87, 92, 91, 91, 91, 38, 94, 95, 
	93, 93, 93, 38, 94, 95, 96, 96, 
	96, 38, 94, 95, 97, 97, 97, 38, 
	94, 95, 38, 99, 98, 91, 91, 38, 
	100, 94, 95, 101, 93, 93, 38, 102, 
	38, 103, 104, 38, 105, 38, 106, 107, 
	38, 108, 38, 95, 109, 38, 95, 110, 
	38, 95, 38, 106, 111, 38, 106, 38, 
	103, 112, 38, 103, 38, 100, 94, 95, 
	113, 96, 96, 38, 100, 94, 95, 97, 
	97, 97, 38, 115, 95, 114, 114, 114, 
	38, 117, 95, 116, 116, 116, 38, 117, 
	95, 118, 118, 118, 38, 117, 95, 119, 
	119, 119, 38, 117, 95, 38, 120, 114, 
	114, 38, 100, 117, 95, 121, 116, 116, 
	38, 100, 117, 95, 122, 118, 118, 38, 
	100, 117, 95, 119, 119, 119, 38, 123, 
	38, 100, 124, 38, 100, 125, 38, 100, 
	38, 99, 38, 24, 0
};

static readonly sbyte[] _tsip_machine_parser_uri_trans_targs =  new sbyte [] {
	26, 1, 32, 3, 4, 2, 26, 6, 
	51, 51, 5, 52, 9, 10, 64, 26, 
	65, 13, 14, 65, 66, 16, 17, 19, 
	0, 20, 19, 20, 22, 102, 21, 23, 
	24, 102, 23, 24, 102, 25, 27, 28, 
	48, 50, 32, 55, 61, 67, 26, 29, 
	34, 46, 30, 31, 26, 33, 35, 36, 
	44, 37, 38, 42, 39, 40, 41, 43, 
	45, 47, 49, 26, 53, 26, 5, 7, 
	8, 54, 56, 57, 58, 59, 26, 60, 
	26, 62, 63, 11, 26, 64, 12, 26, 
	13, 12, 15, 68, 101, 69, 72, 26, 
	70, 71, 73, 88, 74, 86, 75, 76, 
	84, 77, 78, 82, 79, 80, 81, 83, 
	85, 87, 89, 97, 90, 93, 91, 92, 
	94, 95, 96, 98, 99, 100
};

static readonly sbyte[] _tsip_machine_parser_uri_trans_actions =  new sbyte [] {
	25, 0, 13, 0, 0, 0, 27, 0, 
	13, 53, 1, 13, 0, 0, 67, 23, 
	53, 1, 0, 13, 13, 0, 0, 1, 
	0, 1, 0, 0, 5, 38, 0, 1, 
	1, 59, 0, 0, 44, 0, 0, 35, 
	0, 0, 56, 63, 56, 3, 21, 0, 
	0, 0, 0, 0, 17, 13, 0, 0, 
	0, 0, 0, 0, 13, 13, 13, 0, 
	0, 0, 0, 19, 1, 50, 0, 7, 
	0, 0, 13, 13, 0, 13, 29, 0, 
	32, 13, 13, 0, 41, 13, 5, 47, 
	0, 7, 0, 0, 0, 0, 0, 15, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0
};

static readonly sbyte[] _tsip_machine_parser_uri_to_state_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 9, 0, 0, 0, 0, 0, 
	0, 0, 9, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0
};

static readonly sbyte[] _tsip_machine_parser_uri_from_state_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 11, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0
};

static readonly short[] _tsip_machine_parser_uri_eof_trans =  new short [] {
	0, 1, 1, 1, 1, 7, 7, 7, 
	7, 7, 7, 1, 16, 16, 16, 16, 
	16, 16, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 47, 47, 47, 47, 47, 
	53, 53, 47, 47, 47, 47, 47, 53, 
	53, 53, 47, 47, 47, 47, 47, 47, 
	47, 68, 47, 70, 70, 47, 47, 53, 
	53, 53, 79, 53, 81, 53, 53, 53, 
	85, 88, 88, 47, 47, 47, 47, 47, 
	47, 47, 47, 47, 47, 47, 47, 47, 
	47, 47, 47, 47, 47, 47, 47, 47, 
	47, 47, 47, 47, 47, 47, 47, 47, 
	47, 47, 47, 47, 47, 47, 0
};

const int tsip_machine_parser_uri_start = 26;
const int tsip_machine_parser_uri_first_final = 26;
const int tsip_machine_parser_uri_error = 0;

const int tsip_machine_parser_uri_en_sip_usrinfo = 18;
const int tsip_machine_parser_uri_en_main = 26;


/* #line 124 "./ragel/tsip_parser_uri.rl" */

	public static TSIP_Uri Parse (String data)
	{
		if(String.IsNullOrEmpty(data))
		{
			return null;
		}

		int cs = 0;
		int p = 0;
		int pe = data.Length;
		int eof = pe;

		int ts = 0, te = 0;
		int act = 0;

		TSIP_Uri uri = TSIP_Uri.Create(tsip_uri_type_t.Unknown);
		
		int tag_start = 0;
		
		
/* #line 395 "../Parsers/TSIP_ParserUri.cs" */
	{
	cs = tsip_machine_parser_uri_start;
	ts = -1;
	te = -1;
	act = 0;
	}

/* #line 145 "./ragel/tsip_parser_uri.rl" */
		
/* #line 405 "../Parsers/TSIP_ParserUri.cs" */
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
	_acts = _tsip_machine_parser_uri_from_state_actions[cs];
	_nacts = _tsip_machine_parser_uri_actions[_acts++];
	while ( _nacts-- > 0 ) {
		switch ( _tsip_machine_parser_uri_actions[_acts++] ) {
	case 12:
/* #line 1 "./ragel/tsip_parser_uri.rl" */
	{ts = p;}
	break;
/* #line 426 "../Parsers/TSIP_ParserUri.cs" */
		default: break;
		}
	}

	_keys = _tsip_machine_parser_uri_key_offsets[cs];
	_trans = (short)_tsip_machine_parser_uri_index_offsets[cs];

	_klen = _tsip_machine_parser_uri_single_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_uri_trans_keys[_mid] )
				_upper = (short) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_uri_trans_keys[_mid] )
				_lower = (short) (_mid + 1);
			else {
				_trans += (short) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (short) _klen;
		_trans += (short) _klen;
	}

	_klen = _tsip_machine_parser_uri_range_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_uri_trans_keys[_mid] )
				_upper = (short) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_uri_trans_keys[_mid+1] )
				_lower = (short) (_mid + 2);
			else {
				_trans += (short)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (short) _klen;
	}

_match:
	_trans = (short)_tsip_machine_parser_uri_indicies[_trans];
_eof_trans:
	cs = _tsip_machine_parser_uri_trans_targs[_trans];

	if ( _tsip_machine_parser_uri_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_uri_trans_actions[_trans];
	_nacts = _tsip_machine_parser_uri_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_uri_actions[_acts++] )
		{
	case 0:
/* #line 36 "./ragel/tsip_parser_uri.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 41 "./ragel/tsip_parser_uri.rl" */
	{ uri.Scheme = "sip"; uri.Type = tsip_uri_type_t.Sip; }
	break;
	case 2:
/* #line 42 "./ragel/tsip_parser_uri.rl" */
	{ uri.Scheme = "sips"; uri.Type = tsip_uri_type_t.Sips; }
	break;
	case 3:
/* #line 43 "./ragel/tsip_parser_uri.rl" */
	{ uri.Scheme = "tel"; uri.Type = tsip_uri_type_t.Tel; }
	break;
	case 4:
/* #line 46 "./ragel/tsip_parser_uri.rl" */
	{ uri.HostType = tsip_host_type_t.IPv4; }
	break;
	case 5:
/* #line 47 "./ragel/tsip_parser_uri.rl" */
	{ uri.HostType = tsip_host_type_t.IPv6; }
	break;
	case 6:
/* #line 48 "./ragel/tsip_parser_uri.rl" */
	{ uri.HostType = tsip_host_type_t.Hostname; }
	break;
	case 7:
/* #line 54 "./ragel/tsip_parser_uri.rl" */
	{
		uri.UserName = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	break;
	case 8:
/* #line 58 "./ragel/tsip_parser_uri.rl" */
	{
		uri.Password = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	break;
	case 9:
/* #line 70 "./ragel/tsip_parser_uri.rl" */
	{
		TSK_Param param = TSK_RagelState.Parser.GetParam(data, p, tag_start);
        if (param != null)
        {
            uri.Params.Add(param);
        }
	}
	break;
	case 10:
/* #line 84 "./ragel/tsip_parser_uri.rl" */
	{ {cs = 26; if (true) goto _again;} }
	break;
	case 13:
/* #line 1 "./ragel/tsip_parser_uri.rl" */
	{te = p+1;}
	break;
	case 14:
/* #line 97 "./ragel/tsip_parser_uri.rl" */
	{te = p+1;{
								uri.Host = TSK_RagelState.Scanner.GetString(data, ts, te);
								if(uri.HostType == tsip_host_type_t.IPv6){
									uri.Host = TSK_String.UnQuote(uri.Host, '[', ']');
								}
							}}
	break;
	case 15:
/* #line 88 "./ragel/tsip_parser_uri.rl" */
	{te = p;p--;{
								if(TSK_String.Contains(data.Substring(te),(pe - te), "@")){
									{cs = 18; if (true) goto _again;}
								}
							}}
	break;
	case 16:
/* #line 94 "./ragel/tsip_parser_uri.rl" */
	{te = p;p--;{ }}
	break;
	case 17:
/* #line 97 "./ragel/tsip_parser_uri.rl" */
	{te = p;p--;{
								uri.Host = TSK_RagelState.Scanner.GetString(data, ts, te);
								if(uri.HostType == tsip_host_type_t.IPv6){
									uri.Host = TSK_String.UnQuote(uri.Host, '[', ']');
								}
							}}
	break;
	case 18:
/* #line 105 "./ragel/tsip_parser_uri.rl" */
	{te = p;p--;{
								ts++;
								uri.Port = (ushort)TSK_RagelState.Scanner.GetInt32(data, ts, te);
							}}
	break;
	case 19:
/* #line 110 "./ragel/tsip_parser_uri.rl" */
	{te = p;p--;{  }}
	break;
	case 20:
/* #line 111 "./ragel/tsip_parser_uri.rl" */
	{te = p;p--;{  }}
	break;
	case 21:
/* #line 94 "./ragel/tsip_parser_uri.rl" */
	{{p = ((te))-1;}{ }}
	break;
	case 22:
/* #line 97 "./ragel/tsip_parser_uri.rl" */
	{{p = ((te))-1;}{
								uri.Host = TSK_RagelState.Scanner.GetString(data, ts, te);
								if(uri.HostType == tsip_host_type_t.IPv6){
									uri.Host = TSK_String.UnQuote(uri.Host, '[', ']');
								}
							}}
	break;
	case 23:
/* #line 110 "./ragel/tsip_parser_uri.rl" */
	{{p = ((te))-1;}{  }}
	break;
/* #line 615 "../Parsers/TSIP_ParserUri.cs" */
		default: break;
		}
	}

_again:
	_acts = _tsip_machine_parser_uri_to_state_actions[cs];
	_nacts = _tsip_machine_parser_uri_actions[_acts++];
	while ( _nacts-- > 0 ) {
		switch ( _tsip_machine_parser_uri_actions[_acts++] ) {
	case 11:
/* #line 1 "./ragel/tsip_parser_uri.rl" */
	{ts = -1;}
	break;
/* #line 629 "../Parsers/TSIP_ParserUri.cs" */
		default: break;
		}
	}

	if ( cs == 0 )
		goto _out;
	if ( ++p != pe )
		goto _resume;
	_test_eof: {}
	if ( p == eof )
	{
	if ( _tsip_machine_parser_uri_eof_trans[cs] > 0 ) {
		_trans = (short) (_tsip_machine_parser_uri_eof_trans[cs] - 1);
		goto _eof_trans;
	}
	}

	_out: {}
	}

/* #line 146 "./ragel/tsip_parser_uri.rl" */
		
		if( cs < 
/* #line 653 "../Parsers/TSIP_ParserUri.cs" */
26
/* #line 147 "./ragel/tsip_parser_uri.rl" */
 ){
			TSK_Debug.Error("Failed to parse SIP/SIPS/TEL URI");
			uri.Dispose();
			return null;
		}
		
		return uri;
	}
}