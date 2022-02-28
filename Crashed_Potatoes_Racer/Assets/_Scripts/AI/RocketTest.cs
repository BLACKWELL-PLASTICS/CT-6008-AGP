using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTest : MonoBehaviour
{
    public GameObject rocket;
    public float distance;
    public bool poweredUp = false;
    public Transform targetCar;
    public WayPointTest WP;

    private void Start()
    {
        WP = GetComponent<WayPointTest>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.green);

        if (poweredUp == true)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, distance, LayerMask.GetMask("Car")))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.red);
                Debug.Log("Hit");
                targetCar = hit.transform;
                Instantiate(rocket, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.identity, this.transform);
                poweredUp = false;
            }
        }
        
    }
}
