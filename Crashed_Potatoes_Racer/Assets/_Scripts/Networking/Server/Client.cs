//////////////////////////////////////////////////
/// Created: 07/02/2022                        ///
/// Author: Iain Farlow                        ///
/// Edited By:                                 ///
/// Last Edited: 04/04/2022                    ///
//////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class Client : MonoBehaviour
{
    public static Client Instance { set; get; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public NetworkDriver m_driver;
    public Action m_connectionDropped;

    public string m_clientName;

    NetworkConnection m_connection;
    bool m_isActive = false;

    public void Initlialise(string a_ip, ushort a_port)
    {
        m_driver = NetworkDriver.Create();
        NetworkEndPoint endPoint = NetworkEndPoint.Parse(a_ip, a_port);

        m_connection = m_driver.Connect(endPoint);

        Debug.Log("Attempting to connect to Server at: " + endPoint.Address);

        m_isActive = true;

        RegisterToEvent();
    }
    public void Shutdown()
    {
        if (m_isActive)
        {
            PersistentInfo.Instance.Clear();
            UnregisterToEvent();
            m_driver.Disconnect(m_connection);
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
                NetWelcome netWelcome = new NetWelcome();
                netWelcome.m_PlayerNumber = 0;
                netWelcome.m_PlayerName = m_clientName;
                netWelcome.m_CarBody = PersistentInfo.Instance.m_carDesign.m_carChoice;
                netWelcome.m_CarWheels = PersistentInfo.Instance.m_carDesign.m_wheelChoice;
                netWelcome.m_CarGun = PersistentInfo.Instance.m_carDesign.m_gunChoice;
                SendToServer(netWelcome);
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                NetUtility.OnData(stream, default(NetworkConnection));
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

    public void SendToServer(NetMessage a_msg)
    {
        DataStreamWriter writer;
        writer = m_driver.BeginSend(m_connection, 0);
        a_msg.Serialize(ref writer);
        m_driver.EndSend(writer);
    }
    private void RegisterToEvent()
    {
        NetUtility.C_KEEP_ALIVE += OnKeepAlive;
    }
    private void UnregisterToEvent()
    {
        NetUtility.C_KEEP_ALIVE -= OnKeepAlive;
    }
    private void OnKeepAlive(NetMessage a_msg)
    {
        SendToServer(a_msg);
    }
}
