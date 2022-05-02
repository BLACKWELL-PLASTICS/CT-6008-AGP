using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabScript : MonoBehaviour
{
    public float crabDistance = 3;
    public float crabTimer = 5;
    public float crabSpeed = 2;

    private float timer = 0;
    private NavMeshAgent crab;
    private Animator anim;
    private Vector3 newPos;
    private void Start()
    {
        crab = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        crab.speed = crabSpeed;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= crabTimer)
        {
            newPos = RandomPoint(transform.position);
            crab.SetDestination(newPos);
            timer = 0;
        }

        if(transform.position == newPos)
        {
            crab.isStopped = true;
            anim.SetBool("Walking", false);
        }
        else
        {
            crab.isStopped = false;
            anim.SetBool("Walking", true);
        }
    }
    private Vector3 RandomPoint(Vector3 origin)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * crabDistance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, crabDistance, 9);

        return navHit.position;
    }
}
