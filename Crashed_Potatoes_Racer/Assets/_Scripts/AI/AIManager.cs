using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private static AIManager instance;

    //car ai
    [Header("Car Variables")]
    public Transform[] wayPoints;
    public Transform[] wayPoints2;
    public Transform[] wayPoints3;
    public Transform[] wayPoints4;
    public Transform[] wayPoints5;
    public Transform[] wayPoints6;
    public Transform[] wayPoints7;
    public Transform[] wayPoints8;
    public GameObject[] powerUps;
    public float maxSpeed;
    public float maxAcc;
    public float speedIncrease;
    public float slowDownPeriod;
    public float stoppingRay;
    public float detectionRay;
    public float detectionAngle;

    //car get/set
    public static float GetMaxAcc { get { return instance.maxAcc; } }
    public static float GetMaxSpeed { get { return instance.maxSpeed; } }
    public static float GetIncrease { get { return instance.speedIncrease; } }
    public static float GetSlowDownPeriod { get { return instance.slowDownPeriod; } }
    public static float GetStoppingRay { get { return instance.stoppingRay; } }
    public static float GetDetectionRay { get { return instance.detectionRay; } }
    public static float GetDetectionAngle { get { return instance.detectionAngle; } }
    public static Transform[] GetWaypoints { get { return instance.wayPoints; } }
    public static Transform[] GetWaypoints2 { get { return instance.wayPoints2; } }
    public static Transform[] GetWaypoints3 { get { return instance.wayPoints3; } }
    public static Transform[] GetWaypoints4 { get { return instance.wayPoints4; } }
    public static Transform[] GetWaypoints5 { get { return instance.wayPoints5; } }
    public static Transform[] GetWaypoints6 { get { return instance.wayPoints6; } }
    public static Transform[] GetWaypoints7 { get { return instance.wayPoints7; } }
    public static Transform[] GetWaypoints8 { get { return instance.wayPoints8; } }
    public static GameObject[] GetPowerUp { get { return instance.powerUps; } }


    private void Awake()
    {
        instance = this;
               
    }
    
}
