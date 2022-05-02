using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBoost : Node
{
    public IsBoost(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (owner.powerUp1 == SeedPacketScript.POWERUPS.Boost)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
