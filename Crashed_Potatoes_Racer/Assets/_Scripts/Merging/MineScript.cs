//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 21/05/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Terrain")
        {
            //place mine and use gum script to mimic functionality 
            Destroy(GetComponent<Rigidbody>());
            GetComponent<SphereCollider>().isTrigger = true;
            gameObject.AddComponent<GumScript>();
            Destroy(this);
        }
    }
}
