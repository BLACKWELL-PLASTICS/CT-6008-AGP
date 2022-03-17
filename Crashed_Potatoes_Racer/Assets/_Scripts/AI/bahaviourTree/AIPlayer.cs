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
    public bool decreaseCheck = false;

    private float speed = 50;
    private float accel = 0;

    private BT bahaviourTree;
    private void Start()
    {
        carList.Add(this);
        NavComponent = gameObject.GetComponent<NavMeshAgent>();
        RBComponent = gameObject.GetComponent<Rigidbody>();
        bahaviourTree = new BT(this);
        RenderComponent = GetComponent<Renderer>();
    }

    private void Update()
    {
        bahaviourTree.Update();

        NavComponent.speed = speed;
        NavComponent.acceleration = accel;
        NavComponent.SetDestination(target.position);

        if(decreaseCheck == true)
        {
            StartCoroutine(DecreaseSpeed());
        }
    }

    public void IncreaseSpeed()
    {
        accel = Mathf.Lerp(speed, AIManager.GetMaxAcc, Time.deltaTime * AIManager.GetIncrease);
    }

    public IEnumerator DecreaseSpeed()
    {
        speed = Mathf.Lerp(speed, 1.0f, Time.deltaTime * AIManager.GetIncrease * 2);

        yield return new WaitForSeconds(2.0f);

        speed = 50;
        decreaseCheck = false;
    }

}
