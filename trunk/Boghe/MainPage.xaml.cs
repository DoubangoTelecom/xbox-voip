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
            TSIP_Uri realm = TSIP_Uri.Create(String.Format("sip:{0}", DOMAIN));
            TSIP_Uri publicIdentity = TSIP_Uri.Create(String.Format("sip:{0}@{1}", USER_NAME, DOMAIN));


            TSIP_Request sipRequest = new TSIP_Request(TSIP_Request.METHOD_OPTIONS, realm, publicIdentity, publicIdentity, TSIP_HeaderCallId.RandomCallId(), 0); 
            TSIP_TransportUDP transportUdp = new TSIP_TransportUDP("Sip Transport");
            transportUdp.SendTo(TNET_Socket.CreateEndPoint("192.168.0.10", 5060), sipRequest.ToBytes());

        }
    }
}