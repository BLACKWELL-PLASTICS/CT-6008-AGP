using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRocket : MonoBehaviour
{
    private RocketTest RT;

    private void Start()
    {
        RT = GetComponentInParent<RocketTest>();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, RT.targetCar.position, (RT.WP.speed + 0.3f) * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform != transform.parent.transform)
        {
            Destroy(this.gameObject);
        }
    }
}
