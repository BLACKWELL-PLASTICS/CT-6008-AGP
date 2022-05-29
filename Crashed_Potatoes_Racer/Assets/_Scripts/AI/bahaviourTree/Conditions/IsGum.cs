using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGum : Node //checks whether gum power up is active - by anna
{
    public IsGum(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (owner.powerUp1 == SeedPacketScript.POWERUPS.Gum) // if power up is gum
        {
            return NodeState.SUCCESS; //reyurn success
        }
        else
        {
            return NodeState.FAILURE;//return fail
        }
    }
}