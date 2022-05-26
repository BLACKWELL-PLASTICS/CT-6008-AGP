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
        //Debug.Log("Results Manager");
        //Debug.Log("Name 1: " + PersistentInfo.Instance.m_winOrder[0]);
        //Debug.Log("Name 2: " + PersistentInfo.Instance.m_winOrder[1]);
        //Debug.Log("Name 3: " + PersistentInfo.Instance.m_winOrder[2]);
        //Debug.Log("Name 4: " + PersistentInfo.Instance.m_winOrder[3]);
        //Debug.Log("Name 5: " + PersistentInfo.Instance.m_winOrder[4]);
        //Debug.Log("Name 6: " + PersistentInfo.Instance.m_winOrder[5]);
        //Debug.Log("Name 7: " + PersistentInfo.Instance.m_winOrder[6]);
        //Debug.Log("Name 8: " + PersistentInfo.Instance.m_winOrder[7]);
        for (int i = 0; i < m_cars.Length; i++)
        {
            //Debug.Log(i);
            //Debug.Log(m_cars[i]);
            //Debug.Log(m_cars[i].GetComponent<CustomisedSpawning>() != null); //Fine
            //Debug.Log(PersistentInfo.Instance.m_winDesigns.Length);
            //Debug.Log(PersistentInfo.Instance.m_winDesigns[i] != null);

            m_cars[i].GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_winDesigns[i].m_carChoice, PersistentInfo.Instance.m_winDesigns[i].m_wheelChoice, PersistentInfo.Instance.m_winDesigns[i].m_gunChoice);
        }
    }

    public void OnContinueButton()
    {
        PersistentInfo.Instance.Clear();
        SceneManager.LoadScene(0);
    }
}
