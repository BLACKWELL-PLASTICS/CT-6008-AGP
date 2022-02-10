using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public class Server : MonoBehaviour
{
    public static Server Instance { set; get; }
    void Awake()
    {
        Instance = this;
    }

    public NetworkDriver m_driver;
    public Action m_connectionDropped;

    NativeList<NetworkConnection> m_connections;
    bool m_isActive = false;
    const float m_keepAliveTickRate = 20.0f;
    float m_lastKeepAlive;

    public void Initlialise(ushort a_port)
    {
        m_driver = NetworkDriver.Create();
        NetworkEndPoint endPoint = NetworkEndPoint.AnyIpv4;
        endPoint.Port = a_port;

        if (m_driver.Bind(endPoint) != 0)
        {
            Debug.Log("Unable to bind on port " + endPoint.Port);
            return;
        }
        else
        {
            Debug.Log("Listening on port " + endPoint.Port);
            m_driver.Listen();
        }

        m_connections = new NativeList<NetworkConnection>(2, Allocator.Persistent);
        m_isActive = true;
    }
    public void Shutdown()
    {
        if (m_isActive)
        {
            m_driver.Dispose();
            m_connections.Dispose();
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

        KeepAlive();

        m_driver.ScheduleUpdate().Complete();

        CleanupConnections();
        AcceptNewConnections();
        UpdateMessagePump();
    }
    void KeepAlive()
    {
        if (Time.time - m_lastKeepAlive > m_keepAliveTickRate)
        {
            m_lastKeepAlive = Time.time;
            Broadcast(new NetKeepAlive());
        }
    }
    void CleanupConnections()
    {
        for (int i = 0; i < m_connections.Length; i++)
        {
            if (!m_connections[i].IsCreated)
            {
                m_connections.RemoveAtSwapBack(i);
                --i;
            }
        }
    }
    void AcceptNewConnections()
    {
        NetworkConnection networkConnection;
        while ((networkConnection = m_driver.Accept()) != default(NetworkConnection))
        {
            m_connections.Add(networkConnection);
        }
    }
    void UpdateMessagePump()
    {
        DataStreamReader stream;
        for (int i = 0; i < m_connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while ((cmd = m_driver.PopEventForConnection(m_connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    NetUtility.OnData(stream, m_connections[i], this);
                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from server");
                    m_connections[i] = default(NetworkConnection);
                    m_connectionDropped?.Invoke();
                    Shutdown();
                }
            }
        }
    }

    public void SendToClient(NetworkConnection a_connection, NetMessage a_msg)
    {
        DataStreamWriter writer;
        writer = m_driver.BeginSend(a_connection, 0);
        a_msg.Serialize(ref writer);
        m_driver.EndSend(writer);
    }
    public void Broadcast(NetMessage a_msg)
    {
        for (int i = 0; i < m_connections.Length; i++)
        {
            if (m_connections[i].IsCreated)
            {
                Debug.Log($"Sending {a_msg.Code} to : {m_connections[i].InternalId}");
                SendToClient(m_connections[i], a_msg);
            }
        }
    }
}
