using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gum : MonoBehaviour
{
    public GameObject prefab;
    GameObject gum;
    Vector3 eulerAngle;
    Quaternion currentRot;

    public void SpawnGO() {
        Vector3 pos = transform.position - (transform.forward * 2);
        gum = Instantiate(prefab, pos, Quaternion.identity);

        eulerAngle = new Vector3(-90f, 0f, 0f);
        currentRot.eulerAngles = eulerAngle;
        gum.transform.rotation = currentRot;

    }
}
