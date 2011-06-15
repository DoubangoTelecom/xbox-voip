
/* #line 1 "./ragel/tsip_parser_header_Allow_Events.rl" */
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

/* #line 57 "./ragel/tsip_parser_header_Allow_Events.rl" */


using System;
using Doubango_CSharp.tinySAK;
using System.Collections.Generic;

namespace Doubango_CSharp.tinySIP.Headers
{
    public class TSIP_HeaderAllowEvents : TSIP_Header
	{
		private List<String> mEvents;

		public TSIP_HeaderAllowEvents()
            : this(null)
        {
        }

        public TSIP_HeaderAllowEvents(String events)
            : base(tsip_header_type_t.Allow_Events)
        {
            if (events != null)
            {
                this.Events.AddRange(events.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            }
        }

		public List<String> Events
		{
			get 
			{ 
				if(mEvents == null)
				{
					mEvents = new List<String>();
				}
				return mEvents; 
			}
		}

		public override String Value
        {
            get 
            { 
                String ret = String.Empty;
                foreach(String @event in this.Events)
                {
                    if(String.IsNullOrEmpty(ret))
                    {
                        ret = @event;
                    }
                    else
                    {
                        ret += String.Format(",{0}", @event);
                    }
                }
                return ret; 
            }
            set
            {
                TSK_Debug.Error("Not implemented");
            }
        }
		
		public Boolean IsEventAllowed(String @event)
		{
			return this.Events.Exists(
                (x) => { return x.Equals(@event, StringComparison.InvariantCultureIgnoreCase); }
            );
		}

		
/* #line 102 "../Headers/TSIP_HeaderAllowEvents.cs" */
static readonly sbyte[] _tsip_machine_parser_header_Allow_events_actions =  new sbyte [] {
	0, 1, 0, 1, 1, 1, 2
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_events_key_offsets =  new sbyte [] {
	0, 0, 4, 6, 8, 10, 12, 13, 
	15, 17, 19, 21, 23, 25, 28, 44, 
	45, 47, 62, 79, 83, 84, 86, 89, 
	90, 103
};

static readonly char[] _tsip_machine_parser_header_Allow_events_trans_keys =  new char [] {
	'\u0041', '\u0055', '\u0061', '\u0075', '\u004c', '\u006c', '\u004c', '\u006c', 
	'\u004f', '\u006f', '\u0057', '\u0077', '\u002d', '\u0045', '\u0065', '\u0056', 
	'\u0076', '\u0045', '\u0065', '\u004e', '\u006e', '\u0054', '\u0074', '\u0053', 
	'\u0073', '\u0009', '\u0020', '\u003a', '\u0009', '\u000d', '\u0020', '\u0021', 
	'\u0025', '\u0027', '\u002d', '\u007e', '\u002a', '\u002b', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u005f', '\u007a', '\u000a', '\u0009', '\u0020', '\u0009', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u002d', '\u007e', '\u002a', '\u002b', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', '\u000d', 
	'\u0020', '\u0021', '\u0025', '\u0027', '\u002c', '\u002e', '\u007e', '\u002a', 
	'\u002d', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', '\u0009', 
	'\u000d', '\u0020', '\u002c', '\u000a', '\u0009', '\u0020', '\u0009', '\u0020', 
	'\u002c', '\u000a', '\u0021', '\u0025', '\u0027', '\u002d', '\u007e', '\u002a', 
	'\u002b', '\u0030', '\u0039', '\u0041', '\u005a', '\u005f', '\u007a', (char) 0
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_events_single_lengths =  new sbyte [] {
	0, 4, 2, 2, 2, 2, 1, 2, 
	2, 2, 2, 2, 2, 3, 8, 1, 
	2, 7, 9, 4, 1, 2, 3, 1, 
	5, 0
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_events_range_lengths =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 4, 0, 
	0, 4, 4, 0, 0, 0, 0, 0, 
	4, 0
};

static readonly byte[] _tsip_machine_parser_header_Allow_events_index_offsets =  new byte [] {
	0, 0, 5, 8, 11, 14, 17, 19, 
	22, 25, 28, 31, 34, 37, 41, 54, 
	56, 59, 71, 85, 90, 92, 95, 99, 
	101, 111
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_events_indicies =  new sbyte [] {
	0, 2, 0, 2, 1, 3, 3, 1, 
	4, 4, 1, 5, 5, 1, 6, 6, 
	1, 7, 1, 8, 8, 1, 9, 9, 
	1, 10, 10, 1, 11, 11, 1, 12, 
	12, 1, 2, 2, 1, 2, 2, 13, 
	1, 13, 14, 13, 15, 15, 15, 15, 
	15, 15, 15, 15, 15, 1, 16, 1, 
	17, 17, 1, 17, 17, 15, 15, 15, 
	15, 15, 15, 15, 15, 15, 1, 18, 
	19, 18, 20, 20, 20, 21, 22, 20, 
	20, 20, 20, 20, 1, 23, 24, 23, 
	13, 1, 25, 1, 26, 26, 1, 26, 
	26, 13, 1, 27, 1, 20, 20, 20, 
	20, 20, 20, 20, 20, 20, 1, 1, 
	0
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_events_trans_targs =  new sbyte [] {
	2, 0, 13, 3, 4, 5, 6, 7, 
	8, 9, 10, 11, 12, 14, 15, 18, 
	16, 17, 19, 23, 18, 14, 24, 19, 
	20, 21, 22, 25
};

static readonly sbyte[] _tsip_machine_parser_header_Allow_events_trans_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 1, 
	0, 0, 3, 3, 0, 3, 0, 0, 
	0, 0, 0, 5
};

const int tsip_machine_parser_header_Allow_events_start = 1;
const int tsip_machine_parser_header_Allow_events_first_final = 25;
const int tsip_machine_parser_header_Allow_events_error = 0;

const int tsip_machine_parser_header_Allow_events_en_main = 1;


/* #line 127 "./ragel/tsip_parser_header_Allow_Events.rl" */

		public static TSIP_HeaderAllowEvents Parse(String data)
		{
			int cs = 0;
			int p = 0;
			int pe = data.Length;
			int eof = pe;
			TSIP_HeaderAllowEvents Allow_Events = new TSIP_HeaderAllowEvents();
			
			int tag_start = 0;

			
			
/* #line 204 "../Headers/TSIP_HeaderAllowEvents.cs" */
	{
	cs = tsip_machine_parser_header_Allow_events_start;
	}

/* #line 140 "./ragel/tsip_parser_header_Allow_Events.rl" */
			
/* #line 211 "../Headers/TSIP_HeaderAllowEvents.cs" */
	{
	sbyte _klen;
	byte _trans;
	sbyte _acts;
	sbyte _nacts;
	sbyte _keys;

	if ( p == pe )
		goto _test_eof;
	if ( cs == 0 )
		goto _out;
_resume:
	_keys = _tsip_machine_parser_header_Allow_events_key_offsets[cs];
	_trans = (byte)_tsip_machine_parser_header_Allow_events_index_offsets[cs];

	_klen = _tsip_machine_parser_header_Allow_events_single_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _tsip_machine_parser_header_Allow_events_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 1);
			else if ( data[p] > _tsip_machine_parser_header_Allow_events_trans_keys[_mid] )
				_lower = (sbyte) (_mid + 1);
			else {
				_trans += (byte) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (sbyte) _klen;
		_trans += (byte) _klen;
	}

	_klen = _tsip_machine_parser_header_Allow_events_range_lengths[cs];
	if ( _klen > 0 ) {
		sbyte _lower = _keys;
		sbyte _mid;
		sbyte _upper = (sbyte) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (sbyte) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _tsip_machine_parser_header_Allow_events_trans_keys[_mid] )
				_upper = (sbyte) (_mid - 2);
			else if ( data[p] > _tsip_machine_parser_header_Allow_events_trans_keys[_mid+1] )
				_lower = (sbyte) (_mid + 2);
			else {
				_trans += (byte)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (byte) _klen;
	}

_match:
	_trans = (byte)_tsip_machine_parser_header_Allow_events_indicies[_trans];
	cs = _tsip_machine_parser_header_Allow_events_trans_targs[_trans];

	if ( _tsip_machine_parser_header_Allow_events_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _tsip_machine_parser_header_Allow_events_trans_actions[_trans];
	_nacts = _tsip_machine_parser_header_Allow_events_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _tsip_machine_parser_header_Allow_events_actions[_acts++] )
		{
	case 0:
/* #line 33 "./ragel/tsip_parser_header_Allow_Events.rl" */
	{
		tag_start = p;
	}
	break;
	case 1:
/* #line 37 "./ragel/tsip_parser_header_Allow_Events.rl" */
	{
		String @event = TSK_RagelState.Parser.GetString(data, p, tag_start);
        if (!String.IsNullOrEmpty(@event))
        {
            Allow_Events.Events.Add(@event);
        }
	}
	break;
	case 2:
/* #line 45 "./ragel/tsip_parser_header_Allow_Events.rl" */
	{
	}
	break;
/* #line 306 "../Headers/TSIP_HeaderAllowEvents.cs" */
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

/* #line 141 "./ragel/tsip_parser_header_Allow_Events.rl" */
			
			if( cs < 
/* #line 323 "../Headers/TSIP_HeaderAllowEvents.cs" */
25
/* #line 142 "./ragel/tsip_parser_header_Allow_Events.rl" */
 ){
				TSK_Debug.Error("Failed to parse SIP 'Allow' header.");
				Allow_Events.Dispose();
				Allow_Events = null;
			}
			
			return Allow_Events;
		}
	}
}