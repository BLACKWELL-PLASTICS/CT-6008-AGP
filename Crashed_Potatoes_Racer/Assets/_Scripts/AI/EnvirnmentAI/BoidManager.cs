using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public Transform boidLocation;
    public int amountBoids;
    public float areaLimit;
    public float maxBoidSpeed;
    public float rotationSpeed;
    public float neighbourDist;
    public float goalRandomness;
    [HideInInspector]
    public Vector3 goalPos;
    [HideInInspector]
    public GameObject[] boidArray;

    private void Start()
    {
        boidArray = new GameObject[amountBoids];

        for (int i = 0; i < amountBoids; i++)
        {
            Vector3 position = new Vector3(Random.Range(boidLocation.position.x - areaLimit, boidLocation.position.x + areaLimit),
                                           Random.Range(boidLocation.position.y - areaLimit, boidLocation.position.y + areaLimit),
                                           Random.Range(boidLocation.position.z - areaLimit, boidLocation.position.z + areaLimit));
            boidArray[i] = (GameObject)Instantiate(boidPrefab, position, Quaternion.identity);
            boidArray[i].GetComponent<BoidController>().manager = this;
        }
    }
    void Update()
    {
        GoalPosRandom();
    }

    private void GoalPosRandom()
    {
        if (Random.Range(0, goalRandomness) < 50)
        {
            goalPos = new Vector3(Random.Range(boidLocation.position.x - areaLimit, boidLocation.position.x + areaLimit),
                                  Random.Range(boidLocation.position.y - areaLimit, boidLocation.position.y + areaLimit),
                                  Random.Range(boidLocation.position.z - areaLimit, boidLocation.position.z + areaLimit));
        }
    }
}
