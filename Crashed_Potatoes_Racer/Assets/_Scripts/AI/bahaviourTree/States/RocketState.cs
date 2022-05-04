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
        GameObject rocket = Object.Instantiate(AIManager.GetPowerUp[0], owner.transform.position + (owner.transform.forward * 2), Quaternion.LookRotation(owner.gameObject.transform.forward, owner.gameObject.transform.up));
        rocket.GetComponent<Rocket>().Owner(owner.gameObject);
        //Added by Iain
        Vector3 spawnPos = owner.transform.position + (owner.transform.forward * 2);
        Quaternion spawnRot = Quaternion.LookRotation(owner.gameObject.transform.forward, owner.gameObject.transform.up);
        //Rocket start package
        NetRocket netRocket = new NetRocket();
        netRocket.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        netRocket.m_Action = NetRocket.ACTION.FIRE;
        netRocket.m_XPos = spawnPos.x;
        netRocket.m_YPos = spawnPos.y;
        netRocket.m_ZPos = spawnPos.z;
        netRocket.m_XRot = spawnRot.x;
        netRocket.m_YRot = spawnRot.y;
        netRocket.m_ZRot = spawnRot.z;
        netRocket.m_WRot = spawnRot.w;
        Client.Instance.SendToServer(netRocket);
        //Added by Iain ~
        owner.InventoryComponent.UsePowerup();
        return NodeState.SUCCESS;
    }
}
