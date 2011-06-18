# Ragel generator
# For more information about Ragel: http://www.complang.org/ragel/

export OPTIONS="-A -L -T0"

# SIP/SIPS/TEL URI parser
ragel.exe $OPTIONS -o ../Parsers/TSIP_ParserUri.cs ./ragel/tsip_parser_uri.rl

# SIP message (both requests an responses) parser.
ragel.exe $OPTIONS -o ../Parsers/TSIP_ParserMessage.cs ./ragel/tsip_parser_message.rl

# SIP headers parser
ragel.exe $OPTIONS -o ../Parsers/TSIP_ParserHeader.cs ./ragel/tsip_parser_header.rl


# ==Allow
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderAllow.cs ./ragel/tsip_parser_header_Allow.rl

# ==Allow-Events
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderAllowEvents.cs ./ragel/tsip_parser_header_Allow_Events.rl

# ==Authorization
#######ragel.exe $OPTIONS -o ./src/headers/tsip_header_Authorization.c ./ragel/tsip_parser_header_Authorization.rl

# ==Call-ID
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderCallId.cs ./ragel/tsip_parser_header_Call_ID.rl

# ==Content-Length
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderContentLength.cs ./ragel/tsip_parser_header_Content_Length.rl

# ==Content-Type
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderContentType.cs ./ragel/tsip_parser_header_Content_Type.rl

# ==Contact
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderContact.cs ./ragel/tsip_parser_header_Contact.rl

# ==CSeq
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderCSeq.cs ./ragel/tsip_parser_header_CSeq.rl

# ==Date
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Date.c ./ragel/tsip_parser_header_Date.rl

# ==Dummy
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderDummy.cs ./ragel/tsip_parser_header_Dummy.rl

# ==Event
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Event.c ./ragel/tsip_parser_header_Event.rl

# ==Expires
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderExpires.cs ./ragel/tsip_parser_header_Expires.rl

# ==From
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderFrom.cs ./ragel/tsip_parser_header_From.rl

# ==Max-Forwards
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderMaxForwards.cs ./ragel/tsip_parser_header_Max_Forwards.rl

# ==Min-Expires
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Min_Expires.c ./ragel/tsip_parser_header_Min_Expires.rl

# ==Min-SE
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Min_SE.c ./ragel/tsip_parser_header_Min_SE.rl

# ==Path
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Path.c ./ragel/tsip_parser_header_Path.rl

# ==P-Access-Network-Info
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_P_Access_Network_Info.c ./ragel/tsip_parser_header_P_Access_Network_Info.rl

# ==P-Asserted-Identity
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_P_Asserted_Identity.c ./ragel/tsip_parser_header_P_Asserted_Identity.rl

# ==P-Associated-URI
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_P_Associated_URI.c ./ragel/tsip_parser_header_P_Associated_URI.rl

# ==P-Charging-Function-Addresses
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_P_Charging_Function_Addresses.c ./ragel/tsip_parser_header_P_Charging_Function_Addresses.rl

# ==P-Preferred-Identity
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_P_Preferred_Identity.c ./ragel/tsip_parser_header_P_Preferred_Identity.rl

# ==Privacy
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Privacy.c ./ragel/tsip_parser_header_Privacy.rl

# ==Proxy-Authenticate
#########ragel.exe $OPTIONS -o ./src/headers/tsip_header_Proxy_Authenticate.c ./ragel/tsip_parser_header_Proxy_Authenticate.rl

# ==Proxy-Authorization
##########ragel.exe $OPTIONS -o ./src/headers/tsip_header_Proxy_Authorization.c ./ragel/tsip_parser_header_Proxy_Authorization.rl

# ==Proxy-Require
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Proxy_Require.c ./ragel/tsip_parser_header_Proxy_Require.rl

# ==RAck
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_RAck.c ./ragel/tsip_parser_header_RAck.rl

# ==Record-Route
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderRecordRoute.cs ./ragel/tsip_parser_header_Record_Route.rl

# ==Refer-Sub
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Refer_Sub.c ./ragel/tsip_parser_header_Refer_Sub.rl

# ==Refer-To
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Refer_To.c ./ragel/tsip_parser_header_Refer_To.rl

# ==Referred-By
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Referred_By.c ./ragel/tsip_parser_header_Referred_By.rl

# ==Require
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderRequire.cs ./ragel/tsip_parser_header_Require.rl

# == Route
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderRoute.cs ./ragel/tsip_parser_header_Route.rl

# ==RSeq
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_RSeq.c ./ragel/tsip_parser_header_RSeq.rl

# == Server
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Server.c ./ragel/tsip_parser_header_Server.rl

# == SIP-ETag
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_SIP_ETag.c ./ragel/tsip_parser_header_SIP_ETag.rl

# == SIP-If-Match
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_SIP_If_Match.c ./ragel/tsip_parser_header_SIP_If_Match.rl

# == Security-Client
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Security_Client.c ./ragel/tsip_parser_header_Security_Client.rl

# == Security-Server
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Security_Server.c ./ragel/tsip_parser_header_Security_Server.rl

# == Security-Verify
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Security_Verify.c ./ragel/tsip_parser_header_Security_Verify.rl

# == Service-Route
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Service_Route.c ./ragel/tsip_parser_header_Service_Route.rl

# == Session-Expires
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Session_Expires.c ./ragel/tsip_parser_header_Session_Expires.rl

# ==Subscription-State
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Subscription_State.c ./ragel/tsip_parser_header_Subscription_State.rl

# ==Supported
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderSupported.cs ./ragel/tsip_parser_header_Supported.rl

# ==To
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderTo.cs ./ragel/tsip_parser_header_To.rl

# ==User-Agent
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_User_Agent.c ./ragel/tsip_parser_header_User_Agent.rl

# ==Via
ragel.exe $OPTIONS -o ../Headers/TSIP_HeaderVia.cs ./ragel/tsip_parser_header_Via.rl

# ==Warning
#ragel.exe $OPTIONS -o ./src/headers/tsip_header_Warning.c ./ragel/tsip_parser_header_Warning.rl

# ==WWW-Authenticate
##########ragel.exe $OPTIONS -o ./src/headers/tsip_header_WWW_Authenticate.c ./ragel/tsip_parser_header_WWW_Authenticate.rl
