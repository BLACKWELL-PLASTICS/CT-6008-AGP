using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    float forwardAcceleration = 8000f;
    float reverseAcceleration = 4000f;
    float maxSpeed = 50f;
    float turnStrength = 100f;
    float gravityForce = 10f;

    float speedInput, turnInput;

    bool grounded;
    [SerializeField] LayerMask track;
    [SerializeField] LayerMask notTrack;
    float groundRayLength = .5f;
    [SerializeField] Transform groundRayPoint;

    // Start is called before the first frame update
    void Start()
    {
        // unparent sphere to smooth movement
        rb.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset Speed input each frame
        speedInput = 0f;
        // set speed input depending on movement
        if (Input.GetAxis("Vertical") > 0) {
            speedInput = Input.GetAxis("Vertical") * forwardAcceleration;
        } else if (Input.GetAxis("Vertical") < 0) {
            speedInput = Input.GetAxis("Vertical") * reverseAcceleration;
        }

        // Turn input equals the horizontal movement
        turnInput = Input.GetAxis("Horizontal");

        if (grounded) {
            // Set Rotation
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        //Set Posiition
        transform.position = rb.transform.position;
    }

    // All Physics calculations will take place in the fixed update
    private void FixedUpdate() {
        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPoint.position,-transform.up, out hit, groundRayLength, track)) {
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
}
