using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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
    [SerializeField]
    GameObject m_connectionFailedPanel;
    //Info
    [SerializeField]
    GameObject m_connectedPlayerText;
    [SerializeField]
    GameObject[] m_connectedOtherTexts;
    [SerializeField]
    GameObject m_connectionFailedReasonText;
    [SerializeField]
    GameObject[] m_startUI;
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
    //Variables
    [SerializeField]
    float m_startingTimer;
    //Henry Addition
    [SerializeField]
    MainMenuManager m_mainMenuManager;

    private bool m_startCountdown = false;
    private float m_timer = 0;

    private void Start()
    {
        Time.timeScale = 1;
        string path = "/../ServerAdress";
        if (File.Exists(path))
        {
            StreamReader stream = new StreamReader(path);
            string address = stream.ReadLine();
            MenuClient.Instance.Initlialise(address, 8009);
        }
        else
        {
            MenuClient.Instance.Initlialise(m_severAdress, 8009);
        }
        m_startUI = PersistentInfo.Instance.m_CountdownUI;
    }

    private void Update()
    {
        if (PersistentInfo.Instance.m_currentPlayerNum == 1 && !m_startCountdown)
        {
            if (PersistentInfo.Instance.m_readyCars == PersistentInfo.Instance.m_connectedUsers)
            {
                m_startCountdown = true;
                m_timer = 0;
            }
        }
        if (m_startCountdown)
        {
            m_timer += Time.deltaTime;
            if (m_timer < m_startingTimer)
            {
                NetMenuCountdown netMenuCountdown = new NetMenuCountdown();
                netMenuCountdown.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                netMenuCountdown.m_Action = NetMenuCountdown.ACTION.COUNTING;
                netMenuCountdown.m_Count = m_timer;
                Client.Instance.SendToServer(netMenuCountdown);
            }
            else
            {
                m_startCountdown = false;
                NetMenuCountdown netMenuCountdown = new NetMenuCountdown();
                netMenuCountdown.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                netMenuCountdown.m_Action = NetMenuCountdown.ACTION.GO;
                netMenuCountdown.m_Count = m_timer;
                Client.Instance.SendToServer(netMenuCountdown);
            }
        }
    }

    void Awake()
    {
        RegisterEvenets();
    }

    void RegisterEvenets()
    {
        //Server
        NetUtility.S_WELCOME += OnWelcomeServer;
        NetUtility.S_MENU_COUNTDOWN += OnMenuCountdownServer;
        NetUtility.S_CUSTOMISER_UPDATE += OnCustomiserUpdateServer;

        //Client
        NetUtility.C_WELCOME += OnWelcomeClient;
        NetUtility.C_UNWELCOME += OnUnwelcomeClient;
        NetUtility.C_OTHER_CONNECTED += OnOtherConnectedClient;
        NetUtility.C_OTHER_DISCONNECTED += OnOtherDisconnectedClient;
        NetUtility.C_START_GAME += OnStartGameClient;
        NetUtility.C_MENU_COUNTDOWN += OnMenuCountdownClient;
        NetUtility.C_CUSTOMISER_UPDATE += OnCustomiserUpdateClient;

        //Menu Client
        ServerUtility.C_SERVER_START += OnServerStart;
        ServerUtility.C_SERVER_END += OnServerEnd;
        ServerUtility.C_LIST_REQUEST += OnListRequest;
    }
    void UnregisterEvenets()
    {
        //Server
        NetUtility.S_WELCOME -= OnWelcomeServer;
        NetUtility.S_MENU_COUNTDOWN -= OnMenuCountdownServer;
        NetUtility.S_CUSTOMISER_UPDATE -= OnCustomiserUpdateServer;

        //Client
        NetUtility.C_WELCOME -= OnWelcomeClient;
        NetUtility.C_UNWELCOME -= OnUnwelcomeClient;
        NetUtility.C_OTHER_CONNECTED -= OnOtherConnectedClient;
        NetUtility.C_OTHER_DISCONNECTED -= OnOtherDisconnectedClient;
        NetUtility.C_START_GAME -= OnStartGameClient;
        NetUtility.C_MENU_COUNTDOWN -= OnMenuCountdownClient;
        NetUtility.C_CUSTOMISER_UPDATE -= OnCustomiserUpdateClient;

        //Menu Client
        ServerUtility.C_SERVER_START -= OnServerStart;
        ServerUtility.C_SERVER_END -= OnServerEnd;
        ServerUtility.C_LIST_REQUEST -= OnListRequest;
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
            netOthersConnected.m_CarBody = PersistentInfo.Instance.m_carDesigns[i].m_carChoice;
            netOthersConnected.m_CarWheels = PersistentInfo.Instance.m_carDesigns[i].m_wheelChoice;
            netOthersConnected.m_CarGun = PersistentInfo.Instance.m_carDesigns[i].m_gunChoice;
            Server.Instance.SendToClient(a_connection, netOthersConnected);
        }

        netWelcome.m_PlayerCount = PersistentInfo.Instance.m_connectedUsers;
        netWelcome.m_PlayerNumber = PersistentInfo.Instance.m_connectedUsers;
        netWelcome.m_levelNum = PersistentInfo.Instance.m_levelNum;
        Server.Instance.SendToClient(a_connection, netWelcome);
        NetOtherConnected netOtherConnected = new NetOtherConnected();
        netOtherConnected.m_PlayerCount = PersistentInfo.Instance.m_connectedUsers;
        netOtherConnected.m_PlayerName = netWelcome.m_PlayerName;
        netOtherConnected.m_CarBody = netWelcome.m_CarBody;
        netOtherConnected.m_CarWheels = netWelcome.m_CarWheels;
        netOtherConnected.m_CarGun = netWelcome.m_CarGun;
        Server.Instance.SendToOtherClients(a_connection, netOtherConnected);
    }
    void OnMenuCountdownServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetMenuCountdown netMenuCountdown = a_msg as NetMenuCountdown;
        Server.Instance.Broadcast(netMenuCountdown);
    }
    void OnCustomiserUpdateServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetCustomiserUpdate netCustomiserUpdate = a_msg as NetCustomiserUpdate;
        PersistentInfo.Instance.m_carDesigns[netCustomiserUpdate.m_Player - 1].m_carChoice = netCustomiserUpdate.m_CarBody;
        PersistentInfo.Instance.m_carDesigns[netCustomiserUpdate.m_Player - 1].m_wheelChoice = netCustomiserUpdate.m_CarWheels;
        PersistentInfo.Instance.m_carDesigns[netCustomiserUpdate.m_Player - 1].m_gunChoice = netCustomiserUpdate.m_CarGun;

        Server.Instance.Broadcast(netCustomiserUpdate);
    }

    //Client
    void OnWelcomeClient(NetMessage a_msg)
    {
        NetWelcome netWelcome = a_msg as NetWelcome;
        PersistentInfo.Instance.m_connectedUsers = netWelcome.m_PlayerCount;
        PersistentInfo.Instance.m_currentPlayerNum = netWelcome.m_PlayerNumber;
        PersistentInfo.Instance.m_connectedNames.Add(netWelcome.m_PlayerName);
        CarDesigns carDesign = new CarDesigns();
        carDesign.m_carChoice = netWelcome.m_CarBody;
        carDesign.m_wheelChoice = netWelcome.m_CarWheels;
        carDesign.m_gunChoice = netWelcome.m_CarGun;
        PersistentInfo.Instance.m_carDesigns.Add(carDesign);
        PersistentInfo.Instance.m_levelNum = netWelcome.m_levelNum;

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

        m_mainMenuManager.SetActiveMenu(7);

        //m_connectedPanel.SetActive(true);
        //m_connectingPanel.SetActive(false);
    }
    void OnUnwelcomeClient(NetMessage a_msg)
    {
        NetUnwelcome netUnwelcome = a_msg as NetUnwelcome;
        NetUnwelcome.REASON reason = netUnwelcome.m_Reason;

        m_mainMenuManager.SetActiveMenu(6);

        //m_connectionFailedPanel.SetActive(true);
        //m_connectingPanel.SetActive(false);

        switch (reason)
        {
            case NetUnwelcome.REASON.FULL:
                m_connectionFailedReasonText.GetComponent<UnityEngine.UI.Text>().text = "Server Full";
                break;
            case NetUnwelcome.REASON.SERVER_CLOSE:
                m_connectionFailedReasonText.GetComponent<UnityEngine.UI.Text>().text = "Server Closed";
                break;
            default:
                Debug.LogError("Unrecognised reason for unwelcome");
                break;
        }
    }

    void OnOtherConnectedClient(NetMessage a_msg)
    {
        NetOtherConnected netOtherConnected = a_msg as NetOtherConnected;
        PersistentInfo.Instance.m_connectedUsers = netOtherConnected.m_PlayerCount;
        PersistentInfo.Instance.m_connectedNames.Add(netOtherConnected.m_PlayerName);
        CarDesigns carDesign = new CarDesigns();
        carDesign.m_carChoice = netOtherConnected.m_CarBody;
        carDesign.m_wheelChoice = netOtherConnected.m_CarWheels;
        carDesign.m_gunChoice = netOtherConnected.m_CarGun;
        PersistentInfo.Instance.m_carDesigns.Add(carDesign);

        m_connectedPlayerText.GetComponent<UnityEngine.UI.Text>().text = $"You are player {PersistentInfo.Instance.m_currentPlayerNum} of {PersistentInfo.Instance.m_connectedUsers}";
        for (int i = 0; i < PersistentInfo.Instance.m_connectedNames.Count; i++)
        {
            m_connectedOtherTexts[i].GetComponent<UnityEngine.UI.Text>().text = $"{PersistentInfo.Instance.m_connectedNames[i]}";
        }
    }
    void OnOtherDisconnectedClient(NetMessage a_msg)
    {
        NetOtherDisconnected netOtherDisconnected = a_msg as NetOtherDisconnected;
        PersistentInfo.Instance.m_connectedUsers--;
        if (PersistentInfo.Instance.m_currentPlayerNum > (netOtherDisconnected.m_PlayerNum + 1))
        {
            PersistentInfo.Instance.m_currentPlayerNum--;
        }
        PersistentInfo.Instance.m_connectedNames.RemoveAt(netOtherDisconnected.m_PlayerNum);
        PersistentInfo.Instance.m_carDesigns.RemoveAt(netOtherDisconnected.m_PlayerNum);

        m_connectedPlayerText.GetComponent<UnityEngine.UI.Text>().text = $"You are player {PersistentInfo.Instance.m_currentPlayerNum} of {PersistentInfo.Instance.m_connectedUsers}";
        for (int i = 0; i < PersistentInfo.Instance.m_connectedNames.Count; i++)
        {
            m_connectedOtherTexts[i].GetComponent<UnityEngine.UI.Text>().text = $"{PersistentInfo.Instance.m_connectedNames[i]}";
        }
        for (int i = PersistentInfo.Instance.m_connectedNames.Count; i < m_connectedOtherTexts.Length; i++)
        {
            m_connectedOtherTexts[i].GetComponent<UnityEngine.UI.Text>().text = "Empty";
        }
    }

    void OnStartGameClient(NetMessage a_msg)
    {
        StartGame();
    }
    void OnMenuCountdownClient(NetMessage a_msg)
    {
        NetMenuCountdown netMenuCountdown = a_msg as NetMenuCountdown;
        switch (netMenuCountdown.m_Action)
        {
            case NetMenuCountdown.ACTION.READY:
                PersistentInfo.Instance.m_readyCars++;
                break;
            case NetMenuCountdown.ACTION.UNREADY:
                PersistentInfo.Instance.m_readyCars--;
                break;
            case NetMenuCountdown.ACTION.COUNTING:
                if (netMenuCountdown.m_Count < 1.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 0)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else if (netMenuCountdown.m_Count < 2.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 1)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else if (netMenuCountdown.m_Count < 3.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 2)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else if (netMenuCountdown.m_Count < 4.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 3)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else if (netMenuCountdown.m_Count < 5.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 4)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else if (netMenuCountdown.m_Count < 6.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 5)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else if (netMenuCountdown.m_Count < 7.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 6)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else if (netMenuCountdown.m_Count < 8.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 7)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else if (netMenuCountdown.m_Count < 9.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 8)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else if (netMenuCountdown.m_Count < 10.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 9)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else if (netMenuCountdown.m_Count < 11.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 10)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                break;
            case NetMenuCountdown.ACTION.GO:
                for (int i = 0; i < m_startUI.Length; i++)
                {
                    if (i != 10)
                    {
                        m_startUI[i].SetActive(false);
                    }
                    else
                    {
                        m_startUI[i].SetActive(true);
                    }
                }
                for (int i = 0; i < m_startUI.Length; i++)
                {
                    m_startUI[i].SetActive(false);
                }
                m_startUI = new GameObject[0];
                StartGame();
                break;
            default:
                break;
        }
    }
    void OnCustomiserUpdateClient(NetMessage a_msg)
    {
        NetCustomiserUpdate netCustomiserUpdateIn = a_msg as NetCustomiserUpdate;
        PersistentInfo.Instance.m_carDesigns[netCustomiserUpdateIn.m_Player - 1].m_carChoice = netCustomiserUpdateIn.m_CarBody;
        PersistentInfo.Instance.m_carDesigns[netCustomiserUpdateIn.m_Player - 1].m_wheelChoice = netCustomiserUpdateIn.m_CarWheels;
        PersistentInfo.Instance.m_carDesigns[netCustomiserUpdateIn.m_Player - 1].m_gunChoice = netCustomiserUpdateIn.m_CarGun;
    }

    //Menu Client
    void OnServerStart(ServerMessage a_msg)
    {
        ServerHostStart serverStart = a_msg as ServerHostStart;
        m_serverList.GetComponent<ServerListManager>().AddServer(serverStart.m_ServerName, serverStart.m_ServerIP, serverStart.m_level);
    }
    void OnServerEnd(ServerMessage a_msg)
    {
        ServerHostEnd serverEnd = a_msg as ServerHostEnd;
        m_serverList.GetComponent<ServerListManager>().RemoveServer(serverEnd.m_ServerIP);
    }
    void OnListRequest(ServerMessage a_msg)
    {
        ServerListRequest serverListRequest = a_msg as ServerListRequest;
        m_serverList.GetComponent<ServerListManager>().AddServer(serverListRequest.m_ServerName, serverListRequest.m_ServerIP, serverListRequest.m_level);
    }

    //Utilities
    public void ConnectToServer(string a_adress)
    {
        Client.Instance.m_clientName = PersistentInfo.Instance.m_currentPlayerName;
        Client.Instance.Initlialise(a_adress, 8008);
        //m_connectingPanel.SetActive(true);
        //m_serversPanel.SetActive(false);
    }
    public void StartGame()
    {
        PersistentInfo.Instance.m_readyCars = 0;
        switch (PersistentInfo.Instance.m_levelNum)
        {
            case 0:
                SceneManager.LoadScene(2);
                break;
            case 1:
                SceneManager.LoadScene(3);
                break;
            case 2:
                SceneManager.LoadScene(4); //change for new levels
                break;
        }
    }

    private void OnDestroy()
    {
        UnregisterEvenets();
    }
}