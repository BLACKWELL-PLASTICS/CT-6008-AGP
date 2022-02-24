using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public SeedPacketScript.POWERUPS p1;
    public SeedPacketScript.POWERUPS p2;

    public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            switch (p1) {
                case SeedPacketScript.POWERUPS.Forward_Projectile:
                    Debug.Log("FP");
                    break;
                case SeedPacketScript.POWERUPS.Hot_Potato:

                    break;
                case SeedPacketScript.POWERUPS.Blind:

                    break;
                case SeedPacketScript.POWERUPS.Boost:

                    break;
                case SeedPacketScript.POWERUPS.Gum:

                    break;
                case SeedPacketScript.POWERUPS.Obsticles:
                    
                    break;
                case SeedPacketScript.POWERUPS.Size_Increase:
                    //gameObject.AddComponent<SizeIncrease>();
                    break;
                default:
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
