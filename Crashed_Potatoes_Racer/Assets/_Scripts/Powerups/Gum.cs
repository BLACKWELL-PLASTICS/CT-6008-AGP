//////////////////////////////////////////////////
/// Created:                                   ///
/// Author: Oliver Blackwell                   ///
/// Edited By: Iain Farlow                     ///
/// Last Edited: 28/04/2022                    ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gum : MonoBehaviour
{
    public GameObject prefab;
    GameObject gum;
    Vector3 eulerAngle;
    Quaternion currentRot;

    public void SpawnGO() {
        Vector3 pos = transform.position - (transform.forward * 2);
        gum = Instantiate(prefab, pos, Quaternion.identity);

        eulerAngle = new Vector3(0f, 0f, 0f);
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
    }
}
