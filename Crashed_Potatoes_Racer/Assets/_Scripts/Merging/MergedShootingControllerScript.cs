using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class MergedShootingControllerScript : MonoBehaviour
{
    public int m_playerNum;
    public GameObject m_gun;

    [SerializeField]
    bool m_active;
    [SerializeField]
    float m_turnSpeed;
    [SerializeField]
    GameObject m_projectilePrefab;
    [SerializeField]
    GameObject m_explosivePrefab;
    [SerializeField]
    GameObject m_minePrefab;
    [SerializeField]
    float m_fireForce = 100.0f;
    [SerializeField]
    float m_arcForce = 25.0f;
    [SerializeField]
    float m_horizontalCap = 180.0f;
    [SerializeField]
    float m_verticalCap = 360.0f;

    // Controller Vibration
    PlayerIndex index;
    //Projectile
    GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        float verticalRotation = 0.0f;
        float horizontalRotation = 0.0f;
        if (m_active)
        {
            // Controller Support - Done by Oliver 
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            // 0.19 is the deadzone number
            if (vertical > 0.19f || Input.GetKey(KeyCode.S))
            {
                verticalRotation = m_turnSpeed * Time.deltaTime;
                GamePad.SetVibration(index, 0.1f, 0.1f);
            }
            if (vertical < -0.19f || Input.GetKey(KeyCode.W))
            {
                verticalRotation = -1 * m_turnSpeed * Time.deltaTime;
                GamePad.SetVibration(index, 0.1f, 0.1f);
            }
            if (horizontal > 0.19f || Input.GetKey(KeyCode.D))
            {
                horizontalRotation = m_turnSpeed * Time.deltaTime;
                GamePad.SetVibration(index, 0.1f, 0.3f);
            }
            if (horizontal < -0.19f || Input.GetKey(KeyCode.A))
            {
                horizontalRotation = -1 * m_turnSpeed * Time.deltaTime;
                GamePad.SetVibration(index, 0.3f, 0.1f);
            }
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("Fire");
                Fire();
            }

            //if (Input.GetKey(KeyCode.S))
            //{
            //    verticalRotation = m_turnSpeed * Time.deltaTime;
            //}
            //else if (Input.GetKey(KeyCode.W))
            //{
            //    verticalRotation = -1 * m_turnSpeed * Time.deltaTime;
            //}
            //if (Input.GetKey(KeyCode.D))
            //{
            //    horizontalRotation = m_turnSpeed * Time.deltaTime;
            //}
            //else if (Input.GetKey(KeyCode.A))
            //{
            //    horizontalRotation = -1 * m_turnSpeed * Time.deltaTime;
            //}
            //if (Input.GetKeyDown(KeyCode.H))
            //{
            //    Debug.Log("Fire");
            //    Fire();
            //}

            if (GetPartToRotate(this.gameObject, 2).transform.localEulerAngles.y < 180.0f)
            {
                if (GetPartToRotate(this.gameObject, 2).transform.localEulerAngles.y + verticalRotation < m_verticalCap)
                {
                    GetPartToRotate(this.gameObject, 2).transform.Rotate(0, verticalRotation, 0);
                }
            }
            else
            {
                if (GetPartToRotate(this.gameObject, 2).transform.localEulerAngles.y - 360 + verticalRotation > -m_verticalCap)
                {
                    GetPartToRotate(this.gameObject, 2).transform.Rotate(0, verticalRotation, 0);
                }
            }
            if (GetPartToRotate(this.gameObject, 1).transform.localEulerAngles.z < 180.0f)
            {
                if (GetPartToRotate(this.gameObject, 1).transform.localEulerAngles.z + horizontalRotation < m_horizontalCap)
                {
                    GetPartToRotate(this.gameObject, 1).transform.Rotate(0, 0, horizontalRotation);
                }
            }
            else
            {
                if (GetPartToRotate(this.gameObject, 1).transform.localEulerAngles.z - 360 + horizontalRotation > -m_horizontalCap)
                {
                    GetPartToRotate(this.gameObject, 1).transform.Rotate(0, 0, horizontalRotation);
                }
            }

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

    void Fire()
    {
        switch(PersistentInfo.Instance.m_carDesign.m_gunChoice)
        {
            case 0:
                {
                    Vector3 fireAngle = (m_gun.transform.right + (m_gun.transform.forward * 0.5f));

                    NetShoot netShoot = new NetShoot();
                    netShoot.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                    netShoot.m_Action = NetShoot.ACTION.EXPLOSIVE;
                    netShoot.m_Other = 0;
                    netShoot.m_Force = m_arcForce;
                    netShoot.m_XPos = m_gun.transform.position.x;
                    netShoot.m_YPos = m_gun.transform.position.y;
                    netShoot.m_ZPos = m_gun.transform.position.z;
                    netShoot.m_XDir = fireAngle.x;
                    netShoot.m_YDir = fireAngle.y;
                    netShoot.m_ZDir = fireAngle.z;
                    Client.Instance.SendToServer(netShoot);

                    projectile = Instantiate(m_explosivePrefab, m_gun.transform.position, Quaternion.identity);
                    projectile.GetComponent<Rigidbody>().AddForce(fireAngle * m_arcForce, ForceMode.Impulse);
                    break;
                }
            case 1:
                {
                    RaycastHit hit;
                    if (Physics.Raycast(m_gun.transform.position, m_gun.transform.right, out hit, 100000.0f, 11, QueryTriggerInteraction.Collide))
                    {
                        if (hit.transform.gameObject.tag == "Player")
                        {
                            NetShoot netShoot = new NetShoot();
                            netShoot.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                            netShoot.m_Action = NetShoot.ACTION.HITSCAN;
                            netShoot.m_Other = hit.transform.gameObject.GetComponent<CarManagerScript>().m_playerNum;
                            netShoot.m_Force = m_fireForce;
                            netShoot.m_XPos = m_gun.transform.position.x;
                            netShoot.m_YPos = m_gun.transform.position.y;
                            netShoot.m_ZPos = m_gun.transform.position.z;
                            netShoot.m_XDir = m_gun.transform.right.x;
                            netShoot.m_YDir = m_gun.transform.right.y;
                            netShoot.m_ZDir = m_gun.transform.right.z;
                            Client.Instance.SendToServer(netShoot);
                        }
                        else
                        {
                            NetShoot netShoot = new NetShoot();
                            netShoot.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                            netShoot.m_Action = NetShoot.ACTION.HITSCAN;
                            netShoot.m_Other = 0;
                            netShoot.m_Force = m_fireForce;
                            netShoot.m_XPos = m_gun.transform.position.x;
                            netShoot.m_YPos = m_gun.transform.position.y;
                            netShoot.m_ZPos = m_gun.transform.position.z;
                            netShoot.m_XDir = m_gun.transform.right.x;
                            netShoot.m_YDir = m_gun.transform.right.y;
                            netShoot.m_ZDir = m_gun.transform.right.z;
                            Client.Instance.SendToServer(netShoot);
                        }
                    }

                    projectile = Instantiate(m_projectilePrefab, m_gun.transform.position, Quaternion.identity);
                    projectile.GetComponent<Rigidbody>().AddForce(m_gun.transform.right * m_fireForce, ForceMode.Impulse);
                    break;
                }
            case 2:
                {
                    Vector3 fireAngle = (m_gun.transform.right + (m_gun.transform.forward * 0.5f));

                    NetShoot netShoot = new NetShoot();
                    netShoot.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                    netShoot.m_Action = NetShoot.ACTION.MINE;
                    netShoot.m_Other = 0;
                    netShoot.m_Force = m_arcForce;
                    netShoot.m_XPos = m_gun.transform.position.x;
                    netShoot.m_YPos = m_gun.transform.position.y;
                    netShoot.m_ZPos = m_gun.transform.position.z;
                    netShoot.m_XDir = fireAngle.x;
                    netShoot.m_YDir = fireAngle.y;
                    netShoot.m_ZDir = fireAngle.z;
                    Client.Instance.SendToServer(netShoot);

                    projectile = Instantiate(m_minePrefab, m_gun.transform.position, Quaternion.identity);
                    projectile.GetComponent<Rigidbody>().AddForce(fireAngle * m_arcForce, ForceMode.Impulse);
                    break;
                }
            default:
                break;
        }
    }
}
