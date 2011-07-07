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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySIP.Dialogs;
using System.Threading;
using Doubango.tinyNET;

namespace Doubango.tinySIP.Transactions
{
    internal class TSIP_TransacLayer : IDisposable
    {
        private readonly TSIP_Stack mSipStack;
        private readonly IDictionary<Int64,TSIP_Transac> mTransactions;
        private readonly Boolean mReliable;
        private readonly Mutex mMutex;

        internal TSIP_TransacLayer(TSIP_Stack stack)
        {
            mSipStack = stack;
            mTransactions = new Dictionary<Int64,TSIP_Transac>();
            mReliable = TNET_Socket.IsStreamType(mSipStack.ProxyType);
#if WINDOWS_PHONE
            mMutex = new Mutex(true, null);
#else
            mMutex = new Mutex();
#endif
        }

        ~TSIP_TransacLayer()
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

        internal Boolean IsReliable
        {
            get { return mReliable; }
        }

        internal TSIP_Transac CreateTransac(Boolean isClient, TSIP_Message message, TSIP_Dialog dialog)
        {
            TSIP_Transac transac = null;

            mMutex.WaitOne();

            mMutex.ReleaseMutex();

            return transac;
        }

        internal Boolean RemoveTransacById(Int64 transacId)
        {
            mMutex.WaitOne();

            mTransactions.Remove(transacId);

            mMutex.ReleaseMutex();

            return true;
        }

        internal Boolean CancelTransactionByDialogId(Int64 dialogId)
        {
            mMutex.WaitOne();

            mMutex.ReleaseMutex();

            return true;
        }

        internal TSIP_Transac FindClientTransacByMsg(TSIP_Message message)
        {
            TSIP_Transac transac = null;

            mMutex.WaitOne();

            mMutex.ReleaseMutex();

            return transac;
        }

        internal TSIP_Transac FindServerTransacByMsg(TSIP_Message message)
        {
            TSIP_Transac transac = null;

            mMutex.WaitOne();

            mMutex.ReleaseMutex();

            return transac;
        }

        internal Boolean HandleIncomingMsg(TSIP_Message message)
        {
            mMutex.WaitOne();

            mMutex.ReleaseMutex();

            return false;
        }
    }
}
