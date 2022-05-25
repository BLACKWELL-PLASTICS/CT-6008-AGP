using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rocket : MonoBehaviour {

    GameObject target;
    GameObject owner;
    NavMeshAgent agent;
    public void OwnerAndTarget(GameObject owner)
    {
        int i = owner.GetComponent<Position>().currentPosition;
        this.owner = owner;
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player")) {
            if (i != 1) {
                if (item.GetComponent<Position>().currentPosition == i - 1) {
                    target = item;
                    GetComponent<NavMeshAgent>().Warp(transform.position);
                    return;
                }
                Position[] positions = item.GetComponentsInChildren<Position>();
                if (positions.Length > 1)
                {
                    if (positions[1] != null)
                    {
                        if (positions[1].currentPosition == i - 1)
                        {
                            target = item;
                            GetComponent<NavMeshAgent>().Warp(transform.position);
                            return;
                        }
                    }
                }
            } else {
                if (item.GetComponent<Position>().currentPosition == i + 1) {
                    target = item;
                    GetComponent<NavMeshAgent>().Warp(transform.position);
                    return;
                }
                Position[] positions = item.GetComponentsInChildren<Position>();
                if (positions.Length > 1)
                {
                    if (positions[1] != null)
                    {
                        if (positions[1].currentPosition == i + 1)
                        {
                            target = item;
                            GetComponent<NavMeshAgent>().Warp(transform.position);
                            return;
                        }
                    }
                }
            }
        }
    }

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        agent.SetDestination(target.transform.position);
    }

    // If it collides with any object, Explode Rocket
    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player" && other.gameObject != owner) {
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