//      !!DEPRICATED!!      //
//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 07/02/2022                        ///
/// Edited By: Henry Northway                  ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro; //Henry Addition
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.UI; //Henry Addition
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    //Panels
    [SerializeField]
    GameObject m_mainMenuPanel;
    [SerializeField]
    GameObject m_profileMenuPanel;
    [SerializeField]
    GameObject m_onlineMenuPanel;
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
    GameObject m_readyText;
    //Henry Additions
    [SerializeField]
    private TextMeshProUGUI m_panelTitle;
    [SerializeField]
    private Button m_activeButton;
    [SerializeField]
    private InputField m_activeInputField;

    //Info
    private bool m_isReady = false;

    private void Start()
    {
        PersistentInfo.Instance.m_currentPlayerName = "Guest";
    }

    public void OnProfileButton()
    {
        m_profileMenuPanel.SetActive(true);
        //m_mainMenuPanel.SetActive(false);
        SetActiveSelection("MAIN MENU", true); //Henry Addition
    }
    public void OnOnlineButton()
    {
        m_onlineMenuPanel.SetActive(true);
        m_mainMenuPanel.SetActive(false);
        SetActiveSelection("ONLINE", false); //Henry Addition
    }
    public void OnNameInputField(GameObject a_inputField)
    {
        //name set to persitent info for storage
        string text = a_inputField.GetComponent<UnityEngine.UI.Text>().text;
        if (text !=  null && text.Length != 0)
        {
            PersistentInfo.Instance.m_currentPlayerName = a_inputField.GetComponent<UnityEngine.UI.Text>().text;
        }
        else
        {
            PersistentInfo.Instance.m_currentPlayerName = "Guest";
        }
    }
    public void OnProfileCustomiseButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnProfileBackButton()
    {
        //m_mainMenuPanel.SetActive(true);
        m_profileMenuPanel.SetActive(false);
        SetActiveSelection("MAIN MENU", false); //Henry Addition
    }
    public void OnHostButton(GameObject a_inputField)
    {
        //send server data to menu server
        ServerHostStart serverHostStart = new ServerHostStart();
        serverHostStart.m_ServerName = a_inputField.GetComponent<UnityEngine.UI.Text>().text;
        serverHostStart.m_ServerIP = GetLocalIPv4();
        MenuClient.Instance.SendToServer(serverHostStart);
        //initliser server and client
        Server.Instance.Initlialise(8008);
        Client.Instance.m_clientName = PersistentInfo.Instance.m_currentPlayerName;
        //connect to self
        Client.Instance.Initlialise("127.0.0.1", 8008);
        m_connectingPanel.SetActive(true);
        m_onlineMenuPanel.SetActive(false);
        SetActiveSelection("ONLINE", false); //Henry Addition
    }
    public void OnConnectButton(GameObject a_inputField)
    {
        //connec to enter direct address
        Client.Instance.m_clientName = PersistentInfo.Instance.m_currentPlayerName;
        Client.Instance.Initlialise(a_inputField.GetComponent<UnityEngine.UI.Text>().text, 8008);
        m_connectingPanel.SetActive(true);
        m_onlineMenuPanel.SetActive(false);
        SetActiveSelection("ONLINE", false); //Henry Addition
    }
    public void OnServersButton()
    {
        m_serversPanel.SetActive(true);
        m_onlineMenuPanel.SetActive(false);
        SetActiveSelection("ONLINE", false); //Henry Addition
    }
    public void OnOnlineBackButton()
    {
        m_mainMenuPanel.SetActive(true);
        m_onlineMenuPanel.SetActive(false);
        SetActiveSelection("MAIN MENU", false); //Henry Addition
    }
    public void OnServersBackButton()
    {
        m_onlineMenuPanel.SetActive(true);
        m_serversPanel.SetActive(false);
        SetActiveSelection("ONLINE", false); //Henry Addition
    }
    public void OnConnectingCanelButton()
    {
        Client.Instance.Shutdown();
        m_onlineMenuPanel.SetActive(true);
        m_connectingPanel.SetActive(false);
        SetActiveSelection("ONLINE", false); //Henry Addition
    }
    public void OnConnectedDisconnectButton()
    {
        Client.Instance.Shutdown();
        foreach (GameObject text in m_connectedOtherTexts)
        {
            text.GetComponent<UnityEngine.UI.Text>().text = "Empty";
        }
        PersistentInfo.Instance.Clear();
        m_onlineMenuPanel.SetActive(true);
        m_connectedPanel.SetActive(false);
        SetActiveSelection("ONLINE", false); //Henry Addition
    }
    public void OnConnectionFailedBackButton()
    {
        Client.Instance.Shutdown();
        m_onlineMenuPanel.SetActive(true);
        m_connectionFailedPanel.SetActive(false);
        SetActiveSelection("ONLINE", false); //Henry Addition
    }
    public void OnCloseServerButton()
    {
        //send server end message
        ServerHostEnd serverHostEnd = new ServerHostEnd();
        serverHostEnd.m_ServerIP = GetLocalIPv4();
        MenuClient.Instance.SendToServer(serverHostEnd);
        //shutdown server and client
        Client.Instance.Shutdown();
        Server.Instance.Shutdown();
        foreach (GameObject text in m_connectedOtherTexts)
        {
            text.GetComponent<UnityEngine.UI.Text>().text = "Empty";
        }
        m_onlineMenuPanel.SetActive(true);
        m_connectedPanel.SetActive(false);
        SetActiveSelection("ONLINE", false); //Henry Addition
    }
    public void OnStartGameButton()
    {
        Server.Instance.Broadcast(new NetStartGame());
    }
    public void OnReadyButton()
    {
        //ready toggle - when all are ready gmae countdown will trigger
        if (!m_isReady)
        {
            m_readyText.GetComponent<TextMeshPro>().text = "Unready";
            NetMenuCountdown netMenuCountdown = new NetMenuCountdown();
            netMenuCountdown.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
            netMenuCountdown.m_Action = NetMenuCountdown.ACTION.READY;
            Client.Instance.SendToServer(netMenuCountdown);
        }
        else
        {
            m_readyText.GetComponent<TextMeshPro>().text = "Ready Up";
            NetMenuCountdown netMenuCountdown = new NetMenuCountdown();
            netMenuCountdown.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
            netMenuCountdown.m_Action = NetMenuCountdown.ACTION.UNREADY;
            Client.Instance.SendToServer(netMenuCountdown);
        }
        m_isReady = !m_isReady;
    }

    //get ipv4 address used to show host address
    string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
    }
    private void OnDestroy()
    {
        //closed server on close (prevents server being listed when game closes)
        if (PersistentInfo.Instance.m_currentPlayerNum == 1)
        {
            ServerHostEnd serverHostEnd = new ServerHostEnd();
            serverHostEnd.m_ServerIP = GetLocalIPv4();
            MenuClient.Instance.SendToServer(serverHostEnd);
        }
    }

    //Henry Addition
    public void OnOnlinePreviewButton()
    {
        SetActiveSelection("ONLINE", true); //Henry Addition
    }

    private void SetActiveSelection(string title, bool isInputField)
    {
        m_panelTitle.text = title;

        if (isInputField)
        {
            m_activeInputField = FindObjectOfType<InputField>();
            m_activeInputField.Select();
        }
        else
        {
            m_activeButton = FindObjectOfType<Button>();
            m_activeButton.Select();
        }
    }
}
