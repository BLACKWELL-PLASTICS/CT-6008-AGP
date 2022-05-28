using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumState : Node //state for preforming gum power - by anna
{
    private GameObject gum; // temp object for gum object
    private Vector3 eulerAngle; //temp vector for rotation
    private Quaternion currentRot; //temp for curretn rotation
    public GumState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        Vector3 pos = owner.transform.position - (owner.transform.forward * 2); //creates a posotion for spawning gum
        gum = Object.Instantiate(AIManager.GetPowerUp[2], pos, Quaternion.identity); //creates gum 
        owner.InventoryComponent.UsePowerup(); //uses power up

        eulerAngle = new Vector3(-90f, 0f, 0f); //sets up rotation change
        currentRot.eulerAngles = eulerAngle; //sets current to change
        gum.transform.rotation = currentRot; //rotates gum


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

        return NodeState.SUCCESS; //returns success
    }
}

