using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopState : Node
{
    public PoopState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        NetBirdPoop netBirdPoop = new NetBirdPoop();
        netBirdPoop.m_Player = owner.GetComponent<CarManagerScript>().m_playerNum;
        Client.Instance.SendToServer(netBirdPoop);
        FMODUnity.RuntimeManager.PlayOneShot(owner.poop, owner.gameObject.transform.position);
        owner.InventoryComponent.UsePowerup();
        return NodeState.SUCCESS;
    }
}

