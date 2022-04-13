using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRange : Node
{
    public InRange(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.forward) * AIManager.GetDetectionRay, Color.red);
        if (Physics.Raycast(owner.transform.position, owner.transform.TransformDirection(Vector3.forward), out RaycastHit hit, AIManager.GetDetectionRay, LayerMask.GetMask("Player")))
        {
            Debug.Log("AI - forward player in range");
            Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.forward) * AIManager.GetDetectionRay, Color.green);
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
}