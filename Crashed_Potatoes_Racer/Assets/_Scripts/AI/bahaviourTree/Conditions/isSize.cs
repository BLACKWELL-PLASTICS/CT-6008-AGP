using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSize : Node
{
    public IsSize(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if(owner.powerUp1 == SeedPacketScript.POWERUPS.Size_Increase)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
