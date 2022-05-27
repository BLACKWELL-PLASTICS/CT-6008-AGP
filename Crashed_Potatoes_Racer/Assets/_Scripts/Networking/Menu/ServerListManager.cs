//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 09/02/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerListManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_serverButtonPrefab;
    [SerializeField]
    GameObject m_content;
    [SerializeField]
    Sprite[] m_levelPreviews;

    List<GameObject> m_serverButtons = new List<GameObject>();

    public void AddServer(string a_serverName, string a_serverAdress, int a_serverLevel)
    {
        //instantiate button
        GameObject button = Instantiate(m_serverButtonPrefab) as GameObject;
        button.SetActive(true);
        //set script values
        ServerButton serverButton = button.GetComponent<ServerButton>();
        serverButton.m_nameText = a_serverName;
        serverButton.m_passwordProtectedText = "U";
        serverButton.m_pingText = "0ms";
        serverButton.m_adress = a_serverAdress;

        switch (a_serverLevel)
        {
            //switch on each level to set text (now used image instead)
            case 0:
                serverButton.m_levelText = "Jungle Island";
                serverButton.m_levelImage = m_levelPreviews[0];
                break;
            case 1:
                serverButton.m_levelText = "Mineshaft";
                serverButton.m_levelImage = m_levelPreviews[1];
                break;
            case 2:
                serverButton.m_levelText = "Toy Room";
                serverButton.m_levelImage = m_levelPreviews[2];
                break;
            default:
                serverButton.m_levelText = "Jungle Island";
                serverButton.m_levelImage = m_levelPreviews[0];
                break;
        }
        //set ui to ensure correctly updated
        serverButton.SetUI();
        //set parent to the content of scroll
        button.transform.SetParent(m_content.transform);
        //add to buttons
        m_serverButtons.Add(button);
        //ensure correct scale
        button.transform.localScale = new Vector3(1, 1, 1);
    }
    public void RemoveServer(string a_serverAdress)
    {
        //delete from list and destroy gameobject
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
