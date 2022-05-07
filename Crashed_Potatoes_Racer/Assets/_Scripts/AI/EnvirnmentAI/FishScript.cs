using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public Material[] fishSkins;
    public float fishRandomnessSea;
    public float fishRandomnessWater;
    public float fishSpeed;
    public float fishJumpTime;

    private GameObject[] sceneObj;
    private List<GameObject> waterInScene = new List<GameObject>();
    private List<GameObject> oceanInScene = new List<GameObject>();
    private GameObject fish;
    private float[] turn = new float[] { -90, 0, 90 };

    private void Start()
    {
        sceneObj = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(GameObject g in sceneObj)
        {
            if(g.tag == "Ocean")
            {
                oceanInScene.Add(g);
            }
            else if (g.tag == "Water")
            {
                waterInScene.Add(g);
            }
        }
    }
    private void Update()
    {
        if (Random.Range(0, fishRandomnessSea) < 50)
        {
            int ran = Random.Range(0, oceanInScene.Count);
            int ranFish = Random.Range(0, 2);
            int ranSkin = Random.Range(0, 4);
            int ranTurn = Random.Range(0, 3);
            fish = Instantiate(fishPrefabs[ranFish], GetRandomPoint(oceanInScene[ran]), new Quaternion(0, turn[ranTurn], 0, 0));
            fish.GetComponentInChildren<MeshRenderer>().material = fishSkins[ranSkin];
            fish.AddComponent<FishMovement>();
            fish.GetComponent<FishMovement>().spawner = this;
        }

        if (Random.Range(0, fishRandomnessWater) < 50)
        {
            int ran = Random.Range(0, waterInScene.Count);
            int ranFish = Random.Range(0, 2);
            int ranSkin = Random.Range(0, 4);
            int ranTurn = Random.Range(0, 3);
            fish = Instantiate(fishPrefabs[ranFish], GetRandomPoint(waterInScene[ran]), new Quaternion(0, turn[ranTurn], 0, 0));
            fish.GetComponentInChildren<MeshRenderer>().material = fishSkins[ranSkin];
            fish.AddComponent<FishMovement>();
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
