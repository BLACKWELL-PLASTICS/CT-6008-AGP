//////////////////////////////////////////////////
/// Created:                                   ///
/// Author: Oliver Blackwell                   ///
/// Edited By: Iain Farlow                     ///
/// Last Edited: 24/05/2022                    ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap : MonoBehaviour
{ 
    public int individualNo;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.GetComponent<WinCondition>().hasBeenChecked[individualNo] = !other.GetComponent<WinCondition>().hasBeenChecked[individualNo];
            other.GetComponent<WinCondition>().checkpointNumber++;
            if (other.GetComponentInChildren<MergedShootingControllerScript>() != null) // Added by iain
            {
                other.GetComponentInChildren<WinCondition>().checkpointNumber++;
            }

            //Debug.Log(other.GetComponent<WinCondition>().checkpointNumber);
        }
    }
}
