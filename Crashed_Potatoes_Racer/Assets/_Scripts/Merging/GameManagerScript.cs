using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_activeCars;
    [SerializeField]
    GameObject m_carPrefab;
    [SerializeField]
    GameObject m_mergedDrivePrefab;
    [SerializeField]
    GameObject m_mergedShootPrefab;
    [SerializeField]
    float m_maxTimer;

    GameObject m_mergedCar;
    float m_timer;
    bool m_hasMerged;

    // Start is called before the first frame update
    void Start()
    {
        m_timer = m_maxTimer;
        m_hasMerged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_hasMerged)
        {
            if (m_timer > 0)
            {
                m_timer -= Time.deltaTime;
            }
            else
            {
                m_hasMerged = false;
                m_timer = m_maxTimer;
                Vector3 pos = (m_mergedCar.transform.position + (m_mergedCar.transform.right));
                pos.y += 0.2f;
                GameObject car = Instantiate(m_carPrefab, pos, m_mergedCar.transform.rotation);
                car.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                m_activeCars[0] = car;
                pos = (m_mergedCar.transform.position - (m_mergedCar.transform.right));
                car = Instantiate(m_carPrefab, pos, m_mergedCar.transform.rotation);
                car.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                m_activeCars[1] = car;
                Destroy(m_mergedCar);
                m_mergedCar = null;
            }
        }
    }

    public void MergeCars(GameObject a_car1, GameObject a_car2)
    {
        if (m_mergedCar == null)
        {
            Vector3 pos = a_car1.transform.position + (a_car2.transform.position - a_car1.transform.position) / 2;
            Vector3 midDir = (a_car1.transform.forward.normalized + a_car2.transform.forward.normalized).normalized;
            Quaternion dir = Quaternion.Euler(midDir.x, midDir.y, midDir.z);

            GameObject spawnedMergedCar = Instantiate(m_mergedDrivePrefab, pos, dir);
            m_mergedCar = spawnedMergedCar;
            Destroy(a_car1);
            m_activeCars[0] = null;
            Destroy(a_car2);
            m_activeCars[1] = null;
        }
        m_hasMerged = true;
    }
}
