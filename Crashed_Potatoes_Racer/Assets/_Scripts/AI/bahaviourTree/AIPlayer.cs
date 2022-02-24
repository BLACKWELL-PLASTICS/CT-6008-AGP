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

    public float stoppingDistance;
    public int currentWaypoint = 0;
    public Transform target;

    //private float thisMaxSpeed;

    private BT bahaviourTree;
    private void Start()
    {
        carList.Add(this);
        NavComponent = gameObject.GetComponent<NavMeshAgent>();
        bahaviourTree = new BT(this);
        RenderComponent = GetComponent<Renderer>();

        //thisMaxSpeed = Random.Range(AIManager.GetMaxSpeed - 10, AIManager.GetMaxSpeed);
    }

    private void Update()
    {
        bahaviourTree.Update();

        NavComponent.SetDestination(target.position);
    }

}
