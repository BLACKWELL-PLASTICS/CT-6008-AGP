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
        transform.Rotate(-Vector3.forward, 75f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag != "Player") {
            return;
        }
        int i = Random.Range(1, 9);
        choice = (POWERUPS)i;
        collision.gameObject.GetComponent<InventoryScript>().AddPowerup(choice);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag != "Player") {
            return;
        }
        int i = Random.Range(1, 9);
        choice = (POWERUPS)i;
        other.gameObject.GetComponent<InventoryScript>().AddPowerup(choice);
        Destroy(gameObject);
    }
}
