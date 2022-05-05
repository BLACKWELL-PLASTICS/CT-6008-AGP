using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumState : Node
{
    private GameObject gum;
    private Vector3 eulerAngle;
    private Quaternion currentRot;
    public GumState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        Vector3 pos = owner.transform.position - (owner.transform.forward * 2);
        gum = Object.Instantiate(AIManager.GetPowerUp[2], pos, Quaternion.identity);
        owner.InventoryComponent.UsePowerup();

        eulerAngle = new Vector3(-90f, 0f, 0f);
        currentRot.eulerAngles = eulerAngle;
        gum.transform.rotation = currentRot;


        //Added by Iain
        //gum start package
        NetGum netGum = new NetGum();
        netGum.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        netGum.m_Action = NetGum.ACTION.LAND;
        netGum.m_XPos = gum.transform.position.x;
        netGum.m_YPos = gum.transform.position.y;
        netGum.m_ZPos = gum.transform.position.z;
        netGum.m_XRot = gum.transform.rotation.x;
        netGum.m_YRot = gum.transform.rotation.y;
        netGum.m_ZRot = gum.transform.rotation.z;
        netGum.m_WRot = gum.transform.rotation.w;
        Client.Instance.SendToServer(netGum);
        //Added by Iain ~

        return NodeState.SUCCESS;
    }
}

