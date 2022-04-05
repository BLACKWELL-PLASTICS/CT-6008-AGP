using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomisedSpawning : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_cars;
    [SerializeField]
    bool m_hasGun;

    GameObject m_selectedBody;
    GameObject m_selectedWheels;
    GameObject m_selectedGun;
    
    public void Spawn(int a_carBody, int a_carWheels, int a_carGun)
    {
        for (int i = 0; i < m_cars.Length; i++)
        {
            if (i != a_carBody)
            {
                Destroy(m_cars[i]);
                m_cars[i] = null;
            }
            else
            {
                m_cars[i].SetActive(true);
                m_selectedBody = m_cars[i];
            }
        }
        GameObject[] wheels = new GameObject[12];
        int wheelCount = 0;
        GameObject gunbase = null;
        for (int i = 0; i < m_selectedBody.transform.childCount; i++)
        {
            if (m_selectedBody.transform.GetChild(i).gameObject.tag == "DisplayWheels")
            {
                wheels[wheelCount] = m_selectedBody.transform.GetChild(i).gameObject;
                wheelCount++;
            }
            if (m_selectedBody.transform.GetChild(i).gameObject.tag == "DisplayGunBase")
            {
                gunbase = m_selectedBody.transform.GetChild(i).gameObject;
                gunbase.SetActive(true);
            }
        }
        for (int i = 0; i < wheels.Length; i++)
        {
            if (i != a_carWheels)
            {
                Destroy(wheels[i]);
                wheels[i] = null;
            }
            else
            {
                wheels[i].SetActive(true);
                m_selectedWheels = wheels[i];
            }
        }
        GameObject[] guns = new GameObject[3];
        int gunCount = 0;
        for (int i = 0; i < gunbase.transform.childCount; i++)
        {
            if (gunbase.transform.GetChild(i).gameObject.tag == "DisplayGun")
            {
                guns[gunCount] = gunbase.transform.GetChild(i).gameObject;
                gunCount++;
            }
        }
        for (int i = 0; i < guns.Length; i++)
        {
            if (i != a_carGun)
            {
                Destroy(guns[i]);
                guns[i] = null;
            }
            else
            {
                guns[i].SetActive(true);
                m_selectedGun = guns[i];
            }
        }
        if (m_hasGun)
        {
            gunbase.transform.parent = this.transform;
        }
        else
        {
            Destroy(gunbase);
        }
        Destroy(this);
    }
}
