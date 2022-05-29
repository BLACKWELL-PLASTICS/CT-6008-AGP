//////////////////////////////////////////////////
/// Created: 17/04/2022                        ///
/// Author: Iain Farlow                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostOnlineVisualTrigger : MonoBehaviour
{
    float boostTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (boostTimer <= 3f)
        {
            boostTimer += Time.deltaTime;
        }
        else
        {
            //timer off for vfx
            transform.Find("Boost").GetComponent<ParticleSystem>().Stop();
            Destroy(this);
        }
    }
}
