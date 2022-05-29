using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeReverse : Node //checks if their is a car in range behind - by anna
{
    public InRangeReverse(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.back) * AIManager.GetDetectionRay, Color.red); //draw a red ray to notice they have power up
        if (VisionCheck() == true) //if vision cone detects car
        {
            Debug.Log("AI - backwards player in range"); //log
            Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.back) * AIManager.GetDetectionRay, Color.green); //draw a green ray to show firing
            return NodeState.SUCCESS; //return success
        }

        return NodeState.FAILURE; //return fail
    }

    private bool VisionCheck() //vision cone 
    {
        Collider[] targetsInVR = Physics.OverlapSphere(owner.transform.position, AIManager.GetDetectionRay, LayerMask.GetMask("Player")); //gets all colliders in distance listed as player

        for (int i = 0; i < targetsInVR.Length; i++) //loops through
        {
            Transform target = targetsInVR[i].transform; //sets as target
            if(target != owner.transform) //if not this cars transform
            {
                Vector3 dirToTarget = (target.position - owner.transform.position).normalized; //calculate direction to target
                if (Vector3.Angle(owner.transform.TransformDirection(Vector3.back), dirToTarget) < AIManager.GetDetectionAngle)//if within that angle
                {
                    float dstToTarget = Vector3.Distance(target.position, owner.transform.position); //calculate disatnce
                    if (!Physics.Raycast(owner.transform.position, dirToTarget, dstToTarget, LayerMask.GetMask("OffNav"))) //if nothing is inbetween such as walls or cave
                    {
                        return true; //return true
                    }
                }
            }
            
        }

        return false; //return false
    }
}