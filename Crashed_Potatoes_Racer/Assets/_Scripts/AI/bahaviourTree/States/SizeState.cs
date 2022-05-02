using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeState : Node
{

    public SizeState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {

        if (owner.timer > 3f)
        {
            owner.transform.localScale = owner.originalScale;
            owner.timer = 0f;
            owner.transform.position = new Vector3(owner.currentPos.x, owner.currentPos.y - 1f, owner.currentPos.z);
            owner.InventoryComponent.UsePowerup();
            return NodeState.SUCCESS;
        }
        else if(owner.timer == 0)
        {
            owner.transform.localScale = owner.originalScale * 1.5f;
            owner.transform.position = new Vector3(owner.currentPos.x, owner.currentPos.y + 1f, owner.currentPos.z);
        }

        owner.timer += Time.deltaTime;
        return NodeState.RUNNING;
    }
}
