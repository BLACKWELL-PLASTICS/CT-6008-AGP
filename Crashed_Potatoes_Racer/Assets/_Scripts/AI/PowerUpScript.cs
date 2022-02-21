using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    private RocketTest RT;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Car")
        {
            RT = other.GetComponent<RocketTest>();
            RT.poweredUp = true;
            Destroy(this.gameObject);
        }
    }
}
