using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    //Info
    [SerializeField]
    GameObject m_connectedPlayerText;
    [SerializeField]
    GameObject[] m_connectedOtherTexts;
    [SerializeField]
    GameObject m_readyText;

    private InputField m_activeInputField;

    private bool m_isReady = false;

    private void Start()
    {
        PersistentInfo.Instance.m_currentPlayerName = "Guest";
    }

    public void OnNameInputField(GameObject a_inputField)
    {
        string text = a_inputField.GetComponent<UnityEngine.UI.Text>().text;
        if (text != null && text.Length != 0)
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
        // disable raycast
        foreach (var raycaster in Transform.FindObjectsOfType<GraphicRaycaster>())
        {
            raycaster.enabled = false;
        }
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void OnSelectCourseButton(int a_level)
    {
        PersistentInfo.Instance.m_levelNum = a_level;

        //m_activeInputField = FindObjectOfType<InputField>();
        //m_activeInputField.Select();
    }

    public void OnHostButton(GameObject a_inputField)
    {
        ServerHostStart serverHostStart = new ServerHostStart();
        serverHostStart.m_ServerName = a_inputField.GetComponent<UnityEngine.UI.Text>().text;
        serverHostStart.m_ServerIP = GetLocalIPv4();
        serverHostStart.m_level = PersistentInfo.Instance.m_levelNum;
        MenuClient.Instance.SendToServer(serverHostStart);
        Server.Instance.Initlialise(8008);
        Client.Instance.m_clientName = PersistentInfo.Instance.m_currentPlayerName;
        Client.Instance.Initlialise("127.0.0.1", 8008);
        MenuClient.Instance.m_IsHost = true;
    }

    public void OnConnectButton(GameObject a_inputField)
    {
        Client.Instance.m_clientName = PersistentInfo.Instance.m_currentPlayerName;
        Client.Instance.Initlialise(a_inputField.GetComponent<UnityEngine.UI.Text>().text, 8008);
    }

    public void OnConnectingCanelButton()
    {
        Client.Instance.Shutdown();
    }

    public void OnConnectedDisconnectButton()
    {
        Client.Instance.Shutdown();
        foreach (GameObject text in m_connectedOtherTexts)
        {
            text.GetComponent<UnityEngine.UI.Text>().text = "Empty";
        }
        PersistentInfo.Instance.Clear();
    }

    public void OnConnectionFailedBackButton()
    {
        Client.Instance.Shutdown();
    }

    public void OnCloseServerButton()
    {
        ServerHostEnd serverHostEnd = new ServerHostEnd();
        serverHostEnd.m_ServerIP = GetLocalIPv4();
        MenuClient.Instance.SendToServer(serverHostEnd);
        NetUnwelcome netUnwelcome = new NetUnwelcome();
        netUnwelcome.m_Reason = NetUnwelcome.REASON.SERVER_CLOSE;
        Server.Instance.Broadcast(netUnwelcome);
        Client.Instance.Shutdown();
        Server.Instance.Shutdown();
        foreach (GameObject text in m_connectedOtherTexts)
        {
            text.GetComponent<UnityEngine.UI.Text>().text = "Empty";
        }
        MenuClient.Instance.m_IsHost = false;
    }

    public void OnStartGameButton()
    {
        Server.Instance.Broadcast(new NetStartGame());
    }
    public void OnSingleplayerButton()
    {
        Server.Instance.Initlialise(8008);
        Client.Instance.m_clientName = PersistentInfo.Instance.m_currentPlayerName;
        Client.Instance.Initlialise("127.0.0.1", 8008);
        MenuClient.Instance.m_IsHost = true;
        PersistentInfo.Instance.m_singleplayer = true;
    }
    public void OnReadyButton()
    {
        if (!m_isReady)
        {
            m_readyText.GetComponent<TMPro.TextMeshProUGUI>().text = "Unready";
            NetMenuCountdown netMenuCountdown = new NetMenuCountdown();
            netMenuCountdown.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
            netMenuCountdown.m_Action = NetMenuCountdown.ACTION.READY;
            Client.Instance.SendToServer(netMenuCountdown);
        }
        else
        {
            m_readyText.GetComponent<TMPro.TextMeshProUGUI>().text = "Ready Up";
            NetMenuCountdown netMenuCountdown = new NetMenuCountdown();
            netMenuCountdown.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
            netMenuCountdown.m_Action = NetMenuCountdown.ACTION.UNREADY;
            Client.Instance.SendToServer(netMenuCountdown);
        }
        m_isReady = !m_isReady;
    }

    string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
    }

    //private void OnDestroy()
    //{
    //    if (m_isHost)
    //    {
    //        ServerHostEnd serverHostEnd = new ServerHostEnd();
    //        serverHostEnd.m_ServerIP = GetLocalIPv4();
    //        MenuClient.Instance.SendToServer(serverHostEnd);
    //    }
    //}
}
