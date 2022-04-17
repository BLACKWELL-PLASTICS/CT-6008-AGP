using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedShootingControllerScript : MonoBehaviour
{
    public int m_playerNum;

    [SerializeField]
    bool m_active;
    [SerializeField]
    float m_turnSpeed;

    // Update is called once per frame
    void Update()
    {
        float verticalRotation = 0.0f;
        float horizontalRotation = 0.0f;
        if (m_active)
        {
            if (Input.GetKey(KeyCode.W))
            {
                verticalRotation = m_turnSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                verticalRotation = -1 * m_turnSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                horizontalRotation = m_turnSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                horizontalRotation = -1 * m_turnSpeed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("Fire");
            }

            GetPartToRotate(this.gameObject, 1).transform.Rotate(0, 0, horizontalRotation);
            GetPartToRotate(this.gameObject, 2).transform.Rotate(0, verticalRotation, 0);

            NetMerge netMerge = new NetMerge();
            netMerge.m_Player = m_playerNum;
            netMerge.m_Action = NetMerge.ACTION.SHOOT;
            netMerge.m_Other = 0;
            netMerge.m_XPos = 0;
            netMerge.m_YPos = 0;
            netMerge.m_ZPos = 0;
            netMerge.m_XRot = GetPartToRotate(this.gameObject, 1).transform.rotation.x;
            netMerge.m_YRot = GetPartToRotate(this.gameObject, 1).transform.rotation.y;
            netMerge.m_ZRot = GetPartToRotate(this.gameObject, 1).transform.rotation.z;
            netMerge.m_WRot = GetPartToRotate(this.gameObject, 1).transform.rotation.w;
            netMerge.m_secondXRot = GetPartToRotate(this.gameObject, 2).transform.rotation.x;
            netMerge.m_secondYRot = GetPartToRotate(this.gameObject, 2).transform.rotation.y;
            netMerge.m_secondZRot = GetPartToRotate(this.gameObject, 2).transform.rotation.z;
            netMerge.m_secondWRot = GetPartToRotate(this.gameObject, 2).transform.rotation.w;
            Client.Instance.SendToServer(netMerge);
        }
    }

    GameObject GetPartToRotate(GameObject a_base, int a_index)
    {
        GameObject currentPart = a_base;

        for (int i = 0; i <= a_index; i++)
        {
            currentPart = currentPart.transform.GetChild(0).gameObject;
        }

        return currentPart;
    }
}
