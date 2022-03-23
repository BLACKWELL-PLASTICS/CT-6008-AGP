using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour {
    public GameObject prefab;
    GameObject wall;
    float timer;
    bool isSpawned = false;

    Vector3 eulerAngle;
    Quaternion currentRot;

    public void SpawnGO() {
        wall = Instantiate(prefab, transform.position - transform.forward, Quaternion.identity);
        eulerAngle = new Vector3(-90f, 0f, 0f);
        currentRot.eulerAngles = eulerAngle;
        wall.transform.rotation = currentRot;
        isSpawned = true;
    }

    // Update is called once per frame
    void Update() {
        if (isSpawned == true) {
            timer += Time.deltaTime;
            wall.transform.localScale = wall.transform.localScale * 1.0009f;

            if (timer > 5f) {
                isSpawned = false;
                Destroy(wall);
                Destroy(gameObject.GetComponent<Obsticle>());
            }
        }
    }
}
