using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleScaler : MonoBehaviour
{
    float timer;

    private void Awake() {
        // send packet of position and rotation
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.localScale = transform.localScale * 1.0009f;

        if (timer > 5f) {
            Destroy(this.gameObject);
        }
    }
}
