using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour {
    public GameObject prefab;
    GameObject wall;
    float timer;
    bool isSpawned = false;

    public void SpawnGO() {
        wall = Instantiate(prefab, transform.position - transform.forward, Quaternion.identity);
        isSpawned = true;
    }

    // Update is called once per frame
    void Update() {
        if (isSpawned == true) {
            timer += Time.deltaTime;
            wall.transform.localScale = wall.transform.localScale * 1.009f;

            if (timer > 5f) {
                isSpawned = false;
                Destroy(wall);
                Destroy(gameObject.GetComponent<Obsticle>());
            }
        }
    }
}
