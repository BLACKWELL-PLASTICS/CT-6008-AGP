using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTest : MonoBehaviour //test function for a rocket power up - no longer used - By Anna
{
    public GameObject rocket; //object for rocket 
    public float distance; //distance for when to fire rocket
    public bool poweredUp = false; //whether used power up
    public Transform targetCar; //object for car thats been targeted
    public WayPointTest WP; //link to waypoint script

    private void Start()
    {
        WP = GetComponent<WayPointTest>(); //connection to waypoint test
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.green); //draw a green line to show the detetction range

        if (poweredUp == true) //if there is  power up active
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, distance, LayerMask.GetMask("Car"))) //if the line is crossed by another car
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.red); //draw a red ray over green to show target has been set
                Debug.Log("Hit"); //debug log for testing
                targetCar = hit.transform; //set target to hit object
                Instantiate(rocket, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.identity, this.transform); //spawn rocket
                poweredUp = false; //set power up to false
            }
        }
        
    }
}
