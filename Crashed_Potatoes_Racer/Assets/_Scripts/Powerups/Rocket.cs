using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    float timer = 0.0f;

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > 5f) {
            Explode();
        }
    }
    private void FixedUpdate() {
        transform.position += Vector3.forward * 0.5f;
    }

    private void OnTriggerEnter(Collider other) {
        Explode();
    }

    void Explode() {
        Destroy(this.gameObject);
    }
}
