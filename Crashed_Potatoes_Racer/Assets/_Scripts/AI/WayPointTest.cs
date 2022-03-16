using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointTest : MonoBehaviour
{
    public GameObject[] wayPoints;
    public GameObject[] wayPoints2;
    [Range(0.1f, 10)]
    public float stoppingDist;
    [Range(0, 20)]
    public float speed;

    private NavMeshAgent agent;
    private Transform target;
    private int counter = 0;
    private float rand;

    private float vel = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rand = Random.Range(0, 2);
    }

    private void Update()
    {
        
        if(rand == 0)
        {
            agent.speed = Mathf.Lerp(agent.speed, 25, Time.deltaTime * 0.2f); 
            float dist = Vector3.Distance(transform.position, wayPoints[counter].transform.position);
            if (dist <= stoppingDist)
            {
                counter++;
                if (counter >= wayPoints.Length)
                {
                    counter = 0;
                }
            }
            target = wayPoints[counter].transform;
            agent.SetDestination(target.position);
        }
        else if (rand == 1)
        {
            agent.speed = Mathf.Lerp(agent.speed, 25, Time.deltaTime * 0.2f);
            float dist = Vector3.Distance(transform.position, wayPoints2[counter].transform.position);
            if (dist <= stoppingDist)
            {
                counter++;
                if (counter >= wayPoints2.Length)
                {
                    counter = 0;
                }
            }
            target = wayPoints2[counter].transform;
            agent.SetDestination(target.position);
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
