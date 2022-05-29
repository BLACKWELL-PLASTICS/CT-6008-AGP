using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabScript : MonoBehaviour //script for crab's in lvl 1 movement - By Anna
{
    public float crabDistance = 3; //distance it will find a point to move to
    public float crabTimer = 5; //time before moving again
    public float crabSpeed = 2; //speed in which crabs move

    private float timer = 0; //temp timer 
    private NavMeshAgent crab; //link to nav mesh agent
    private Animator anim; //link to animator
    private Vector3 newPos; //temp vec3 for new position
    private void Start()
    {
        crab = GetComponent<NavMeshAgent>(); //connection to nav mesh agent
        anim = GetComponentInChildren<Animator>(); //connection to animator
        crab.speed = crabSpeed; //connection to crab speed
    }
    private void Update()
    {
        timer += Time.deltaTime; //start timer
        if (timer >= crabTimer) //if bigger than crab time
        {
            newPos = RandomPoint(transform.position); //set new pos to value from function
            crab.SetDestination(newPos); //move to new pos
            timer = 0; //reset timer
        }

        if(transform.position == newPos) //if at new pos
        {
            crab.isStopped = true; //stop walking
            anim.SetBool("Walking", false); //start idle animation
        }
        else //else
        {
            crab.isStopped = false; //start moving
            anim.SetBool("Walking", true);//set animation to walking
        }
    }
    private Vector3 RandomPoint(Vector3 origin) //function for getting a random pos in a sphere
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * crabDistance; //create a sphere with distance of crab distance

        randomDirection += origin; //centre it around crab

        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, crabDistance, 9); //get a point within sphere in crab distance

        return navHit.position; //return position
    }
}
