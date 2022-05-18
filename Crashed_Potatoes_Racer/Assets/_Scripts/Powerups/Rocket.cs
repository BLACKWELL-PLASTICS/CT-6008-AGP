using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rocket : MonoBehaviour {

    GameObject target;
    NavMeshAgent agent;
    bool isFirst = false;
    public void OwnerAndTarget(GameObject owner)
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player")) {
            int i = owner.GetComponent<Position>().currentPosition;
            if (i != 1) {
                isFirst = false;
                if (item.GetComponent<Position>().currentPosition == i - 1) {
                    target = item;
                }
                GetComponent<NavMeshAgent>().Warp(transform.position);
            } else {
                isFirst = true;
                transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z) + Vector3.forward * 2;
            }
        }
    }

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (isFirst == true) {
            transform.position += (transform.forward * 30) * Time.deltaTime;
        } else {
            agent.SetDestination(target.transform.position);
            if (transform.position == target.transform.position) {
                Explode(target);
            }
        }

    }
    // If it collides with any object, Explode Rocket
    private void OnTriggerEnter(Collider other) {
        if (isFirst == true) {
            Explode(other.gameObject);
        } else {
            return;
        }
    }

    void Explode(GameObject go) {
        if (go.tag == "Player") {
            go.GetComponent<PlayerHit>().HitSpin();
        }
        Destroy(this.gameObject);
    }
}