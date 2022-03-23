using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour {
    public GameObject prefab;
    GameObject wall;
    Vector3 eulerAngle;
    Quaternion currentRot;

    public void SpawnGO() {
        wall = Instantiate(prefab, transform.position - transform.forward, Quaternion.identity);
        eulerAngle = new Vector3(-90f, 0f, 0f);
        currentRot.eulerAngles = eulerAngle;
        wall.transform.rotation = currentRot;
    }
}
