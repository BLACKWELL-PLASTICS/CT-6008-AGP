using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour//script for the movement and boid algoirthm of ptera in lvl 1 - by Anna
{
    private float speed; //temp value for speed
    [HideInInspector]
    public BoidManager manager; //connection to boid manager

    void Start()
    {
        speed = Random.Range(1, manager.maxBoidSpeed); //set speed by being random from 1 to max speed
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, manager.boidLocation.position) >= manager.areaLimit) //if the boid is outside or on the areas limit  
        {
            Vector3 direction = manager.boidLocation.position - transform.position; //direction from boid position to centre
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.rotationSpeed * Time.deltaTime);  //rotate towards new direction
            speed = Random.Range(1, 15); //set a new speed
        }
        else //if within limits
        {
            if (Random.Range(0, 8) < 1) //with some randomness
            {
                ApplyRules(); //apply boid rules
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);  //move boid
    }

    private void ApplyRules()
    {
        GameObject[] otherBoids; //array of all boids
        otherBoids = manager.boidArray; //set from manager script

        Vector3 center = Vector3.zero; //centre of group
        Vector3 avoid = Vector3.zero;  //avoid distance
        Vector3 goalPos = manager.goalPos; //goal pos from manager script

        float distance; //distance between this boid and next
        int groupSize = 0; //size of boid group
        float averageSpeed = 0.1f; //group speed

        foreach (GameObject i in otherBoids) //loop through boids
        {
            if (i != this.gameObject)  //if not this current boid
            {
                distance = Vector3.Distance(i.transform.position, this.transform.position); //work out distance from current boid to next
                if (distance <= manager.neighbourDist) //if within neighbour distance
                {
                    center += i.transform.position; //add that boids position to group centre
                    groupSize++; //increase group size

                    if (distance < 3) //if distance is smaller avoid distance
                    {
                        avoid = avoid + (this.transform.position - i.transform.position); //avoid chnages to include current boid pos - next boid
                    }
                    BoidController anotherFlock = i.GetComponent<BoidController>(); //get controller of other boid
                    averageSpeed += anotherFlock.speed; //set group speed to include other boids speed
                }
            }
        }
        if (groupSize > 0) //if there is a group
        {
            center = center / groupSize + (goalPos - this.transform.position); //set the centre of the group with goal pos
            speed = averageSpeed / groupSize; //set the speed based on average speed and group size

            Vector3 direction = (center + avoid) - transform.position; //set the direction to include centre and avoidance
            if (direction != manager.boidLocation.position) // if not the cenre of all boids
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.rotationSpeed * Time.deltaTime); //rotate in new direction

        }

    }
}
