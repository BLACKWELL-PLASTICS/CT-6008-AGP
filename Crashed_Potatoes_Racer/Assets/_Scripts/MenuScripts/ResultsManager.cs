//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 24/05/2022                        ///
/// Edited By: Henry Northway                  ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] m_nameFields;
    [SerializeField]
    GameObject[] m_cars;

    [SerializeField]
    Image[] m_iconFields;
    [SerializeField]
    Sprite[] m_carIcons;

    void Start()
    {
        //Updates the name and icons of each player strip
        for (int i = 0; i < PersistentInfo.Instance.m_winOrder.Length; i++)
        {
            //if data exists assign it (there is currently a bug with the player position script that causing an issue with here)
            if (PersistentInfo.Instance.m_winOrder[i] != null)
            {
                m_nameFields[i].text = PersistentInfo.Instance.m_winOrder[i];
            }
        }

        for (int i = 0; i < PersistentInfo.Instance.m_winDesigns.Length; i++)
        {
            //if data exists assign it (there is currently a bug with the player position script that causing an issue with here)
            if (PersistentInfo.Instance.m_winDesigns[i] != null)
            {
                //Updates the car models for top-three
                if (i < m_cars.Length)
                {
                    m_cars[i].GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_winDesigns[i].m_carChoice, PersistentInfo.Instance.m_winDesigns[i].m_wheelChoice, PersistentInfo.Instance.m_winDesigns[i].m_gunChoice);
                }
                m_iconFields[i].sprite = UpdateIcon(PersistentInfo.Instance.m_winDesigns[i].m_carChoice);
            }
        }
    }

    public Sprite UpdateIcon(int a_targetCarBody)
    {
        for (int i = 0; i < m_iconFields.Length; i++)
        {
            if (i != a_targetCarBody)
            {
                Debug.Log("FALSE (Target " + a_targetCarBody + ")");
            }
            else
            {
                Debug.Log("TRUE (Target " + a_targetCarBody + ")");
                return m_carIcons[i];
            }
        }

        return null;
    }

    public void OnContinueButton()
    {
        PersistentInfo.Instance.Clear();
        SceneManager.LoadScene(0);
    }
}
