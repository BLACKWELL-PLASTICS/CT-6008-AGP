using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPacketScript : MonoBehaviour
{
    public enum POWERUPS {
        None,
        Forward_Projectile,
        Hot_Potato,
        Blind,
        Boost,
        Gum,
        Obsticles,
        Size_Increase
    };

    POWERUPS choice;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 75f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag != "Player") {
            return;
        }
        int i = 1/*Random.Range(1, 9)*/;
        choice = (POWERUPS)i;
        other.gameObject.GetComponent<InventoryScript>().AddPowerup(choice);
        Destroy(gameObject);
    }
}
