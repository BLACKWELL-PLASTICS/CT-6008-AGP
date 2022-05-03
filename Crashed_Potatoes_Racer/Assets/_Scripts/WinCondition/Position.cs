using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public int currentPosition;

    private void Start() {
        currentPosition = GetComponent<CarManagerScript>().m_playerNum;
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
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            // if the first checkpoints not been gone past
            if (GetComponent<WinCondition>().checkpointNumber < 0) {
                return;
            }
            // Check if on the same lap
            if (other.gameObject.GetComponent<WinCondition>().lap != GetComponent<WinCondition>().lap) {
                return;
            } else { // On the same lap
                GameObject checkpoint = GetComponent<WinCondition>().array[GetComponent<WinCondition>().checkpointNumber];
                float carOneDistance = Vector3.Distance(checkpoint.transform.position, transform.position);
                float carTwoDistance = Vector3.Distance(checkpoint.transform.position, other.transform.position);
                if (carOneDistance < carTwoDistance) { // this car is closer
                    currentPosition++;
                } else {
                    currentPosition--;
                }
            }
        }
    }

    public void MoveUpOne() {
        currentPosition++;
    }
    public void MoveDownOne() {
        currentPosition--;
    }
}
