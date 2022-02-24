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
    [SerializeField]
    GameObject[] m_connectedOtherTexts;
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
        NetUtility.C_OTHER_CONNECTED += OnOtherConnectedClient;
        NetUtility.C_START_GAME += OnStartGameClient;

        //Menu Client
        ServerUtility.C_SERVER_START += OnServerStart;
        ServerUtility.C_SERVER_END += OnServerEnd;
        ServerUtility.C_LIST_REQUEST += OnListRequest;
    }
    void UnregisterEvenets()
    {

    }

    //Server
    void OnWelcomeServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetWelcome netWelcome = a_msg as NetWelcome;
        PersistentInfo.Instance.m_connectedUsers++;

        for (int i = 0; i < PersistentInfo.Instance.m_connectedNames.Count; i++)
        {
            NetOtherConnected netOthersConnected = new NetOtherConnected();
            netOthersConnected.m_PlayerCount = PersistentInfo.Instance.m_connectedUsers;
            netOthersConnected.m_PlayerName = PersistentInfo.Instance.m_connectedNames[i];
            Server.Instance.SendToClient(a_connection, netOthersConnected);
        }

        netWelcome.m_PlayerCount = PersistentInfo.Instance.m_connectedUsers;
        netWelcome.m_PlayerNumber = PersistentInfo.Instance.m_connectedUsers;
        Server.Instance.SendToClient(a_connection, netWelcome);
        NetOtherConnected netOtherConnected = new NetOtherConnected();
        netOtherConnected.m_PlayerCount = PersistentInfo.Instance.m_connectedUsers;
        netOtherConnected.m_PlayerName = netWelcome.m_PlayerName;
        Server.Instance.SendToOtherClients(a_connection, netOtherConnected);
    }

    //Client
    void OnWelcomeClient(NetMessage a_msg)
    {
        NetWelcome netWelcome = a_msg as NetWelcome;
        PersistentInfo.Instance.m_connectedUsers = netWelcome.m_PlayerCount;
        PersistentInfo.Instance.m_currentPlayerNum = netWelcome.m_PlayerNumber;
        PersistentInfo.Instance.m_connectedNames.Add(netWelcome.m_PlayerName);

        m_connectedPlayerText.GetComponent<UnityEngine.UI.Text>().text = $"You are player {PersistentInfo.Instance.m_currentPlayerNum} of {PersistentInfo.Instance.m_connectedUsers}";
        for (int i = 0; i < PersistentInfo.Instance.m_connectedNames.Count; i++)
        {
            m_connectedOtherTexts[i].GetComponent<UnityEngine.UI.Text>().text = $"{PersistentInfo.Instance.m_connectedNames[i]}";
        }

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
    void OnOtherConnectedClient(NetMessage a_msg)
    {
        NetOtherConnected netOtherConnected = a_msg as NetOtherConnected;
        PersistentInfo.Instance.m_connectedUsers = netOtherConnected.m_PlayerCount;
        PersistentInfo.Instance.m_connectedNames.Add(netOtherConnected.m_PlayerName);

        m_connectedPlayerText.GetComponent<UnityEngine.UI.Text>().text = $"You are player {PersistentInfo.Instance.m_currentPlayerNum} of {PersistentInfo.Instance.m_connectedUsers}";
        for (int i = 0; i < PersistentInfo.Instance.m_connectedNames.Count; i++)
        {
            m_connectedOtherTexts[i].GetComponent<UnityEngine.UI.Text>().text = $"{PersistentInfo.Instance.m_connectedNames[i]}";
        }
    }
    void OnStartGameClient(NetMessage a_msg)
    {
        SceneManager.LoadScene(1);
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
    void OnListRequest(ServerMessage a_msg)
    {
        ServerListRequest serverListRequest = a_msg as ServerListRequest;
        m_serverList.GetComponent<ServerListManager>().AddServer(serverListRequest.m_ServerName, serverListRequest.m_ServerIP);
    }

    //Utilities
    public void ConnectToServer(string a_adress)
    {
        Client.Instance.m_clientName = PersistentInfo.Instance.m_currentPlayerName;
        Client.Instance.Initlialise(a_adress, 8008);
        m_connectingPanel.SetActive(true);
        m_serversPanel.SetActive(false);
    }
}
