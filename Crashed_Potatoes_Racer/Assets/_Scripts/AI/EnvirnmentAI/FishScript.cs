using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour //script for spawning the fish in lvl 1 - By Anna
{
    public GameObject[] fishPrefabs; //array of fish prefabs
    public Material[] fishSkins; //array of fish textures
    public float fishRandomnessSea; //randomness of spawning fish in sea area
    public float fishRandomnessWater; //randomness of spawning fish in water area
    public float fishSpeed; //speed of fish movement
    public float fishJumpTime; //time between fish jump up and down

    private GameObject[] sceneObj; //temp array of objects
    private List<GameObject> waterInScene = new List<GameObject>(); //array of areas listed as water
    private List<GameObject> oceanInScene = new List<GameObject>(); //array of area lsited as sea
    private GameObject fish; //temp object for fish
    private float[] turn = new float[] { -90, 0, 90 }; //float for turn value 

    private void Start()
    {
        sceneObj = FindObjectsOfType(typeof(GameObject)) as GameObject[]; //search through all gameobjects
        foreach(GameObject g in sceneObj) //for each object
        {
            if(g.tag == "Ocean") //find those tagged as ocean
            {
                oceanInScene.Add(g); //add to sea array
            }
            else if (g.tag == "Water") //find those tagged as water
            {
                waterInScene.Add(g); //add to water array
            }
        }
    }
    private void Update()
    {
        if (Random.Range(0, fishRandomnessSea) < 50) // when a random number 0 to sea randomness is bigger than 50
        {
            int ran = Random.Range(0, oceanInScene.Count); //random number between 0 and amount of sea objects
            int ranFish = Random.Range(0, 2); //random number between 0, 1 for fish models
            int ranSkin = Random.Range(0, 4); //random number between 0, 3 for fish textures
            int ranTurn = Random.Range(0, 3); // random number between 0, 2 for for turn randomness
            fish = Instantiate(fishPrefabs[ranFish], GetRandomPoint(oceanInScene[ran]), new Quaternion(0, turn[ranTurn], 0, 0));//instanstiate random fish and set to object
            fish.GetComponentInChildren<MeshRenderer>().material = fishSkins[ranSkin]; //set texture
            fish.AddComponent<FishMovement>(); //add fish movment script to spawned fish
            fish.GetComponent<FishMovement>().spawner = this; //connect this script to movement script
        }

        if (Random.Range(0, fishRandomnessWater) < 50)// when a random number 0 to water randomness is bigger than 50
        {
            int ran = Random.Range(0, waterInScene.Count);//random number between 0 and amount of sea objects
            int ranFish = Random.Range(0, 2); //random number between 0, 1 for fish models
            int ranSkin = Random.Range(0, 4); //random number between 0, 3 for fish textures
            int ranTurn = Random.Range(0, 3); // random number between 0, 2 for for turn randomness
            fish = Instantiate(fishPrefabs[ranFish], GetRandomPoint(waterInScene[ran]), new Quaternion(0, turn[ranTurn], 0, 0));//instanstiate random fish and set to object
            fish.GetComponentInChildren<MeshRenderer>().material = fishSkins[ranSkin];//set texture
            fish.AddComponent<FishMovement>();//add fish movment script to spawned fish
            fish.GetComponent<FishMovement>().spawner = this;//connect this script to movement script
        }
    }

    private Vector3 GetRandomPoint(GameObject water)//creates the random point for fish to spawn
    {
        Bounds bounds = water.GetComponent<Collider>().bounds; //gets bounds of selected area

        return new Vector3(Random.Range(bounds.min.x + 1, bounds.max.x - 1), //returns a random point within bounds
                           //Random.Range(bounds.min.y, bounds.max.y),
                           bounds.min.y - 5,
                           Random.Range(bounds.min.z + 1, bounds.max.z - 1));
    }
}
