using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRocket : Node
{
    public IsRocket(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if(owner.powerUp1 == SeedPacketScript.POWERUPS.Forward_Projectile)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
