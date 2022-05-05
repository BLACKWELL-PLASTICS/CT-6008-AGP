using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleScaler : MonoBehaviour
{
    float timer;
    float scaleValue = 1.002f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.localScale = transform.localScale * scaleValue;

        if (timer > 4f) {
            Destroy(this.gameObject);
        }
    }
}
