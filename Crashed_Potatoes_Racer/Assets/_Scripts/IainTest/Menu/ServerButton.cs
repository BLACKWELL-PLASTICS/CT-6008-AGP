using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerButton : MonoBehaviour
{
    public string m_nameText;
    public string m_passwordProtectedText;
    public string m_pingText;
    public string m_adress;

    [SerializeField]
    public GameObject m_nameTextField;
    [SerializeField]
    public GameObject m_passwordProtectedTextField;
    [SerializeField]
    public GameObject m_pingTextField;
    private void Start()
    {
        m_nameTextField.GetComponent<UnityEngine.UI.Text>().text = m_nameText;
        m_passwordProtectedTextField.GetComponent<UnityEngine.UI.Text>().text = m_passwordProtectedText;
        m_pingTextField.GetComponent<UnityEngine.UI.Text>().text = m_pingText;
    }
    public void OnServerConnect()
    {
        Transform thisParent = gameObject.transform.parent;
        while (thisParent != null)
        {
            if (thisParent.GetComponent<MenuManager>() != null)
            {
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
