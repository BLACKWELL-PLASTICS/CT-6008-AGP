using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyState : Node
{
    private GameObject wall;
    private Vector3 eulerAngle;
    private Quaternion currentRot;

    public FloatyState(AIPlayer owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        Vector3 pos = owner.transform.position - owner.transform.forward;
        wall = Object.Instantiate(AIManager.GetPowerUp[1], pos, Quaternion.identity);
        owner.InventoryComponent.UsePowerup();

        //eulerAngle = new Vector3(-90f, 0f, 0f);
        //currentRot.eulerAngles = eulerAngle;
        //wall.transform.rotation = currentRot;
        ////Wall start package
        //NetWall netWall = new NetWall();
        //netWall.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        //netWall.m_Action = NetWall.ACTION.LAND;
        //netWall.m_XPos = wall.transform.position.x;
        //netWall.m_YPos = wall.transform.position.y;
        //netWall.m_ZPos = wall.transform.position.z;
        //netWall.m_XRot = wall.transform.rotation.x;
        //netWall.m_YRot = wall.transform.rotation.y;
        //netWall.m_ZRot = wall.transform.rotation.z;
        //netWall.m_WRot = wall.transform.rotation.w;
        //Client.Instance.SendToServer(netWall);

        return NodeState.SUCCESS;
    }
}
