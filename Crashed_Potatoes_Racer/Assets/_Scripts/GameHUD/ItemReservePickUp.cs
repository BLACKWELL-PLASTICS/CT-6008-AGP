using UnityEngine;

public class ItemReservePickUp : MonoBehaviour
{
    public Animator powerUpAnimator;

    void Update()
    {
        //Debugging purposes; replace with Oli's code when the player picks up a power-up and call "ItemPickup()"
        switch (Input.inputString)
        {
            case "9":
                ItemPickup();
                break;
        }
    }

    public void ItemPickup()
    {
        powerUpAnimator.SetTrigger("itemPickup");
    }
}
