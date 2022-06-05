//////////////////////////////////////////////////
/// Created:                                   ///
/// Author: Oliver Blackwell                   ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerups : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[7];

    public SeedPacketScript.POWERUPS powerup;

    public void UpdateIcon()
    {
        if (GameObject.Find("Car_Reg(Clone)") != null) {
            powerup = GameObject.Find("Car_Reg(Clone)").GetComponent<InventoryScript>().p1;

            switch (powerup) {
                case SeedPacketScript.POWERUPS.None:
                    gameObject.GetComponent<Image>().overrideSprite = null;
                    break;
                case SeedPacketScript.POWERUPS.Forward_Projectile:
                    gameObject.GetComponent<Image>().overrideSprite = sprites[0];
                    break;
                case SeedPacketScript.POWERUPS.Blind:
                    gameObject.GetComponent<Image>().overrideSprite = sprites[1];
                    break;
                case SeedPacketScript.POWERUPS.Boost:
                    gameObject.GetComponent<Image>().overrideSprite = sprites[2];
                    break;
                case SeedPacketScript.POWERUPS.Gum:
                    gameObject.GetComponent<Image>().overrideSprite = sprites[3];
                    break;
                case SeedPacketScript.POWERUPS.Obsticles:
                    gameObject.GetComponent<Image>().overrideSprite = sprites[4];
                    break;
                case SeedPacketScript.POWERUPS.Size_Increase:
                    gameObject.GetComponent<Image>().overrideSprite = sprites[5];
                    break;
            }
        } else {
            gameObject.GetComponent<Image>().overrideSprite = null;
        }

    }
}
