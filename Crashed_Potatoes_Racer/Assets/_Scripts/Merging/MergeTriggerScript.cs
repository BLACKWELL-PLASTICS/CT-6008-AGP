//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 26/01/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //player check
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<CarManagerScript>().m_mergeOn)
            {
                //if the other player car is mergable merge
                transform.parent.GetComponent<CarManagerScript>().EnteredMerge(other.gameObject);
            }
        }
    }
}
