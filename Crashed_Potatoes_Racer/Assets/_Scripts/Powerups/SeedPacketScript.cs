using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPacketScript : MonoBehaviour {
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

    bool isActive = true;
    float timer = 0.0f;

    [SerializeField]
    public int m_packetNum;

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
                Client.Instance.SendToServer(netPickedUp);
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
        int i = Random.Range(1, 9);
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
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger" + other.gameObject.name);
        if (other.transform.tag != "Player") {
            return;
        }
        int i = Random.Range(1, 9);
        choice = (POWERUPS)i;
        other.gameObject.GetComponent<InventoryScript>().AddPowerup(choice);
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
        //Destroy(gameObject);
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