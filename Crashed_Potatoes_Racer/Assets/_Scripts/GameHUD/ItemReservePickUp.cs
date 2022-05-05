using UnityEngine;
using UnityEngine.UI;

public class ItemReservePickUp : MonoBehaviour {
    public Animator powerUpAnimator;

    public Sprite[] images;

    public Sprite current;

    void Update() {

        switch (GameObject.Find("Car_Reg(Clone)").GetComponent<InventoryScript>().p1) {
            case SeedPacketScript.POWERUPS.Forward_Projectile:

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

                break;
            case SeedPacketScript.POWERUPS.Hot_Potato:

                break;
        }

    }

    public void ItemPickup() {
        powerUpAnimator.SetTrigger("itemPickup");
    }
}
