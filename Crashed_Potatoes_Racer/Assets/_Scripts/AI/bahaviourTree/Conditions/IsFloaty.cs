using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFloaty : Node //checks whether floaty power up is active - by anna
{
    public IsFloaty(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (owner.powerUp1 == SeedPacketScript.POWERUPS.Obsticles) //if power up is floaty
        {
            return NodeState.SUCCESS;//return success
        }
        else
        {
            return NodeState.FAILURE;//reyurn fail
        }
    }
}
