using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public GameObject fishPrefab;

    private GameObject[] sceneObj;
    private List<GameObject> waterInScene = new List<GameObject>();
    private GameObject fish;

    private void Start()
    {
        sceneObj = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(GameObject g in sceneObj)
        {
            if(g.tag == "Water")
            {
                waterInScene.Add(g);
            }
        }
    }
    private void Update()
    {
        if (Random.Range(0, AIManager.GetFishRandomness) < 50)
        {
            int ran = Random.Range(0, waterInScene.Count);
            fish = Instantiate(fishPrefab, GetRandomPoint(waterInScene[ran]), Quaternion.identity);
            fish.GetComponent<FishMovement>().spawner = this;
        }
    }

    private Vector3 GetRandomPoint(GameObject water)
    {
        Bounds bounds = water.GetComponent<Collider>().bounds;

        return new Vector3(Random.Range(bounds.min.x + 1, bounds.max.x - 1),
                           //Random.Range(bounds.min.y, bounds.max.y),
                           bounds.min.y - 5,
                           Random.Range(bounds.min.z + 1, bounds.max.z - 1));
    }
}
