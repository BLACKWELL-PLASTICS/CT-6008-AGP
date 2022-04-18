using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BaseComp 
{
    public Selector(AIPlayer owner) : base(owner)
    {

    }
    public override NodeState Update()
    {
        NodeState _nodeState = NodeState.FAILURE; 
        Node currentNode = nodes[currentNodeIndex]; 

        if (currentNode != null) 
        {
            NodeState currentNodeState = currentNode.Update(); 

            if (currentNodeState == NodeState.FAILURE) 
            {
                if (currentNodeIndex == nodes.Count - 1) 
                {
                    _nodeState = NodeState.FAILURE; 
                }
                else 
                {
                    ++currentNodeIndex; 
                    _nodeState = NodeState.RUNNING; 
                }
            }
            else 
            {
                _nodeState = currentNodeState; 
            }
        }

        if (_nodeState == NodeState.SUCCESS || _nodeState == NodeState.FAILURE) 
        {
             ResetIndex(); 
        }

        return _nodeState; 
    }
}



