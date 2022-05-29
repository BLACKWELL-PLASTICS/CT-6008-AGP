using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishScript : MonoBehaviour //script for controlling jellyfish in lvl 2 - By Anna
{
    [SerializeField]
    private float amount; //amount of movment
    [SerializeField]
    private float speed; //speed of movment

    Vector3 posOffset = new Vector3(); //position offset
    Vector3 tempPos = new Vector3(); //temp positiom


    private void Start()
    {
        posOffset = transform.position; //set offset to current
    }
    void Update()
    {
        tempPos = posOffset; //temp to offset
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * amount; //add on the sin of time by pi by speed by amount

        transform.position = tempPos; //current pos to temp pos
    }

}
