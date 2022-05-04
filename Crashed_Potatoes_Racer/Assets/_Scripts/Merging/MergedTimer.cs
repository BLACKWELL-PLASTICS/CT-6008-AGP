using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedTimer : MonoBehaviour
{
    public float m_maxTimer;

    [SerializeField]
    bool m_drivingVarient;

    float m_timer;
    GameObject m_timerBar;

    // Start is called before the first frame update
    void Start()
    {
        m_timer = m_maxTimer;
        m_timerBar = GameObject.Find("MergeSlider");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timer > 0)
        {
            m_timer -= Time.deltaTime;
            m_timerBar.GetComponent<UnityEngine.UI.Slider>().value = m_timer / m_maxTimer;
        }
        else
        {
            if (m_drivingVarient)
            {
                NetMerge netMerge = new NetMerge();
                netMerge.m_Player = GetComponent<CarManagerScript>().m_playerNum;
                netMerge.m_Action = NetMerge.ACTION.DEMERGE;
                netMerge.m_Other = GetComponentInChildren<MergedShootingControllerScript>().m_playerNum;
                netMerge.m_XPos = 0;
                netMerge.m_YPos = 0;
                netMerge.m_ZPos = 0;
                netMerge.m_XRot = 0;
                netMerge.m_YRot = 0;
                netMerge.m_ZRot = 0;
                netMerge.m_WRot = 0;
                netMerge.m_secondXRot = 0;
                netMerge.m_secondYRot = 0;
                netMerge.m_secondZRot = 0;
                netMerge.m_secondWRot = 0;
                //netMerge.m_lapNum = GetComponentInChildren<WinCondition>().lap;
                //netMerge.m_lapNum = GetComponentInChildren<WinCondition>().checkpointNumber;
                Client.Instance.SendToServer(netMerge);
            }
            else
            {
                NetMerge netMerge = new NetMerge();
                netMerge.m_Player = GetComponentInChildren<MergedShootingControllerScript>().m_playerNum;
                netMerge.m_Action = NetMerge.ACTION.DEMERGE;
                netMerge.m_Other = GetComponent<CarManagerScript>().m_playerNum;
                netMerge.m_XPos = 0;
                netMerge.m_YPos = 0;
                netMerge.m_ZPos = 0;
                netMerge.m_XRot = 0;
                netMerge.m_YRot = 0;
                netMerge.m_ZRot = 0;
                netMerge.m_WRot = 0;
                netMerge.m_secondXRot = 0;
                netMerge.m_secondYRot = 0;
                netMerge.m_secondZRot = 0;
                netMerge.m_secondWRot = 0;
                //netMerge.m_lapNum = GetComponent<WinCondition>().lap;
                //netMerge.m_lapNum = GetComponent<WinCondition>().checkpointNumber;
                Client.Instance.SendToServer(netMerge);
            }
            m_timerBar.GetComponent<UnityEngine.UI.Slider>().value = 1;
            m_timerBar.SetActive(false);
            Destroy(this);
        }
    }
}
