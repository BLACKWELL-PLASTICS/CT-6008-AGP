using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    public int currentPosition;
    private int newPosition;

    GameObject UI;

    private void Start() {
        if (GetComponent<CarManagerScript>() != null)
        {
            currentPosition = GetComponent<CarManagerScript>().m_playerNum;
        }
        else
        {
            currentPosition = GetComponent<MergedShootingControllerScript>().m_playerNum;
        }
        if (gameObject.name == "Car_Reg(Clone)" || gameObject.name == "Car_MergeDrive(Clone)" || (gameObject.tag == "DisplayGunBase" && transform.parent.gameObject.name == "Car_MergeShoot(Clone)")) {
            UI = GameObject.Find("Placing Prefab");
            if (Time.timeScale == 0) {
                UI.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        // If the car is finished, stop calculating
        if (GetComponent<WinCondition>().isFinished != true) {
            newPosition = 8;
            foreach (GameObject car in GameObject.Find("Manager").GetComponent<MultiplayerManager>().m_activeCars)
            {
                if (car != this.gameObject)
                {
                    CheckPosition(car);
                    WinCondition[] winConditions = car.GetComponentsInChildren<WinCondition>();
                    if (winConditions.Length > 1)
                    {
                        if (winConditions[1] != null)
                        {
                            CheckPosition(winConditions[1].gameObject);
                        }
                    }
                }
            }

            if (GetComponent<WinCondition>().checkpointNumber >= 0)
            {
                currentPosition = newPosition;
                if (currentPosition < 1)
                {
                    currentPosition = 1;
                }
                else if (currentPosition > 8)
                {
                    currentPosition = 8;
                }
            }

            if (gameObject.name == "Car_Reg(Clone)" || gameObject.name == "Car_MergeDrive(Clone)" || (gameObject.tag == "DisplayGunBase" && transform.parent.gameObject.name == "Car_MergeShoot(Clone)"))
            {
                {
                    if (Time.timeScale != 0)
                    {
                        UI.SetActive(true);
                        UI.GetComponent<PlacingRank>().UpdateRank(currentPosition);
                    }
                }
            }
        }
    }
    void CheckPosition(GameObject car)
    {
        if (car.GetComponent<WinCondition>().checkpointNumber < 0 || gameObject.GetComponent<WinCondition>().checkpointNumber < 0)
        {
            return;
        }

        // Check the lap
        if (gameObject.GetComponent<WinCondition>().lap > car.GetComponent<WinCondition>().lap)
        {
            newPosition--;
        }
        else if (gameObject.GetComponent<WinCondition>().lap == car.GetComponent<WinCondition>().lap)
        {
            if (gameObject.GetComponent<WinCondition>().checkpointNumber > car.GetComponent<WinCondition>().checkpointNumber)
            {
                // if this car has a higher checkpoint number
                newPosition--;
            }
            else if (gameObject.GetComponent<WinCondition>().checkpointNumber == car.GetComponent<WinCondition>().checkpointNumber)
            {
                // if the checkpoint number is the same
                GameObject checkpoint = null;
                if (gameObject.GetComponent<WinCondition>().checkpointNumber < GameObject.FindGameObjectsWithTag("Waypoints").Length)
                {
                    checkpoint = gameObject.GetComponent<WinCondition>().array[gameObject.GetComponent<WinCondition>().checkpointNumber];
                }
                else
                {
                    checkpoint = gameObject.GetComponent<WinCondition>().array[0];
                }
                float carOneDistance = Vector3.Distance(checkpoint.transform.position, gameObject.transform.position);
                float carTwoDistance = Vector3.Distance(checkpoint.transform.position, car.transform.position);
                if (carOneDistance < carTwoDistance)
                {
                    //might do both
                    newPosition--;
                }
            }
        }
    }
}
