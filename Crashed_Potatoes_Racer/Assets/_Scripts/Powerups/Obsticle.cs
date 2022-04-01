using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour {
    public GameObject prefab;
    GameObject wall;
    Vector3 eulerAngle;
    Quaternion currentRot;

    public void SpawnGO() {
        Vector3 pos = transform.position - transform.forward;
        pos.y = 2.3f;
        wall = Instantiate(prefab, pos, Quaternion.identity);

        eulerAngle = new Vector3(-90f, 0f, 0f);
        currentRot.eulerAngles = eulerAngle;
        wall.transform.rotation = currentRot;
        //Wall start package
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
    }
}
