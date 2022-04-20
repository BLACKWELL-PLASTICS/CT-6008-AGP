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

    public void AddServer(string a_serverName, string a_serverAdress, int a_serverLevel)
    {
        GameObject button = Instantiate(m_serverButtonPrefab) as GameObject;
        button.SetActive(true);
        ServerButton serverButton = button.GetComponent<ServerButton>();
        serverButton.m_nameText = a_serverName;
        serverButton.m_passwordProtectedText = "U";
        serverButton.m_pingText = "0ms";
        serverButton.m_adress = a_serverAdress;
        switch (a_serverLevel)
        {
            case 0:
                serverButton.m_levelText = "Jungle";
                break;
            case 1:
                serverButton.m_levelText = "Mine";
                break;
            case 2:
                serverButton.m_levelText = "Bedroom";
                break;
            default:
                serverButton.m_levelText = "Jungle";
                break;
        }
        serverButton.SetUI();
        button.transform.SetParent(m_content.transform);
        m_serverButtons.Add(button);
        button.transform.localScale = new Vector3(1, 1, 1);
    }
    public void RemoveServer(string a_serverAdress)
    {
        for (int i = 0; i < m_serverButtons.Count; i++)
        {
            if (m_serverButtons[i].GetComponent<ServerButton>().m_adress == a_serverAdress)
            {
                Destroy(m_serverButtons[i]);
                m_serverButtons.Remove(m_serverButtons[i]);
            }
        }
    }
}
