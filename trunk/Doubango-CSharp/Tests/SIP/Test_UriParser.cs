using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doubango.tinySIP;
using Doubango.tinySAK;

namespace Doubango.Tests.SIP
{
    internal static class Test_UriParser
    {
        const String SIP_REQUEST =
               "REGISTER sip:open-ims.test SIP/2.0\r\n" +
               "Test-Header: 0\r\n" +
               "v: SIP/2.0/UDP [::]:1988;test=1234;comp=sigcomp;rport=254;ttl=457;received=192.0.2.101;branch=z9hG4bK1245420841406\r\n" +
               "f: \"Mamadou\" <sip:mamadou@open-ims.test>;tag=29358\r\n" +
               "t:    <sip:mamadou@open-ims.test>;tag= 12345\r\n" +
               "i: M-fa53180346f7f55ceb8d8670f9223dbb\r\n" +
               "CSeq: 201 REGISTER\r\n" +
               "Max-Forwards: 70\r\n" +
               "Allow: INVITE, ACK, CANCEL, BYE, MESSAGE, OPTIONS, NOTIFY, PRACK\r\n" +
               "Allow: REFER, UPDATE\r\n" +
               "u: talk, hold, conference, LocalModeStatus\r\n" +
               "m: <sip:mamadou@[::]:1988;comp=sigcomp;transport=udp>;expires=600000;+deviceID=\"3ca50bcb-7a67-44f1-afd0-994a55f930f4\";mobility=\"fixed\";+g.3gpp.cs-voice;+g.3gpp.app%5fref=\"urn%3Aurnxxx%3A3gpp-application.ims.iari.gsmais\";+g.oma.sip-im.large-message;+g.oma.sip-im\r\n" +
               "User-Agent: IM-client/OMA1.0 doubango/v0.0.0\r\n" +
               "Require: pref, path\r\n" +
               "Service-Route: <sip:orig@open-ims.test:6060;lr>,<sip:orig2@open-ims.test:6060;lr>\r\n" +
               "Path: <sip:term@open-ims.test:4060;lr>\r\n" +
               "Require: 100rel\r\n" +
               "P-Preferred-Identity: <sip:mamadou@open-ims.test>\r\n" +
               "k: path\r\n" +
               "k: gruu, outbound, timer\r\n" +
               "P-Access-Network-Info: 3GPP-UTRAN-TDD;utran-cell-id-3gpp=00000000\r\n" +
               "Privacy: none;user;id\r\n" +
               "Supported: gruu, outbound, path, timer\r\n" +
               "Expires12: 1983\r\n" +
               "l: 180\r\n" +
               "\r\n";

        const String SIP_RESPONSE =
            "SIP/2.0 200 This is my reason phrase\r\n" +
            "To: <sip:mamadou@open-ims.test>;tag=bweyal\r\n" +
            "Via: SIP/2.0/UDP 192.168.0.11:63140;branch=z9hG4bK1261611942868;rport=63140\r\n" +
            "CSeq: 31516 REGISTER\r\n" +
            "Content-Length: 0\r\n" +
            "Call-ID: 1261611941121\r\n" +
            "Min-Expires: 30\r\n" +
            "Event: reg\r\n" +
            "From: <sip:mamadou@open-ims.test>;tag=1261611941121\r\n" +
            "Contact: <sip:mamadou@192.168.0.12:58827;transport=udp>;mobility=fixed;+deviceid=\"DD1289FA-C3D7-47bd-A40D-F1F1B2CC5FFC\";expires=300,<sip:mamadou@192.168.0.12:58828;transport=udp>;mobility=fixed;+deviceid=\"DD1289FA-C3D7-47bd-A40D-F1F1B2CC5FFC\";expires=300,<sip:mamadou@192.168.0.12:58829;transport=udp>;mobility=fixed;+deviceid=\"DD1289FA-C3D7-47bd-A40D-F1F1B2CC5FFC\";expires=300\r\n" +
            "Contact: <sip:mamadou@192.168.0.11:63140>;expires=3600;q=1.0,<sip:mamadou@192.168.0.11:56717>;expires=3600;q=1.0\r\n" +
            "Contact: <sip:mamadou@127.0.0.1:5060>;expires=3600;q=1.0\r\n" +
            "Contact: <sip:mamadou@127.0.0.1>;expires=3600;q=1.0\r\n" +
            "P-Preferred-Identity: <sip:mamadou@open-ims.test>\r\n" +
            "Service-Route: <sip:orig@open-ims.test:6060;lr><sip:orig2@open-ims.test:6060;lr>,<sip:orig3@open-ims.test:6060;lr>\r\n" +
            "Path: <sip:term@open-ims.test:4060;lr>\r\n" +
            "P-Access-Network-Info: 3GPP-UTRAN-TDD;utran-cell-id-3gpp=00000000\r\n" +
            "Authorization: Digest username=\"Alice\", realm=\"atlanta.com\",nonce=\"84a4cc6f3082121f32b42a2187831a9e\",response=\"7587245234b3434cc3412213e5f113a5432,test=123\"\r\n" +
            "Privacy: none;user;id\r\n" +
            "Proxy-Authenticate: Digest realm=\"atlanta.com\",domain=\"sip:ss1.carrier.com\",qop=\"auth,auth-int\",nonce=\"f84f1cec41e6cbe5aea9c8e88d359\",opaque=\"\", stale=FALSE, algorithm=MD5,test=124\r\n" +
            "Authorization: Digest username=\"bob\", realm=\"atlanta.example.com\",nonce=\"ea9c8e88df84f1cec4341ae6cbe5a359\", opaque=\"\",uri=\"sips:ss2.biloxi.example.com\",test=\"7854\",response=\"dfe56131d1958046689d83306477ecc\"\r\n" +
            "Proxy-Authorization: Digest username=\"Alice\", test=666,realm=\"atlanta.com\",nonce=\"c60f3082ee1212b402a21831ae\",response=\"245f23415f11432b3434341c022\"\r\n" +
            "WWW-Authenticate: Digest realm=\"atlanta.com\",domain=\"sip:boxesbybob.com\", qop=\"auth\",nonce=\"f84f1cec41e6cbe5aea9c8e88d359\",opaque=\"\",stale=FALSE,algorithm=MD5,test=\"3\"\r\n" +
            "l: 0\r\n" +
            "Subscription-State: active;reason=deactivated;expires=507099;retry-after=145;test=jk\r\n" +
            "\r\n";

        const String SIP_MESSAGE =
            "MESSAGE sip:mamadou@open-ims.test SIP/2.0\r\n" +
            "Via: SIP/2.0/tcp 127.0.0.1:5082;branch=z9hG4bKc16be5aee32df400d01015675ab911ba,SIP/2.0/udp 127.0.0.1:5082;branch=z9hG4bKeec53b25db240bec92ea250964b8c1fa;received_port_ext=5081;received=192.168.0.13,SIP/2.0/UDP 192.168.0.12:57121;rport=57121;branch=z9hG4bK1274980921982;received_port_ext=5081;received=192.168.0.12\r\n" +
            "From: Bob <sip:bob@open-ims.test>;tag=mercuro\r\n" +
            "To: \"Alice\"<sip:alice@open-ims.test>\r\n" +
            "m: <sip:mamadou@127.0.0.1:5060>\r\n" +
            "Call-ID: 1262767804423\r\n" +
            "CSeq: 8 MESSAGE\r\n" +
            "Refer-To: <sips:a8342043f@atlanta.example.com?Replaces=12345601%40atlanta.example.com%3Bfrom-tag%3D314159%3Bto-tag%3D1234567>\r\n" +
            "Refer-To: sip:conf44@example.com;isfocus\r\n" +
            "Referred-By: <sip:referrer@referrer.example>;cid=\"20398823.2UWQFN309shb3@referrer.example\"\r\n" +
            "Refer-Sub: false;test=45;op\r\n" +
            "Refer-Sub: true;p\r\n" +
            "RSeq: 17422\r\n" +
            "RAck: 776656 1 INVITE\r\n" +
            "Min-SE: 90;test;y=0\r\n" +
            "Session-Expires: 95;refresher=uas;y=4\r\n" +
            "x: 95;refresher=uac;o=7;k\r\n" +
            "Max-Forwards: 70\r\n" +
            "Date: Wed, 28 Apr 2010 23:42:50 GMT\r\n" +
            "Date: Sun, 2 May 2010 20:27:49 GMT\r\n" +
            "Allow: INVITE, ACK, CANCEL, BYE, MESSAGE, OPTIONS, NOTIFY, PRACK, UPDATE, REFER\r\n" +
            "User-Agent: IM-client/OMA1.0 Mercuro-Bronze/v4.0.1508.0\r\n" +
            "c: text/plain; charset=utf-8\r\n" +
            "Security-Client: ipsec-3gpp;alg=hmac-md5-96;ealg=aes-cbc;prot=esp;mod=trans;port-c=61676;port-s=61662;spi-c=4294967295;spi-s=67890,tls;q=0.2\r\n" +
            "Security-Client: ipsec-ike;q=0.1,tls;q=0.2;test=123\r\n" +
            "Security-Server: ipsec-ike;q=0.1,ipsec-3gpp;alg=hmac-md5-96;prot=esp;mod=trans;ealg=aes-cbc;spi-c=5000;spi-s=5001;port-c=78952;port-s=77854\r\n" +
            "Security-Verify: ipsec-3gpp;alg=hmac-md5-96;prot=esp;mod=trans;ealg=aes-cbc;spi-c=5000;spi-s=5001;port-c=9999;port-s=20000,ipsec-ike;q=0.1;test=458;toto\r\n" +
            "Service-Route: <sip:orig@open-ims.test:6060;lr;transport=udp>,<sip:atlanta.com>,\"Originating\" <sip:orig2@open-ims.test:6060;lr>\r\n" +
            "Path: <sip:term@open-ims.test:4060;lr>\r\n" +
            "Route: \"Prox-CSCF\" <sip:pcscf.open-ims.test:4060;lr;transport=udp>;test=1,\"Originating\" <sip:orig@scscf.open-ims.test:6060;lr>\r\n" +
            "Record-Route: <sip:mo@pcscf.ims.inexbee.com:4060;lr>,\"Originating\"<sip:pcscf.open-ims.test:4060;lr;transport=udp>;test=2\r\n" +
            "P-Preferred-Identity: <sip:bob@open-ims.test\r\n" +
            "Allow-Events: presence, presence.winfo\r\n" +
            "Event: reg\r\n" +
            "P-Associated-URI: <sip:bob@open-ims.test>, <sip:0600000001@open-ims.test>, <sip:0100000001@open-ims.test>\r\n" +
            "P-Charging-Function-Addresses: ccf=pri_ccf_address\r\n" +
            "Server: Sip EXpress router (2.0.0-dev1 OpenIMSCore (i386/linux))\r\n" +
            "Warning: 392 192.168.0.15:6060 \"Noisy feedback tells:  pid=4521 req_src_ip=192.168.0.15 req_src_port=5060 in_uri=sip:scscf.open-ims.test:6060 out_uri=sip:scscf.open-ims.test:6060 via_cnt==3\"\r\n" +
            "P-Asserted-Identity: \"Cullen Jennings\" <sip:fluffy@cisco.com>\r\n" +
            "P-Asserted-Identity: tel:+14085264000\r\n" +
            "WWW-Authenticate: Digest realm=\"ims.inexbee.com\", nonce=\"iTaxDEv2uO8sKxzVVaRy6IkU9Lra6wAA2xv4BrmCzvY=\", algorithm=AKAv1-MD5, qop=\"auth\"\r\n" +
            "WWW-Authenticate: Digest realm=\"ims.cingularme.com\",\r\n   nonce=\"b7c9036dbf3054aea9404c7286aee9703dc8f84c2008\",\r\n   opaque=\"Lss:scsf-stdn.imsgroup0-001.ims1.wtcdca1.mobility.att.net:5060\",\r\n   algorithm=MD5,\r\n   qop=\"auth\"\r\n" +
            "Content-Length: 11\r\n" +
            "\r\n" +
            "How are you";

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

        internal static void TestMessageParser()
        {
            TSIP_Message message = TSIP_ParserMessage.Parse(Encoding.UTF8.GetBytes(SIP_MESSAGE), true);
            if (message != null)
            {
                TSK_Debug.Info("Request = {0}", message);
            }
        }

        internal static void TestUriParser()
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
