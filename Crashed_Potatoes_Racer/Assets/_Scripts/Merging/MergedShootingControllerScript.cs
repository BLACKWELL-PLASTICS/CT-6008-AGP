using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedShootingControllerScript : MonoBehaviour
{
    [SerializeField]
    float m_turnSpeed;

    // Update is called once per frame
    void Update()
    {
        float verticalRotation = 0.0f;
        float horizontalRotation = 0.0f;

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
        transform.Rotate(verticalRotation, horizontalRotation, 0);
        NetMerge netMerge = new NetMerge();
        netMerge.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        netMerge.m_Action = NetMerge.ACTION.SHOOT;
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
