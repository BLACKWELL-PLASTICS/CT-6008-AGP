using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sequencer : BaseComp //loops through nodes, only moving to next if current succeeds, only succeeds if all succeed - by Anna
{
     public Sequencer(AIPlayer owner) : base(owner)
     {

     }
     public override NodeState Update()
     {
        NodeState _nodeState = NodeState.FAILURE; //set fail as default 
        Node currentNode = nodes[currentNodeIndex];//current node - current node list + index

        if (currentNode != null)   //if theres a node
        {
            NodeState currentNodeState = currentNode.Update();  //run update on that node

            if (currentNodeState == NodeState.SUCCESS)  //if success
            {
                if (currentNodeIndex == nodes.Count - 1) //if not more nodes
                {
                    _nodeState = NodeState.SUCCESS; //succeed sequencer
                }
                else  //if more nodes
                {
                    ++currentNodeIndex;//move to next node
                    _nodeState = NodeState.RUNNING;//set sequencer to running
                }
            }
            else //if anything else
            {
                _nodeState = currentNodeState;//node state is current node state
            }
        }
        //if fail or success
        if (_nodeState == NodeState.SUCCESS || _nodeState == NodeState.FAILURE)
        {
            ResetIndex();//reset index
        }

        return _nodeState;  //return state
    }
}


