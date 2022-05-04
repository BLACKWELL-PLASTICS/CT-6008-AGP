﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBird : Node
{
    public IsBird(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (owner.powerUp1 == SeedPacketScript.POWERUPS.Blind)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
