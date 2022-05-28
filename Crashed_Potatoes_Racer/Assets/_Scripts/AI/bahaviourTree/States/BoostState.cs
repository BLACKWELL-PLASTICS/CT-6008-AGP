using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostState : Node //state for preforming boost power up - by anna
{
    public BoostState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        owner.transform.Find("Boost").GetComponent<ParticleSystem>().Play(); //plays vfx
        FMODUnity.RuntimeManager.PlayOneShot(owner.InventoryComponent.boostSound, owner.gameObject.transform.position); //plays sound effect
        owner.BoostSpeed(); //runs boost function in owner
        if (owner.timer >= 3f) //if over timer
        {
            owner.decreaseCheck = true;
            owner.transform.Find("Boost").GetComponent<ParticleSystem>().Stop(); //stops vfx
            owner.timer = 0f; //resets timer
            //Added by Iain
            //Boost start package
            NetBoost netBoost = new NetBoost();
            netBoost.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
            netBoost.m_CarNum = owner.GetComponent<CarManagerScript>().m_playerNum;
            netBoost.m_Action = NetBoost.ACTION.START;
            Client.Instance.SendToServer(netBoost);
            //Added by Iain ~
            owner.InventoryComponent.UsePowerup();//uses power up
            return NodeState.SUCCESS; //reyurns success
        }
        owner.timer += Time.deltaTime; //start timer
        return NodeState.RUNNING;//return running
    }
}
