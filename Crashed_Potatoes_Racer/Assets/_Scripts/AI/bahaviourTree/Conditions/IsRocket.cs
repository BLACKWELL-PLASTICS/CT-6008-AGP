using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRocket : Node //checks whether rocket power up is active- by anna
{
    public IsRocket(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if(owner.powerUp1 == SeedPacketScript.POWERUPS.Forward_Projectile) //if power up is rocket
        {
            return NodeState.SUCCESS; //reyurn success
        }
        else
        {
            return NodeState.FAILURE; //return fail
        }
    }
}
