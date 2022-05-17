using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public int currentPosition;
    private int newPosition;

    private void Start() {
        currentPosition = GetComponent<CarManagerScript>().m_playerNum;
    }

    // Update is called once per frame
    void Update()
    {
        newPosition = 8;
        foreach (GameObject car in GameObject.Find("Manager").GetComponent<MultiplayerManager>().m_activeCars) {
            if (car != this.gameObject) {
                CheckPosition(car);
            }
        }
        if (GetComponent<WinCondition>().checkpointNumber >= 0) {
            currentPosition = newPosition;
            if (currentPosition < 1) {
                currentPosition = 1;
            } else if (currentPosition > 8) {
                currentPosition = 8;
            }
        }
    }

    void CheckPosition(GameObject car) {
        if (car.GetComponent<WinCondition>().checkpointNumber < 0 || gameObject.GetComponent<WinCondition>().checkpointNumber < 0) {
            return;
        }
        // Check the lap
        if (car.GetComponent<WinCondition>().lap != gameObject.GetComponent<WinCondition>().lap) {
            newPosition--;
        }
        if (gameObject.GetComponent<WinCondition>().checkpointNumber > car.GetComponent<WinCondition>().checkpointNumber) {
            // if this car has a higher checkpoint number
            newPosition--;
        }
        else if (gameObject.GetComponent<WinCondition>().checkpointNumber == car.GetComponent<WinCondition>().checkpointNumber) {
            // if the checkpoint number is the same
            GameObject checkpoint = gameObject.GetComponent<WinCondition>().array[gameObject.GetComponent<WinCondition>().checkpointNumber];
            float carOneDistance = Vector3.Distance(checkpoint.transform.position, gameObject.transform.position);
            float carTwoDistance = Vector3.Distance(checkpoint.transform.position, car.transform.position);
            if (carOneDistance < carTwoDistance) {
                newPosition--;
            }
        }
    }
}
