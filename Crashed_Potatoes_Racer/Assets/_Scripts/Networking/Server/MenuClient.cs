using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class MenuClient : MonoBehaviour
{
    public static MenuClient Instance { set; get; }
    void Awake()
    {
        Instance = this;
    }

    public NetworkDriver m_driver;
    public Action m_connectionDropped;

    NetworkConnection m_connection;
    bool m_isActive = false;

    public void Initlialise(string a_ip, ushort a_port)
    {
        m_driver = NetworkDriver.Create();
        NetworkEndPoint endPoint = NetworkEndPoint.Parse(a_ip, a_port);

        m_connection = m_driver.Connect(endPoint);

        Debug.Log("Attempting to connect to Menu Server at: " + endPoint.Address);

        m_isActive = true;

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
        Shutdown();
    }

    public void Update()
    {
        if (!m_isActive)
        {
            return;
        }

        m_driver.ScheduleUpdate().Complete();

        CheckAlive();

        UpdateMessagePump();
    }
    void CheckAlive()
    {
        if (!m_connection.IsCreated && m_isActive)
        {
            Debug.Log("Lost connection to server");
            m_connectionDropped?.Invoke();
            Shutdown();
        }
    }
    void UpdateMessagePump()
    {
        DataStreamReader stream;
        NetworkEvent.Type cmd;
        while ((cmd = m_connection.PopEvent(m_driver, out stream)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect)
            {
                ServerListRequest serverListRequest = new ServerListRequest();
                SendToServer(serverListRequest);
                Debug.Log("Connected to Menu Server");
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                ServerUtility.OnData(stream, default(NetworkConnection));
            }
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
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
}
