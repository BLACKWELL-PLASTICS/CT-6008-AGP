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

    private InputField m_activeInputField;

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
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }

    public void OnSelectCourseButton(int a_level)
    {
        PersistentInfo.Instance.m_levelNum = a_level;

        m_activeInputField = FindObjectOfType<InputField>();
        m_activeInputField.Select();
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
        Client.Instance.Shutdown();
        Server.Instance.Shutdown();
        foreach (GameObject text in m_connectedOtherTexts)
        {
            text.GetComponent<UnityEngine.UI.Text>().text = "Empty";
        }
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
