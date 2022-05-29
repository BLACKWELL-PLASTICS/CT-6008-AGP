using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour //script for the movement of the fish in lvl 1 - By Anna
{
    [HideInInspector]
    public FishScript spawner; //connection to fish script / spawner

    private Transform child; //link to actual models transform
    private void Start()
    {
        child = GetComponentInChildren<Transform>(); //connection to childs transform
    }

    void Update()
    {
        StartCoroutine(Move()); //start ienumerator
    }

    private IEnumerator Move() //moves the fish up and rotates before down and deletes
    {
        //move the fish upwards
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), spawner.fishSpeed * Time.deltaTime);
        //rotate model slowly 
        child.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(transform.rotation.x, transform.rotation.y, -90, transform.rotation.w), Time.deltaTime * 0.005f);
        //wait for set seconds
        yield return new WaitForSeconds(spawner.fishJumpTime);
        //move fish downwards
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 11, transform.position.z), (spawner.fishSpeed * 2) * Time.deltaTime);
        //slowly rotate model
        child.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(transform.rotation.x, transform.rotation.y, -90, transform.rotation.w), Time.deltaTime * 0.025f);
        //wait for set seconds + extra
        yield return new WaitForSeconds(spawner.fishJumpTime + 0.2f);
        //destory fish
        Destroy(this.gameObject);
    }
}
