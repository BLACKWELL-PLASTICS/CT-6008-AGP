using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshBaker : MonoBehaviour
{
    private NavMeshSurface[] walkableArea;
    private void Awake()
    {
        walkableArea = FindObjectsOfType<NavMeshSurface>();

        for(int i = 0; i < walkableArea.Length; i++)
        {
            walkableArea[i].BuildNavMesh();
        }
    }
}
