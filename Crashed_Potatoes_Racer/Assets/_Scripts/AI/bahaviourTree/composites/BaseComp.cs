using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseComp : Node 
{
    public List<Node> nodes { get; private set; } 
    protected int currentNodeIndex = 0;

    protected BaseComp(AIPlayer owner) : base(owner)
    {
        currentNodeIndex = 0; 
        nodes = new List<Node>(); 
    }

    public void AddNode(Node newNode)
    {
        nodes.Add(newNode);
    }

    protected void ResetIndex() 
    {
        currentNodeIndex = 0;
    }
}


