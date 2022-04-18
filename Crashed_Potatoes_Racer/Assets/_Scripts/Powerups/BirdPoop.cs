//////////////////////////////////////////////////
/// Created: 17/04/2022                        ///
/// Author: Iain Farlow                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPoop : MonoBehaviour
{
    float timer = 5.0f;
    private void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer < 0.0f)
            {
                ToogleActive();
            }
        }
    }

    bool isActive = false;
    public void ToogleActive()
    {
        isActive = !isActive;
        this.gameObject.SetActive(isActive);
        timer = 5.0f;
    }
}
