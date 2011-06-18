
/* #line 1 "./ragel/tsip_parser_header_CSeq.rl" */
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

/* #line 51 "./ragel/tsip_parser_header_CSeq.rl" */


using System;
using Doubango.tinySAK;
using Doubango.tinySIP;

namespace Doubango.tinySIP.Headers
{
    public class TSIP_HeaderCSeq : TSIP_Header
	{
		private String mMethod;
		private UInt32 mCSeq;
		private TSIP_Message.tsip_request_type_t mRequestType;

		public TSIP_HeaderCSeq()
            : this(0, null)
        {
        }
		
		public TSIP_HeaderCSeq(UInt32 cseq, String method)
            : base(tsip_header_type_t.CSeq)
        {
			this.CSeq = cseq;
			this.Method = method;
        }

		public override String Value
        {
            get 
            { 
				return String.Format("{0} {1}", this.CSeq, this.Method);
            }
            set
            {
                TSK_Debug.Error("Not implemented");
            }
        }

		public String Method
        {
            get { return mMethod; }
            set 
			{ 
				if((mMethod = value) != null)
				{
					this.RequestType = TSIP_Message.GetRequestType(mMethod);
				}
			}
        }

		public UInt32 CSeq
        {
            get { return mCSeq; }
            set { mCSeq = value; }
        }

		public TSIP_Message.tsip_request_type_t RequestType
        {
            get { return mRequestType; }
            set { mRequestType = value; }
        }

		
/* #line 93 "../Headers/TSIP_HeaderCSeq.cs" */
static readonly sbyte[] _tsip_machine_parser_header_CSeq_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2, 1, 
	3
};

static readonly sbyte[] _tsip_machine_parser_header_CSeq_key_offsets =  new sbyte [] {
	0, 0, 2, 4, 6, 8, 11, 16, 
	17, 19, 23, 28, 45, 46, 48, 64, 
	79, 80
};

static readonly char[] _tsip_machine_parser_header_CSeq_trans_keys =  new char [] {
	'\u0043', '\u0063', '\u0053', '\u0073', '\u0045', '\u0065', '\u0051', '\u0071', 
	'\u0009', '\u0020', '\u003a', '\u0009', '\u000d', '\u0020', '\u0030', '\u0039', 
	'\u000a', '\u0009', '\u0020', '\u0009', '\u0020', '\u0030', '\u0039', '\u0009', 
	'\u000d', '\u0020', '\u0030', '\u0039', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', '\u002e', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', 
	'\u0009', '\u0020', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', 
	'\u002d', '\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', 
	'\u000d', '\u0021', '\u0025', '\u0027', '\u007e', '\u002a', '\u002b', '\u002d', 
	'\u002e', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u000a', 
	(char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_CSeq_single_lengths =  new sbyte [] {
	0, 2, 2, 2, 2, 3, 3, 1, 
	2, 2, 3, 7, 1, 2, 6, 5, 
	1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_CSeq_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 1, 0, 
	0, 1, 1, 5, 0, 0, 5, 5, 
	0, 0
};

static readonly sbyte[] _tsip_machine_parser_header_CSeq_index_offsets =  new sbyte [] {
	0, 0, 3, 6, 9, 12, 16, 21, 
	23, 26, 30, 35, 48, 50, 53, 65, 
	76, 78
};

static readonly sbyte[] _tsip_machine_parser_header_CSeq_indicies =  new sbyte [] {
	0, 0, 1, 2, 2, 1, 3, 3, 
	1, 4, 4, 1, 4, 4, 5, 1, 
	5, 6, 5, 7, 1, 8, 1, 9, 
	9, 1, 9, 9, 7, 1, 10, 11, 
	10, 12, 1, 13, 14, 13, 15, 15, 
	15, 15, 15, 15, 15, 15, 15, 1, 
	16, 1, 17, 17, 1, 17, 17, 15, 
	15, 15, 15, 15, 15, 15, 15, 15, 
	1, 18, 19, 19, 19, 19, 19, 19, 
	19, 19, 19, 1, 20, 1, 1, 0
};

static readonly sbyte[] _tsip_machine_parser_header_CSeq_trans_targs =  new sbyte [] {
	2, 0, 3, 4, 5, 6, 7, 10, 
	8, 9, 11, 12, 10, 11, 12, 15, 
	13, 14, 16, 15, 17
};

static readonly sbyte[] _tsip_machine_parser_header_CSeq_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 1, 
	0, 0, 5, 5, 0, 0, 0, 1, 
	0, 0, 3, 0, 7
};

const int tsip_machine_parser_header_CSeq_start = 1;
const int tsip_machine_parser_header_CSeq_first_final = 17;
const int tsip_machine_parser_header_CSeq_error = 0;

const int tsip_machine_parser_header_CSeq_en_main = 1;


/* #line 114 "./ragel/tsip_parser_header_CSeq.rl" */

		public static TSIP_HeaderCSeq Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderCSeq hdr_cseq = new TSIP_HeaderCSeq();
			
			int tag_start = 0;

			
			
/* #line 183 "../Headers/TSIP_HeaderCSeq.cs" */
	{
	cs = tsip_machine_parser_header_CSeq_start;
	}

/* #line 127 "./ragel/tsip_parser_header_CSeq.rl" */
			
/* #line 190 "../Headers/TSIP_HeaderCSeq.cs" */
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
	_keys = _tsip_machine_parser_header_CSeq_key_offsets[cs];
	_trans = (sbyte)_tsip_machine_parser_header_CSeq_index_offsets[cs];

	_klen = _tsip_machine_parser_header_CSeq_single_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_CSeq_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_CSeq_trans_keys[_mid] )
				_lower = (sbyte) (_mid + 1);
			else {
				_trans += (sbyte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (sbyte) _klen;
		_trans += (sbyte) _klen;
	}

	_klen = _tsip_machine_parser_header_CSeq_range_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_CSeq_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_CSeq_trans_keys[_mid+1] )
				_lower = (sbyte) (_mid + 2);
			else {
				_trans += (sbyte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (sbyte) _klen;
	}

_match:
	_trans = (sbyte)_tsip_machine_parser_header_CSeq_indicies[_trans];
	cs = _tsip_machine_parser_header_CSeq_trans_targs[_trans];

	if ( _tsip_machine_parser_header_CSeq_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_CSeq_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_CSeq_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_CSeq_actions[_acts++] )
		{
	case 0:
/* #line 31 "./ragel/tsip_parser_header_CSeq.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 35 "./ragel/tsip_parser_header_CSeq.rl" */
	{
		hdr_cseq.Method = TSK_RagelState.Parser.GetString(data, p, tag_start);
	}
	break;
	case 2:
/* #line 39 "./ragel/tsip_parser_header_CSeq.rl" */
	{
		hdr_cseq.CSeq = TSK_RagelState.Parser.GetUInt32(data, p, tag_start);
	}
	break;
	case 3:
/* #line 43 "./ragel/tsip_parser_header_CSeq.rl" */
	{
	}
	break;
/* #line 287 "../Headers/TSIP_HeaderCSeq.cs" */
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

/* #line 128 "./ragel/tsip_parser_header_CSeq.rl" */
			
			if( cs < 
/* #line 304 "../Headers/TSIP_HeaderCSeq.cs" */
17
/* #line 129 "./ragel/tsip_parser_header_CSeq.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'CSeq' header.");
				hdr_cseq.Dispose();
				hdr_cseq = null;
			}
			
			return hdr_cseq;
		}
	}
}