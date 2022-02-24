using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public Transform[] wayPoints;
    public float maxSpeed;

    private static AIManager instance;
    public static float GetMaxSpeed { get { return instance.maxSpeed; } }
    public static Transform[] GetWaypoints {get {return instance.wayPoints; } }

    public static float LowerSpeed(float currentSpeed) { currentSpeed -= 4; return currentSpeed; }

    private void Awake()
    {
        instance = this;
    }
}
