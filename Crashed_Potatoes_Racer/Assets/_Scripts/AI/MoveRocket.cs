using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRocket : MonoBehaviour //test move rocket script - no longer used - By Anna
{
    private RocketTest RT;//link to rocket script

    private void Start()
    {
        RT = GetComponentInParent<RocketTest>(); //connect to rocket script in parent
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, RT.targetCar.position, (RT.WP.speed + 0.3f) * Time.deltaTime); //move forward
    }
    private void OnTriggerEnter(Collider other) //on contact with object
    {
        if(other.transform != transform.parent.transform) //if that object isnt the parent car
        {
            Destroy(this.gameObject); //destory 
        }
    }
}
