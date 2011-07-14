using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinyNET;
using tnet_socket_type_t = Doubango.tinyNET.TNET_Socket.tnet_socket_type_t;

namespace Doubango.tinySIP.Transports
{
    internal class TSIP_TransportTCP : TSIP_Transport
    {
        internal TSIP_TransportTCP(TSIP_Stack stack, String host, ushort port, bool useIPv6, String description)
            : base(stack, host, port, useIPv6 ? tnet_socket_type_t.tnet_socket_type_tcp_ipv6 : tnet_socket_type_t.tnet_socket_type_tcp_ipv4, description)
        {

        }
        internal TSIP_TransportTCP(TSIP_Stack stack, String host, ushort port, String description)
            : this(stack, host, port, false, description)
        {

        }
    }
}
