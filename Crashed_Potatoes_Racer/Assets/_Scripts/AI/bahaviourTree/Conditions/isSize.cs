using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSize : Node //checks if size power up is active- by anna
{
    public IsSize(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if(owner.powerUp1 == SeedPacketScript.POWERUPS.Size_Increase) //if power up is zie
        {
            return NodeState.SUCCESS; //return success
        }
        else
        {
            return NodeState.FAILURE; //return fail
        }
    }
}
