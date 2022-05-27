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

public class ServerButton : MonoBehaviour
{
    public string m_nameText;
    public string m_passwordProtectedText;
    public string m_pingText;
    public string m_adress;
    public string m_levelText;
    public Sprite m_levelImage;

    [SerializeField]
    public GameObject m_nameTextField;
    [SerializeField]
    public GameObject m_passwordProtectedTextField;
    [SerializeField]
    public GameObject m_pingTextField;
    [SerializeField]
    public GameObject m_levelTextField;
    [SerializeField]
    public GameObject m_levelImageField;

    public void SetUI()
    {
        //sets relevent areas of the server button
        m_nameTextField.GetComponent<Text>().text = m_nameText;
        m_passwordProtectedTextField.GetComponent<Text>().text = m_passwordProtectedText;
        m_pingTextField.GetComponent<Text>().text = m_pingText;
        m_levelTextField.GetComponent<Text>().text = m_levelText;
        m_levelImageField.GetComponent<Image>().sprite = m_levelImage;
    }

    public void OnServerConnect()
    {
        //onclick
        Transform thisParent = gameObject.transform.parent;
        while (thisParent != null)
        {
            if (thisParent.GetComponent<MenuManager>() != null)
            {
                //connect to server
                thisParent.GetComponent<MenuManager>().ConnectToServer(m_adress);
                thisParent = null;
            }
            else
            {
                thisParent = thisParent.parent;
            }
        }
    }
}
