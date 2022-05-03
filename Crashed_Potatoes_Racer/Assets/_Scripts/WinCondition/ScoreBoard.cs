using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    GameObject[] cars = new GameObject[8];
    // Start is called before the first frame update
    void Start()
    {
        cars = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        for (int one = 0; one < cars.Length; one++) {
            for (int two = 0; two < cars.Length; two++) {
                if (cars[one].GetComponent<WinCondition>().checkpointNumber < 0) {
                    return;
                }
                if (one == two) { // if the car compared is the same car
                    return;
                } else { // Check Lap
                    // if the car is a lap ahead
                    if (cars[one].GetComponent<WinCondition>().lap > cars[two].GetComponent<WinCondition>().lap) {
                        cars[one].GetComponent<Position>().MoveUpOne();
                        cars[two].GetComponent<Position>().MoveDownOne();
                    }
                    else if (cars[one].GetComponent<WinCondition>().lap < cars[two].GetComponent<WinCondition>().lap) { // if the car is a lap behind
                        cars[one].GetComponent<Position>().MoveDownOne();
                        cars[two].GetComponent<Position>().MoveUpOne();
                    } else if (cars[one].GetComponent<WinCondition>().lap == cars[two].GetComponent<WinCondition>().lap) { // if the cars are on the same lap
                        // Check Waypoints
                        if (cars[one].GetComponent<WinCondition>().checkpointNumber > cars[two].GetComponent<WinCondition>().checkpointNumber) {
                            cars[one].GetComponent<Position>().MoveUpOne();
                            cars[two].GetComponent<Position>().MoveDownOne();
                        } else if (cars[one].GetComponent<WinCondition>().checkpointNumber < cars[two].GetComponent<WinCondition>().checkpointNumber) {
                            cars[one].GetComponent<Position>().MoveDownOne();
                            cars[two].GetComponent<Position>().MoveUpOne();
                        } else if (cars[one].GetComponent<WinCondition>().checkpointNumber == cars[two].GetComponent<WinCondition>().checkpointNumber) {
                            // If on same lap and same waypoint, Who is closer to the next waypoint
                            GameObject checkpoint = cars[one].GetComponent<WinCondition>().array[cars[one].GetComponent<WinCondition>().checkpointNumber];
                            float carOneDistance = Vector3.Distance(checkpoint.transform.position, cars[one].transform.position);
                            float carTwoDistance = Vector3.Distance(checkpoint.transform.position, cars[two].transform.position);
                            if (carOneDistance < carTwoDistance) { // if the first cars distance to the checkpoint is smaller
                                // move up position
                                cars[one].GetComponent<Position>().MoveUpOne();
                                cars[two].GetComponent<Position>().MoveDownOne();
                            } else {
                                cars[one].GetComponent<Position>().MoveDownOne();
                                cars[two].GetComponent<Position>().MoveUpOne();
                            }
                        }
                    }
                }
            }
        }
    }
}
