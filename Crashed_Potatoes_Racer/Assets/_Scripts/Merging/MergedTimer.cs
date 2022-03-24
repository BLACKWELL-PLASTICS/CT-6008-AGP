using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedTimer : MonoBehaviour
{
    public float m_maxTimer;

    [SerializeField]
    bool m_drivingVarient;

    float m_timer;

    // Start is called before the first frame update
    void Start()
    {
        m_timer = m_maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timer > 0)
        {
            m_timer -= Time.deltaTime;
        }
        else
        {
            //if (m_drivingVarient)
            //{
            //    NetMerge netMerge = new NetMerge();
            //    netMerge.m_Player = GetComponent<CarManagerScript>().m_playerNum;
            //    netMerge.m_Action = NetMerge.ACTION.DEMERGE;
            //    netMerge.m_Other = GetComponentInChildren<MergedShootingControllerScript>().m_playerNum;
            //    netMerge.m_XPos = 0;
            //    netMerge.m_YPos = 0;
            //    netMerge.m_ZPos = 0;
            //    netMerge.m_XRot = 0;
            //    netMerge.m_YRot = 0;
            //    netMerge.m_ZRot = 0;
            //    netMerge.m_WRot = 0;
            //    Client.Instance.SendToServer(netMerge);
            //}
            //else
            //{
            //    NetMerge netMerge = new NetMerge();
            //    netMerge.m_Player = GetComponentInChildren<MergedShootingControllerScript>().m_playerNum;
            //    netMerge.m_Action = NetMerge.ACTION.DEMERGE;
            //    netMerge.m_Other = GetComponent<CarManagerScript>().m_playerNum;
            //    netMerge.m_XPos = 0;
            //    netMerge.m_YPos = 0;
            //    netMerge.m_ZPos = 0;
            //    netMerge.m_XRot = 0;
            //    netMerge.m_YRot = 0;
            //    netMerge.m_ZRot = 0;
            //    netMerge.m_WRot = 0;
            //    Client.Instance.SendToServer(netMerge);
            //}
        }
    }
}
