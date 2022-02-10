using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSever : MonoBehaviour
{
    [SerializeField]
    GameObject m_serverButtonPrefab;
    [SerializeField]
    GameObject m_content;

    public void OnAddServerButton()
    {
        GameObject button = Instantiate(m_serverButtonPrefab) as GameObject;
        button.SetActive(true);
        ServerButton serverButton = button.GetComponent<ServerButton>();
        serverButton.m_nameText = "ADemoServer";
        serverButton.m_passwordProtectedText = "U";
        serverButton.m_pingText = "0ms";
        serverButton.m_adress = "127.0.0.1";
        button.transform.SetParent(m_content.transform);
    }
}
