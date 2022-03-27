using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    private float speed;
    [HideInInspector]
    public AIManager manager;

    void Start()
    {
        speed = Random.Range(1, AIManager.GetMaxBoidSpeed);
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, AIManager.GetBoidStart.position) >= AIManager.GetAreaLimit)
        {
            Vector3 direction = AIManager.GetBoidStart.position - transform.position; 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), AIManager.GetBoidRotation * Time.deltaTime); 
            speed = Random.Range(1, 15); 
        }
        else
        {
            if (Random.Range(0, 5) < 1) 
            {
                ApplyRules(); 
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed); 
    }

    private void ApplyRules()
    {
        GameObject[] otherBoids;
        otherBoids = AIManager.GetBoids; 

        Vector3 center = Vector3.zero; 
        Vector3 avoid = Vector3.zero; 
        Vector3 goalPos = AIManager.GetGoalPos;

        float distance; 
        int groupSize = 0; 
        float averageSpeed = 0.1f; 

        foreach (GameObject i in otherBoids) 
        {
            if (i != this.gameObject) 
            {
                distance = Vector3.Distance(i.transform.position, this.transform.position); 
                if (distance <= AIManager.GetNeighbourDist) 
                {
                    center += i.transform.position; 
                    groupSize++; 

                    if (distance < 1.5) 
                    {
                        avoid = avoid + (this.transform.position - i.transform.position); 
                    }
                    BoidController anotherFlock = i.GetComponent<BoidController>(); 
                    averageSpeed += anotherFlock.speed;
                }
            }
        }
        if (groupSize > 0)
        {
            center = center / groupSize + (goalPos - this.transform.position);
            speed = averageSpeed / groupSize;

            Vector3 direction = (center + avoid) - transform.position;
            if (direction != AIManager.GetBoidStart.position)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), AIManager.GetBoidRotation * Time.deltaTime);

        }

    }
}
