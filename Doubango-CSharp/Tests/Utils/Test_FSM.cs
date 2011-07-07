using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySAK;
using __A__ = Doubango.Tests.Utils.Test_FSM.test_fsm_action_t;
using __S__ = Doubango.Tests.Utils.Test_FSM.test_fsm_state_t;

namespace Doubango.Tests.Utils
{
    internal static class Test_FSM
    {
        static Boolean unSubscribing = false;

        /* ======================== actions ======================== */
        internal enum test_fsm_action_t
        {
            Subscribe,
            R1xx,
            R2xx,
            R401_407_421_494,
            R423,
            R300_to_699,
            Cancel,
            Notify,
            Unsubscribe,
            Refresh,
            Transporterror,
            Error,
        };

        /* ======================== states ======================== */
        internal enum test_fsm_state_t
        {
            Started,
            Trying,
            Connected,
            Terminated
        };

        /* ======================== execs ======================== */
        static Boolean test_fsm_exec_Started_2_Trying_X_subscribe(params Object[] parameters)
        {
            return true;
        }

        static Boolean test_fsm_exec_Trying_2_Trying_X_1xx(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

	        return true;
        }

        static Boolean test_fsm_exec_Trying_2_Terminated_X_2xx(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Trying_2_Connected_X_2xx(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Trying_2_Trying_X_401_407_421_494(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Trying_2_Trying_X_423(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Trying_2_Terminated_X_300_to_699(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Trying_2_Terminated_X_cancel(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Trying_2_Trying_X_NOTIFY(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Connected_2_Trying_X_unsubscribe(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            unSubscribing = true;

            return true;
        }

        static Boolean test_fsm_exec_Connected_2_Trying_X_refresh(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Connected_2_Connected_X_NOTIFY(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Connected_2_Terminated_X_NOTIFY(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Any_2_Trying_X_hangup(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Any_2_Terminated_X_transportError(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        static Boolean test_fsm_exec_Any_2_Terminated_X_Error(params Object[] parameters)
        {
            Object userData = parameters[0];
            Object message = parameters[1];

            return true;
        }

        /* ======================== conds ======================== */
        static Boolean test_fsm_cond_unsubscribing(Object userData, Object obj)
        {
            return unSubscribing;
        }
        static Boolean test_fsm_cond_subscribing(Object userData, Object obj)
        {
            return !test_fsm_cond_unsubscribing(userData, obj);
        }


        static Boolean test_fsm_onterminated()
        {
	        TSK_Debug.Info("FSM in terminal state.");
	        return true;
        }

        internal static void DefaultTest()
        {


            TSK_StateMachine stateMachine = new TSK_StateMachine((Int32)__S__.Started, (Int32)__S__.Terminated, test_fsm_onterminated, null);
            
            /*=======================
			* === Started === 
			*/
			// Started -> (Send) -> Trying
            stateMachine.AddAlwaysEntry((Int32)__S__.Started, (Int32)__A__.Subscribe, (Int32)__S__.Trying, test_fsm_exec_Started_2_Trying_X_subscribe, "test_fsm_exec_Started_2_Trying_X_subscribe")
            // Started -> (Any) -> Started
            .AddAlwaysNothingEntry((Int32)__S__.Started, "test_fsm_exec_Started_2_Started_X_any")

            /*=======================
			* === Trying === 
			*/
			// Trying -> (1xx) -> Trying
            .AddAlwaysEntry((Int32)__S__.Trying, (Int32)__A__.R1xx, (Int32)__S__.Trying, test_fsm_exec_Trying_2_Trying_X_1xx, "test_fsm_exec_Trying_2_Trying_X_1xx")
            // Trying -> (2xx) -> Terminated
            .AddEntry((Int32)__S__.Trying, (Int32)__A__.R2xx, test_fsm_cond_unsubscribing, (Int32)__S__.Terminated, test_fsm_exec_Trying_2_Terminated_X_2xx, "test_fsm_exec_Trying_2_Terminated_X_2xx")
            // Trying -> (2xx) -> Connected
            .AddEntry((Int32)__S__.Trying, (Int32)__A__.R2xx, test_fsm_cond_subscribing, (Int32)__S__.Connected, test_fsm_exec_Trying_2_Connected_X_2xx, "test_fsm_exec_Trying_2_Connected_X_2xx")
			

            /*=======================
			* === Connected === 
			*/
			// Connected -> (SUBSCRIBE) -> Trying
            .AddAlwaysEntry((Int32)__S__.Connected, (Int32)__A__.Unsubscribe, (Int32)__S__.Trying, test_fsm_exec_Connected_2_Trying_X_unsubscribe, "test_fsm_exec_Connected_2_Trying_X_unsubscribe")

            /*=======================
			* === Any === 
			*/
            // Any -> (error) -> Terminated
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)__A__.Error, (Int32)__S__.Terminated, test_fsm_exec_Any_2_Terminated_X_Error, "test_fsm_exec_Any_2_Terminated_X_Error")
			
            // Fake to test ANY
            .AddAlwaysEntry(TSK_StateMachine.STATE_ANY, (Int32)__A__.Subscribe, (Int32)__S__.Trying, test_fsm_exec_Started_2_Trying_X_subscribe, "fakefakefakefakefakefakefakefakefakefake")
            ;



            stateMachine.IsDebugEnabled = true;

            stateMachine.ExecuteAction((Int32)__A__.Subscribe, null, null, null, null, null, null);
            stateMachine.ExecuteAction((Int32)__A__.R1xx, null, null, null, null, null, null);
            stateMachine.ExecuteAction((Int32)__A__.R2xx, null, null, null, null, null, null);

            stateMachine.ExecuteAction((Int32)__A__.Unsubscribe, null, null, null, null, null, null);
            stateMachine.ExecuteAction((Int32)__A__.R2xx, null, null, null, null, null, null);

            stateMachine.ExecuteAction((Int32)__A__.R2xx, null, null, null, null, null, null);
        }
    }
}
