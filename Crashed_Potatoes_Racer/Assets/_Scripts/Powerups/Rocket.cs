using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rocket : MonoBehaviour {
    float timer = 0.0f;
    GameObject owner;
    GameObject target;

    NavMeshAgent agent;

    public void PositionData(GameObject owner/*, GameObject target*/)
    {
        this.owner = owner; 
        //this.target = target;
    }

    private void Start() {
        // Set initial Position
        transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z) + Vector3.forward * 2;
        // Set destination
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update() {
        // Update targets position
        agent.SetDestination(target.transform.position);

        // if the positions are the same
        if (agent.transform.position == target.transform.position) {
            // explode
            Explode(target);
        }
    }

    void Explode(GameObject go) {
        if (go.tag == "Player") {
            // spin cars
            go.GetComponent<PlayerHit>().HitSpin();
        }
        // destroy rocket
        Destroy(this.gameObject);
    }
}