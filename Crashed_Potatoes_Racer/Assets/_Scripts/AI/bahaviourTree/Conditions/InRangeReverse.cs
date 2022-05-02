using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeReverse : Node
{
    public InRangeReverse(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.back) * AIManager.GetDetectionRay, Color.red);
        if (VisionCheck() == true)
        {
            Debug.Log("AI - backwards player in range");
            Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.back) * AIManager.GetDetectionRay, Color.green);
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }

    private bool VisionCheck()
    {
        Collider[] targetsInVR = Physics.OverlapSphere(owner.transform.position, AIManager.GetDetectionRay, LayerMask.GetMask("Player"));

        for (int i = 0; i < targetsInVR.Length; i++)
        {
            Transform target = targetsInVR[i].transform;
            if(target != owner.transform)
            {
                Vector3 dirToTarget = (target.position - owner.transform.position).normalized;
                if (Vector3.Angle(owner.transform.TransformDirection(Vector3.back), dirToTarget) < AIManager.GetDetectionAngle)
                {
                    float dstToTarget = Vector3.Distance(target.position, owner.transform.position);
                    if (!Physics.Raycast(owner.transform.position, dirToTarget, dstToTarget, LayerMask.GetMask("OffNav"))) //stuff like cave or walls or volcano
                    {
                        return true;
                    }
                }
            }
            
        }

        return false;
    }
}