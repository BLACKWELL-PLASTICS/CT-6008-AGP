//////////////////////////////////////////////////
/// Created: 17/04/2022                        ///
/// Author: Iain Farlow                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumOnlineVisualTrigger : MonoBehaviour
{
    float gumTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (gumTimer <= 3f)
        {
            gumTimer += Time.deltaTime;
        }
        else
        {
            transform.Find("Smoke").GetComponent<ParticleSystem>().Stop();
            Destroy(this);
        }
    }
}
