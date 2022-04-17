using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketState : Node
{
    public RocketState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        Object.Instantiate(AIManager.GetPowerUp[0], owner.transform.position + (owner.transform.forward * 2), Quaternion.LookRotation(owner.gameObject.transform.forward, owner.gameObject.transform.up));
        owner.InventoryComponent.UsePowerup();
        return NodeState.SUCCESS;
    }
}
