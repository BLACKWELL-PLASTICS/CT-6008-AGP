//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 26/01/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

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
            //Merge toggle
            GetComponent<CarManagerScript>().ToggleMerging(true);
        }
    }
}
