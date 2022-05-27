//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 15/02/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public enum OpCode
{
    //messages op codes used to identify them
    KEEP_ALIVE = 1,
    SERVER_START = 2,
    SERVER_END = 3,
    LIST_REQUEST = 4
}
public static class ServerUtility
{
    public static void OnData(DataStreamReader a_stream, NetworkConnection a_connection, Server a_server = null)
    {
        ServerMessage msg = null;
        OpCode opCode = (OpCode)a_stream.ReadByte();
        switch (opCode)
        {
            //swtich on op code to allow for packets to be proccessed 
            case OpCode.KEEP_ALIVE:
                msg = new ServerKeepAlive(a_stream);
                break;
            case OpCode.SERVER_START:
                msg = new ServerHostStart(a_stream);
                break;
            case OpCode.SERVER_END:
                msg = new ServerHostEnd(a_stream);
                break;
            case OpCode.LIST_REQUEST:
                msg = new ServerListRequest(a_stream);
                break;
            default:
                Debug.LogError("Message received has unrecognised OpCode");
                break;
        }

        //check whether received on server or client
        if (a_server != null)
        {
            msg.RevievedOnServer(a_connection);
        }
        else
        {
            msg.RecievedOnClient();
        }
    }
    //Net Messages

    //Client
    public static Action<ServerMessage> C_KEEP_ALIVE;
    public static Action<ServerMessage> C_SERVER_START;
    public static Action<ServerMessage> C_SERVER_END;
    public static Action<ServerMessage> C_LIST_REQUEST;

    //Server
    public static Action<ServerMessage, NetworkConnection> S_KEEP_ALIVE;
    public static Action<ServerMessage, NetworkConnection> S_SERVER_START;
    public static Action<ServerMessage, NetworkConnection> S_SERVER_END;
    public static Action<ServerMessage, NetworkConnection> S_LIST_REQUEST;
}
