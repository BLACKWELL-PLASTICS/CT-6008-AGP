using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap : MonoBehaviour
{ 
    public int individualNo;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            //isDrivenThrough = !isDrivenThrough;
            other.GetComponent<WinCondition>().hasBeenChecked[individualNo] = !other.GetComponent<WinCondition>().hasBeenChecked[individualNo];
            other.GetComponent<WinCondition>().checkpointNumber++;

            Debug.LogError(other.GetComponent<WinCondition>().checkpointNumber);
        }
    }
}
