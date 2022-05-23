using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveScript : MonoBehaviour
{
    [SerializeField]
    float radius = 1.5f;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(GetComponent<Rigidbody>());
        GetComponent<SphereCollider>().isTrigger = true;
        gameObject.AddComponent<GumScript>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                collider.gameObject.GetComponent<PlayerHit>().HitSpin();
            }
        }
        Destroy(this.gameObject);
    }
}
