using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Terrain")
        {
            Destroy(GetComponent<Rigidbody>());
            GetComponent<SphereCollider>().isTrigger = true;
            gameObject.AddComponent<GumScript>();
            Destroy(this);
        }
    }
}
