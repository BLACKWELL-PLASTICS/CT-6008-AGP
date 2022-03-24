using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NetMakeMove netMakeMove = new NetMakeMove();
        netMakeMove.m_Player = GetComponent<CarManagerScript>().m_playerNum;
        netMakeMove.m_XPos = gameObject.transform.position.x;
        netMakeMove.m_YPos = gameObject.transform.position.y;
        netMakeMove.m_ZPos = gameObject.transform.position.z;
        netMakeMove.m_XRot = gameObject.transform.rotation.x;
        netMakeMove.m_YRot = gameObject.transform.rotation.y;
        netMakeMove.m_ZRot = gameObject.transform.rotation.z;
        netMakeMove.m_WRot = gameObject.transform.rotation.w;
        Client.Instance.SendToServer(netMakeMove);
    }
}
