//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 24/05/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveScript : MonoBehaviour
{
    [SerializeField]
    float radius = 1.5f;

    private void OnCollisionEnter(Collision collision)
    {
        //on collision destoy self's ridigid and check area for 
        Destroy(GetComponent<Rigidbody>());
        GetComponent<SphereCollider>().isTrigger = true;
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
