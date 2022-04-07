using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    float timer = 0.0f;
    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > 2f) {
            Explode();
        }
    }
    private void FixedUpdate() {
        transform.position += transform.forward * 0.05f * Time.deltaTime;
    }

    // If it collides with any object, Explode Rocket
    private void OnTriggerEnter(Collider other) {
        Explode();
    }
    private void OnCollisionEnter(Collision collision) {
        Explode();
    }

    void Explode() {
        rb.AddExplosionForce(10f, transform.position, 5f);
        Destroy(this.gameObject);
    }
}
