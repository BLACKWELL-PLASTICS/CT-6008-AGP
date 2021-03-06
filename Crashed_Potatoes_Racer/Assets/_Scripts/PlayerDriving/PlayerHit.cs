//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 07/05/2022                        ///
/// Edited By: Anna Morgan                     ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField]
    float m_rotAngle = 1.0f;
    private float m_aculumlatedRoation = 0.0f;
    private bool m_clockwise = false;
    private bool m_hit = false;
    public void HitSpin()
    {
        if (GetComponent<InventoryScript>() != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot(GetComponent<InventoryScript>().spinSound, gameObject.transform.position); //added by anna
        }
        if (GetComponent<AIPlayer>() != null)
        {
            //decresse speed for AI
            GetComponent<AIPlayer>().decreaseBoostCheck = true;
        }
        if (!m_hit)
        {
            //rnd for direction of spin
            int rnd = Random.Range(0, 2);
            if (rnd == 0)
            {
                m_clockwise = true;
            }
            else
            {
                m_clockwise = false;
            }
            m_hit = true;
        }
    }

    private void Update()
    {
        if (m_aculumlatedRoation > 360.0f)
        {
            //reset
            m_aculumlatedRoation = 0.0f;
            m_hit = false;
        }
        if (m_hit)
        {
            //rotate player for spin out 
            if (m_clockwise)
            {
                transform.RotateAround(transform.position, Vector3.up, m_rotAngle * Time.deltaTime);
            }
            else
            {
                transform.RotateAround(transform.position, Vector3.up, -m_rotAngle * Time.deltaTime);
            }
            m_aculumlatedRoation += m_rotAngle * Time.deltaTime;
        }

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    NetShoot netShoot = new NetShoot();
        //    netShoot.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        //    netShoot.m_Action = NetShoot.ACTION.FIRE;
        //    netShoot.m_Other = PersistentInfo.Instance.m_currentPlayerNum;
        //    netShoot.m_Force = 100;
        //    netShoot.m_XPos = 0;
        //    netShoot.m_YPos = 0;
        //    netShoot.m_ZPos = 0;
        //    netShoot.m_XDir = 0;
        //    netShoot.m_YDir = 0;
        //    netShoot.m_ZDir = 0;
        //    Client.Instance.SendToServer(netShoot);
        //}
    }
}
