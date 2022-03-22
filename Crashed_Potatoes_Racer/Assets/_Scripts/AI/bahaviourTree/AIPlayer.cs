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

    public float stoppingDistance = 15;
    public int currentWaypoint = 0;
    public Transform target;
    public bool decreaseCheck = false;

    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private float accel = 0;
    [SerializeField]
    private float speedDecrease = 0;

    private BT bahaviourTree;
    private void Start()
    {
        carList.Add(this);
        NavComponent = gameObject.GetComponent<NavMeshAgent>();
        RBComponent = gameObject.GetComponent<Rigidbody>();
        bahaviourTree = new BT(this);
        RenderComponent = GetComponent<Renderer>();

        speedDecrease = Random.Range(0.5f, 1.0f);
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
        accel = Mathf.Lerp(accel, AIManager.GetMaxAcc, Time.deltaTime * AIManager.GetIncrease);
        speed = Mathf.Lerp(speed, AIManager.GetMaxSpeed, Time.deltaTime * AIManager.GetIncrease);

    }

    public IEnumerator DecreaseSpeed()
    {
        accel = Mathf.Lerp(accel, 10.0f, Time.deltaTime * speedDecrease);
        speed = Mathf.Lerp(speed, 10.0f, Time.deltaTime * speedDecrease);

        yield return new WaitForSeconds(AIManager.GetSlowDownPeriod);

        IncreaseSpeed();
        decreaseCheck = false;
    }

}
