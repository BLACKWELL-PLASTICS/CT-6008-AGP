using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopState : Node //state which preforms bird poop power up - by anna
{
    public PoopState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        //network stuff by iain
        //poop start packet
        NetBirdPoop netBirdPoop = new NetBirdPoop();
        netBirdPoop.m_Player = owner.GetComponent<CarManagerScript>().m_playerNum;
        Client.Instance.SendToServer(netBirdPoop);
        FMODUnity.RuntimeManager.PlayOneShot(owner.InventoryComponent.poopSound, owner.gameObject.transform.position); //plays sound effect
        owner.InventoryComponent.UsePowerup(); //uses power up
        return NodeState.SUCCESS; //returns success
    }
}

