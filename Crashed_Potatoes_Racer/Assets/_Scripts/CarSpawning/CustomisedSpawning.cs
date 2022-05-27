//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 05/04/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

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
        //for each car
        for (int i = 0; i < m_cars.Length; i++)
        {
            if (i != a_carBody)
            {
                //if its not selected destroy it
                Destroy(m_cars[i]);
                m_cars[i] = null;
            }
            else
            {
                //set active and asssign
                m_cars[i].SetActive(true);
                m_selectedBody = m_cars[i];
            }
        }
        GameObject[] wheels = new GameObject[12];
        int wheelCount = 0;
        GameObject gunbase = null;
        //get all wheels from selcted car gameobject
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
        //foeach wheel - turn on correct wheel
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
        //get all guns on gameobject 
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
        //set selected 
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
        //if this prefabs is using gun
        if (m_hasGun)
        {
            //get differnt parts of gun
            gunbase.transform.parent = this.transform;
            GameObject basePlate = gunbase.transform.GetChild(2).gameObject;
            GameObject shaftLower = gunbase.transform.GetChild(0).gameObject;
            GameObject shaftUpper = gunbase.transform.GetChild(1).gameObject;

            //parent
            shaftLower.transform.parent = basePlate.transform;
            shaftUpper.transform.parent = shaftLower.transform;
            m_selectedGun.transform.parent = shaftUpper.transform;

            if (gunbase.GetComponent<MergedShootingControllerScript>() != null)
            {
                //set gun to the selected
                gunbase.GetComponent<MergedShootingControllerScript>().m_gun = m_selectedGun;
            }
        }
        else
        {
            //if not using gun destroy it
            Destroy(gunbase);
        }
        Destroy(this);
    }
}
