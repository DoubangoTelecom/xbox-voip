/*Copyright (C) 2010-2011 Mamadou Diop.
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Doubango.tinySAK
{
    /// <summary>
    /// Final State machine engine
    /// </summary>
    public class TSK_StateMachine : IDisposable
    {
        public delegate Boolean OnTerminated();
        public delegate Boolean Condition(Object obj1, Object obj2);
        public delegate Boolean Execute(params Object[] parameters);

        public const Int32 STATE_ANY = -0xFFFF;
        private const Int32 STATE_DEFAULT = -0xFFF0;
        private const Int32 STATE_NONE = -0xFF00;
        private const Int32 STATE_FINAL = -0xF000;

        private const Int32 ACTION_ANY = -0xFFFF;

        private Boolean mDebug;
        private Int32 mCurrentSate;
        private readonly Int32 mTermState;
        private readonly OnTerminated mOnTerminated;
        private readonly Object mUserData;
        private readonly Mutex mMutex;
        private readonly List<Entry> mEntries;

        public TSK_StateMachine(Int32 currentState, Int32 termState, OnTerminated onTerminated, Object userData)
        {
            mCurrentSate = currentState;
            mTermState = termState;
            mOnTerminated = onTerminated;
            mUserData = userData;
            mEntries = new List<Entry>();
#if WINDOWS_PHONE
            mMutex = new Mutex(false, null); // unnamed mutex
#else
            mMutex = new Mutex();
#endif
        }

        ~TSK_StateMachine()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (mMutex != null)
            {
                mMutex.Close();
            }
        }

        public Boolean IsTerminated
        {
            get { return mCurrentSate == mTermState; }
        }

        public Boolean IsDebugEnabled
        {
            get { return mDebug; }
            set { mDebug = value; }
        }

        public Boolean ExecuteAction(Int32 action, Object obj1, Object obj2, params Object[] parameters)
        {
            Boolean found = false;
            Boolean terminates = false;
            Boolean ok = true;

            if (this.IsTerminated)
            {
                TSK_Debug.Warn("The FSM is in the final state.");
                return false;
            }

            // lock
            mMutex.WaitOne();

            foreach (Entry entry in mEntries)
            {
                if ((entry.StateFrom != TSK_StateMachine.STATE_ANY) && (entry.StateFrom != mCurrentSate))
                {
                    continue;
                }

                if ((entry.Action != TSK_StateMachine.ACTION_ANY) && (entry.Action != action))
                {
                    continue;
                }

                // check condition
                if (entry.Condition == null || entry.Condition(obj1, obj2))
                {
                    // For debug information
                    if (mDebug)
                    {
                        TSK_Debug.Info("State machine: {0}", entry.Description);
                    }

                    if (entry.StateTo != TSK_StateMachine.STATE_ANY)
                    { /* Stay at the current state if dest. state is Any */
                        mCurrentSate = entry.StateTo;
                    }

                    if (entry.Execute != null)
                    {
                        if (!(ok = entry.Execute(parameters)))
                        {
                            TSK_Debug.Info("State machine: Exec function failed. Moving to terminal state.");
                        }
                        else { }// Do nothing

                        terminates = (!ok || (mCurrentSate == mTermState));
                        found = true;
                        break;
                    }
                }
            }

            mMutex.ReleaseMutex();

            /* Only call the callback function after unlock. */
            if (terminates)
            {
                mCurrentSate = mTermState;
                if (mOnTerminated != null)
                {
                    mOnTerminated();
                }
            }

            if (!found)
            {
                TSK_Debug.Warn("State machine: No matching state found.");
            }

            return ok;
        }

        public TSK_StateMachine AddEntry(Int32 stateFrom, Int32 action, Condition condition, Int32 stateTo, Execute execute, String description)
        {
            mEntries.Add(
                new Entry(stateFrom, action, condition, stateTo, execute, description)
                );
            mEntries.Sort();
            return this;
        }

        public TSK_StateMachine AddAlwaysEntry(Int32 stateFrom, Int32 action, Int32 stateTo, Execute execute, String description)
        {
            return this.AddEntry(stateFrom, action, null, stateTo, execute, description);
        }

        public TSK_StateMachine AddNothingEntry(Int32 stateFrom, Int32 action, Condition condition, String description)
        {
            return this.AddEntry(stateFrom, action, condition, stateFrom, null, description);
        }

        public TSK_StateMachine AddAlwaysNothingEntry(Int32 stateFrom, String description)
        {
            return this.AddEntry(stateFrom, TSK_StateMachine.STATE_ANY, null, stateFrom, null, description);
        }


        /// <summary>
        /// Final State Machine entry
        /// </summary>
        class Entry : IComparable
        {
            private readonly Int32 mStateFrom;
            private readonly Int32 mAction;
            private readonly Condition mCondition;
            private readonly Int32 mStateTo;
            private readonly Execute mExecute;
            private readonly String mDescription;

            public Entry(Int32 stateFrom, Int32 action, Condition condition, Int32 stateTo, Execute execute, String description)
            {
                mStateFrom = stateFrom;
                mAction = action;
                mCondition = condition;
                mStateTo = stateTo;
                mExecute = execute;
                mDescription = description;
            }

            public int CompareTo(object obj)
            {
                if (obj != null && obj is Entry)
                {
                    Entry otherEntry = (obj as Entry);

                    /* Put "Any" states at the bottom. (Strong)*/
                    if (this.StateFrom == TSK_StateMachine.STATE_ANY)
                    {
                        return +20;
                    }
                    else if (otherEntry.StateFrom == TSK_StateMachine.STATE_ANY)
                    {
                        return -20;
                    }

                    /* Put "Any" actions at the bottom. (Weak)*/
                    if (this.Action == TSK_StateMachine.ACTION_ANY)
                    {
                        return +10;
                    }
                    else if (otherEntry.Action == TSK_StateMachine.ACTION_ANY) // FIXME: Not the same as Doubango
                    {
                        return -10;
                    }
                }
                return 0;
            }

            public Int32 StateFrom
            {
                get { return mStateFrom; }
            }

            public Int32 Action
            {
                get { return mAction; }
            }

            public Condition Condition
            {
                get { return mCondition; }
            }

            public Int32 StateTo
            {
                get { return mStateTo; }
            }

            public Execute Execute
            {
                get { return mExecute; }
            }

            public String Description
            {
                get { return mDescription; }
            }
        }
    }
}
