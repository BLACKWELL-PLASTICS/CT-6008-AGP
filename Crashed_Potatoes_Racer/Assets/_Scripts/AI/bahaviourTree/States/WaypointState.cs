using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointState : Node
{
    public WaypointState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if(IsCorner() == true)
        {
            owner.decreaseCheck = true;
        }
        else
        {
            owner.IncreaseSpeed();
        }

        float dist = Vector3.Distance(owner.transform.position, AIManager.GetWaypoints[owner.currentWaypoint].transform.position);
        if (dist <= owner.stoppingDistance)
        {
            Debug.Log("waypoint +");
            owner.currentWaypoint++;
            if (owner.currentWaypoint >= AIManager.GetWaypoints.Length)
            {
                owner.currentWaypoint = 0;
            }
        }
        owner.target = AIManager.GetWaypoints[owner.currentWaypoint].transform;
        return NodeState.SUCCESS;
    }

    private bool IsCorner()
    {
        Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.forward) * 10, Color.red);
        if (Physics.Raycast(owner.transform.position, owner.transform.TransformDirection(Vector3.forward), out RaycastHit hit, 10, LayerMask.GetMask("Corner")))
        {
            Debug.Log("Slowing down");
            return true;
        }
        else
        {
            return false;
        }
    }

}
