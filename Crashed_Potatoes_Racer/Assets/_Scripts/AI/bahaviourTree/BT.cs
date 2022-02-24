using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT : BTBase
{
    //composities
    private Selector rootSelector; //power up or none

    //nodes
    private WaypointState waypointNode;
    public BT(AIPlayer owner) : base(owner)
    {

        rootSelector = new Selector(owner);

        waypointNode = new WaypointState(owner);
    }
}
