using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    float timer = 0.0f;
    Rigidbody rb;
    GameObject owner;
    public void Owner(GameObject gameObject)
    {
        owner = gameObject;
    }

    private void Start() {

        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z) + Vector3.forward * 2;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > 5.0f) {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate() {
        transform.position += (transform.forward * 22) * Time.deltaTime;
    }

    // If it collides with any object, Explode Rocket
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == owner) {
            return;
        }
        Explode(other.gameObject);
    }

    void Explode(GameObject go) {
        if (go.tag == "Player") {
            go.GetComponent<PlayerHit>().HitSpin();
        }
        Destroy(this.gameObject);
    }
}