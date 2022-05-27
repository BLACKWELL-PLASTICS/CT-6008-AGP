//////////////////////////////////////////////////
/// Created: 07/02/2022                        ///
/// Author: Iain Farlow                        ///
/// Edited By:                                 ///
/// Last Edited: 07/02/2022                    ///
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
        //set instance
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
        //create driver
        m_driver = NetworkDriver.Create();
        //set ip
        NetworkEndPoint endPoint = NetworkEndPoint.Parse(a_ip, a_port);

        m_connection = m_driver.Connect(endPoint);

        Debug.Log("Attempting to connect to Server at: " + endPoint.Address);

        m_isActive = true;
        //register events
        RegisterToEvent();
    }
    public void Shutdown(bool a_clearPI = true)
    {
        //clear server stuff
        if (m_isActive)
        {
            if (a_clearPI)
            {
                //if told to clear persistent info
                PersistentInfo.Instance.Clear();
            }
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
        //keep alive sends message to ensure that there is not a timeout on the server
        m_driver.ScheduleUpdate().Complete();
        //ensure the client is still okay
        CheckAlive();
        if (m_driver.IsCreated)
        {
            UpdateMessagePump();
        }
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
        if (m_connection == default(NetworkConnection))
        {
            return;
        }
        while ((cmd = m_connection.PopEvent(m_driver, out stream)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect)
            {
                //wehn connected
                //send clients info
                NetWelcome netWelcome = new NetWelcome();
                netWelcome.m_PlayerNumber = 0;
                netWelcome.m_PlayerName = m_clientName;
                netWelcome.m_CarBody = PersistentInfo.Instance.m_carDesign.m_carChoice;
                netWelcome.m_CarWheels = PersistentInfo.Instance.m_carDesign.m_wheelChoice;
                netWelcome.m_CarGun = PersistentInfo.Instance.m_carDesign.m_gunChoice;
                SendToServer(netWelcome);
                Debug.Log("Connected to Game Server");
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                //proccess data
                NetUtility.OnData(stream, default(NetworkConnection));
            }
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
                //disconnect. Loads into main scene
                Debug.Log("Client got disconnected from server");
                m_connection = default(NetworkConnection);
                m_connectionDropped?.Invoke();
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                Shutdown();
            }
        }
    }

    public void SendToServer(NetMessage a_msg)
    {
        //datastream 
        DataStreamWriter writer;
        writer = m_driver.BeginSend(m_connection, 0);
        a_msg.Serialize(ref writer);
        m_driver.EndSend(writer);
    }
    //keep aqlives are to ensure drop doesnt happen
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
