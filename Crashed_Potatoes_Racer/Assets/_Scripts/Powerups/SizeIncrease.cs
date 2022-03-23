using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeIncrease : MonoBehaviour {
    Vector3 OriginalScale;
    float timer = 0f;
    Vector3 oPos;
    Vector3 pos;

    private void Awake() {
        oPos = transform.position;
        OriginalScale = transform.localScale;
        transform.localScale = OriginalScale * 2f;
        pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + (OriginalScale.y / 2), pos.y);
        //grow packet
        NetGrow netGrow = new NetGrow();
        netGrow.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        netGrow.m_Action = NetGrow.ACTION.START;
        Client.Instance.SendToServer(netGrow);
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > 3f) {
            pos = transform.position;
            transform.localScale = OriginalScale;
            timer = 0f;
            transform.position = new Vector3(pos.x, oPos.y, pos.z);
            //shrink package
            NetGrow netGrow = new NetGrow();
            netGrow.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
            netGrow.m_Action = NetGrow.ACTION.END;
            Client.Instance.SendToServer(netGrow);
            Destroy(gameObject.GetComponent<SizeIncrease>());
        }
    }
}
