using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public enum ServerOpCode
{
    KEEP_ALIVE = 1,
    WELCOME = 2,
    UNWELCOME = 3,
    OTHER_CONNECTED = 4,
    OTHER_DISCONNECTED = 5,
    START_GAME = 6,
    MAKE_MOVE = 7,
    PICKED_UP = 8,
    GROW = 9,
    WALL = 10,
    ROCKET = 11,
    MERGE = 12,
    BIRD_POOP = 13,
    GAME_COUNTDOWN = 14,
    MENU_COUNTDOWN = 15,
    SHOOT = 16,
    GUM = 17,
    BOOST = 18,
    CUSTOMISER_UPDATE = 19
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
            case ServerOpCode.UNWELCOME:
                msg = new NetUnwelcome(a_stream);
                break;
            case ServerOpCode.OTHER_CONNECTED:
                msg = new NetOtherConnected(a_stream);
                break;
            case ServerOpCode.OTHER_DISCONNECTED:
                msg = new NetOtherDisconnected(a_stream);
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
            case ServerOpCode.ROCKET:
                msg = new NetRocket(a_stream);
                break;
            case ServerOpCode.MERGE:
                msg = new NetMerge(a_stream);
                break;
            case ServerOpCode.BIRD_POOP:
                msg = new NetBirdPoop(a_stream);
                break;
            case ServerOpCode.GAME_COUNTDOWN:
                msg = new NetGameCountdown(a_stream);
                break;
            case ServerOpCode.MENU_COUNTDOWN:
                msg = new NetMenuCountdown(a_stream);
                break;
            case ServerOpCode.SHOOT:
                msg = new NetShoot(a_stream);
                break;
            case ServerOpCode.GUM:
                msg = new NetGum(a_stream);
                break;
            case ServerOpCode.BOOST:
                msg = new NetBoost(a_stream);
                break;
            case ServerOpCode.CUSTOMISER_UPDATE:
                msg = new NetCustomiserUpdate(a_stream);
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
    public static Action<NetMessage> C_UNWELCOME;
    public static Action<NetMessage> C_OTHER_CONNECTED;
    public static Action<NetMessage> C_OTHER_DISCONNECTED;
    public static Action<NetMessage> C_START_GAME;
    public static Action<NetMessage> C_MAKE_MOVE;
    public static Action<NetMessage> C_PICKED_UP;
    public static Action<NetMessage> C_GROW;
    public static Action<NetMessage> C_WALL;
    public static Action<NetMessage> C_ROCKET;
    public static Action<NetMessage> C_MERGE;
    public static Action<NetMessage> C_BIRD_POOP;
    public static Action<NetMessage> C_GAME_COUNTDOWN;
    public static Action<NetMessage> C_MENU_COUNTDOWN;
    public static Action<NetMessage> C_SHOOT;
    public static Action<NetMessage> C_GUM;
    public static Action<NetMessage> C_BOOST;
    public static Action<NetMessage> C_CUSTOMISER_UPDATE;

    //Server
    public static Action<NetMessage, NetworkConnection> S_KEEP_ALIVE;
    public static Action<NetMessage, NetworkConnection> S_WELCOME;
    public static Action<NetMessage, NetworkConnection> S_UNWELCOME;
    public static Action<NetMessage, NetworkConnection> S_OTHER_CONNECTED;
    public static Action<NetMessage, NetworkConnection> S_OTHER_DISCONNECTED;
    public static Action<NetMessage, NetworkConnection> S_START_GAME;
    public static Action<NetMessage, NetworkConnection> S_MAKE_MOVE;
    public static Action<NetMessage, NetworkConnection> S_PCICKED_UP;
    public static Action<NetMessage, NetworkConnection> S_GROW;
    public static Action<NetMessage, NetworkConnection> S_WALL;
    public static Action<NetMessage, NetworkConnection> S_ROCKET;
    public static Action<NetMessage, NetworkConnection> S_MERGE;
    public static Action<NetMessage, NetworkConnection> S_BIRD_POOP;
    public static Action<NetMessage, NetworkConnection> S_GAME_COUNTDOWN;
    public static Action<NetMessage, NetworkConnection> S_MENU_COUNTDOWN;
    public static Action<NetMessage, NetworkConnection> S_SHOOT;
    public static Action<NetMessage, NetworkConnection> S_GUM;
    public static Action<NetMessage, NetworkConnection> S_BOOST;
    public static Action<NetMessage, NetworkConnection> S_CUSTOMISER_UPDATE;
}
