using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango_CSharp.tinySIP;
using Doubango_CSharp.tinySAK;

namespace Doubango_CSharp.Tests.SIP
{
    internal static class Test_UriParser
    {
        static String[] __Uris = 
        {
        	
	        //== SIP:
	        "sip:123.com",
	        "sip:open-ims.test",
	        "sip:pcscf.open-ims.test:4060;lr;transport=udp",
	        "sip:2233392625@sip2sip.info",
	        "sip:alice@iatlanta.com;p1=23",
	        "sip:*666*@atlanta.com",
	        "sip:#66#@atlanta.com", // should fail: # must be replaced with %23
	        "sip:alice:secretword@atlanta.com",
	        "sip:alice:secretword@atlanta.com:65535;transport=tcp",
            "sip:+1-212-555-1212:1234@gateway.com;user=phone",
	        "sip:alice@192.0.2.4:5060", // Fails: host_type=hostname which is not correct
	        "sip:alice@[1111::aaa:bbb:ccc:ddd]:5060",
	        "sip:atlanta.com",
	        "sip:alice@[1111::aaa:bbb:ccc:ddd]",
	        "sip:alice@[1111::aaa:bbb:ccc:ddd]:5060;user=phone",
	        "sip:alice@1111::aaa:bbb:ccc:ddd", // should fail
	        "sip:alice@[::127]",
	        "sip:ss2.biloxi.example.com;lr",// FIXME
            "sip:atlanta.com;method=REGISTER?to=alice%40atlanta.com",
            "sip:alice@atlanta.com;maddr=239.255.255.1;ttl=15",
            "sip:alice@atlanta.com;comp=sigcomp",
	        "sip:atlanta.com;method=REGISTER?to=alice%40atlanta.com",
            "sip:alice@atlanta.com?subject=project%20x&priority=urgent",

	        //== SIPS:
	        "sips:alice@atlanta.com",
            "sips:alice:secretword@atlanta.com;transport=tcp",
            "sips:+1-212-555-1212:1234@gateway.com;user=phone",
            "sips:alice@192.0.2.4",
            "sips:atlanta.com;method=REGISTER?to=alice%40atlanta.com",
            "sips:alice@atlanta.com;maddr=239.255.255.1;ttl=15",
            "sips:alice@atlanta.com;comp=sigcomp",
	        "sips:atlanta.com;method=REGISTER?to=alice%40atlanta.com",
            "sips:alice@atlanta.com?subject=project%20x&priority=urgent",

	        //== TEL:
	        "tel:+1-201-555-0123",
            "tel:7042;phone-context=example.com;ff=ff",
            "tel:863-1234;phone-context=+1-914-555",
	        "tel:#666#",
        };

        internal static void TestParser()
        {
            int i;

	        for(i=0; i<__Uris.Length; i++)
	        {
		        TSIP_Uri uri = TSIP_ParserUri.Parse(__Uris[i]);
        				
		        TSK_Debug.Info("\n== Parsing {{ {0} }} ==\n", __Uris[i]);
        		
		        if(uri != null)
		        {
			        TSK_Debug.Info("scheme: {0}", uri.Scheme);
			        TSK_Debug.Info("user-name: {0}", uri.UserName);
			        TSK_Debug.Info("password: {0}", uri.Password);
			        TSK_Debug.Info("host: {0}", uri.Host);
			        TSK_Debug.Info("port: {0}", uri.Port);
			        TSK_Debug.Info("host-type: {0}", uri.HostType == tsip_host_type_t.IPv4 ? "IPv4" : (uri.HostType == tsip_host_type_t.IPv6 ? "IPv6" : (uri.HostType == tsip_host_type_t.Hostname ? "HOSTNAME" : "UNKNOWN")) );
        			
			        TSK_Debug.Info("---PARAMS---");

			        /* dump all parameters */
                    foreach (TSK_Param param in uri.Params)
			        {
                        TSK_Debug.Info("-->{0}={1}", param.Name, param.Value);
			        }

                    TSK_Debug.Info("Is-secure: {0}", uri.IsSecure ? "YES" : "NO");

                    TestToString(uri);

                    uri.Dispose();
		        }
		        else
		        {
                    TSK_Debug.Error("INVALID SIP URI");
		        }

                TSK_Debug.Info("\n\n");
	        }
        }

        internal static void TestToString(TSIP_Uri uri)
        {
            TSK_Debug.Info("uri_to_string={0}", uri);
        }
    }
}
