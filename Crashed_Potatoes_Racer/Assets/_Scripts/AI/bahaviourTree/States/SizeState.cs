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

        //if (owner.timer > 3f)
        //{
        //    owner.transform.localScale = owner.originalScale;
        //    owner.timer = 0f;
        //    owner.transform.position = new Vector3(owner.currentPos.x, owner.currentPos.y - 1f, owner.currentPos.z);
        //    owner.InventoryComponent.UsePowerup();
        //    NetGrow netGrow = new NetGrow();
        //    netGrow.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        //    netGrow.m_Action = NetGrow.ACTION.START;
        //    Client.Instance.SendToServer(netGrow);
        //    //Added by Iain ~
        //    return NodeState.SUCCESS;
        //}
        //else if(owner.timer == 0)
        //{
        //    owner.transform.localScale = owner.originalScale * 1.5f;
        //    owner.transform.position = new Vector3(owner.currentPos.x, owner.currentPos.y + 1f, owner.currentPos.z);
        //    //Added by Iain
        //    //shrink package
        //    NetGrow netGrow = new NetGrow();
        //    netGrow.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        //    netGrow.m_Action = NetGrow.ACTION.END;
        //    Client.Instance.SendToServer(netGrow);
        //    Destroy(gameObject.GetComponent<SizeIncrease>());
        //    //Added by Iain ~
        //}

        //owner.timer += Time.deltaTime;
        ////Added by Iain
        ////grow packet
        //return NodeState.RUNNING;

        owner.gameObject.AddComponent<SizeIncrease>();
        owner.InventoryComponent.UsePowerup();
        return NodeState.SUCCESS;
    }
}
