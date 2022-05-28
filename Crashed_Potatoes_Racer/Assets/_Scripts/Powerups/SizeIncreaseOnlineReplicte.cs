using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeIncreaseOnlineReplicte : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHit>().HitSpin();
        }
    }
}
