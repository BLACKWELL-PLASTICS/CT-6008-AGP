using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : MonoBehaviour
{
    [HideInInspector]
    public static List<AIPlayer> carList = new List<AIPlayer>();

    public NavMeshAgent NavComponent { get; private set; } 
    public Renderer RenderComponent { get; private set; }
    public InventoryScript InventoryComponent { get; private set; }
    public Rigidbody RBComponent { get; private set; }
    public FMODUnity.StudioEventEmitter emitter { get; private set; }

    public float stoppingDistance = 15;
    public int currentWaypoint = 0;
    public Transform target;
    public bool decreaseCheck = false;
    public bool decreaseBoostCheck = false;
    public SeedPacketScript.POWERUPS powerUp1;
    public Vector3 originalPos;
    public Vector3 currentPos;
    public Vector3 originalScale;
    public float timer = 0;

    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private float accel = 0;
    [SerializeField]
    private float speedDecrease = 0;
    private float speedIncrease = 0;
    private int randomInOut;
    private int randomSecret;
    private int randomRoute;

    private Transform backLeft;
    private Transform backRight;
    private Transform frontLeft;
    private Transform frontRight;

    private BT bahaviourTree;
    private void Start()
    {
        randomInOut = Random.Range(0, 2);
        randomSecret = Random.Range(0, 6);
        randomRoute = Random.Range(0, 4);

        target = AIManager.GetWaypoints[0];

        carList.Add(this);
        NavComponent = gameObject.GetComponent<NavMeshAgent>();
        RBComponent = gameObject.GetComponent<Rigidbody>();
        InventoryComponent = gameObject.GetComponent<InventoryScript>();
        RenderComponent = GetComponent<Renderer>();
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();

        //ray locations for up orientation
        backLeft = gameObject.transform.Find("backLeft").transform;
        backRight = gameObject.transform.Find("backRight").transform;
        frontLeft = gameObject.transform.Find("frontLeft").transform;
        frontRight = gameObject.transform.Find("frontRight").transform;

        //behaviour tree
        bahaviourTree = new BT(this);

        //size increase power up
        originalScale = transform.localScale;

        //car movement
        speedDecrease = Random.Range(0.7f, 2f);
        speedIncrease = Random.Range(0.17f, AIManager.GetIncrease);
        
    }

    private void Update()
    {
        //waypoints
        if(    AIManager.GetWaypoints8.Length == 0)
        {
            WayPoint3();
        }
        else if(AIManager.GetWaypoints8.Length != 0)
        {
            WayPoint8();
        }

        //correct up direction with terrian
        //transform.up = Vector3.Lerp(transform.up, OrientateUp(), Time.deltaTime);

        //power ups
        powerUp1 = InventoryComponent.p1;
        currentPos = transform.position;

        bahaviourTree.Update();

        NavComponent.speed = speed;
        emitter.SetParameter("Speed", NavComponent.speed);
        NavComponent.acceleration = accel;

        NavComponent.SetDestination(target.position);

        if(powerUp1 == SeedPacketScript.POWERUPS.None)
        {
            InventoryComponent.MovePowerup();
        }

        if(decreaseCheck == true)
        {
            StartCoroutine(DecreaseSpeed());
        }
    }

    public void IncreaseSpeed()
    {
        accel = Mathf.Lerp(accel, AIManager.GetMaxAcc, Time.deltaTime * speedIncrease);
        speed = Mathf.Lerp(speed, AIManager.GetMaxSpeed, Time.deltaTime * speedIncrease);

    }

    public IEnumerator DecreaseSpeed()
    {
        accel = Mathf.Lerp(accel, 10.0f, Time.deltaTime * speedDecrease);
        speed = Mathf.Lerp(speed, 10.0f, Time.deltaTime * speedDecrease);

        yield return new WaitForSeconds(AIManager.GetSlowDownPeriod);

        IncreaseSpeed();
        decreaseCheck = false;
    }

    //boosting
    public void BoostSpeed()
    {
        accel = Mathf.Lerp(accel, 70, Time.deltaTime * speedIncrease);
        speed = Mathf.Lerp(speed, 70, Time.deltaTime * speedIncrease);

    }
    public IEnumerator DeBoostSpeed()
    {
        accel = Mathf.Lerp(accel, 5, Time.deltaTime * speedDecrease * 2);
        speed = Mathf.Lerp(speed, 5, Time.deltaTime * speedDecrease * 2);

        yield return new WaitForSeconds(AIManager.GetSlowDownPeriod);

        IncreaseSpeed();
        decreaseBoostCheck = false;

    }

    private void WayPoint3()
    {
        if (IsCorner() == true)
        {
            decreaseCheck = true;
        }
        else
        {
            IncreaseSpeed();
        }

        if (randomSecret == 3)
        {
            float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints3[currentWaypoint].transform.position);
            if (dist <= stoppingDistance)
            {
                Debug.Log("waypoint +");
                currentWaypoint++;
                if (currentWaypoint >= AIManager.GetWaypoints3.Length)
                {
                    currentWaypoint = 0;
                }
            }
            target = AIManager.GetWaypoints3[currentWaypoint].transform;
        }
        else
        {
            if (randomInOut == 0)
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints[currentWaypoint].transform.position);
                if (dist <= stoppingDistance)
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;
                    if (currentWaypoint >= AIManager.GetWaypoints.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = AIManager.GetWaypoints[currentWaypoint].transform;
            }
            else if (randomInOut == 1)
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints2[currentWaypoint].transform.position);
                if (dist <= stoppingDistance)
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;
                    if (currentWaypoint >= AIManager.GetWaypoints2.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = AIManager.GetWaypoints2[currentWaypoint].transform;
            }
        }

    }

    private void WayPoint8()
    {
        if (IsCorner() == true)
        {
            decreaseCheck = true;
        }
        else
        {
            IncreaseSpeed();
        }

        if(randomRoute == 0)
        {
            if (randomInOut == 0)
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints[currentWaypoint].transform.position);
                if (dist <= stoppingDistance)
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;
                    if (currentWaypoint >= AIManager.GetWaypoints.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = AIManager.GetWaypoints[currentWaypoint].transform;
            }
            else if (randomInOut == 1)
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints2[currentWaypoint].transform.position);
                if (dist <= stoppingDistance)
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;
                    if (currentWaypoint >= AIManager.GetWaypoints2.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = AIManager.GetWaypoints2[currentWaypoint].transform;
            }
        }
        else if(randomRoute == 1)
        {
            if (randomInOut == 0)
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints3[currentWaypoint].transform.position);
                if (dist <= stoppingDistance)
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;
                    if (currentWaypoint >= AIManager.GetWaypoints3.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = AIManager.GetWaypoints3[currentWaypoint].transform;
            }
            else if (randomInOut == 1)
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints4[currentWaypoint].transform.position);
                if (dist <= stoppingDistance)
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;
                    if (currentWaypoint >= AIManager.GetWaypoints4.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = AIManager.GetWaypoints4[currentWaypoint].transform;
            }
        }
        else if (randomRoute == 2)
        {
            if (randomInOut == 0)
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints5[currentWaypoint].transform.position);
                if (dist <= stoppingDistance)
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;
                    if (currentWaypoint >= AIManager.GetWaypoints5.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = AIManager.GetWaypoints5[currentWaypoint].transform;
            }
            else if (randomInOut == 1)
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints6[currentWaypoint].transform.position);
                if (dist <= stoppingDistance)
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;
                    if (currentWaypoint >= AIManager.GetWaypoints6.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = AIManager.GetWaypoints6[currentWaypoint].transform;
            }
        }
        else if (randomRoute == 3)
        {
            if (randomInOut == 0)
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints7[currentWaypoint].transform.position);
                if (dist <= stoppingDistance)
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;
                    if (currentWaypoint >= AIManager.GetWaypoints7.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = AIManager.GetWaypoints7[currentWaypoint].transform;
            }
            else if (randomInOut == 1)
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints8[currentWaypoint].transform.position);
                if (dist <= stoppingDistance)
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;
                    if (currentWaypoint >= AIManager.GetWaypoints8.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = AIManager.GetWaypoints8[currentWaypoint].transform;
            }
        }

    }

    private bool IsCorner()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * AIManager.GetStoppingRay, Color.white);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, AIManager.GetStoppingRay, LayerMask.GetMask("Corner")))
        {
            Debug.Log("Slowing down");
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector3 OrientateUp()
    {
        Physics.Raycast(backLeft.position + Vector3.up, Vector3.down, out RaycastHit lBack);
        Physics.Raycast(backRight.position + Vector3.up, Vector3.down, out RaycastHit rBack);
        Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out RaycastHit lFront);
        Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out RaycastHit rFront);

        Vector3 a = rBack.point - lBack.point;
        Vector3 b = rFront.point - rBack.point;
        Vector3 c = lFront.point - rFront.point;
        Vector3 d = rBack.point - lFront.point;

        // Get the normal at each corner

        Vector3 crossBA = Vector3.Cross(b, a);
        Vector3 crossCB = Vector3.Cross(c, b);
        Vector3 crossDC = Vector3.Cross(d, c);
        Vector3 crossAD = Vector3.Cross(a, d);

        // Calculate composite normal

        Vector3 newUp = (crossBA + crossCB + crossDC + crossAD).normalized;
        return newUp;
    }
}
