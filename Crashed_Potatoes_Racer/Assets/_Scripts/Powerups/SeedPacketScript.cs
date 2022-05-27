//////////////////////////////////////////////////
/// Created:                                   ///
/// Author:                                    ///
/// Edited By: Iain Farlow                     ///
/// Last Edited: 28/04/2022                    ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPacketScript : MonoBehaviour {
    public enum POWERUPS {
        None,
        Forward_Projectile,
        Blind,
        Boost,
        Gum,
        Obsticles,
        Size_Increase,
        Hot_Potato
    };

    POWERUPS choice;

    bool isActive = true;
    float timer = 0.0f;

    [SerializeField]
    public int m_packetNum;

    public Animator itemAnim;

    private void Start() {
        itemAnim = GameObject.Find("Item Reserve").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(-Vector3.forward, 75f * Time.deltaTime);
        if (isActive == false) {
            timer += Time.deltaTime;
            if (timer > 5f) {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<MeshCollider>().enabled = true;
                //Added by Iain
                //Packet start packet
                NetPickedUp netPickedUp = new NetPickedUp();
                netPickedUp.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                netPickedUp.m_PickUp = m_packetNum;
                netPickedUp.m_Action = NetPickedUp.ACTION.APPEAR;
                if (Client.Instance.m_driver.IsCreated)
                {
                    Client.Instance.SendToServer(netPickedUp);
                }
                //Added by Iain ~
                timer = 0.0f;
                isActive = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision" + collision.gameObject.name);
        if (collision.transform.tag != "Player") {
            return;
        }
        int i = Random.Range(1, 7);
        choice = (POWERUPS)i;
        collision.gameObject.GetComponent<InventoryScript>().AddPowerup(choice);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
        //Added by Iain
        //Packet start packet
        NetPickedUp netPickedUp = new NetPickedUp();
        netPickedUp.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        netPickedUp.m_PickUp = m_packetNum;
        netPickedUp.m_Action = NetPickedUp.ACTION.DISAPPEAR;
        Client.Instance.SendToServer(netPickedUp);
        //Added by Iain ~
        isActive = false;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger" + other.gameObject.name);
        if (other.transform.tag != "Player") {
            return;
        }

        isActive = false;

        if (other.gameObject.name == "Car_Reg(Clone)") { // if the car is the player
            //Added by Iain
            //Packet start packet
            NetPickedUp netPickedUp = new NetPickedUp();
            netPickedUp.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
            netPickedUp.m_PickUp = m_packetNum;
            netPickedUp.m_Action = NetPickedUp.ACTION.DISAPPEAR;
            Client.Instance.SendToServer(netPickedUp);
            //Added by Iain ~
            // Run animation then give powerup
            itemAnim.SetBool("itemPickup", true);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            StartCoroutine(AnimationDuration(other));
        } else { // if the car isnt the player
            // Give Powerup
            GivePowerup(other);
        }
    }

    IEnumerator AnimationDuration(Collider other) {
        yield return new WaitForSeconds(2);
        GivePowerup(other);
    }

    private void GivePowerup(Collider other) {
        itemAnim.SetBool("itemPickup", false);
        int i = Random.Range(1, 7);
        choice = (POWERUPS)i;
        if (other.gameObject.GetComponent<InventoryScript>() != null)
        {
            other.gameObject.GetComponent<InventoryScript>().AddPowerup(choice);
        }
    }

    //Added by Iain
    public void Appear()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;
        isActive = true;
        timer = 0.0f;
    }

    public void Disappear()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
        isActive = false;
    }
    //Added by Iain ~
}