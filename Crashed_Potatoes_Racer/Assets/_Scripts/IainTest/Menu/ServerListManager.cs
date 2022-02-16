using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerListManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_serverButtonPrefab;
    [SerializeField]
    GameObject m_content;

    List<GameObject> m_serverButtons = new List<GameObject>();

    public void AddServer(string a_serverName, string a_serverAdress)
    {
        GameObject button = Instantiate(m_serverButtonPrefab) as GameObject;
        button.SetActive(true);
        ServerButton serverButton = button.GetComponent<ServerButton>();
        serverButton.m_nameText = a_serverName;
        serverButton.m_passwordProtectedText = "U";
        serverButton.m_pingText = "0ms";
        serverButton.m_adress = a_serverAdress;
        serverButton.SetUI();
        button.transform.SetParent(m_content.transform);
        m_serverButtons.Add(button);
    }
    public void RemoveServer(string a_serverAdress)
    {
        foreach (GameObject button in m_serverButtons)
        {
            if (button.GetComponent<ServerButton>().m_adress == a_serverAdress)
            {
                m_serverButtons.Remove(button);
                Destroy(button);
            }
        }
    }
}
