using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.Networking.Transport;
using UnityEngine;

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

    private void Start()
    {
        PersistentInfo.Instance.m_currentPlayerName = "Guest";
    }

    public void OnProfileButton()
    {
        m_profileMenuPanel.SetActive(true);
        m_mainMenuPanel.SetActive(false);
    }
    public void OnOnlineButton()
    {
        m_onlineMenuPanel.SetActive(true);
        m_mainMenuPanel.SetActive(false);
    }
    public void OnNameInputField(GameObject a_inputField)
    {
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
    public void OnProfileBackButton()
    {
        m_mainMenuPanel.SetActive(true);
        m_profileMenuPanel.SetActive(false);
    }
    public void OnHostButton(GameObject a_inputField)
    {
        ServerHostStart serverHostStart = new ServerHostStart();
        serverHostStart.m_ServerName = a_inputField.GetComponent<UnityEngine.UI.Text>().text;
        serverHostStart.m_ServerIP = GetLocalIPv4();
        MenuClient.Instance.SendToServer(serverHostStart);
        Server.Instance.Initlialise(8008);
        Client.Instance.m_clientName = PersistentInfo.Instance.m_currentPlayerName;
        Client.Instance.Initlialise("127.0.0.1", 8008);
        m_connectingPanel.SetActive(true);
        m_onlineMenuPanel.SetActive(false);
    }
    public void OnConnectButton(GameObject a_inputField)
    {
        Client.Instance.m_clientName = PersistentInfo.Instance.m_currentPlayerName;
        Client.Instance.Initlialise(a_inputField.GetComponent<UnityEngine.UI.Text>().text, 8008);
        m_connectingPanel.SetActive(true);
        m_onlineMenuPanel.SetActive(false);
    }
    public void OnServersButton()
    {
        m_serversPanel.SetActive(true);
        m_onlineMenuPanel.SetActive(false);
    }
    public void OnOnlineBackButton()
    {
        m_mainMenuPanel.SetActive(true);
        m_onlineMenuPanel.SetActive(false);
    }
    public void OnServersBackButton()
    {
        m_onlineMenuPanel.SetActive(true);
        m_serversPanel.SetActive(false);
    }
    public void OnConnectingCanelButton()
    {
        Client.Instance.Shutdown();
        m_onlineMenuPanel.SetActive(true);
        m_connectingPanel.SetActive(false);
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
    }
    public void OnConnectionFailedBackButton()
    {
        Client.Instance.Shutdown();
        m_onlineMenuPanel.SetActive(true);
        m_connectionFailedPanel.SetActive(false);
    }
    public void OnCloseServerButton()
    {
        ServerHostEnd serverHostEnd = new ServerHostEnd();
        serverHostEnd.m_ServerIP = GetLocalIPv4();
        MenuClient.Instance.SendToServer(serverHostEnd);
        Client.Instance.Shutdown();
        Server.Instance.Shutdown();
        foreach (GameObject text in m_connectedOtherTexts)
        {
            text.GetComponent<UnityEngine.UI.Text>().text = "Empty";
        }
        m_onlineMenuPanel.SetActive(true);
        m_connectedPanel.SetActive(false);
    }
    public void OnStartGameButton()
    {
        Server.Instance.Broadcast(new NetStartGame());
    }

    string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
    }
    private void OnDestroy()
    {
        if (PersistentInfo.Instance.m_currentPlayerNum == 1)
        {
            ServerHostEnd serverHostEnd = new ServerHostEnd();
            serverHostEnd.m_ServerIP = GetLocalIPv4();
            MenuClient.Instance.SendToServer(serverHostEnd);
        }
    }
}
