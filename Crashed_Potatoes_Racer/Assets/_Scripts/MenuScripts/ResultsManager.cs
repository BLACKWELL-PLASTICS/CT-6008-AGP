using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_nameFields;
    [SerializeField]
    GameObject[] m_cars;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < PersistentInfo.Instance.m_winOrder.Length; i++)
        {
            m_nameFields[i].GetComponent<TextMeshProUGUI>().text = PersistentInfo.Instance.m_winOrder[i];
        }
        for(int i = 0; i < PersistentInfo.Instance.m_winDesigns.Length; i++)
        {
            m_cars[i].GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_winDesigns[i].m_carChoice, PersistentInfo.Instance.m_winDesigns[i].m_wheelChoice, PersistentInfo.Instance.m_winDesigns[i].m_gunChoice);
        }
    }

    public void OnContinueButton()
    {
        PersistentInfo.Instance.Clear();
        SceneManager.LoadScene(0);
    }
}
