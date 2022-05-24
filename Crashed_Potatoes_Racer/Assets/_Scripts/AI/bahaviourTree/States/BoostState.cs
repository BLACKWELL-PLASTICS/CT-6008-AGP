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
        FMODUnity.RuntimeManager.PlayOneShot(owner.boost, owner.gameObject.transform.position);
        owner.BoostSpeed();
        if (owner.timer >= 3f)
        {
            owner.DeBoostSpeed();
            owner.transform.Find("Boost").GetComponent<ParticleSystem>().Stop();
            owner.timer = 0f;
            //Added by Iain
            //Boost start package
            NetBoost netBoost = new NetBoost();
            netBoost.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
            netBoost.m_CarNum = owner.GetComponent<CarManagerScript>().m_playerNum;
            netBoost.m_Action = NetBoost.ACTION.START;
            Client.Instance.SendToServer(netBoost);
            //Added by Iain ~
            owner.InventoryComponent.UsePowerup();
            return NodeState.SUCCESS;
        }
        owner.timer += Time.deltaTime;
        return NodeState.RUNNING;
    }
}
