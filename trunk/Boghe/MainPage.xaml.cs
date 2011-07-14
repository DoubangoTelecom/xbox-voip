using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Doubango.tinySIP;
using System.Text;
using Doubango.tinySAK;
using Doubango.tinySIP.Transports;
using Doubango.tinyNET;
using Doubango.tinySIP.Headers;
using Doubango.tinySIP.Events;
using Doubango.tinySIP.Sessions;

namespace Boghe
{
    public partial class MainPage : PhoneApplicationPage
    {
        const String PROXY_HOST = "192.168.0.10";
        const ushort PROXY_PORT = 5060;
        const String DOMAIN = "doubango.org";
        const String USER_NAME = "bob";

        // Constructor
        public MainPage()
        {
            InitializeComponent();


            String mm = "SIP/2.0 200 OK\r\n" +
            "Via: SIP/2.0/UDP 192.168.0.13:5060;branch=z9hG4bK_56ef1cda-aef6-4d76-ab7e-d148af3b18ac;rport=51049\r\n" +
            "Record-Route: <sip:192.168.0.10:5060;lr;transport=udp>\r\n" +
            "To: <sip:004@doubango.org>\r\n" +
            "From: <sip:004@doubango.org>;tag=9bff9c34-35e3-4fa8-8de4-290ce275c981\r\n" +
            "Call-ID: 272b67b9-08b2-4fce-8f6e-6ccaf5e5ec9a\r\n" +
            "CSeq: 1990525622 REGISTER\r\n" +
            "Server: mjsip stack 1.6\r\n" +
            "Contact: <sip:004@192.168.0.13:5060>;expires=10\r\n" +
            "Content-Length: 0\r\n" +
            "\r\n";

            /*TSIP_Message message = TSIP_ParserMessage.Parse(UTF8Encoding.UTF8.GetBytes(mm), true);
            if (message != null)
            {
            }*/



            TSIP_Stack sipStack = new TSIP_Stack(
                TSIP_Uri.Create("sip:doubango.org"),
                "004",
                TSIP_Uri.Create("sip:004@doubango.org"),
                "192.168.0.10", 5060
                );

            // sipStack.Headers.Add(new TSK_Param("User-Agent", "wp-ngn-stack"));
            sipStack.AoRIP = "192.168.0.13";
            sipStack.AoRPort = 5060;

            sipStack.Callback = delegate(TSIP_Event @event)
            {
                switch (@event.Type)
                {
                    case TSIP_Event.tsip_event_type_t.DIALOG:
                        {
                            TSIP_EventDialog eventDialog = (@event as TSIP_EventDialog);
                            break;
                        }

                    case TSIP_Event.tsip_event_type_t.REGISTER:
                        {
                            TSIP_EventRegister eventDialog = (@event as TSIP_EventRegister);
                            break;
                        }

                    default:
                        {
                            return false;
                        }
                }
                return true;
            };


            if (sipStack.Start())
            {
                TSIP_SessionRegister register = new TSIP_SessionRegister(sipStack);
                register.Register();
            }

        }
    }
}