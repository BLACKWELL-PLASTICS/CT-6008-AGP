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

    public Rigidbody RBComponent { get; private set; }

    public float stoppingDistance;
    public int currentWaypoint = 0;
    public Transform target;

    private float speed = 0;

    private BT bahaviourTree;
    private void Start()
    {
        carList.Add(this);
        RBComponent = gameObject.GetComponent<Rigidbody>();
        bahaviourTree = new BT(this);
        RenderComponent = GetComponent<Renderer>();
    }

    private void Update()
    {
        bahaviourTree.Update();

        this.transform.LookAt(target);
        RBComponent.AddForce(transform.forward * speed);
    }

    public void IncreaseSpeed()
    {
        speed = Mathf.Lerp(speed, AIManager.GetMaxSpeed, Time.deltaTime * AIManager.GetSpeedIncrease);
    }

    public void DecreaseSpeed()
    {
        speed = Mathf.Lerp(speed, 1.0f, Time.deltaTime * (AIManager.GetSpeedIncrease * 3f));
    }

}
