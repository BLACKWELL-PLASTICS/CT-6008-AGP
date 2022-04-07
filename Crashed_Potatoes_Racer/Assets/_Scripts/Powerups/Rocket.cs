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
        transform.position += (transform.position + transform.forward) * 0.05f;
    }

    // If it collides with any object, Explode Rocket
    private void OnTriggerEnter(Collider other) {
        Explode();
    }
    private void OnCollisionEnter(Collision collision) {
        Explode();
    }

    void Explode() {
        // This needs to be added to (to add force at the area, etc)
        Destroy(this.gameObject);
    }
}
