using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFloaty : Node
{
    public IsFloaty(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (owner.powerUp1 == SeedPacketScript.POWERUPS.Obsticles)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
