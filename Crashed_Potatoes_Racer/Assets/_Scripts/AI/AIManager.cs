using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public Transform[] wayPoints;
    //public Transform[] corners;
    public float maxSpeed;
    public float speedIncrease;

    private static AIManager instance;
    public static float GetMaxAcc { get { return instance.maxSpeed; } }
    public static float GetIncrease { get { return instance.speedIncrease; } }
    public static Transform[] GetWaypoints {get {return instance.wayPoints; } }
    //public static Transform[] GetCorners { get { return instance.corners; } }

    public static float LowerSpeed(float currentSpeed) { currentSpeed -= 4; return currentSpeed; }

    private void Awake()
    {
        instance = this;
    }
}
