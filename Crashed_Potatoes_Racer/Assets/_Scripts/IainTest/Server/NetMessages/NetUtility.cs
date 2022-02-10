using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public enum OpCode
{
    KEEP_ALIVE = 1,
    WELCOME = 2,
    START_GAME = 3,
    MAKE_MOVE = 4,
}
public static class NetUtility
{
    public static void OnData(DataStreamReader a_stream, NetworkConnection a_connection, Server a_server = null)
    {
        NetMessage msg = null;
        OpCode opCode = (OpCode)a_stream.ReadByte();
        switch (opCode)
        {
            case OpCode.KEEP_ALIVE:
                msg = new NetKeepAlive(a_stream);
                break;
            case OpCode.WELCOME:
                msg = new NetWelcome(a_stream);
                break;
            case OpCode.START_GAME:
                msg = new NetStartGame(a_stream);
                break;
            case OpCode.MAKE_MOVE:
                msg = new NetMakeMove(a_stream);
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
    public static Action<NetMessage> C_START_GAME;
    public static Action<NetMessage> C_MAKE_MOVE;
    public static Action<NetMessage> C_REMATCH;

    //Server
    public static Action<NetMessage, NetworkConnection> S_KEEP_ALIVE;
    public static Action<NetMessage, NetworkConnection> S_WELCOME;
    public static Action<NetMessage, NetworkConnection> S_START_GAME;
    public static Action<NetMessage, NetworkConnection> S_MAKE_MOVE;
    public static Action<NetMessage, NetworkConnection> S_REMATCH;
}
