using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeControllerScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            GetComponent<CarManagerScript>().ToggleMerging(true);
        }
    }
}
