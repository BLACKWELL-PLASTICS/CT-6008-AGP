using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishScript : MonoBehaviour
{
    [SerializeField]
    private float amount;
    [SerializeField]
    private float speed;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();


    private void Start()
    {
        posOffset = transform.position;
    }
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * amount;

        transform.position = tempPos;
    }

}
