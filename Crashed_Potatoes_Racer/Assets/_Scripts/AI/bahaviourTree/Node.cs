using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState //all the sates of a node
{
    RUNNING,
    SUCCESS,
    FAILURE,
    NONE
}

public abstract class Node //template class that all nodes inherit from - by anna
{

    public AIPlayer owner { get; private set; } //getter setter to ai script  

    public Node(AIPlayer owner) //constructor sets ai script as owner
    {
        this.owner = owner;
    }

    public virtual NodeState Update() //acts as update function for nodes
    {
        return NodeState.NONE;
    }
}



