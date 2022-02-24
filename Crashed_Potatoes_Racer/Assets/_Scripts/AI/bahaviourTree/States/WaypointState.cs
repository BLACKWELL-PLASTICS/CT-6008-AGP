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
        owner.NavComponent.speed = AIManager.GetMaxSpeed; //TD gradual increase of speed

        float dist = Vector3.Distance(owner.transform.position, AIManager.GetWaypoints[owner.currentWaypoint].transform.position);
        if (dist <= owner.stoppingDistance)
        {
            owner.currentWaypoint++;
            if (owner.currentWaypoint >= AIManager.GetWaypoints.Length)
            {
                owner.currentWaypoint = 0;
            }
        }
        owner.target = AIManager.GetWaypoints[owner.currentWaypoint].transform;
        return NodeState.SUCCESS;
    }
}
