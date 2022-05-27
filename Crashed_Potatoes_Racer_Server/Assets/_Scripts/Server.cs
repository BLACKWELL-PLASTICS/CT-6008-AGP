//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 03/02/2022                        ///
/// Edited By:                                 ///
/// Last Edited: 01/03/2022                    ///
//////////////////////////////////////////////////

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
        //set instance
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
        //create driver
        m_driver = NetworkDriver.Create();
        //get ip
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
        //create connection list
        m_connections = new NativeList<NetworkConnection>(2, Allocator.Persistent);
        m_isActive = true;
    }
    public void Shutdown()
    {
        if (m_isActive)
        {
            //turn off server
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

        //keep alive sends message to ensure that there is not a timeout on the server
        KeepAlive();
        //ensure the jobs handled by the server are complete
        m_driver.ScheduleUpdate().Complete();
        //clean connection stuff
        CleanupConnections();
        //get incomming connections
        AcceptNewConnections();
        UpdateMessagePump();
    }
    void KeepAlive()
    {
        //keep alive timer to prevent time out
        if (Time.time - m_lastKeepAlive > m_keepAliveTickRate)
        {
            m_lastKeepAlive = Time.time;
            Broadcast(new ServerKeepAlive());
        }
    }
    void CleanupConnections()
    {
        for (int i = 0; i < m_connections.Length; i++)
        {
            if (!m_connections[i].IsCreated)
            {
                //if remove if issue
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
            //add to connections
            m_connections.Add(networkConnection);
        }
    }
    void UpdateMessagePump()
    {
        DataStreamReader stream;
        //go through each connection for packets
        for (int i = 0; i < m_connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            //check driver is events 
            while ((cmd = m_driver.PopEventForConnection(m_connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                //if data refer to utility to proccess 
                if (cmd == NetworkEvent.Type.Data)
                {
                    ServerUtility.OnData(stream, m_connections[i], this);
                }
                //if dc default connection and invoke connection dropped action
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from server");
                    m_connections[i] = default(NetworkConnection);
                    m_connectionDropped?.Invoke();
                    //Shutdown();
                }
            }
        }
    }

    //send to client for sending to invedivual 
    public void SendToClient(NetworkConnection a_connection, ServerMessage a_msg)
    {

        //writes a data scream
        DataStreamWriter writer;
        //begin send to connection
        writer = m_driver.BeginSend(a_connection, 0);
        //serialise data
        a_msg.Serialize(ref writer);
        //end send
        m_driver.EndSend(writer);
    }
    //braodcast sends to call clients
    public void Broadcast(ServerMessage a_msg)
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