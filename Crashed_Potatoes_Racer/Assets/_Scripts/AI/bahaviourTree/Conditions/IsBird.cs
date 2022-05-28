using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBird : Node //checks if bird poop is active - by anna
{
    public IsBird(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (owner.powerUp1 == SeedPacketScript.POWERUPS.Blind) //if power up is bird poop
        {
            return NodeState.SUCCESS;//return success
        }
        else
        {
            return NodeState.FAILURE;//return fail
        }
    }
}
