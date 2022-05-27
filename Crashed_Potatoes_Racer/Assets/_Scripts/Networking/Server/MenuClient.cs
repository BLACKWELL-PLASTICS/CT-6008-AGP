//////////////////////////////////////////////////
/// Created: 07/02/2022                        ///
/// Author: Iain Farlow                        ///
/// Edited By:                                 ///
/// Last Edited: 14/02/2022                    ///
//////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using System.Linq;
using System.Net;

public class MenuClient : MonoBehaviour
{
    public static MenuClient Instance { set; get; }
    public bool m_IsHost { get; set; }
    void Awake()
    {
        //set instance
        Instance = this;
    }

    public NetworkDriver m_driver;
    public Action m_connectionDropped;

    NetworkConnection m_connection;
    bool m_isActive = false;

    public void Initlialise(string a_ip, ushort a_port)
    {
        //create driver
        m_driver = NetworkDriver.Create();
        //set ip
        NetworkEndPoint endPoint = NetworkEndPoint.Parse(a_ip, a_port);

        m_connection = m_driver.Connect(endPoint);

        Debug.Log("Attempting to connect to Menu Server at: " + endPoint.Address);

        m_isActive = true;
        //register events
        RegisterToEvent();
    }
    public void Shutdown()
    {
        if (m_isActive)
        {
            UnregisterToEvent();
            m_driver.Dispose();
            m_connection = default(NetworkConnection);
            m_isActive = false;
        }
    }
    public void OnDestroy()
    {
        if (m_IsHost)
        {
            //if hosting send close server info
            ServerHostEnd serverHostEnd = new ServerHostEnd();
            serverHostEnd.m_ServerIP = GetLocalIPv4();
            MenuClient.Instance.SendToServer(serverHostEnd);
        }
        Shutdown();
    }

    public void Update()
    {
        if (!m_isActive)
        {
            return;
        }
        //keep alive sends message to ensure that there is not a timeout on the server
        m_driver.ScheduleUpdate().Complete();
        //ensure the client is still okay
        CheckAlive();

        UpdateMessagePump();
    }
    void CheckAlive()
    {
        if (!m_connection.IsCreated && m_isActive)
        {
            //if it looses connection
            Debug.Log("Lost connection to server");
            m_connectionDropped?.Invoke();
            Shutdown();
        }
    }
    void UpdateMessagePump()
    {
        DataStreamReader stream;
        NetworkEvent.Type cmd;
        //if connection defaults
        while ((cmd = m_connection.PopEvent(m_driver, out stream)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect)
            {
                //confirm connection
                ServerListRequest serverListRequest = new ServerListRequest();
                SendToServer(serverListRequest);
                Debug.Log("Connected to Menu Server");
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                //proccess data
                ServerUtility.OnData(stream, default(NetworkConnection));
            }
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
                //disconnect
                Debug.Log("Client got disconnected from server");
                m_connection = default(NetworkConnection);
                m_connectionDropped?.Invoke();
                Shutdown();
            }
        }
    }

    public void SendToServer(ServerMessage a_msg)
    {
        DataStreamWriter writer;
        writer = m_driver.BeginSend(m_connection, 0);
        a_msg.Serialize(ref writer);
        m_driver.EndSend(writer);
    }
    //keep aqlives are to ensure drop doesnt happen
    private void RegisterToEvent()
    {
        ServerUtility.C_KEEP_ALIVE += OnKeepAlive;
    }
    private void UnregisterToEvent()
    {
        ServerUtility.C_KEEP_ALIVE -= OnKeepAlive;
    }
    private void OnKeepAlive(ServerMessage a_msg)
    {
        SendToServer(a_msg);
    }
    //gets ip
    string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
    }
}
