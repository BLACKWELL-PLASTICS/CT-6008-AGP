using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabScript : MonoBehaviour
{
    private float timer = 0;
    private NavMeshAgent crab;
    private void Start()
    {
        crab = GetComponent<NavMeshAgent>();
        crab.speed = AIManager.GetCrabSpeed;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= AIManager.GetCrabTimer)
        {
            Vector3 newPos = RandomPoint(transform.position);
            crab.SetDestination(newPos);
            timer = 0;
        }
    }
    private static Vector3 RandomPoint(Vector3 origin)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * AIManager.GetCrabDistance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, AIManager.GetCrabDistance, 9);

        return navHit.position;
    }
}
