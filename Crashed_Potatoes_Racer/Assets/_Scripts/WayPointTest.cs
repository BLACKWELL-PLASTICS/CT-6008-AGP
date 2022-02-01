using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointTest : MonoBehaviour
{
    public GameObject[] wayPoints;
    [Range(0.1f, 10)]
    public float stoppingDist;
    [Range(0, 20)]
    public float speed;

    private NavMeshAgent agent;
    private Transform target;
    private int counter = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.speed = speed;
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
