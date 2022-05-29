using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour //script for spawning boids/ptera in lvl 1 - By Anna
{
    public GameObject boidPrefab; //prefab of ptera
    public Transform boidLocation; //centre of flock
    public int amountBoids; //amount of pteras
    public float areaLimit; //area they flock in
    public float maxBoidSpeed; //max speed
    public float rotationSpeed; //rotation speed
    public float neighbourDist; //distance for boids to become neighbours
    public float goalRandomness; //randomness of goal seeking
    [HideInInspector]
    public Vector3 goalPos; //temp vector for goal position
    [HideInInspector]
    public GameObject[] boidArray; //temp array of boids

    private void Start()
    {
        boidArray = new GameObject[amountBoids]; //set array length to amount of boids

        for (int i = 0; i < amountBoids; i++) //loop through amount of boids
        {
            Vector3 position = new Vector3(Random.Range(boidLocation.position.x - areaLimit, boidLocation.position.x + areaLimit), //create a random position from centre within area limits
                                           Random.Range(boidLocation.position.y - areaLimit, boidLocation.position.y + areaLimit),
                                           Random.Range(boidLocation.position.z - areaLimit, boidLocation.position.z + areaLimit));
            boidArray[i] = (GameObject)Instantiate(boidPrefab, position, Quaternion.identity); //spawn a ptera at new pos
            boidArray[i].GetComponent<BoidController>().manager = this; //connection this script to boids movement script
        }
    }
    void Update()
    {
        GoalPosRandom(); //run function for finding  goal
    }

    private void GoalPosRandom() //creates a random position for the boids to flock towards
    {
        if (Random.Range(0, goalRandomness) < 50) 
        {
            goalPos = new Vector3(Random.Range(boidLocation.position.x - areaLimit, boidLocation.position.x + areaLimit),
                                  Random.Range(boidLocation.position.y - areaLimit, boidLocation.position.y + areaLimit),
                                  Random.Range(boidLocation.position.z - areaLimit, boidLocation.position.z + areaLimit));
        }
    }
}
