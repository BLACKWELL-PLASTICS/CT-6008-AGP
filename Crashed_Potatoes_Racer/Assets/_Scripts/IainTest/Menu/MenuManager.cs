using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Menu Server
    [SerializeField]
    string m_severAdress;
    //Panels
    [SerializeField]
    GameObject m_serversPanel;
    [SerializeField]
    GameObject m_connectingPanel;
    [SerializeField]
    GameObject m_connectedPanel;
    //Info
    [SerializeField]
    GameObject m_connectedPlayerText;
    //Host Specific Fields
    [SerializeField]
    GameObject m_connectedDisconnectButton;
    [SerializeField]
    GameObject m_connectedCloseServerButton;
    [SerializeField]
    GameObject m_connectedStartButton;
    //Server List
    [SerializeField]
    GameObject m_serverList;

    private void Start()
    {
        MenuClient.Instance.Initlialise(m_severAdress, 8009);
    }

    void Awake()
    {
        RegisterEvenets();
    }

    void RegisterEvenets()
    {
        //Server
        NetUtility.S_WELCOME += OnWelcomeServer;

        //Client
        NetUtility.C_WELCOME += OnWelcomeClient;
        NetUtility.C_START_GAME += OnStartGameClient;

        //Menu Client
        ServerUtility.C_SERVER_START += OnServerStart;
        ServerUtility.C_SERVER_END += OnServerEnd;
    }
    void UnregisterEvenets()
    {

    }

    //Server
    void OnWelcomeServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetWelcome netWelcome = a_msg as NetWelcome;
        PersistentInfo.Instance.m_connectedUsers++;
        netWelcome.m_PlayerCount = PersistentInfo.Instance.m_connectedUsers;
        netWelcome.m_PlayerNumber = PersistentInfo.Instance.m_connectedUsers;
        //PersistentInfo.Instance.m_connectedNames.Add(netWelcome.m_PlayerName);
        Server.Instance.SendToClient(a_connection, netWelcome);
    }

    //Client
    void OnWelcomeClient(NetMessage a_msg)
    {
        NetWelcome netWelcome = a_msg as NetWelcome;
        PersistentInfo.Instance.m_connectedUsers = netWelcome.m_PlayerCount;
        PersistentInfo.Instance.m_currentPlayerNum = netWelcome.m_PlayerNumber;
        //PersistentInfo.Instance.m_connectedNames.Add(netWelcome.m_PlayerName);

        m_connectedPlayerText.GetComponent<UnityEngine.UI.Text>().text = $"You are player {PersistentInfo.Instance.m_currentPlayerNum} of {PersistentInfo.Instance.m_connectedUsers}";

        if (PersistentInfo.Instance.m_currentPlayerNum == 1)
        {
            m_connectedDisconnectButton.SetActive(false);
            m_connectedCloseServerButton.SetActive(true);
            m_connectedStartButton.SetActive(true);
        }
        else
        {
            m_connectedDisconnectButton.SetActive(true);
            m_connectedCloseServerButton.SetActive(false);
            m_connectedStartButton.SetActive(false);
        }

        m_connectedPanel.SetActive(true);
        m_connectingPanel.SetActive(false);
    }
    void OnStartGameClient(NetMessage a_msg)
    {
        SceneManager.LoadScene(1);
    }

    public void ConnectToServer(string a_adress)
    {
        Client.Instance.m_clientName = PersistentInfo.Instance.m_currentPlayerName;
        Client.Instance.Initlialise(a_adress, 8008);
        m_connectingPanel.SetActive(true);
        m_serversPanel.SetActive(false);
    }

    //Menu Client
    void OnServerStart(ServerMessage a_msg)
    {
        ServerHostStart serverStart = a_msg as ServerHostStart;
        m_serverList.GetComponent<ServerListManager>().AddServer(serverStart.m_ServerName, serverStart.m_ServerIP);
    }
    void OnServerEnd(ServerMessage a_msg)
    {
        ServerHostEnd serverEnd = a_msg as ServerHostEnd;
        m_serverList.GetComponent<ServerListManager>().RemoveServer(serverEnd.m_ServerIP);
    }
}
