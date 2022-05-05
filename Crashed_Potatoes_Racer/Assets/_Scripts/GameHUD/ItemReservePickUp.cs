using UnityEngine;
using UnityEngine.UI;

public class ItemReservePickUp : MonoBehaviour {
    public Animator powerUpAnimator;

    public Sprite[] images;

    public Image current;

    void Update() {

        switch (GameObject.Find("Car_Reg(Clone)").GetComponent<InventoryScript>().p1) {
            case SeedPacketScript.POWERUPS.Forward_Projectile:
                current.sprite = images[3];
                break;
            case SeedPacketScript.POWERUPS.Blind:
                current.sprite = images[2];
                break;
            case SeedPacketScript.POWERUPS.Boost:
                current.sprite = images[0];
                break;
            case SeedPacketScript.POWERUPS.Gum:
                current.sprite = images[5];
                break;
            case SeedPacketScript.POWERUPS.Obsticles:
                current.sprite = images[1];
                break;
            case SeedPacketScript.POWERUPS.Size_Increase:
                current.sprite = images[4];
                break;
            case SeedPacketScript.POWERUPS.Hot_Potato:
                break;
        }

    }

    public void ItemPickup() {
        powerUpAnimator.SetTrigger("itemPickup");
    }
}
