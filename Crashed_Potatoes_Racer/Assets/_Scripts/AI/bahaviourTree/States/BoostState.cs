using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostState : Node
{
    public BoostState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        owner.transform.Find("Boost").GetComponent<ParticleSystem>().Play();
        owner.BoostSpeed();
        if (owner.timer >= 3f)
        {
            owner.DeBoostSpeed();
            owner.transform.Find("Boost").GetComponent<ParticleSystem>().Stop();
            owner.timer = 0f;
            owner.InventoryComponent.UsePowerup();
            return NodeState.SUCCESS;
        }
        owner.timer += Time.deltaTime;
        return NodeState.RUNNING;
    }
}
