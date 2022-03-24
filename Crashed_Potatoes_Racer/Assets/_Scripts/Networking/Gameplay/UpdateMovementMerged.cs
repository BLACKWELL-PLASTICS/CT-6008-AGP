using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMovementMerged : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        NetMerge netMerge = new NetMerge();
        netMerge.m_Player = GetComponent<CarManagerScript>().m_playerNum;
        netMerge.m_Action = NetMerge.ACTION.DRIVE;
        netMerge.m_Other = 0;
        netMerge.m_XPos = gameObject.transform.position.x;
        netMerge.m_YPos = gameObject.transform.position.y;
        netMerge.m_ZPos = gameObject.transform.position.z;
        netMerge.m_XRot = gameObject.transform.rotation.x;
        netMerge.m_YRot = gameObject.transform.rotation.y;
        netMerge.m_ZRot = gameObject.transform.rotation.z;
        netMerge.m_WRot = gameObject.transform.rotation.w;
        Client.Instance.SendToServer(netMerge);
    }
}
