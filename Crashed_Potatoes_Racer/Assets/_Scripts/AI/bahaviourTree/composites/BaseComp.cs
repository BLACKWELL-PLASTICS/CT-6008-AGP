using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseComp : Node //base script that hold data and functions shared by selectors and sequences - by Anna
{
    public List<Node> nodes { get; private set; } //start a list of all nodes
    protected int currentNodeIndex = 0; //index for current node

    protected BaseComp(AIPlayer owner) : base(owner) //constructor for setting up list and index
    {
        currentNodeIndex = 0; 
        nodes = new List<Node>(); 
    }

    public void AddNode(Node newNode) //function for adding node to node list
    {
        nodes.Add(newNode);
    }

    protected void ResetIndex() //function for setting index and current node
    {
        currentNodeIndex = 0;
    }
}


