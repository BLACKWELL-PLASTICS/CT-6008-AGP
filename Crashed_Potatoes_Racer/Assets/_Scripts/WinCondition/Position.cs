using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public int currentPosition;

    GameObject manager;

    private void Start() {
        currentPosition = GetComponent<CarManagerScript>().m_playerNum;
        manager = FindObjectOfType<MultiplayerManager>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPosition < 1) {
            currentPosition = 1;
        }
        if (currentPosition > 8) {
            currentPosition = 8;
        }

        for (int i = 0; i < 8; i++) {
            if (manager.GetComponent<MultiplayerManager>().m_activeCars[i] == 
                manager.GetComponent<MultiplayerManager>().m_activeCars[GetComponent<CarManagerScript>().m_playerNum]) {
                return;
            } else {
                ComparePositions(manager.GetComponent<MultiplayerManager>().m_activeCars[i]);
            }
        }
    }

    void ComparePositions(GameObject other) {
        // if the car isnt at first place / position already
        if (other.GetComponent<Position>().currentPosition > 1) {

        }
    }

    //private void OnTriggerEnter(Collider other) {
    //    if (other.gameObject.tag == "Player") {
    //        // Check if on the same lap
    //        if (other.gameObject.GetComponent<WinCondition>().lap != GetComponent<WinCondition>().lap) {
    //            return;
    //        } else { // On the same lap
    //            GameObject checkpoint = null;
    //            if (GetComponent<WinCondition>().checkpointNumber == GetComponent<WinCondition>().hasBeenChecked.Length - 1) {
    //                checkpoint = GetComponent<WinCondition>().array[0];
    //            } else {
    //                checkpoint = GetComponent<WinCondition>().array[GetComponent<WinCondition>().checkpointNumber + 1];
    //            }
    //            float carOneDistance = Vector3.Distance(checkpoint.transform.position, transform.position);
    //            float carTwoDistance = Vector3.Distance(checkpoint.transform.position, other.transform.position);
    //            if (carOneDistance < carTwoDistance) { // this car is closer
    //                currentPosition++;
    //            } else {
    //                currentPosition--;
    //            }
    //        }
    //    }
    //}
}
