using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    [SerializeField] Rigidbody rb;
    float forwardAcceleration = 5500f;
    float reverseAcceleration = 3000f;
    float maxSpeed = 70f;
    float turnStrength = 100f;
    float gravityForce = 10f;

    float speedInput, turnInput;

    bool grounded;
    [SerializeField] LayerMask track;
    [SerializeField] LayerMask notTrack;
    float groundRayLength = .5f;
    [SerializeField] Transform groundRayPoint;

    // BOOSTING POWERUP
    bool isBoosting = false;
    float timer = 0f;

    // Start is called before the first frame update
    void Start() {
        // unparent sphere to smooth movement
        rb.transform.parent = null;
    }

    // Update is called once per frame
    void Update() {
        // BOOSTING CODE
        if (isBoosting == true && timer <= 3f) {
            timer += Time.deltaTime;
            forwardAcceleration = 6500f;
            maxSpeed = 85f;
        } else {
            isBoosting = false;
            timer = 0f;
            maxSpeed = 70f;
            forwardAcceleration = 5500f;
        }

        // Reset Speed input each frame
        speedInput = 0f;
        // set speed input depending on movement
        if (Input.GetAxis("Vertical") > 0 && speedInput != maxSpeed) {
            speedInput = Input.GetAxis("Vertical") * forwardAcceleration;
        } else if (Input.GetAxis("Vertical") < 0 && speedInput != maxSpeed) {
            speedInput = Input.GetAxis("Vertical") * reverseAcceleration;
        }

        // Turn input equals the horizontal movement
        turnInput = Input.GetAxis("Horizontal");

        if (grounded) {
            // Set Rotation
            Quaternion q = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
            q.eulerAngles = new Vector3(0.0f, q.eulerAngles.y, 0.0f);
            transform.rotation = q;
        }

        //Set Position
        transform.position = rb.transform.position;
    }

    // All Physics calculations will take place in the fixed update
    private void FixedUpdate() {
        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, track)) {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, notTrack)) {
            speedInput = speedInput * 0.8f;
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (grounded) {
            rb.drag = 3f;
            if (Mathf.Abs(speedInput) > 0) {
                rb.AddForce(transform.forward * speedInput);
            }
        } else {
            rb.drag = 0.1f;
            rb.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }

    public void Boost() {
        timer = 0f;
        isBoosting = true;
    }
}
