﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Controller : MonoBehaviour {
    public Rigidbody rb;
    float forwardAcceleration = 5500f;
    float reverseAcceleration = -3000f;
    float maxSpeed = 70f;
    float turnStrength = 100f;
    float gravityForce = 10f;

    float speedInput, turnInput;
    
    bool grounded;
    [SerializeField] LayerMask track;
    [SerializeField] LayerMask navMesh2;
    [SerializeField] LayerMask notTrack;
    float groundRayLength = .5f;
    [SerializeField] Transform groundRayPoint;

    // Wheels
    [SerializeField] Transform frontLeftWheel, frontRightWheel;
    float maxWheelTurn = 10;

    // BOOSTING POWERUP
    bool isBoosting = false;
    float boostTimer = 0f;

    public bool isStuck = false;
    float gumTimer = 0f;

    PlayerIndex index;

    private FMODUnity.StudioEventEmitter emitter;

    // Start is called before the first frame update
    void Start() {
        // unparent sphere to smooth movement
        rb.transform.parent = null;
        //fmod 
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update() {
        if (frontLeftWheel == null)
        {
            frontLeftWheel = GameObject.FindGameObjectWithTag("FLW").GetComponent<Transform>();
        }
        if (frontRightWheel == null)
        {
            frontRightWheel = GameObject.FindGameObjectWithTag("FRW").GetComponent<Transform>();
        }
        // BOOSTING CODE
        if (isBoosting == true && boostTimer <= 3f) {
            boostTimer += Time.deltaTime;
            forwardAcceleration = 10000f;
            maxSpeed = 150f;
        } else {
            isBoosting = false;
            transform.Find("Boost").GetComponent<ParticleSystem>().Stop();
            boostTimer = 0f;
            maxSpeed = 70f;
            forwardAcceleration = 5500f;
        }

        // GUM CODE
        if (isStuck == true && gumTimer <= 3f) {
            gumTimer += Time.deltaTime;
            forwardAcceleration = 2000f;
            maxSpeed = 30f;
        } else {
            transform.Find("Smoke").GetComponent<ParticleSystem>().Stop();
            isStuck = false;
            gumTimer = 0f;
            maxSpeed = 70f;
            forwardAcceleration = 5500f;
        }

        // Reset Speed input each frame
        speedInput = 0f;
        // set speed input depending on movement
        if ((Input.GetAxisRaw("RT") > 0.01f && speedInput != maxSpeed) || (Input.GetKey(KeyCode.W) && speedInput != maxSpeed)) {
            speedInput = forwardAcceleration;
            GamePad.SetVibration(index, 0.2f, 0.2f);
        } else if ((Input.GetAxisRaw("LT") > 0.01f && speedInput != maxSpeed) || (Input.GetKey(KeyCode.W) && speedInput != maxSpeed)) {
            speedInput = reverseAcceleration;
            GamePad.SetVibration(index, 0.1f, 0.1f);
        } else {
            GamePad.SetVibration(index, 0f, 0f);
        }

        // Turn input equals the horizontal movement
        turnInput = Input.GetAxisRaw("Horizontal");

        if (grounded) {
            // Set Rotation
            Quaternion q = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime, 0f));
            q.eulerAngles = new Vector3(0.0f, q.eulerAngles.y, 0.0f);
            transform.rotation = q;
        }

        frontLeftWheel.localRotation = Quaternion.Euler(frontLeftWheel.localRotation.eulerAngles.x, frontLeftWheel.localRotation.eulerAngles.y, (turnInput * maxWheelTurn));
        frontRightWheel.localRotation = Quaternion.Euler(frontRightWheel.localRotation.eulerAngles.x, frontRightWheel.localRotation.eulerAngles.y, turnInput * maxWheelTurn);

        //Set Position
        transform.position = rb.transform.position;

        //fmod
        emitter.SetParameter("Speed", rb.velocity.magnitude);
    }

    // All Physics calculations will take place in the fixed update
    private void FixedUpdate() {
        grounded = false;
        RaycastHit hit;
        // checks if driving on track
        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, track | navMesh2)) {
            grounded = true;
        
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (grounded) {
            rb.drag = 3f;
            if (Mathf.Abs(speedInput) > 0) {
                rb.AddForce(transform.forward * speedInput);
            }
        } else {
            rb.drag = 0f;
            rb.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }

    public void Boost() {
        boostTimer = 0f;
        isBoosting = true;
    }
}
