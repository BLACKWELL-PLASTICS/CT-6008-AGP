using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public enum ServerOpCode
{
    KEEP_ALIVE = 1,
    WELCOME = 2,
    OTHER_CONNECTED = 3,
    START_GAME = 4,
    MAKE_MOVE = 5,
    PICKED_UP = 6,
    GROW = 7,
    WALL = 8
}
public static class NetUtility
{
    public static void OnData(DataStreamReader a_stream, NetworkConnection a_connection, Server a_server = null)
    {
        NetMessage msg = null;
        ServerOpCode opCode = (ServerOpCode)a_stream.ReadByte();
        switch (opCode)
        {
            case ServerOpCode.KEEP_ALIVE:
                msg = new NetKeepAlive(a_stream);
                break;
            case ServerOpCode.WELCOME:
                msg = new NetWelcome(a_stream);
                break;
            case ServerOpCode.OTHER_CONNECTED:
                msg = new NetOtherConnected(a_stream);
                break;
            case ServerOpCode.START_GAME:
                msg = new NetStartGame(a_stream);
                break;
            case ServerOpCode.MAKE_MOVE:
                msg = new NetMakeMove(a_stream);
                break;
            case ServerOpCode.PICKED_UP:
                msg = new NetPickedUp(a_stream);
                break;
            case ServerOpCode.GROW:
                msg = new NetGrow(a_stream);
                break;
            case ServerOpCode.WALL:
                msg = new NetWall(a_stream);
                break;
            default:
                Debug.LogError("Message received has unrecognised OpCode");
                break;
        }

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
    public static Action<NetMessage> C_KEEP_ALIVE;
    public static Action<NetMessage> C_WELCOME;
    public static Action<NetMessage> C_OTHER_CONNECTED;
    public static Action<NetMessage> C_START_GAME;
    public static Action<NetMessage> C_MAKE_MOVE;
    public static Action<NetMessage> C_PICKED_UP;
    public static Action<NetMessage> C_GROW;
    public static Action<NetMessage> C_WALL;

    //Server
    public static Action<NetMessage, NetworkConnection> S_KEEP_ALIVE;
    public static Action<NetMessage, NetworkConnection> S_WELCOME;
    public static Action<NetMessage, NetworkConnection> S_OTHER_CONNECTED;
    public static Action<NetMessage, NetworkConnection> S_START_GAME;
    public static Action<NetMessage, NetworkConnection> S_MAKE_MOVE;
    public static Action<NetMessage, NetworkConnection> S_PCICKED_UP;
    public static Action<NetMessage, NetworkConnection> S_GROW;
    public static Action<NetMessage, NetworkConnection> S_WALL;
}
