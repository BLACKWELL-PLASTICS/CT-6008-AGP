using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumScript : MonoBehaviour
{
    float timer = 0f;
    int counter = 0;

    private void Update() {
        timer += Time.deltaTime;
        if (counter == 1) {
            gameObject.transform.localScale = gameObject.transform.localScale / 2;
        }
        if (timer >= 30f  || counter > 2) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Terrain") { return; }
        other.gameObject.transform.Find("Smoke").GetComponent<ParticleSystem>().Play();
        other.gameObject.GetComponent<Controller>().isStuck = true;
        counter++;
    }

}
