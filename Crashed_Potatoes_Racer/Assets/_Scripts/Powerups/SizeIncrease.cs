//////////////////////////////////////////////////
/// Created:                                   ///
/// Author: Oliver Blackwell                   ///
/// Edited By: Iain Farlow                     ///
/// Last Edited: 10/03/2022                    ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeIncrease : MonoBehaviour {
    Vector3 originalScale;
    float timer = 0f;
    Vector3 originalPos;
    Vector3 currentPos;

    private void Awake() {
        originalPos = transform.position;
        originalScale = transform.localScale;
        transform.localScale = originalScale * 1.5f;
        currentPos = transform.position;

        Transform sphere = null;
        if (GetComponent<AIPlayer>() == null)
        {
            sphere = GetComponent<Controller>().rb.transform;
        }
        else
        {
            sphere = GetComponentInChildren<Rigidbody>().transform;
        }
        transform.position = new Vector3(currentPos.x, currentPos.y + 1f, currentPos.z);
        sphere.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //Added by Iain
        //grow packet
        NetGrow netGrow = new NetGrow();
        netGrow.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        netGrow.m_CarNum = GetComponent<CarManagerScript>().m_playerNum;
        netGrow.m_Action = NetGrow.ACTION.START;
        Client.Instance.SendToServer(netGrow);
        //Added by Iain ~
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > 3f) {
            currentPos = transform.position;
            transform.localScale = originalScale;
            timer = 0f;
            transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);

            //Added by Iain
            //shrink package
            NetGrow netGrow = new NetGrow();
            netGrow.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
            netGrow.m_CarNum = GetComponent<CarManagerScript>().m_playerNum;
            netGrow.m_Action = NetGrow.ACTION.END;
            Client.Instance.SendToServer(netGrow);
            //Added by Iain ~
            Destroy(gameObject.GetComponent<SizeIncrease>());
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerHit>().HitSpin();
        }
    }
}
