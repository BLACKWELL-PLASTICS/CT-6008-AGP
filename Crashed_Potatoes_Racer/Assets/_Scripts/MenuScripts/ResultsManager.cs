using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_nameFields;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < PersistentInfo.Instance.m_winOrder.Length; i++)
        {
            m_nameFields[i].GetComponent<TextMeshPro>().text = PersistentInfo.Instance.m_winOrder[i];
        }
    }

    public void OnContinueButton()
    {
        PersistentInfo.Instance.Clear();
        SceneManager.LoadScene(0);
    }
}
