//////////////////////////////////////////////////
/// Created:                                   ///
/// Author: Oliver Blackwell                   ///
/// Edited By: Iain Farlow                     ///
/// Last Edited: 29/04/2022                    ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public SeedPacketScript.POWERUPS p1;
    public SeedPacketScript.POWERUPS p2;

    public GameObject[] prefabs;

    public FMODUnity.EventReference boostSound; //added by anna
    public FMODUnity.EventReference sizeSound; //added by anna
    public FMODUnity.EventReference poopSound; //added by anna
    public FMODUnity.EventReference spinSound; //added by anna

    // Update is called once per frame
    void Update() {
        if (GetComponent<AIPlayer>() == null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                switch (p1)
                {
                    case SeedPacketScript.POWERUPS.Forward_Projectile:
                        //Added by Iain
                        Vector3 spawnPos = transform.position + (transform.forward * 2);
                        Quaternion spawnRot = Quaternion.LookRotation(this.gameObject.transform.forward, this.gameObject.transform.up);
                        //Rocket start packet
                        NetRocket netRocket = new NetRocket();
                        netRocket.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                        netRocket.m_Action = NetRocket.ACTION.FIRE;
                        netRocket.m_XPos = spawnPos.x;
                        netRocket.m_YPos = spawnPos.y;
                        netRocket.m_ZPos = spawnPos.z;
                        netRocket.m_XRot = spawnRot.x;
                        netRocket.m_YRot = spawnRot.y;
                        netRocket.m_ZRot = spawnRot.z;
                        netRocket.m_WRot = spawnRot.w;
                        Client.Instance.SendToServer(netRocket);
                        //Added by Iain ~

                        GameObject rocket = Instantiate(prefabs[0], transform.position + (transform.forward * 2), Quaternion.LookRotation(this.gameObject.transform.forward, this.gameObject.transform.up));
                        rocket.GetComponent<Rocket>().OwnerAndTarget(this.gameObject);
                        break;
                    case SeedPacketScript.POWERUPS.Blind:
                        //Added by Iain
                        //Bird Poop start package
                        NetBirdPoop netBirdPoop = new NetBirdPoop();
                        netBirdPoop.m_Player = GetComponent<CarManagerScript>().m_playerNum;
                        Client.Instance.SendToServer(netBirdPoop);
                        //Added by Iain ~
                        //added by anna
                        FMODUnity.RuntimeManager.PlayOneShot(poopSound, gameObject.transform.position);
                        break;
                    case SeedPacketScript.POWERUPS.Boost:
                        GetComponent<Controller>().Boost();
                        transform.Find("Boost").GetComponent<ParticleSystem>().Play();
                        FMODUnity.RuntimeManager.PlayOneShot(boostSound, gameObject.transform.position); //added by anna
                        //Added by Iain
                        //Boost start package
                        NetBoost netBoost = new NetBoost();
                        netBoost.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                        netBoost.m_CarNum = GetComponent<CarManagerScript>().m_playerNum;
                        netBoost.m_Action = NetBoost.ACTION.START;
                        Client.Instance.SendToServer(netBoost);
                        //Added by Iain ~
                        break;
                    case SeedPacketScript.POWERUPS.Gum:
                        gameObject.GetComponent<Gum>().SpawnGO();
                        break;
                    case SeedPacketScript.POWERUPS.Obsticles:
                        gameObject.GetComponent<Obsticle>().SpawnGO();
                        break;
                    case SeedPacketScript.POWERUPS.Size_Increase:
                        gameObject.AddComponent<SizeIncrease>();
                        //added by anna
                        FMODUnity.RuntimeManager.PlayOneShot(sizeSound, gameObject.transform.position);
                        break;
                }
                UsePowerup();
            }
        }
    }

    public void AddPowerup(SeedPacketScript.POWERUPS power) {
        if (p1 == SeedPacketScript.POWERUPS.None) {
            p1 = power;
        } else if (p2 == SeedPacketScript.POWERUPS.None) {
            p2 = power;
        } else {
            return;
        }
    }

    public void MovePowerup() {
        p1 = p2;
        p2 = SeedPacketScript.POWERUPS.None;
    }

    public void UsePowerup() {
        p1 = SeedPacketScript.POWERUPS.None;
        MovePowerup();
    }
}