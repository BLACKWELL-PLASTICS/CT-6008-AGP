using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class WaypointState : Node not used - by anna
//{
//    public WaypointState(AIPlayer owner) : base(owner)
//    {

//    }

//    public override NodeState Update()
//    {
//        if(IsCorner() == true)
//        {
//            owner.decreaseCheck = true;
//        }
//        else
//        {
//            owner.IncreaseSpeed();
//        }

//        if (owner.randomSecret == 3)
//        {
//            float dist = Vector3.Distance(owner.transform.position, AIManager.GetWaypoints3[owner.currentWaypoint].transform.position);
//            if (dist <= owner.stoppingDistance)
//            {
//                Debug.Log("waypoint +");
//                owner.currentWaypoint++;
//                if (owner.currentWaypoint >= AIManager.GetWaypoints3.Length)
//                {
//                    owner.currentWaypoint = 0;
//                }
//            }
//            owner.target = AIManager.GetWaypoints3[owner.currentWaypoint].transform;
//            return NodeState.SUCCESS;
//        }
//        else
//        {
//            if (owner.randomInOut == 0)
//            {
//                float dist = Vector3.Distance(owner.transform.position, AIManager.GetWaypoints[owner.currentWaypoint].transform.position);
//                if (dist <= owner.stoppingDistance)
//                {
//                    Debug.Log("waypoint +");
//                    owner.currentWaypoint++;
//                    if (owner.currentWaypoint >= AIManager.GetWaypoints.Length)
//                    {
//                        owner.currentWaypoint = 0;
//                    }
//                }
//                owner.target = AIManager.GetWaypoints[owner.currentWaypoint].transform;
//                return NodeState.SUCCESS;
//            }
//            else if (owner.randomInOut == 1)
//            {
//                float dist = Vector3.Distance(owner.transform.position, AIManager.GetWaypoints2[owner.currentWaypoint].transform.position);
//                if (dist <= owner.stoppingDistance)
//                {
//                    Debug.Log("waypoint +");
//                    owner.currentWaypoint++;
//                    if (owner.currentWaypoint >= AIManager.GetWaypoints2.Length)
//                    {
//                        owner.currentWaypoint = 0;
//                    }
//                }
//                owner.target = AIManager.GetWaypoints2[owner.currentWaypoint].transform;
//                return NodeState.SUCCESS;
//            }
//        }

//        return NodeState.FAILURE;

//    }

//    private bool IsCorner()
//    {
//        Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.forward) * AIManager.GetStoppingRay, Color.white);
//        if (Physics.Raycast(owner.transform.position, owner.transform.TransformDirection(Vector3.forward), out RaycastHit hit, AIManager.GetStoppingRay, LayerMask.GetMask("Corner")))
//        {
//            Debug.Log("Slowing down");
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//}
