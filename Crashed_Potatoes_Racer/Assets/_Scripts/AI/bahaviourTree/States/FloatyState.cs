using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyState : Node //state for preforming floaty power up - by anna
{
    private GameObject wall;// temp object for floaty object
    private Vector3 eulerAngle; //temp vector for rotation
    private Quaternion currentRot; //temp for curretn rotation

    public FloatyState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        Vector3 pos = owner.transform.position - owner.transform.forward;//creates a posotion for spawning floaty
        wall = Object.Instantiate(AIManager.GetPowerUp[1], pos, Quaternion.identity);//creates floaty 
        owner.InventoryComponent.UsePowerup(); //uses power up

        eulerAngle = new Vector3(-90f, 0f, 0f);//sets up rotation change
        currentRot.eulerAngles = eulerAngle;//sets current to change
        wall.transform.rotation = currentRot;//rotates gum
        //Wall start package -- Iain
        NetWall netWall = new NetWall();
        netWall.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        netWall.m_Action = NetWall.ACTION.LAND;
        netWall.m_XPos = wall.transform.position.x;
        netWall.m_YPos = wall.transform.position.y;
        netWall.m_ZPos = wall.transform.position.z;
        netWall.m_XRot = wall.transform.rotation.x;
        netWall.m_YRot = wall.transform.rotation.y;
        netWall.m_ZRot = wall.transform.rotation.z;
        netWall.m_WRot = wall.transform.rotation.w;
        Client.Instance.SendToServer(netWall);

        return NodeState.SUCCESS;//returns success
    }
}
