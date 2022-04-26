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
        if (Physics.Raycast(owner.transform.position, owner.transform.TransformDirection(Vector3.back), out RaycastHit hit, AIManager.GetDetectionRay, LayerMask.GetMask("Player")))
        {
            Debug.Log("AI - backwards player in range");
            Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.back) * AIManager.GetDetectionRay, Color.green);
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}