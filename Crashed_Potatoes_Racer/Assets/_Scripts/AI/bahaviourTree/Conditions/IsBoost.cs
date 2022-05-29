using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBoost : Node // checks if boost power up is active - by anna
{
    public IsBoost(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (owner.powerUp1 == SeedPacketScript.POWERUPS.Boost) //if power up is boost
        {
            return NodeState.SUCCESS;//return success
        }
        else
        {
            return NodeState.FAILURE;//return fail
        }
    }
}
