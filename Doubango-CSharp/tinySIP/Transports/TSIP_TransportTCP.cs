using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinyNET;
using tnet_socket_type_t = Doubango.tinyNET.TNET_Socket.tnet_socket_type_t;

namespace Doubango.tinySIP.Transports
{
    public class TSIP_TransportTCP : TSIP_Transport
    {
        public TSIP_TransportTCP(String host, ushort port, bool useIPv6, String description)
            : base(host, port, useIPv6 ? tnet_socket_type_t.tnet_socket_type_tcp_ipv6 : tnet_socket_type_t.tnet_socket_type_tcp_ipv4, description)
        {

        }
        public TSIP_TransportTCP(String host, ushort port, String description)
            : this(host, port, false, description)
        {

        }
    }
}
