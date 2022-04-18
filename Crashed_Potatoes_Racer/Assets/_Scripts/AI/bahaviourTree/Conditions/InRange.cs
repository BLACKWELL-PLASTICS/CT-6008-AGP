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
        //owner.target = FindClosestCar();
        return NodeState.FAILURE;
    }

    //private Transform FindClosestCar()
    //{
    //    GameObject[] gos;
    //    gos = GameObject.FindGameObjectsWithTag("Player");
    //    GameObject closest = null;
    //    float distance = Mathf.Infinity;
    //    Vector3 position = owner.transform.position;
    //    foreach (GameObject go in gos)
    //    {
    //        Vector3 diff = go.transform.position - position;
    //        float curDistance = diff.sqrMagnitude;
    //        if (curDistance < distance && go != owner.gameObject)
    //        {
    //            closest = go;
    //            distance = curDistance;
    //        }
    //    }
    //    return closest.transform;
    //}
}