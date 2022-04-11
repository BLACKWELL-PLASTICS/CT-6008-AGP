using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    float timer = 0.0f;
    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > 5.0f) {
            Explode();
        }
    }
    private void FixedUpdate() {
        transform.position += (transform.forward * 22) * Time.deltaTime;
    }

    // If it collides with any object, Explode Rocket
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player" || other.gameObject.tag != "Terrain") return;
        Debug.Log("T" + other.gameObject.name);
        Explode();
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "Terrain") return;
        Debug.Log("C" + collision.gameObject.name);
        Explode();
    }

    void Explode() {
        rb.AddExplosionForce(10f, transform.position, 5f);
        Destroy(this.gameObject);
    }
}
