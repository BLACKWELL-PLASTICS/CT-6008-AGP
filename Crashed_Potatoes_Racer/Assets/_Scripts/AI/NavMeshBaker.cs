using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshBaker : MonoBehaviour //script for baking a nav mesh on run time - no longer used as we dont use spline - By Anna
{
    private NavMeshSurface[] walkableArea; //array to hold all walkable areas
    private void Awake() //on awake
    {
        walkableArea = FindObjectsOfType<NavMeshSurface>(); //find all objects with nav mesh surface script attached

        for(int i = 0; i < walkableArea.Length; i++) //loop through them
        {
            walkableArea[i].BuildNavMesh(); //build them
        }
    }
}
