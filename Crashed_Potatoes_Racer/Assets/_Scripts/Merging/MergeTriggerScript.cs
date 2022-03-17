using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            if (other.GetComponent<CarManagerScript>().m_mergeOn)
            {
                transform.parent.GetComponent<CarManagerScript>().EnteredMerge(other.gameObject);
            }
        }
    }
}
