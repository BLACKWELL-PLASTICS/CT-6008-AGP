using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : MonoBehaviour //main script for ai, deals with speed and movement - by anna 
{
    //definitions of variables used by the AI
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
    //power up
    public SeedPacketScript.POWERUPS powerUp1;
    public Vector3 originalPos;
    public Vector3 currentPos;
    public Vector3 originalScale;
    //temp timer
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
        //random values for waypoint systems
        randomInOut = Random.Range(0, 2); 
        randomSecret = Random.Range(0, 6);
        randomRoute = Random.Range(0, 4);

        target = AIManager.GetWaypoints[0]; //set target to first waypoint

        carList.Add(this); //adds current car to list
        NavComponent = gameObject.GetComponent<NavMeshAgent>(); //connection to nav mesh agent
        RBComponent = gameObject.GetComponent<Rigidbody>(); //connection to rigid body
        InventoryComponent = gameObject.GetComponent<InventoryScript>(); //connection to inventory script
        RenderComponent = GetComponent<Renderer>(); //connection to renderer component - not needed
        emitter = GetComponent<FMODUnity.StudioEventEmitter>(); //connection to fmod car sound emitter

        //ray locations for up orientation - not used
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
        if(    AIManager.GetWaypoints8.Length == 0) //checks which waypoint system for each level to use
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
        powerUp1 = InventoryComponent.p1; //set current power up to power up temp value
        currentPos = transform.position; //set current to current position

        bahaviourTree.Update(); //update the behaviour tree

        NavComponent.speed = speed; //set speed to temp value
        emitter.SetParameter("Speed", NavComponent.speed); //adjust car sound with the speed
        NavComponent.acceleration = accel; //set accleration to temp value

        NavComponent.SetDestination(target.position); //move towards target

        if(powerUp1 == SeedPacketScript.POWERUPS.None) //if power up is empty
        {
            InventoryComponent.MovePowerup(); //move the second power up to first power up
        }

        if(decreaseCheck == true) //if decrease is triggered
        {
            StartCoroutine(DecreaseSpeed());
        }
        if (decreaseBoostCheck == true) //if decrease is triggered for boost
        {
            StartCoroutine(DeBoostSpeed());
        }
    }

    public void IncreaseSpeed() //slowly increase speed to max
    {
        accel = Mathf.Lerp(accel, AIManager.GetMaxAcc, Time.deltaTime * speedIncrease);
        speed = Mathf.Lerp(speed, AIManager.GetMaxSpeed, Time.deltaTime * speedIncrease);

    }

    public IEnumerator DecreaseSpeed() //decrease speed slowly then wait for a period before speeding back up
    {
        accel = Mathf.Lerp(accel, 10.0f, Time.deltaTime * speedDecrease);
        speed = Mathf.Lerp(speed, 10.0f, Time.deltaTime * speedDecrease);

        yield return new WaitForSeconds(AIManager.GetSlowDownPeriod);

        IncreaseSpeed();
        decreaseCheck = false;
    }

    //boosting
    public void BoostSpeed() //function for slowly increasing the speed to an even higher value for boosting
    {
        accel = Mathf.Lerp(accel, 70, Time.deltaTime * speedIncrease);
        speed = Mathf.Lerp(speed, 70, Time.deltaTime * speedIncrease);

    }
    public IEnumerator DeBoostSpeed() //function which slow decreases speed to a very low amount for when power ups effect AI then speeds them back up
    {
        accel = Mathf.Lerp(accel, 5, Time.deltaTime * speedDecrease * 2);
        speed = Mathf.Lerp(speed, 5, Time.deltaTime * speedDecrease * 2);

        yield return new WaitForSeconds(AIManager.GetSlowDownPeriod);

        IncreaseSpeed();
        decreaseBoostCheck = false;

    }

    private void WayPoint3()//waypoint system for lvl 1
    {
        if (IsCorner() == true)//if there is a corner
        {
            decreaseCheck = true;//decrease speed
        }
        else //if not 
        {
            IncreaseSpeed();//increase speed
        }

        if (randomSecret == 3)  //short cut route
        {
            float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints3[currentWaypoint].transform.position); //distance from ai to current waypoint
            if (dist <= stoppingDistance)//if within stopping distance
            {
                Debug.Log("waypoint +");
                currentWaypoint++;//move to next waypoint
                if (currentWaypoint >= AIManager.GetWaypoints3.Length)//if not more waypoints
                {
                    currentWaypoint = 0;//reset waypoints
                }
            }
            target = AIManager.GetWaypoints3[currentWaypoint].transform;//set target to waypoint
        }
        else
        {
            if (randomInOut == 0) //route one
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints[currentWaypoint].transform.position); //distance from ai to current waypoint
                if (dist <= stoppingDistance)//if within stopping distance
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;//move to next waypoint
                    if (currentWaypoint >= AIManager.GetWaypoints.Length)//if not more waypoints
                    {
                        currentWaypoint = 0;//reset waypoints
                    }
                }
                target = AIManager.GetWaypoints[currentWaypoint].transform;//set target to waypoint
            }
            else if (randomInOut == 1) //route two
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints2[currentWaypoint].transform.position); //distance from ai to current waypoint
                if (dist <= stoppingDistance)//if within stopping distance
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;//move to next waypoint
                    if (currentWaypoint >= AIManager.GetWaypoints2.Length)//if not more waypoints
                    {
                        currentWaypoint = 0;//reset waypoints
                    }
                }
                target = AIManager.GetWaypoints2[currentWaypoint].transform;//set target to waypoint
            }
        }

    }

    private void WayPoint8() //waypoint system for lvl 2
    {
        if (IsCorner() == true) //if there is a corner
        {
            decreaseCheck = true; //decrease speed
        }
        else //if not 
        {
            IncreaseSpeed(); //increase speed
        }

        if(randomRoute == 0) //route one
        {
            if (randomInOut == 0) //left side of route
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints[currentWaypoint].transform.position); //distance from ai to current waypoint
                if (dist <= stoppingDistance) //if within stopping distance
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++; //move to next waypoint
                    if (currentWaypoint >= AIManager.GetWaypoints.Length) //if not more waypoints
                    {
                        currentWaypoint = 0; //reset waypoints
                    }
                }
                target = AIManager.GetWaypoints[currentWaypoint].transform; //set target to waypoint
            }
            else if (randomInOut == 1) //right side of route
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints2[currentWaypoint].transform.position);//distance from ai to current waypoint
                if (dist <= stoppingDistance)//if within stopping distance
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;//move to next waypoint
                    if (currentWaypoint >= AIManager.GetWaypoints2.Length)//if not more waypoints
                    {
                        currentWaypoint = 0;//reset waypoints
                    }
                }
                target = AIManager.GetWaypoints2[currentWaypoint].transform; //set target to waypoint
            }
        }
        else if(randomRoute == 1) //route 2
        {
            if (randomInOut == 0) //left side of route
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints3[currentWaypoint].transform.position);//distance from ai to current waypoint
                if (dist <= stoppingDistance)//if within stopping distance
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;//move to next waypoint
                    if (currentWaypoint >= AIManager.GetWaypoints3.Length)//if not more waypoints
                    {
                        currentWaypoint = 0;//reset waypoints
                    }
                }
                target = AIManager.GetWaypoints3[currentWaypoint].transform; //set target to waypoint
            }
            else if (randomInOut == 1) //right side of route
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints4[currentWaypoint].transform.position);//distance from ai to current waypoint
                if (dist <= stoppingDistance)//if within stopping distance
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;//move to next waypoint
                    if (currentWaypoint >= AIManager.GetWaypoints4.Length)//if not more waypoints
                    {
                        currentWaypoint = 0;//reset waypoints
                    }
                }
                target = AIManager.GetWaypoints4[currentWaypoint].transform; //set target to waypoint
            }
        }
        else if (randomRoute == 2)//route 3
        {
            if (randomInOut == 0)//left side of route
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints5[currentWaypoint].transform.position);//distance from ai to current waypoint
                if (dist <= stoppingDistance)//if within stopping distance
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;//move to next waypoint
                    if (currentWaypoint >= AIManager.GetWaypoints5.Length)//if not more waypoints
                    {
                        currentWaypoint = 0;//reset waypoints
                    }
                }
                target = AIManager.GetWaypoints5[currentWaypoint].transform; //set target to waypoint
            }
            else if (randomInOut == 1)//right side of route
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints6[currentWaypoint].transform.position);//distance from ai to current waypoint
                if (dist <= stoppingDistance)//if within stopping distance
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;//move to next waypoint
                    if (currentWaypoint >= AIManager.GetWaypoints6.Length)//if not more waypoints
                    {
                        currentWaypoint = 0;//reset waypoints
                    }
                }
                target = AIManager.GetWaypoints6[currentWaypoint].transform; //set target to waypoint
            }
        }
        else if (randomRoute == 3)//route 4
        {
            if (randomInOut == 0)//left side of route
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints7[currentWaypoint].transform.position);//distance from ai to current waypoint
                if (dist <= stoppingDistance)//if within stopping distance
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;//move to next waypoint
                    if (currentWaypoint >= AIManager.GetWaypoints7.Length)//if not more waypoints
                    {
                        currentWaypoint = 0;//reset waypoints
                    }
                }
                target = AIManager.GetWaypoints7[currentWaypoint].transform; //set target to waypoint
            }
            else if (randomInOut == 1)//right side of route
            {
                float dist = Vector3.Distance(transform.position, AIManager.GetWaypoints8[currentWaypoint].transform.position);//distance from ai to current waypoint
                if (dist <= stoppingDistance)//if within stopping distance
                {
                    Debug.Log("waypoint +");
                    currentWaypoint++;//move to next waypoint
                    if (currentWaypoint >= AIManager.GetWaypoints8.Length)//if not more waypoints
                    {
                        currentWaypoint = 0;//reset waypoints
                    }
                }
                target = AIManager.GetWaypoints8[currentWaypoint].transform; //set target to waypoint
            }
        }

    }

    private bool IsCorner() //checks if there is a corner in range and returns true if so
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

    private Vector3 OrientateUp() //script for making ai stay flat with the ground - not used as models orientation is scewed and other code uses that orientation
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
