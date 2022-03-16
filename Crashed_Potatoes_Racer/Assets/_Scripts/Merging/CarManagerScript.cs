using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManagerScript : MonoBehaviour
{
    public bool m_mergeOn;
    public GameObject m_gameManagerHolder;

    [SerializeField]
    Material m_canMergeMaterial;
    [SerializeField]
    Material m_normalMaterial;
    [SerializeField]
    GameObject m_mergeTrigger;

    GameManagerScript m_gms;

    // Start is called before the first frame update
    void Start()
    {
        m_gms = m_gameManagerHolder.GetComponent<GameManagerScript>();
        m_mergeOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMerging()
    {
        m_mergeOn = !m_mergeOn;
        m_mergeTrigger.SetActive(m_mergeOn);
        if (m_mergeOn)
        {
            GetComponent<Renderer>().material = m_canMergeMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = m_normalMaterial;
        }
    }
    public void EnteredMerge(GameObject a_other)
    {
        m_gms.MergeCars(this.gameObject, a_other);
    }
}
