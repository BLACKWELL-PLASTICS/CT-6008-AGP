using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour //power up test script - no longer used - By Anna
{
    private RocketTest RT; //link to rocket test script
    private void OnTriggerEnter(Collider other) //when in contact
    {
        if(other.gameObject.tag == "Car") //if in contact with car
        {
            RT = other.GetComponent<RocketTest>(); //connect to rocket script
            RT.poweredUp = true; //set power up to active
            Destroy(this.gameObject); //destory 
        }
    }
}
