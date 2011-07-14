
/* #line 1 "./ragel/tsip_parser_header_Subscription_State.rl" */
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

/* #line 64 "./ragel/tsip_parser_header_Subscription_State.rl" */


using System;
using Doubango.tinySAK;
using System.Collections.Generic;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderSubscriptionState : TSIP_Header
	{
		private String mState;
		private String mReason;
		private Int32 mExpires;
		private Int32 mRetryAfter;

		public TSIP_HeaderSubscriptionState()
            : base(tsip_header_type_t.Subscription_State)
        {
            mExpires = -1;
			mRetryAfter = -1;
        }


		public String State
        {
            get { return mState; }
            set{ mState = value; }
        }

		public String Reason
        {
            get { return mReason; }
            set{ mReason = value; }
        }

		public Int32 Expires
        {
            get { return mExpires; }
            set{ mExpires = value; }
        }

		public Int32 RetryAfter 
        {
            get { return mRetryAfter; }
            set{ mRetryAfter = value; }
        }

		public override String Value
        {
            get 
            {                 
                String ret = String.Format("{0}{1}{1}", 
			        this.State,
        			
                    !String.IsNullOrEmpty(this.Reason) ? ";reason=" : String.Empty,
			        !String.IsNullOrEmpty(this.Reason) ? this.Reason : String.Empty				
			        );

		        if(this.Expires >= 0)
                {
                    ret+= String.Format(";expires={0}", this.Expires);
		        }
		        if(this.RetryAfter >= 0)
                {
                    ret+= String.Format(";retry-after={0}", this.RetryAfter);
		        }
                return ret; 
            }
            set
            {
                TSK_Debug.Error("Not implemented");
            }
        }

		
/* #line 105 "../Headers/TSIP_HeaderSubscriptionState.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Subscription_State_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2, 1, 
	3, 1, 4, 1, 5, 1, 6
};

static readonly short[] _tsip_machine_parser_header_Subscription_State_key_offsets =  new short [] {
	0, 0, 2, 4, 6, 8, 10, 12, 
	14, 16, 18, 20, 22, 24, 25, 27, 
	29, 31, 33, 35, 38, 55, 56, 58, 
	74, 92, 96, 97, 99, 102, 123, 124, 
	126, 146, 165, 170, 171, 173, 177, 196, 
	197, 199, 218, 219, 221, 224, 232, 233, 
	235, 239, 240, 246, 264, 271, 279, 287, 
	295, 297, 304, 313, 315, 318, 320, 323, 
	325, 328, 331, 332, 335, 336, 339, 340, 
	349, 358, 366, 374, 382, 390, 392, 398, 
	407, 416, 425, 427, 430, 433, 434, 435, 
	456, 477, 498, 519, 540, 561, 580, 585, 
	586, 588, 592, 611, 612, 614, 633, 639, 
	660, 683, 704, 725, 746, 765, 770, 771, 
	773, 777, 796, 797, 799, 818, 836, 857, 
	878, 897, 918, 939, 960, 981, 1002, 1021, 
	1026, 1027, 1029, 1033, 1052, 1053, 1055, 1074, 
	1080
};

static readonly char[] _tsip_machine_parser_header_Subscription_State_trans_keys =  new char [] {
	'\u0053', '\u0073', '\u0055', '\u0075', '\u0042', '\u0062', '\u0053', '\u0073', 
	'\u0043', '\u0063', '\u0052', '\u0072', '\u0049', '\u0069', '\u0050', '\u0070', 
	'\u0054', '\u0074', '\u0049', '\u0069', '\u004f', '\u006f', '\u004e', '\u006e', 
	'\u002d', '\u0053', '\u0073', '\u0054', '\u0074', '\u0041', '\u0061', '\u0054', 
	'\u0074', '\u0045', '\u0065', '\u0009', '\u0020', '\u003a', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', 
	'\u0009', '\u0020', '\u0009', '\u0020', '\u0021', '\u0025', '\u0027', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', 
	'\u003b', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u003b', 
	'\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u003b', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u0045', '\u0052', '\u0065', '\u0072', 
	'\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u0045', '\u0052', '\u0065', '\u0072', '\u007e', 
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
	'\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u0022', 
	'\u0009', '\u000d', '\u0022', '\u005c', '\u0020', '\u007e', '\u0080', '\u00ff', 
	'\u000a', '\u0009', '\u0020', '\u0009', '\u000d', '\u0020', '\u003b', '\u000a', 
	'\u0000', '\u0009', '\u000b', '\u000c', '\u000e', '\u007f', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u003b', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u003a', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', 
	'\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', 
	'\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', 
	'\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', 
	'\u005d', '\u003a', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u002e', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', 
	'\u0066', '\u0030', '\u0039', '\u002e', '\u0030', '\u0039', '\u0030', '\u0039', 
	'\u002e', '\u0030', '\u0039', '\u0030', '\u0039', '\u005d', '\u0030', '\u0039', 
	'\u005d', '\u0030', '\u0039', '\u005d', '\u002e', '\u0030', '\u0039', '\u002e', 
	'\u002e', '\u0030', '\u0039', '\u002e', '\u002e', '\u003a', '\u005d', '\u0030', 
	'\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u002e', '\u003a', '\u005d', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', '\u005d', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', '\u005d', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', '\u005d', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', '\u005d', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u003a', '\u005d', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u002e', '\u003a', 
	'\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u002e', 
	'\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', 
	'\u002e', '\u003a', '\u005d', '\u0030', '\u0039', '\u0041', '\u0046', '\u0061', 
	'\u0066', '\u0030', '\u0039', '\u002e', '\u0030', '\u0039', '\u002e', '\u0030', 
	'\u0039', '\u002e', '\u003a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u003b', '\u003d', '\u0058', '\u0078', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003b', '\u003d', 
	'\u0050', '\u0070', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u003b', '\u003d', '\u0049', '\u0069', '\u007e', 
	'\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', 
	'\u003b', '\u003d', '\u0052', '\u0072', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003b', '\u003d', '\u0045', 
	'\u0065', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u003b', '\u003d', '\u0053', '\u0073', '\u007e', '\u002a', 
	'\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003b', 
	'\u003d', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u003b', 
	'\u003d', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u003b', '\u003d', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0022', '\u0025', '\u0027', '\u005b', 
	'\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0022', '\u0025', '\u0027', '\u005b', '\u007e', '\u002a', 
	'\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u0009', '\u000d', '\u0020', '\u003b', '\u0030', '\u0039', '\u0009', 
	'\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003b', '\u003d', '\u0045', 
	'\u0065', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u003b', '\u003d', '\u0041', '\u0054', '\u0061', '\u0074', 
	'\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0042', 
	'\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u003b', '\u003d', '\u0053', '\u0073', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003b', '\u003d', 
	'\u004f', '\u006f', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u003b', '\u003d', '\u004e', '\u006e', '\u007e', 
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
	'\u0025', '\u0027', '\u003b', '\u003d', '\u0052', '\u0072', '\u007e', '\u002a', 
	'\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003b', 
	'\u003d', '\u0059', '\u0079', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u002d', '\u002e', '\u003b', '\u003d', 
	'\u007e', '\u002a', '\u002b', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', 
	'\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003b', 
	'\u003d', '\u0041', '\u0061', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', 
	'\u0030', '\u0039', '\u0042', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u003b', '\u003d', '\u0046', '\u0066', 
	'\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u0021', '\u0025', 
	'\u0027', '\u003b', '\u003d', '\u0054', '\u0074', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u0009', '\u000d', '\u0020', '\u0021', '\u0025', '\u0027', '\u003b', '\u003d', 
	'\u0045', '\u0065', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', '\u0020', 
	'\u0021', '\u0025', '\u0027', '\u003b', '\u003d', '\u0052', '\u0072', '\u007e', 
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
	'\u005f', '\u007a', '\u0009', '\u000d', '\u0020', '\u003b', '\u0030', '\u0039', 
	(char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Subscription_State_single_lengths =  new sbyte [] {
	0, 2, 2, 2, 2, 2, 2, 2, 
	2, 2, 2, 2, 2, 1, 2, 2, 
	2, 2, 2, 3, 7, 1, 2, 6, 
	8, 4, 1, 2, 3, 11, 1, 2, 
	10, 9, 5, 1, 2, 4, 9, 1, 
	2, 9, 1, 2, 3, 4, 1, 2, 
	4, 1, 0, 8, 1, 2, 2, 2, 
	2, 1, 3, 0, 1, 0, 1, 0, 
	1, 1, 1, 1, 1, 1, 1, 3, 
	3, 2, 2, 2, 2, 2, 0, 3, 
	3, 3, 0, 1, 1, 1, 1, 11, 
	11, 11, 11, 11, 11, 9, 5, 1, 
	2, 4, 9, 1, 2, 9, 4, 11, 
	13, 11, 11, 11, 9, 5, 1, 2, 
	4, 9, 1, 2, 9, 8, 11, 11, 
	11, 11, 11, 11, 11, 11, 9, 5, 
	1, 2, 4, 9, 1, 2, 9, 4, 
	0
};

static readonly sbyte[] _tsip_machine_parser_header_Subscription_State_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 5, 0, 0, 5, 
	5, 0, 0, 0, 0, 5, 0, 0, 
	5, 5, 0, 0, 0, 0, 5, 0, 
	0, 5, 0, 0, 0, 2, 0, 0, 
	0, 0, 3, 5, 3, 3, 3, 3, 
	0, 3, 3, 1, 1, 1, 1, 1, 
	1, 1, 0, 1, 0, 1, 0, 3, 
	3, 3, 3, 3, 3, 0, 3, 3, 
	3, 3, 1, 1, 1, 0, 0, 5, 
	5, 5, 5, 5, 5, 5, 0, 0, 
	0, 0, 5, 0, 0, 5, 1, 5, 
	5, 5, 5, 5, 5, 0, 0, 0, 
	0, 5, 0, 0, 5, 5, 5, 5, 
	4, 5, 5, 5, 5, 5, 5, 0, 
	0, 0, 0, 5, 0, 0, 5, 1, 
	0
};

static readonly short[] _tsip_machine_parser_header_Subscription_State_index_offsets =  new short [] {
	0, 0, 3, 6, 9, 12, 15, 18, 
	21, 24, 27, 30, 33, 36, 38, 41, 
	44, 47, 50, 53, 57, 70, 72, 75, 
	87, 101, 106, 108, 111, 115, 132, 134, 
	137, 153, 168, 174, 176, 179, 184, 199, 
	201, 204, 219, 221, 224, 228, 235, 237, 
	240, 245, 247, 251, 265, 270, 276, 282, 
	288, 291, 296, 303, 305, 308, 310, 313, 
	315, 318, 321, 323, 326, 328, 331, 333, 
	340, 347, 353, 359, 365, 371, 374, 378, 
	385, 392, 399, 401, 404, 407, 409, 411, 
	428, 445, 462, 479, 496, 513, 528, 534, 
	536, 539, 544, 559, 561, 564, 579, 585, 
	602, 621, 638, 655, 672, 687, 693, 695, 
	698, 703, 718, 720, 723, 738, 752, 769, 
	786, 802, 819, 836, 853, 870, 887, 902, 
	908, 910, 913, 918, 933, 935, 938, 953, 
	959
};

static readonly byte[] _tsip_machine_parser_header_Subscription_State_indicies =  new byte [] {
	0, 0, 1, 2, 2, 1, 3, 3, 
	1, 4, 4, 1, 5, 5, 1, 6, 
	6, 1, 7, 7, 1, 8, 8, 1, 
	9, 9, 1, 10, 10, 1, 11, 11, 
	1, 12, 12, 1, 13, 1, 14, 14, 
	1, 15, 15, 1, 16, 16, 1, 17, 
	17, 1, 18, 18, 1, 18, 18, 19, 
	1, 19, 20, 19, 21, 21, 21, 21, 
	21, 21, 21, 21, 21, 1, 22, 1, 
	23, 23, 1, 23, 23, 21, 21, 21, 
	21, 21, 21, 21, 21, 21, 1, 24, 
	25, 24, 26, 26, 26, 27, 26, 26, 
	26, 26, 26, 26, 1, 28, 29, 28, 
	30, 1, 31, 1, 32, 32, 1, 32, 
	32, 30, 1, 30, 33, 30, 34, 34, 
	34, 35, 36, 35, 36, 34, 34, 34, 
	34, 34, 34, 1, 37, 1, 38, 38, 
	1, 38, 38, 34, 34, 34, 35, 36, 
	35, 36, 34, 34, 34, 34, 34, 34, 
	1, 39, 40, 39, 41, 41, 41, 42, 
	43, 41, 41, 41, 41, 41, 41, 1, 
	44, 45, 44, 30, 43, 1, 46, 1, 
	47, 47, 1, 47, 47, 30, 43, 1, 
	43, 48, 43, 49, 50, 49, 49, 51, 
	49, 49, 49, 49, 49, 49, 1, 52, 
	1, 53, 53, 1, 53, 54, 53, 49, 
	50, 49, 49, 51, 49, 49, 49, 49, 
	49, 49, 1, 55, 1, 56, 56, 1, 
	56, 56, 50, 1, 50, 57, 58, 59, 
	50, 50, 1, 60, 1, 50, 50, 1, 
	61, 40, 61, 42, 1, 62, 1, 50, 
	50, 50, 1, 61, 40, 61, 49, 49, 
	49, 42, 49, 49, 49, 49, 49, 49, 
	1, 64, 63, 63, 63, 1, 66, 58, 
	65, 65, 65, 1, 66, 58, 67, 67, 
	67, 1, 66, 58, 68, 68, 68, 1, 
	66, 58, 1, 70, 69, 63, 63, 1, 
	71, 66, 58, 72, 65, 65, 1, 73, 
	1, 74, 75, 1, 76, 1, 77, 78, 
	1, 79, 1, 58, 80, 1, 58, 81, 
	1, 58, 1, 77, 82, 1, 77, 1, 
	74, 83, 1, 74, 1, 71, 66, 58, 
	84, 67, 67, 1, 71, 66, 58, 68, 
	68, 68, 1, 86, 58, 85, 85, 85, 
	1, 88, 58, 87, 87, 87, 1, 88, 
	58, 89, 89, 89, 1, 88, 58, 90, 
	90, 90, 1, 88, 58, 1, 91, 85, 
	85, 1, 71, 88, 58, 92, 87, 87, 
	1, 71, 88, 58, 93, 89, 89, 1, 
	71, 88, 58, 90, 90, 90, 1, 94, 
	1, 71, 95, 1, 71, 96, 1, 71, 
	1, 70, 1, 39, 40, 39, 41, 41, 
	41, 42, 43, 97, 97, 41, 41, 41, 
	41, 41, 41, 1, 39, 40, 39, 41, 
	41, 41, 42, 43, 98, 98, 41, 41, 
	41, 41, 41, 41, 1, 39, 40, 39, 
	41, 41, 41, 42, 43, 99, 99, 41, 
	41, 41, 41, 41, 41, 1, 39, 40, 
	39, 41, 41, 41, 42, 43, 100, 100, 
	41, 41, 41, 41, 41, 41, 1, 39, 
	40, 39, 41, 41, 41, 42, 43, 101, 
	101, 41, 41, 41, 41, 41, 41, 1, 
	39, 40, 39, 41, 41, 41, 42, 43, 
	102, 102, 41, 41, 41, 41, 41, 41, 
	1, 103, 40, 103, 41, 41, 41, 42, 
	104, 41, 41, 41, 41, 41, 41, 1, 
	105, 106, 105, 30, 104, 1, 107, 1, 
	108, 108, 1, 108, 108, 30, 104, 1, 
	104, 109, 104, 49, 50, 49, 49, 51, 
	49, 49, 49, 110, 49, 49, 1, 111, 
	1, 112, 112, 1, 112, 54, 112, 49, 
	50, 49, 49, 51, 49, 49, 49, 110, 
	49, 49, 1, 113, 114, 113, 116, 115, 
	1, 39, 40, 39, 41, 41, 41, 42, 
	43, 117, 117, 41, 41, 41, 41, 41, 
	41, 1, 39, 40, 39, 41, 41, 41, 
	42, 43, 118, 119, 118, 119, 41, 41, 
	41, 41, 41, 41, 1, 39, 40, 39, 
	41, 41, 41, 42, 43, 120, 120, 41, 
	41, 41, 41, 41, 41, 1, 39, 40, 
	39, 41, 41, 41, 42, 43, 121, 121, 
	41, 41, 41, 41, 41, 41, 1, 39, 
	40, 39, 41, 41, 41, 42, 43, 122, 
	122, 41, 41, 41, 41, 41, 41, 1, 
	123, 40, 123, 41, 41, 41, 42, 124, 
	41, 41, 41, 41, 41, 41, 1, 125, 
	126, 125, 30, 124, 1, 127, 1, 128, 
	128, 1, 128, 128, 30, 124, 1, 124, 
	129, 124, 130, 50, 130, 130, 51, 130, 
	130, 130, 130, 130, 130, 1, 131, 1, 
	132, 132, 1, 132, 54, 132, 130, 50, 
	130, 130, 51, 130, 130, 130, 130, 130, 
	130, 1, 133, 134, 133, 135, 135, 135, 
	136, 135, 135, 135, 135, 135, 135, 1, 
	39, 40, 39, 41, 41, 41, 42, 43, 
	137, 137, 41, 41, 41, 41, 41, 41, 
	1, 39, 40, 39, 41, 41, 41, 42, 
	43, 138, 138, 41, 41, 41, 41, 41, 
	41, 1, 39, 40, 39, 41, 41, 41, 
	139, 41, 42, 43, 41, 41, 41, 41, 
	41, 1, 39, 40, 39, 41, 41, 41, 
	42, 43, 140, 140, 41, 41, 41, 41, 
	41, 41, 1, 39, 40, 39, 41, 41, 
	41, 42, 43, 141, 141, 41, 41, 41, 
	41, 41, 41, 1, 39, 40, 39, 41, 
	41, 41, 42, 43, 142, 142, 41, 41, 
	41, 41, 41, 41, 1, 39, 40, 39, 
	41, 41, 41, 42, 43, 143, 143, 41, 
	41, 41, 41, 41, 41, 1, 39, 40, 
	39, 41, 41, 41, 42, 43, 144, 144, 
	41, 41, 41, 41, 41, 41, 1, 145, 
	40, 145, 41, 41, 41, 42, 146, 41, 
	41, 41, 41, 41, 41, 1, 147, 148, 
	147, 30, 146, 1, 149, 1, 150, 150, 
	1, 150, 150, 30, 146, 1, 146, 151, 
	146, 49, 50, 49, 49, 51, 49, 49, 
	49, 152, 49, 49, 1, 153, 1, 154, 
	154, 1, 154, 54, 154, 49, 50, 49, 
	49, 51, 49, 49, 49, 152, 49, 49, 
	1, 155, 156, 155, 158, 157, 1, 1, 
	0
};

static readonly byte[] _tsip_machine_parser_header_Subscription_State_trans_targs =  new byte [] {
	2, 0, 3, 4, 5, 6, 7, 8, 
	9, 10, 11, 12, 13, 14, 15, 16, 
	17, 18, 19, 20, 21, 24, 22, 23, 
	25, 49, 24, 29, 25, 26, 29, 27, 
	28, 30, 33, 87, 103, 31, 32, 34, 
	49, 33, 29, 38, 34, 35, 36, 37, 
	39, 51, 45, 52, 40, 41, 42, 43, 
	44, 46, 48, 50, 47, 25, 136, 53, 
	86, 54, 57, 55, 56, 58, 73, 59, 
	71, 60, 61, 69, 62, 63, 67, 64, 
	65, 66, 68, 70, 72, 74, 82, 75, 
	78, 76, 77, 79, 80, 81, 83, 84, 
	85, 88, 89, 90, 91, 92, 93, 94, 
	98, 94, 95, 96, 97, 99, 102, 100, 
	101, 25, 49, 102, 29, 104, 105, 118, 
	106, 107, 108, 109, 113, 109, 110, 111, 
	112, 114, 117, 115, 116, 25, 49, 117, 
	29, 119, 120, 121, 122, 123, 124, 125, 
	126, 127, 131, 127, 128, 129, 130, 132, 
	135, 133, 134, 25, 49, 135, 29
};

static readonly sbyte[] _tsip_machine_parser_header_Subscription_State_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 1, 0, 0, 
	3, 3, 0, 3, 0, 0, 0, 0, 
	0, 0, 1, 1, 1, 0, 0, 11, 
	11, 0, 11, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 11, 13, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 11, 
	0, 0, 0, 0, 0, 0, 1, 0, 
	0, 7, 7, 0, 7, 0, 0, 0, 
	0, 0, 0, 11, 0, 0, 0, 0, 
	0, 0, 1, 0, 0, 5, 5, 0, 
	5, 0, 0, 0, 0, 0, 0, 0, 
	0, 11, 0, 0, 0, 0, 0, 0, 
	1, 0, 0, 9, 9, 0, 9
};

const int tsip_machine_parser_header_Subscription_State_start = 1;
const int tsip_machine_parser_header_Subscription_State_first_final = 136;
const int tsip_machine_parser_header_Subscription_State_error = 0;

const int tsip_machine_parser_header_Subscription_State_en_main = 1;


/* #line 139 "./ragel/tsip_parser_header_Subscription_State.rl" */

		public static TSIP_HeaderSubscriptionState Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderSubscriptionState hdr_Subscription_State = new TSIP_HeaderSubscriptionState();
			
			int tag_start = 0;
			
			
/* #line 524 "../Headers/TSIP_HeaderSubscriptionState.cs" */
	{
	cs = tsip_machine_parser_header_Subscription_State_start;
	}

/* #line 151 "./ragel/tsip_parser_header_Subscription_State.rl" */
			
/* #line 531 "../Headers/TSIP_HeaderSubscriptionState.cs" */
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
	_keys = _tsip_machine_parser_header_Subscription_State_key_offsets[cs];
	_trans = (short)_tsip_machine_parser_header_Subscription_State_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Subscription_State_single_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Subscription_State_trans_keys[_mid] )
				_upper = (short) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Subscription_State_trans_keys[_mid] )
				_lower = (short) (_mid + 1);
			else {
				_trans += (short) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (short) _klen;
		_trans += (short) _klen;
	}

	_klen = _tsip_machine_parser_header_Subscription_State_range_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Subscription_State_trans_keys[_mid] )
				_upper = (short) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Subscription_State_trans_keys[_mid+1] )
				_lower = (short) (_mid + 2);
			else {
				_trans += (short)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (short) _klen;
	}

_match:
	_trans = (short)_tsip_machine_parser_header_Subscription_State_indicies[_trans];
	cs = _tsip_machine_parser_header_Subscription_State_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Subscription_State_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Subscription_State_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Subscription_State_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Subscription_State_actions[_acts++] )
		{
	case 0:
/* #line 31 "./ragel/tsip_parser_header_Subscription_State.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 35 "./ragel/tsip_parser_header_Subscription_State.rl" */
	{
		hdr_Subscription_State.State = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	break;
	case 2:
/* #line 39 "./ragel/tsip_parser_header_Subscription_State.rl" */
	{
		hdr_Subscription_State.Reason = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	break;
	case 3:
/* #line 43 "./ragel/tsip_parser_header_Subscription_State.rl" */
	{
		hdr_Subscription_State.Expires = TSK_RagelState.Parser.GetInt32(data, p, tag_start);
	}
	break;
	case 4:
/* #line 47 "./ragel/tsip_parser_header_Subscription_State.rl" */
	{
		hdr_Subscription_State.RetryAfter = TSK_RagelState.Parser.GetInt32(data, p, tag_start);
	}
	break;
	case 5:
/* #line 51 "./ragel/tsip_parser_header_Subscription_State.rl" */
	{
		hdr_Subscription_State.Params = TSK_RagelState.Parser.AddParam(data, p, tag_start, hdr_Subscription_State.Params);
	}
	break;
	case 6:
/* #line 55 "./ragel/tsip_parser_header_Subscription_State.rl" */
	{
	}
	break;
/* #line 646 "../Headers/TSIP_HeaderSubscriptionState.cs" */
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

/* #line 152 "./ragel/tsip_parser_header_Subscription_State.rl" */
			
			if( cs < 
/* #line 663 "../Headers/TSIP_HeaderSubscriptionState.cs" */
136
/* #line 153 "./ragel/tsip_parser_header_Subscription_State.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Subscription-State' header.");
				hdr_Subscription_State.Dispose();
				hdr_Subscription_State = null;
			}
			
			return hdr_Subscription_State;
		}
	}
}