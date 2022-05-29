//////////////////////////////////////////////////
/// Created:                                   ///
/// Author: Oliver Blackwell                   ///
/// Edited By:			                       ///
/// Last Edited:		                       ///
//////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject car;

    Vector3 offset;
    Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - car.transform.position;
        car = this.transform.parent.gameObject;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        rot = car.transform.rotation.eulerAngles;
        transform.position = car.transform.position + offset;
        if (car.transform.rotation.y > rot.y + 5 || car.transform.rotation.y < rot.y - 5) {
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = rot;
            transform.rotation = rotation;
        }
    }
}
