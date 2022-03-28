using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private static AIManager instance;

    //car ai
    [Header("Car Variables")]
    public Transform[] wayPoints;
    public float maxSpeed;
    public float maxAcc;
    public float speedIncrease;
    public float speedDecrease;
    public float slowDownPeriod;
    public float stoppingRay;
    //boid variables
    [Header("Boid Variables")]
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
    //fish variables
    [Header("Fish Variables")]
    public float fishRandomness;

    //car get/set
    public static float GetMaxAcc { get { return instance.maxAcc; } }
    public static float GetMaxSpeed { get { return instance.maxSpeed; } }
    public static float GetIncrease { get { return instance.speedIncrease; } }
    public static float GetDecrease { get { return instance.speedDecrease; } }
    public static float GetSlowDownPeriod { get { return instance.slowDownPeriod; } }
    public static float GetStoppingRay { get { return instance.stoppingRay; } }
    public static Transform[] GetWaypoints { get { return instance.wayPoints; } }

    //boid get/set
    public static float GetMaxBoidSpeed { get { return instance.maxBoidSpeed; } }
    public static float GetAreaLimit { get { return instance.areaLimit; } }
    public static float GetBoidRotation { get { return instance.rotationSpeed; } }
    public static float GetNeighbourDist { get { return instance.neighbourDist; } }
    public static Vector3 GetGoalPos { get { return instance.goalPos; } }
    public static GameObject[] GetBoids { get { return instance.boidArray; } }
    public static Transform GetBoidStart { get { return instance.boidLocation; } }

    //fish get/set
    public static float GetFishRandomness { get { return instance.fishRandomness; } }

    private void Awake()
    {
        instance = this;

        boidArray = new GameObject[amountBoids];

        for (int i = 0; i < amountBoids; i++)
        {
            Vector3 position = new Vector3(Random.Range(boidLocation.position.x - areaLimit, boidLocation.position.x + areaLimit), 
                                           Random.Range(boidLocation.position.y -areaLimit, boidLocation.position.y + areaLimit), 
                                           Random.Range(boidLocation.position.z -areaLimit, boidLocation.position.z + areaLimit));
            boidArray[i] = (GameObject)Instantiate(boidPrefab, position, Quaternion.identity); 
            boidArray[i].GetComponent<BoidController>().manager = this; 
        }
    }
    private void Update()
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
