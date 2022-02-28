using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CarState
{
    
}

public class AIPlayer : MonoBehaviour
{
    [HideInInspector]
    public static List<AIPlayer> carList = new List<AIPlayer>();

    public NavMeshAgent NavComponent { get; private set; } 
    public Renderer RenderComponent { get; private set; } 

    private BT bahaviourTree;
    private void Start()
    {
        carList.Add(this);
        NavComponent = gameObject.GetComponent<NavMeshAgent>();
        bahaviourTree = new BT(this);
        RenderComponent = GetComponent<Renderer>();
    }

    private void Update()
    {
        bahaviourTree.Update(); 

    }

}
