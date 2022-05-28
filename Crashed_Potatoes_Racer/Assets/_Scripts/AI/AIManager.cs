using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour //static variable script, accessible by all scripts, allows designers and programmers to easily tweak ai values - By Anna
{
    private static AIManager instance; //instance of this class

    //car ai
    [Header("Car Variables")]
    //waypoints
    public Transform[] wayPoints;
    public Transform[] wayPoints2;
    public Transform[] wayPoints3;
    public Transform[] wayPoints4;
    public Transform[] wayPoints5;
    public Transform[] wayPoints6;
    public Transform[] wayPoints7;
    public Transform[] wayPoints8;
    //power up prefabs
    public GameObject[] powerUps;
    //max speed and acceleration
    public float maxSpeed;
    public float maxAcc;
    //speed in which it speeds up again
    public float speedIncrease;
    //time it slows down for
    public float slowDownPeriod;
    //distance it needs to be for it to waypoint before stopping
    public float stoppingRay;
    //length of detection ray, detecting corners and players
    public float detectionRay;
    //angle for vision cones
    public float detectionAngle;

    //car getert/setter
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
        instance = this; //set instance to this
               
    }
    
}
