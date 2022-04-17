using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public SeedPacketScript.POWERUPS p1;
    public SeedPacketScript.POWERUPS p2;

    public GameObject[] prefabs;

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            switch (p1) {
                case SeedPacketScript.POWERUPS.Forward_Projectile:
                    Instantiate(prefabs[0], transform.position + (transform.forward * 2) , Quaternion.LookRotation(this.gameObject.transform.forward, this.gameObject.transform.up));
                    //Added by Iain
                    Vector3 spawnPos = transform.position + (transform.forward * 2);
                    Quaternion spawnRot = Quaternion.LookRotation(this.gameObject.transform.forward, this.gameObject.transform.up);
                    //Rocket start package
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
                    Debug.Log("FP");
                    break;
                case SeedPacketScript.POWERUPS.Hot_Potato:

                    Debug.Log("HP");
                    break;
                case SeedPacketScript.POWERUPS.Blind:

                    Debug.Log("BLIND");
                    break;
                case SeedPacketScript.POWERUPS.Boost:
                    GetComponent<Controller>().Boost();
                    break;
                case SeedPacketScript.POWERUPS.Gum:
                    Debug.Log("GUM");
                    break;
                case SeedPacketScript.POWERUPS.Obsticles:
                    gameObject.GetComponent<Obsticle>().SpawnGO();
                    break;
                case SeedPacketScript.POWERUPS.Size_Increase:
                    gameObject.AddComponent<SizeIncrease>();
                    break;
            }
            UsePowerup();
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