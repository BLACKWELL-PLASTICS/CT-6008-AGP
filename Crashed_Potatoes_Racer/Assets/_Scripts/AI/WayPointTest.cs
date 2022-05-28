using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointTest : MonoBehaviour //orignal test for car movement using waypoints, no longer used- By Anna
{
    public GameObject[] wayPoints; //waypoints in scene
    public GameObject[] wayPoints2; //alternate waypoints in scene
    [Range(0.1f, 10)]
    public float stoppingDist; //distance that the ai will count the waypoint as triggered
    [Range(0, 20)]
    public float speed; //speed of the ai

    private NavMeshAgent agent; //nav mesh agaent link
    private Transform target; //transform used to set waypoint 
    private int counter = 0; //counter used to count waypoints
    private float rand; //random number for deciding which waypoint route to take

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //connects to nav mesh agent on gameObject
        rand = Random.Range(0, 2); //random number either 0, 1
    }

    private void Update()
    {
        
        if(rand == 0) //if 0 use set 1 of waypoints
        {
            agent.speed = Mathf.Lerp(agent.speed, 25, Time.deltaTime * 0.2f); //gradually change speed from current speed to 25 (test value) 
            float dist = Vector3.Distance(transform.position, wayPoints[counter].transform.position); //work out distance from ai to current waypoint 
            if (dist <= stoppingDist) //if within stopping distance
            {
                counter++; //move to next waypoint
                if (counter >= wayPoints.Length) //if out of waypoints
                {
                    counter = 0; //reset waypoint
                }
            }
            target = wayPoints[counter].transform; //set target to waypoints position
            agent.SetDestination(target.position); //move towards target
        }
        else if (rand == 1) //if 1 use set 2 of waypoints
        {
            agent.speed = Mathf.Lerp(agent.speed, 25, Time.deltaTime * 0.2f);//gradually change speed from current speed to 25 (test value) 
            float dist = Vector3.Distance(transform.position, wayPoints2[counter].transform.position);//work out distance from ai to current waypoint 
            if (dist <= stoppingDist)//if within stopping distance
            {
                counter++;//move to next waypoint
                if (counter >= wayPoints2.Length)//if out of waypoints
                {
                    counter = 0;//reset waypoint
                }
            }
            target = wayPoints2[counter].transform; //set target to waypoints position
            agent.SetDestination(target.position);//move towards target
        }
        
        //transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
        
    }

    //private int FindClosest()
    //{
    //    int counter = 0;
    //    float tempClostest = Vector3.Distance(this.transform.position, wayPoints[0].transform.position);
    //    for (int i = 0; i < wayPoints.Length; i++)
    //    {
    //        float distance = Vector3.Distance(this.transform.position, wayPoints[i].transform.position);
    //        if (distance < tempClostest)
    //        {
    //            tempClostest = distance;
    //            counter = i;
    //        }
    //    }

    //    return counter;
    //}

}
