﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleScaler : MonoBehaviour
{
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.localScale = transform.localScale * 1.005f;

        if (timer > 5f) {
            Destroy(this.gameObject);
        }
    }
}
