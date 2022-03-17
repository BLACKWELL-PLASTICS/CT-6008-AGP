using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT : BTBase
{
    //composities
    private Selector rootSelector; //power up or none
    private Selector powerUpSelector;

    //conditons
    //private IfActive ifActive;

    //nodes
    private WaypointState waypointNode;
    //private BoostNode boostNode;
    public BT(AIPlayer owner) : base(owner)
    {

        rootSelector = new Selector(owner);
        powerUpSelector = new Selector(owner);

        //ifActive = new IfActive(owner, AIManager.GetPowerUp);

        waypointNode = new WaypointState(owner);
       // boostNode = new BoostNode(owner);

        //linking nodes
        Root = rootSelector;

       // rootSelector.AddNode(powerUpSelector);
        rootSelector.AddNode(waypointNode);

        //powerUpSelector.AddNode(ifActive); //looks at active power up 
        //powerUpSelector.AddNode(boostNode); 
    }
}
