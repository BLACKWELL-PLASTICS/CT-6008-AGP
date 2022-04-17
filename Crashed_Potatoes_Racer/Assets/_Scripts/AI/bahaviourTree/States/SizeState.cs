using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeState : Node
{
    private float timer = 0f;

    public SizeState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        

        return NodeState.SUCCESS;
    }
}
