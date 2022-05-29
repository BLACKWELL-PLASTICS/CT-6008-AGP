//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 07/05/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerManager : MonoBehaviour
{
    public List<GameObject> m_activeCars;

    [SerializeField]
    GameObject m_DivableCar;
    [SerializeField]
    GameObject m_onlineCar;
    [SerializeField]
    GameObject m_AICar;
    [SerializeField]
    GameObject m_mergedDrivePrefab;
    [SerializeField]
    GameObject m_mergedShootPrefab;
    [SerializeField]
    GameObject m_mergedOnlinePrefab;
    [SerializeField]
    GameObject[] m_startPoints;
    [SerializeField]
    float m_maxTimer;
    [SerializeField]
    float m_startingTimer;
    [SerializeField]
    GameObject[] m_startUI;

    [SerializeField]
    GameObject m_timerSlider;
    [SerializeField]
    GameObject[] m_seedPackets;
    [SerializeField]
    GameObject m_obstacle;
    [SerializeField]
    GameObject m_rocket;
    [SerializeField]
    GameObject m_BirdPoop;
    [SerializeField]
    GameObject m_Gum;
    [SerializeField]
    GameObject m_projectile;
    [SerializeField]
    GameObject m_explosivePrefab;
    [SerializeField]
    GameObject m_minePrefab;

    Dictionary<int, int> m_mergeCars = new Dictionary<int, int>();
    Dictionary<int, int> m_demergeCars = new Dictionary<int, int>();

    private float m_timer = 0;
    private bool m_startCountdown = false;
    private float m_startTime = 0.0f;

    private void Start()
    {
        //start on timescale 0
        Time.timeScale = 0;
        //foreach start point
        for (int i = 0; i < m_startPoints.Length; i++)
        {
            //if host
            if (PersistentInfo.Instance.m_currentPlayerNum == 1)
            {
                //get pos rot
                Vector3 pos = m_startPoints[i].transform.position;
                Quaternion rot = m_startPoints[i].transform.rotation;
                GameObject car;
                //if player
                if (i < PersistentInfo.Instance.m_connectedUsers)
                {
                    //if this
                    if (i == PersistentInfo.Instance.m_currentPlayerNum - 1)
                    {
                        //spawn player
                        car = Instantiate(m_DivableCar, pos, rot);
                        //car = Instantiate(m_mergedShootPrefab, pos, rot);
                    }
                    else
                    {
                        //spawn online
                        car = Instantiate(m_onlineCar, pos, rot);
                    }
                }
                else
                {
                    //spawn ai
                    car = Instantiate(m_AICar, pos, rot);
                }
                //set player num 
                car.GetComponent<CarManagerScript>().m_playerNum = i + 1;
                //give it this
                car.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                if (i < PersistentInfo.Instance.m_carDesigns.Count)
                {
                    //spawn all car designs
                    car.GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_carDesigns[i].m_carChoice,
                                                                 PersistentInfo.Instance.m_carDesigns[i].m_wheelChoice,
                                                                 PersistentInfo.Instance.m_carDesigns[i].m_gunChoice);
                }
                else
                {
                    //randomise all ai car designs 
                    int body = Random.Range(0, 8);
                    int wheels = Random.Range(0, 12);
                    int guns = Random.Range(0, 3);
                    NetAISpawn netAISpawn = new NetAISpawn();
                    netAISpawn.m_Player = car.GetComponent<CarManagerScript>().m_playerNum;
                    netAISpawn.m_Body = body;
                    netAISpawn.m_Wheels = wheels;
                    netAISpawn.m_Gun = guns;
                    Server.Instance.Broadcast(netAISpawn);
                }
                //add to cars
                m_activeCars.Add(car);
            }
            //if not host
            else
            {
                //get spawn info
                Vector3 pos = m_startPoints[i].transform.position;
                Quaternion rot = m_startPoints[i].transform.rotation;
                GameObject car;
                if (i == PersistentInfo.Instance.m_currentPlayerNum - 1)
                {
                    //if this spawn drivable
                    car = Instantiate(m_DivableCar, pos, rot);;
                }
                else
                {
                    //spawn online
                    car = Instantiate(m_onlineCar, pos, rot);
                }
                //set cars info
                car.GetComponent<CarManagerScript>().m_playerNum = i + 1;
                car.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                if (i < PersistentInfo.Instance.m_carDesigns.Count)
                {
                    //spawn each car design
                    car.GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_carDesigns[i].m_carChoice,
                                                                 PersistentInfo.Instance.m_carDesigns[i].m_wheelChoice,
                                                                 PersistentInfo.Instance.m_carDesigns[i].m_gunChoice);
                }
                //don't spawn ai designs (host sends this data)
                //add to car list
                m_activeCars.Add(car);
            }
        }
        //send that the player is loaded in
        NetGameCountdown netGameCountdown = new NetGameCountdown();
        netGameCountdown.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
        netGameCountdown.m_Action = NetGameCountdown.ACTION.READY;
        netGameCountdown.m_Count = 0;
        Client.Instance.SendToServer(netGameCountdown);
    }

    private void Update()
    {
        //if host
        if (PersistentInfo.Instance.m_currentPlayerNum == 1 && m_timer == 0)
        {
            //when all cars on all instances of the game are loaded in
            if (PersistentInfo.Instance.m_readyCars == PersistentInfo.Instance.m_connectedUsers)
            {
                //start countdown
                m_startCountdown = true;
                m_startTime = Time.realtimeSinceStartup;
            }
        }
        if (m_startCountdown)
        {
            m_timer = Time.realtimeSinceStartup - m_startTime;
            if (m_timer < m_startingTimer)
            {
                //send counting info
                NetGameCountdown netGameCountdown = new NetGameCountdown();
                netGameCountdown.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                netGameCountdown.m_Action = NetGameCountdown.ACTION.COUNTING;
                netGameCountdown.m_Count = m_timer;
                Client.Instance.SendToServer(netGameCountdown);
            }
            else
            {
                //send end of counting
                m_startCountdown = false;
                NetGameCountdown netGameCountdown = new NetGameCountdown();
                netGameCountdown.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                netGameCountdown.m_Action = NetGameCountdown.ACTION.GO;
                netGameCountdown.m_Count = m_timer;
                Client.Instance.SendToServer(netGameCountdown);
            }
        }

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    int pI = PersistentInfo.Instance.m_currentPlayerNum;
        //    Client.Instance.Shutdown(false);
        //    if (pI == 1)
        //    {
        //        Server.Instance.Shutdown(false);
        //    }
        //    foreach (GameObject car in m_activeCars)
        //    {
        //        if (car.GetComponent<CarManagerScript>().m_playerNum - 1 < PersistentInfo.Instance.m_connectedNames.Count)
        //        {
        //            PersistentInfo.Instance.m_winOrder[car.GetComponent<Position>().currentPosition - 1] = PersistentInfo.Instance.m_connectedNames[car.GetComponent<CarManagerScript>().m_playerNum - 1];
        //        }
        //        else
        //        {
        //            PersistentInfo.Instance.m_winOrder[car.GetComponent<Position>().currentPosition - 1] = "AI";
        //        }

        //        if (car.GetComponent<CarManagerScript>().m_playerNum - 1 < PersistentInfo.Instance.m_carDesigns.Count)
        //        {
        //            PersistentInfo.Instance.m_winDesigns[car.GetComponent<Position>().currentPosition - 1] = PersistentInfo.Instance.m_carDesigns[car.GetComponent<CarManagerScript>().m_playerNum - 1];
        //        }
        //        else
        //        {
        //            PersistentInfo.Instance.m_winDesigns[car.GetComponent<Position>().currentPosition - 1] = PersistentInfo.Instance.m_AIDesigns[(car.GetComponent<CarManagerScript>().m_playerNum - 1) - PersistentInfo.Instance.m_carDesigns.Count];
        //        }
        //    }
        //    SceneManager.LoadScene(4);
        //}
    }

    void Awake()
    {
        //ensure timescale is 0
        Time.timeScale = 0;
        //register events to start listening 
        RegisterEvenets();
    }

    void RegisterEvenets()
    {
        //Server
            //Moving
        NetUtility.S_MAKE_MOVE += OnMoveServer;
            //Merging
        NetUtility.S_MERGE += OnMergeServer;
        NetUtility.S_SHOOT += OnShootServer;
            //Power Ups
        NetUtility.S_WALL += OnObstacleServer;
        NetUtility.S_GROW += OnSizeIncreaseServer;
        NetUtility.S_ROCKET += OnRocketServer;
        NetUtility.S_PCICKED_UP += OnPickUpServer;
        NetUtility.S_BIRD_POOP += OnBirdPoopServer;
        NetUtility.S_GUM += OnGumServer;
        NetUtility.S_BOOST += OnBoostServer;
            //Timer
        NetUtility.S_GAME_COUNTDOWN += OnGameCountdownServer;
            //End Game
        NetUtility.S_FINISHED += OnFinishedServer;

        //Client
        //Moving
        NetUtility.C_MAKE_MOVE += OnMoveClient;
            //Merging
        NetUtility.C_MERGE += OnMergeClient;
        NetUtility.C_SHOOT += OnShootClient;
            //Power Ups
        NetUtility.C_WALL += OnObstacleClient;
        NetUtility.C_GROW += OnSizeIncreaseClient;
        NetUtility.C_ROCKET += OnRocketClient;
        NetUtility.C_PICKED_UP += OnPickUpClient;
        NetUtility.C_BIRD_POOP += OnBirdPoopClient;
        NetUtility.C_GUM += OnGumClient;
        NetUtility.C_BOOST += OnBoostClient;
            //Timer
        NetUtility.C_GAME_COUNTDOWN += OnGameCountdownClient;
            //Cusomised Spawning
        NetUtility.C_AI_SPAWN += OnAISpawnClient;
            //End Game
        NetUtility.C_FINISHED += OnFinishedClient;


    }
    void UnregisterEvenets()
    {
        //Server
            //Moving
        NetUtility.S_MAKE_MOVE -= OnMoveServer;
            //Merging
        NetUtility.S_MERGE -= OnMergeServer;
        NetUtility.S_SHOOT -= OnShootServer;
            //Power Ups
        NetUtility.S_WALL -= OnObstacleServer;
        NetUtility.S_GROW -= OnSizeIncreaseServer;
        NetUtility.S_ROCKET -= OnRocketServer;
        NetUtility.S_PCICKED_UP -= OnPickUpServer;
        NetUtility.S_BIRD_POOP -= OnBirdPoopServer;
        NetUtility.S_GUM -= OnGumServer;
        NetUtility.S_BOOST -= OnBoostServer;
            //Timer
        NetUtility.S_GAME_COUNTDOWN -= OnGameCountdownServer;
            //End Game
        NetUtility.S_FINISHED -= OnFinishedServer;

        //Client
            //Moving
        NetUtility.C_MAKE_MOVE -= OnMoveClient;
            //Merging
        NetUtility.C_MERGE -= OnMergeClient;
        NetUtility.C_SHOOT -= OnShootClient;
            //Power Ups
        NetUtility.C_WALL -= OnObstacleClient;
        NetUtility.C_GROW -= OnSizeIncreaseClient;
        NetUtility.C_ROCKET -= OnRocketClient;
        NetUtility.C_PICKED_UP -= OnPickUpClient;
        NetUtility.C_BIRD_POOP -= OnBirdPoopClient;
        NetUtility.C_GUM -= OnGumClient;
        NetUtility.C_BOOST -= OnBoostClient;
            //Timer
        NetUtility.C_GAME_COUNTDOWN -= OnGameCountdownClient;
            //Cusomised Spawning
        NetUtility.C_AI_SPAWN -= OnAISpawnClient;
        //End Game
        NetUtility.C_FINISHED -= OnFinishedClient;
    }

    public void MergeCars(GameObject a_car1, GameObject a_car2)
    {
        //send merge data
        NetMerge netMerge = new NetMerge();
        netMerge.m_Player = a_car1.GetComponent<CarManagerScript>().m_playerNum;
        netMerge.m_Action = NetMerge.ACTION.MERGE;
        netMerge.m_Other = a_car2.GetComponent<CarManagerScript>().m_playerNum;
        netMerge.m_XPos = 0;
        netMerge.m_YPos = 0;
        netMerge.m_ZPos = 0;
        netMerge.m_XRot = 0;
        netMerge.m_YRot = 0;
        netMerge.m_ZRot = 0;
        netMerge.m_WRot = 0;
        netMerge.m_secondXRot = 0;
        netMerge.m_secondYRot = 0;
        netMerge.m_secondZRot = 0;
        netMerge.m_secondWRot = 0;
        //netMerge.m_lapNum = a_car1.GetComponent<WinCondition>().lap;
        //netMerge.m_checkpointNum = a_car1.GetComponent<WinCondition>().checkpointNumber;
        //netMerge.m_lapNumOther = a_car2.GetComponent<WinCondition>().lap;
        //netMerge.m_checkpointNumOther = a_car2.GetComponent<WinCondition>().checkpointNumber;
        Client.Instance.SendToServer(netMerge);
    }
    GameObject GetPartToRotate(GameObject a_base, int a_index)
    {
        //loop through gun parts to get
        GameObject currentPart = a_base;

        for (int i = 0; i <= a_index; i++)
        {
            currentPart = currentPart.transform.GetChild(0).gameObject;
        }

        return currentPart;
    }


    //Server
    //most send data from server to client
    void OnMoveServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetMakeMove netMakeMove = a_msg as NetMakeMove;
        Server.Instance.SendToOtherClients(a_connection ,netMakeMove);
    }
    void OnMergeServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetMerge netMerge = a_msg as NetMerge;
        switch (netMerge.m_Action)
        {
            case NetMerge.ACTION.MERGE:
                //check if other player has also said they wanna merge
                if (m_mergeCars.ContainsKey(netMerge.m_Other))
                {
                    //if other has 
                    if (m_mergeCars[netMerge.m_Other] == netMerge.m_Player)
                    {
                        //remove from list and send merge to call
                        m_mergeCars.Remove(netMerge.m_Other);
                        Server.Instance.Broadcast(netMerge);
                    }
                    else
                    {
                        //add to merge dict
                        m_mergeCars.Add(netMerge.m_Player, netMerge.m_Other);
                    }
                }
                else
                {
                    //add to merge dict
                    m_mergeCars.Add(netMerge.m_Player, netMerge.m_Other);
                }
                break;
            case NetMerge.ACTION.DEMERGE:
                if (m_demergeCars.ContainsKey(netMerge.m_Other))
                {
                    //if other has said to demerge 
                    if (m_demergeCars[netMerge.m_Other] == netMerge.m_Player)
                    {
                        m_demergeCars.Remove(netMerge.m_Other);
                        Server.Instance.Broadcast(netMerge);
                    }
                    else
                    {
                        m_demergeCars.Add(netMerge.m_Player, netMerge.m_Other);
                    }
                }
                else
                {
                    m_demergeCars.Add(netMerge.m_Player, netMerge.m_Other);
                }
                break;
            default:
                Server.Instance.Broadcast(netMerge);
                break;
        }
    }
    void OnObstacleServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetWall netWall = a_msg as NetWall;
        Server.Instance.Broadcast(netWall);
    }
    void OnSizeIncreaseServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetGrow netGrow = a_msg as NetGrow;
        Server.Instance.Broadcast(netGrow);
    }
    void OnRocketServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetRocket netRocket = a_msg as NetRocket;
        Server.Instance.Broadcast(netRocket);
    }
    void OnPickUpServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetPickedUp netPickedUp = a_msg as NetPickedUp;
        Server.Instance.Broadcast(netPickedUp);
    }
    void OnBirdPoopServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetBirdPoop netBirdPoop = a_msg as NetBirdPoop;
        Server.Instance.Broadcast(netBirdPoop);
    }
    void OnGameCountdownServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetGameCountdown netGameCountdown = a_msg as NetGameCountdown;
        Server.Instance.Broadcast(netGameCountdown);
    }
    void OnShootServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetShoot netShoot = a_msg as NetShoot;
        Server.Instance.Broadcast(netShoot);
    }
    void OnGumServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetGum netGum = a_msg as NetGum;
        Server.Instance.Broadcast(netGum);
    }
    void OnBoostServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetBoost netBoost = a_msg as NetBoost;
        Server.Instance.Broadcast(netBoost);
    }
    void OnFinishedServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetFinished netFinished = a_msg as NetFinished;
        Server.Instance.Broadcast(netFinished);
    }

    //Client
    void OnMoveClient(NetMessage a_msg)
    {
        //set onlines pos & rot
        NetMakeMove netMakeMove = a_msg as NetMakeMove; 
        //not self
        if (netMakeMove.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
        {
            //go through active cars
            foreach (GameObject car in m_activeCars)
            {
                //if car sent is this
                if (car.GetComponent<CarManagerScript>().m_playerNum == netMakeMove.m_Player)
                {
                    car.transform.position = new Vector3(netMakeMove.m_XPos, netMakeMove.m_YPos, netMakeMove.m_ZPos);
                    car.transform.rotation = new Quaternion(netMakeMove.m_XRot, netMakeMove.m_YRot, netMakeMove.m_ZRot, netMakeMove.m_WRot);
                }
            }
        }
    }
    void OnMergeClient(NetMessage a_msg)
    {
        NetMerge netMerge = a_msg as NetMerge;
        switch (netMerge.m_Action)
        {
            //turn on or off merge
            case NetMerge.ACTION.ACTIVATE:
                if (netMerge.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
                {
                    foreach (GameObject car in m_activeCars)
                    {
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netMerge.m_Player)
                        {
                            car.GetComponent<CarManagerScript>().ToggleMerging(false);
                        }
                    }
                }
                break;
            case NetMerge.ACTION.DEACTIVATE:
                if (netMerge.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
                {
                    foreach (GameObject car in m_activeCars)
                    {
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netMerge.m_Player)
                        {
                            car.GetComponent<CarManagerScript>().ToggleMerging(false);
                        }
                    }
                }
                break;
            case NetMerge.ACTION.MERGE:
                GameObject car1 = null;
                GameObject car2 = null;
                foreach (GameObject car in m_activeCars)
                {
                    //get this and other car
                    if (car.GetComponent<CarManagerScript>().m_playerNum == netMerge.m_Player)
                    {
                        car1 = car;
                    }
                    if (car.GetComponent<CarManagerScript>().m_playerNum == netMerge.m_Other)
                    {
                        car2 = car;
                    }
                }
                //combine positions
                Vector3 pos = new Vector3(car1.transform.position.x + (car2.transform.position.x - car1.transform.position.x) / 2,
                    car1.transform.position.y + (car2.transform.position.y - car1.transform.position.y) / 2,
                    car1.transform.position.z + (car2.transform.position.z - car1.transform.position.z) / 2);
                Vector3 midDir = (car1.transform.eulerAngles + car2.transform.eulerAngles) / 2;

                //spawns prefab based on where the player is driving or shooting
                if (netMerge.m_Player == PersistentInfo.Instance.m_currentPlayerNum)
                {
                    //timer slider
                    m_timerSlider.SetActive(true);
                    //spawen driv prefab
                    GameObject car = Instantiate(m_mergedDrivePrefab, pos, Quaternion.identity);
                    car.transform.eulerAngles = midDir;
                    car.transform.up = Vector3.up;
                    //sapwn car design
                    car.GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_carDesigns[netMerge.m_Player - 1].m_carChoice,
                                                                 PersistentInfo.Instance.m_carDesigns[netMerge.m_Player - 1].m_wheelChoice,
                                                                 PersistentInfo.Instance.m_carDesigns[netMerge.m_Other - 1].m_gunChoice);
                    //set cars values from other cars
                    car.GetComponent<CarManagerScript>().m_playerNum = netMerge.m_Player;
                    car.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum = netMerge.m_Other;
                    car.GetComponent<MergedTimer>().m_maxTimer = m_maxTimer;
                    car.GetComponent<WinCondition>().lap = car1.GetComponent<WinCondition>().lap;
                    car.GetComponent<WinCondition>().checkpointNumber = car1.GetComponent<WinCondition>().checkpointNumber;
                    car.GetComponent<WinCondition>().hasBeenChecked = car1.GetComponent<WinCondition>().hasBeenChecked;
                    //get set guns data
                    WinCondition[] winConditions = car.GetComponentsInChildren<WinCondition>();
                    if (winConditions.Length > 1)
                    {
                        winConditions[1].lap = car2.GetComponent<WinCondition>().lap;
                        winConditions[1].checkpointNumber = car2.GetComponent<WinCondition>().checkpointNumber;
                        winConditions[1].hasBeenChecked = car2.GetComponent<WinCondition>().hasBeenChecked;
                    }
                    //add to active cars
                    m_activeCars.Add(car);
                }
                else if (netMerge.m_Other == PersistentInfo.Instance.m_currentPlayerNum)
                {
                    //timer slider
                    m_timerSlider.SetActive(true);
                    //spawen driv prefab
                    GameObject car = Instantiate(m_mergedShootPrefab, pos, Quaternion.identity);
                    //sapwn car design
                    car.transform.eulerAngles = midDir;
                    car.transform.up = Vector3.up;
                    //sapwn design
                    car.GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_carDesigns[netMerge.m_Player - 1].m_carChoice,
                                                                 PersistentInfo.Instance.m_carDesigns[netMerge.m_Player - 1].m_wheelChoice,
                                                                 PersistentInfo.Instance.m_carDesigns[netMerge.m_Other - 1].m_gunChoice);
                    //set cars values
                    car.GetComponent<CarManagerScript>().m_playerNum = netMerge.m_Player;
                    car.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum = netMerge.m_Other;
                    car.GetComponent<MergedTimer>().m_maxTimer = m_maxTimer;
                    car.GetComponent<WinCondition>().lap = car1.GetComponent<WinCondition>().lap;
                    car.GetComponent<WinCondition>().checkpointNumber = car1.GetComponent<WinCondition>().checkpointNumber;
                    car.GetComponent<WinCondition>().hasBeenChecked = car1.GetComponent<WinCondition>().hasBeenChecked;
                    WinCondition[] winConditions = car.GetComponentsInChildren<WinCondition>();
                    //set guns values
                    if (winConditions.Length > 1)
                    {
                        winConditions[1].lap = car2.GetComponent<WinCondition>().lap;
                        winConditions[1].checkpointNumber = car2.GetComponent<WinCondition>().checkpointNumber;
                        winConditions[1].hasBeenChecked = car2.GetComponent<WinCondition>().hasBeenChecked;
                    }
                    //add to list
                    m_activeCars.Add(car);
                }
                else
                {
                    //spawn online same ways as others, just not with any human controls
                    GameObject car = Instantiate(m_mergedOnlinePrefab, pos, Quaternion.identity);
                    car.transform.eulerAngles = midDir;
                    car.transform.up = Vector3.up;
                    car.GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_carDesigns[netMerge.m_Player - 1].m_carChoice,
                                                                 PersistentInfo.Instance.m_carDesigns[netMerge.m_Player - 1].m_wheelChoice,
                                                                 PersistentInfo.Instance.m_carDesigns[netMerge.m_Other - 1].m_gunChoice);
                    car.GetComponent<CarManagerScript>().m_playerNum = netMerge.m_Player;
                    car.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum = netMerge.m_Other;
                    car.GetComponent<WinCondition>().lap = car1.GetComponent<WinCondition>().lap;
                    car.GetComponent<WinCondition>().checkpointNumber = car1.GetComponent<WinCondition>().checkpointNumber;
                    car.GetComponent<WinCondition>().hasBeenChecked = car1.GetComponent<WinCondition>().hasBeenChecked;
                    WinCondition[] winConditions = car.GetComponentsInChildren<WinCondition>();
                    if (winConditions.Length > 1)
                    {
                        winConditions[1].lap = car2.GetComponent<WinCondition>().lap;
                        winConditions[1].checkpointNumber = car2.GetComponent<WinCondition>().checkpointNumber;
                        winConditions[1].hasBeenChecked = car2.GetComponent<WinCondition>().hasBeenChecked;
                    }
                    m_activeCars.Add(car);
                }
                //get rid of old cars
                m_activeCars.Remove(car1);
                Destroy(car1);
                m_activeCars.Remove(car2);
                Destroy(car2);
                break;
            case NetMerge.ACTION.DEMERGE:
                GameObject mergedCar = null;
                foreach (GameObject car in m_activeCars)
                {
                    //get his car
                    if (car.GetComponent<CarManagerScript>().m_playerNum == netMerge.m_Player || car.GetComponent<CarManagerScript>().m_playerNum == netMerge.m_Other)
                    {
                        mergedCar = car;
                    }
                }

                //Spawn drivers car
                Vector3 leftPos = mergedCar.transform.position - (mergedCar.transform.right);
                if (mergedCar.GetComponent<CarManagerScript>().m_playerNum == PersistentInfo.Instance.m_currentPlayerNum)
                {
                    GameObject newCar = Instantiate(m_DivableCar, leftPos, mergedCar.transform.rotation);
                    //spawn car design
                    newCar.GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponent<CarManagerScript>().m_playerNum - 1].m_carChoice,
                                                                    PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponent<CarManagerScript>().m_playerNum - 1].m_wheelChoice,
                                                                    PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponent<CarManagerScript>().m_playerNum - 1].m_gunChoice);
                    //give information
                    newCar.GetComponent<WinCondition>().lap = mergedCar.GetComponent<WinCondition>().lap;
                    newCar.GetComponent<WinCondition>().checkpointNumber = mergedCar.GetComponent<WinCondition>().checkpointNumber;
                    newCar.GetComponent<WinCondition>().hasBeenChecked = mergedCar.GetComponent<WinCondition>().hasBeenChecked;
                    newCar.GetComponent<CarManagerScript>().m_playerNum = mergedCar.GetComponent<CarManagerScript>().m_playerNum;
                    newCar.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                    //set active
                    m_activeCars.Add(newCar);
                }
                else
                {
                    //spawn online same way as /\
                    GameObject newCar = Instantiate(m_onlineCar, leftPos, mergedCar.transform.rotation);
                    newCar.GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponent<CarManagerScript>().m_playerNum - 1].m_carChoice,
                                                                    PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponent<CarManagerScript>().m_playerNum - 1].m_wheelChoice,
                                                                    PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponent<CarManagerScript>().m_playerNum - 1].m_gunChoice);
                    newCar.GetComponent<WinCondition>().lap = mergedCar.GetComponent<WinCondition>().lap;
                    newCar.GetComponent<WinCondition>().checkpointNumber = mergedCar.GetComponent<WinCondition>().checkpointNumber;
                    newCar.GetComponent<WinCondition>().hasBeenChecked = mergedCar.GetComponent<WinCondition>().hasBeenChecked;
                    newCar.GetComponent<CarManagerScript>().m_playerNum = mergedCar.GetComponent<CarManagerScript>().m_playerNum;
                    newCar.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                    m_activeCars.Add(newCar);
                }

                //Spawn gunners car - same method as driver but spawns based on the gunners data
                Vector3 rightPos = mergedCar.transform.position + (mergedCar.transform.right);
                if (mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum == PersistentInfo.Instance.m_currentPlayerNum)
                {
                    GameObject newCar = Instantiate(m_DivableCar, rightPos, mergedCar.transform.rotation);
                    newCar.GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum - 1].m_carChoice,
                                                                    PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum - 1].m_wheelChoice,
                                                                    PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum - 1].m_gunChoice);
                    WinCondition[] winConditions = mergedCar.GetComponentsInChildren<WinCondition>();
                    if (winConditions.Length > 1)
                    {
                        newCar.GetComponent<WinCondition>().lap = winConditions[1].lap;
                        newCar.GetComponent<WinCondition>().checkpointNumber = winConditions[1].checkpointNumber;
                        newCar.GetComponent<WinCondition>().hasBeenChecked = winConditions[1].hasBeenChecked;
                    }
                    newCar.GetComponent<CarManagerScript>().m_playerNum = mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum;
                    newCar.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                    m_activeCars.Add(newCar);
                }
                else
                {
                    GameObject newCar = Instantiate(m_onlineCar, rightPos, mergedCar.transform.rotation);
                    newCar.GetComponent<CustomisedSpawning>().Spawn(PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum - 1].m_carChoice,
                                                                    PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum - 1].m_wheelChoice,
                                                                    PersistentInfo.Instance.m_carDesigns[mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum - 1].m_gunChoice);
                    WinCondition[] winConditions = mergedCar.GetComponentsInChildren<WinCondition>();
                    if (winConditions.Length > 1)
                    {
                        newCar.GetComponent<WinCondition>().lap = winConditions[1].lap;
                        newCar.GetComponent<WinCondition>().checkpointNumber = winConditions[1].checkpointNumber;
                        newCar.GetComponent<WinCondition>().hasBeenChecked = winConditions[1].hasBeenChecked;
                    }
                    newCar.GetComponent<CarManagerScript>().m_playerNum = mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum;
                    newCar.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                    m_activeCars.Add(newCar);
                }

                m_activeCars.Remove(mergedCar);
                Destroy(mergedCar);
                break;
            case NetMerge.ACTION.DRIVE:
                if (netMerge.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
                {
                    foreach (GameObject car in m_activeCars)
                    {
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netMerge.m_Player)
                        {
                            //move the merged car
                            car.transform.position = new Vector3(netMerge.m_XPos, netMerge.m_YPos, netMerge.m_ZPos);
                            car.transform.rotation = new Quaternion(netMerge.m_XRot, netMerge.m_YRot, netMerge.m_ZRot, netMerge.m_WRot);
                        }
                    }
                }
                break;
            case NetMerge.ACTION.SHOOT:
                if (netMerge.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
                {
                    foreach (GameObject car in m_activeCars)
                    {
                        if (car.GetComponentInChildren<MergedShootingControllerScript>() != null)
                        {
                            if (car.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum == netMerge.m_Player)
                            {
                                //rotation of gun
                                GetPartToRotate(car.transform.GetChild(car.transform.childCount - 1).gameObject, 1).transform.rotation = new Quaternion(netMerge.m_XRot, netMerge.m_YRot, netMerge.m_ZRot, netMerge.m_WRot);
                                GetPartToRotate(car.transform.GetChild(car.transform.childCount - 1).gameObject, 2).transform.rotation = new Quaternion(netMerge.m_secondXRot, netMerge.m_secondYRot, netMerge.m_secondZRot, netMerge.m_secondWRot);
                            }
                        }
                    }
                }
                break;
            default:
                Debug.LogError("Unknown Action");
                break;
        }
    }
    void OnObstacleClient(NetMessage a_msg)
    {
        NetWall netWall = a_msg as NetWall;
        if (netWall.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
        {
            //spawn with given pos and rot
            Vector3 pos = new Vector3(netWall.m_XPos, netWall.m_YPos, netWall.m_ZPos);
            Quaternion rot = new Quaternion(netWall.m_XRot, netWall.m_YRot, netWall.m_ZRot, netWall.m_WRot);
            GameObject wall = Instantiate(m_obstacle, pos, rot);
        }
    }
    void OnSizeIncreaseClient(NetMessage a_msg)
    {
        NetGrow netGrow = a_msg as NetGrow;
        if (netGrow.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
        {
            switch (netGrow.m_Action)
            {
                case NetGrow.ACTION.START:
                    foreach (GameObject car in m_activeCars)
                    {
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netGrow.m_CarNum)
                        {
                            //scale the player up
                            car.GetComponent<CarManagerScript>().m_oPos = car.transform.position;
                            car.GetComponent<CarManagerScript>().m_OriginalScale = car.transform.localScale;
                            car.transform.localScale = car.GetComponent<CarManagerScript>().m_OriginalScale * 1.5f;
                            Vector3 pos = transform.position;
                            car.transform.position = new Vector3(pos.x, pos.y + (car.GetComponent<CarManagerScript>().m_OriginalScale.y / 1.5f), pos.y);
                            //collision check
                            car.AddComponent<SizeIncreaseOnlineReplicte>();
                        }
                    }
                    break;
                case NetGrow.ACTION.END:
                    foreach (GameObject car in m_activeCars)
                    {
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netGrow.m_CarNum)
                        {
                            //scale the player down
                            Vector3 pos = car.transform.position;
                            car.transform.localScale = car.GetComponent<CarManagerScript>().m_OriginalScale;
                            car.transform.position = new Vector3(pos.x, pos.y - (car.GetComponent<CarManagerScript>().m_OriginalScale.y / 1.5f), pos.z);

                            //removes size online
                            if (car.GetComponent<SizeIncreaseOnlineReplicte>() != null)
                            {
                                Destroy(car.GetComponent<SizeIncreaseOnlineReplicte>());
                            }
                        }
                    }
                    break;
                default:
                    Debug.LogError("Unknown Action");
                    break;
            }
        }
    }
    void OnRocketClient(NetMessage a_msg)
    {
        NetRocket netRocket = a_msg as NetRocket;
        if (netRocket.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
        {
            switch (netRocket.m_Action)
            {
                case NetRocket.ACTION.FIRE:
                    foreach (GameObject car in m_activeCars)
                    {
                        //when rocket fire packet received mimmic its spawning
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netRocket.m_Player)
                        {
                            GameObject rocket = Instantiate(m_rocket, new Vector3(netRocket.m_XPos, netRocket.m_YPos, netRocket.m_ZPos), new Quaternion(netRocket.m_XRot, netRocket.m_YRot, netRocket.m_ZRot, netRocket.m_WRot));
                            //set its owner (this calculatrs its target)
                            rocket.GetComponent<Rocket>().OwnerAndTarget(car.gameObject);
                        }
                    }
                    break;
                default:
                    Debug.LogError("Unknown Action");
                    break;
            }
        }
    }
    void OnPickUpClient(NetMessage a_msg)
    {
        NetPickedUp netPickedUp = a_msg as NetPickedUp;
        if (netPickedUp.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
        {
            switch (netPickedUp.m_Action)
            {
                //when the packet comes in saying packet is picked up show or hide the packet
                case NetPickedUp.ACTION.APPEAR:
                    foreach (GameObject pickUp in m_seedPackets)
                    {
                        if (pickUp.GetComponent<SeedPacketScript>().m_packetNum == netPickedUp.m_PickUp)
                        {
                            pickUp.GetComponent<SeedPacketScript>().Appear();
                        }
                    }
                    break;
                case NetPickedUp.ACTION.DISAPPEAR:
                    foreach (GameObject pickUp in m_seedPackets)
                    {
                        if (pickUp.GetComponent<SeedPacketScript>().m_packetNum == netPickedUp.m_PickUp)
                        {
                            pickUp.GetComponent<SeedPacketScript>().Disappear();
                        }
                    }
                    break;
                default:
                    Debug.LogError("Unknown Action");
                    break;
            }
        }
    }
    void OnBirdPoopClient(NetMessage a_msg)
    {
        NetBirdPoop netBirdPoop = a_msg as NetBirdPoop;
        if (PersistentInfo.Instance.m_currentPlayerNum == 1)
        {
            //if host slow down ai
            foreach (GameObject car in m_activeCars)
            {
                if (netBirdPoop.m_Player != car.GetComponent<CarManagerScript>().m_playerNum)
                {
                    if (car.GetComponent<AIPlayer>() != null)
                    {
                        car.GetComponent<AIPlayer>().decreaseCheck = true;
                    }
                }
            }
        }
        //if not self toggle the ui to show bird poop
        if (netBirdPoop.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
        {
            m_BirdPoop.GetComponent<BirdPoop>().ToogleActive();
        }
    }
    void OnGameCountdownClient(NetMessage a_msg)
    {
        NetGameCountdown netGameCountdown = a_msg as NetGameCountdown;
        switch (netGameCountdown.m_Action)
        {
            case NetGameCountdown.ACTION.READY:
                //checkw when players are loaded in
                PersistentInfo.Instance.m_readyCars++;
                break;
            case NetGameCountdown.ACTION.UNREADY:
                PersistentInfo.Instance.m_readyCars--;
                break;
                //begins countdown 
            case NetGameCountdown.ACTION.COUNTING:
                if (netGameCountdown.m_Count < 5.0f)
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 0)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < m_startUI.Length; i++)
                    {
                        if (i != 1)
                        {
                            m_startUI[i].SetActive(false);
                        }
                        else
                        {
                            m_startUI[i].SetActive(true);
                        }
                    }
                }
                //else if (netGameCountdown.m_Count < 2.0f)
                //{
                //    for (int i = 0; i < m_startUI.Length; i++)
                //    {
                //        if (i != 1)
                //        {
                //            m_startUI[i].SetActive(false);
                //        }
                //        else
                //        {
                //            m_startUI[i].SetActive(true);
                //        }
                //    }
                //}
                //else if (netGameCountdown.m_Count < 3.0f)
                //{
                //    for (int i = 0; i < m_startUI.Length; i++)
                //    {
                //        if (i != 2)
                //        {
                //            m_startUI[i].SetActive(false);
                //        }
                //        else
                //        {
                //            m_startUI[i].SetActive(true);
                //        }
                //    }
                //}
                //else if (netGameCountdown.m_Count < 4.0f)
                //{
                //    for (int i = 0; i < m_startUI.Length; i++)
                //    {
                //        if (i != 3)
                //        {
                //            m_startUI[i].SetActive(false);
                //        }
                //        else
                //        {
                //            m_startUI[i].SetActive(true);
                //        }
                //    }
                //}
                break;
            case NetGameCountdown.ACTION.GO:
                //for (int i = 0; i < m_startUI.Length; i++)
                //{
                //    if (i != 1)
                //    {
                //        m_startUI[i].SetActive(false);
                //    }
                //    else
                //    {
                //        m_startUI[i].SetActive(true);
                //    }
                //}
                //for (int i = 0; i < m_startUI.Length; i++)
                //{
                //    Destroy(m_startUI[i]);
                //    m_startUI[i] = null;
                //}
                //m_startUI = new GameObject[0];
                
                //sets ctime to 1 for game start
                Time.timeScale = 1;
                break;
            default:
                break;
        }
    }
    void OnAISpawnClient(NetMessage a_msg)
    {
        NetAISpawn netAISpawn = a_msg as NetAISpawn;
        if ((netAISpawn.m_Player > PersistentInfo.Instance.m_connectedUsers))
        {
            foreach (GameObject car in m_activeCars)
            {
                if (car.GetComponent<CarManagerScript>().m_playerNum == netAISpawn.m_Player)
                {
                    //host spawns ai
                    //this is then sent to other players for their ai to spawn. 
                    //if player is still loaded in when packet is received it still works 
                    //packets on loading are ketp
                    car.GetComponent<CustomisedSpawning>().Spawn(netAISpawn.m_Body,
                                                                 netAISpawn.m_Wheels,
                                                                 netAISpawn.m_Gun);
                    //saves design for use in the results screeen
                    CarDesigns carDesigns = new CarDesigns();
                    carDesigns.m_carChoice = netAISpawn.m_Body;
                    carDesigns.m_wheelChoice = netAISpawn.m_Wheels;
                    carDesigns.m_gunChoice = netAISpawn.m_Gun;
                    PersistentInfo.Instance.m_AIDesigns.Add(carDesigns);
                }
            }
        }
    }

    void OnShootClient(NetMessage a_msg)
    {
        NetShoot netShoot = a_msg as NetShoot;
        //switch on gun type
        switch (netShoot.m_Action)
        {
            case NetShoot.ACTION.EXPLOSIVE:
                {
                    if (netShoot.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
                    {
                        //sapwns the fired projectile 
                        Vector3 spawnPos = new Vector3(netShoot.m_XPos, netShoot.m_YPos, netShoot.m_ZPos);
                        Vector3 spawnDir = new Vector3(netShoot.m_XDir, netShoot.m_YDir, netShoot.m_ZDir);
                        GameObject projectile = Instantiate(m_explosivePrefab, spawnPos, Quaternion.identity);
                        projectile.GetComponent<Rigidbody>().AddForce(spawnDir * netShoot.m_Force, ForceMode.Impulse);
                    }
                    break;
                }
            case NetShoot.ACTION.HITSCAN:
                {
                    //hitscan weapon spawns display projectile
                    if (netShoot.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
                    {
                        Vector3 spawnPos = new Vector3(netShoot.m_XPos, netShoot.m_YPos, netShoot.m_ZPos);
                        Vector3 spawnDir = new Vector3(netShoot.m_XDir, netShoot.m_YDir, netShoot.m_ZDir);
                        GameObject projectile = Instantiate(m_projectile, spawnPos, Quaternion.identity);
                        projectile.GetComponent<Rigidbody>().AddForce(spawnDir * netShoot.m_Force, ForceMode.Impulse);
                    }

                    //if the hitscan has shot someone shoot them on this side
                    foreach (GameObject car in m_activeCars)
                    {
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netShoot.m_Other)
                        {
                            car.GetComponent<PlayerHit>().HitSpin();
                        }
                    }
                    break;
                }
            case NetShoot.ACTION.MINE:
                {
                    //simulate spawning of mine prefab
                    if (netShoot.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
                    {
                        Vector3 spawnPos = new Vector3(netShoot.m_XPos, netShoot.m_YPos, netShoot.m_ZPos);
                        Vector3 spawnDir = new Vector3(netShoot.m_XDir, netShoot.m_YDir, netShoot.m_ZDir);
                        GameObject projectile = Instantiate(m_minePrefab, spawnPos, Quaternion.identity);
                        projectile.GetComponent<Rigidbody>().AddForce(spawnDir * netShoot.m_Force, ForceMode.Impulse);
                    }
                    break;
                }
            default:
                Debug.LogError("Unknow ACTION in shoot");
                break;
        }
    }
    void OnGumClient(NetMessage a_msg)
    {
        NetGum netGum = a_msg as NetGum;
        if (netGum.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
        {
            //spawn the gum on position given
            Vector3 pos = new Vector3(netGum.m_XPos, netGum.m_YPos, netGum.m_ZPos);
            Quaternion rot = new Quaternion(netGum.m_XRot, netGum.m_YRot, netGum.m_ZRot, netGum.m_WRot);
            GameObject gum = Instantiate(m_Gum, pos, rot);
        }
    }
    void OnBoostClient(NetMessage a_msg)
    {
        NetBoost netBoost = a_msg as NetBoost;
        switch (netBoost.m_Action)
        {
            case NetBoost.ACTION.START:
                if (netBoost.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
                {
                    foreach (GameObject car in m_activeCars)
                    {
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netBoost.m_CarNum)
                        {
                            //play the boost partical system
                            car.transform.Find("Boost").GetComponent<ParticleSystem>().Play();
                            car.gameObject.AddComponent<BoostOnlineVisualTrigger>();
                        }
                    }
                }
                break;
            case NetBoost.ACTION.END:
                if (netBoost.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
                {
                    foreach (GameObject car in m_activeCars)
                    {
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netBoost.m_CarNum)
                        {
                            //end the boost partical system - driving simulates slow down
                            car.transform.Find("Boost").GetComponent<ParticleSystem>().Stop();
                        }
                    }
                }
                break;
            default:
                break;
        }
    }
    void OnFinishedClient(NetMessage a_msg)
    {
        NetFinished netFinished = a_msg as NetFinished;
        switch (netFinished.m_Action)
        {
            case NetFinished.ACTION.INDEVIDUAL:
                if (netFinished.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
                {
                    foreach (GameObject car in m_activeCars)
                    {
                        //when the player finishes send to all other instances of game
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netFinished.m_Player)
                        {
                            car.GetComponent<WinCondition>().isFinished = true;
                            WinCondition[] winCondition = car.GetComponentsInChildren<WinCondition>();
                            if (winCondition.Length > 1)
                            {
                                //if it has a gun say ended in that too
                                winCondition[1].isFinished = true;
                            }
                        }
                    }
                }
                break;
            case NetFinished.ACTION.ALL:
                foreach (GameObject car in m_activeCars)
                {
                    //when finish is everyone
                    if (car.GetComponent<CarManagerScript>().m_playerNum - 1 < PersistentInfo.Instance.m_connectedNames.Count)
                    {
                        //add the players name to list based on their position
                        PersistentInfo.Instance.m_winOrder[car.GetComponent<Position>().currentPosition - 1] = PersistentInfo.Instance.m_connectedNames[car.GetComponent<CarManagerScript>().m_playerNum - 1];
                        if (car.GetComponentInChildren<MergedShootingControllerScript>() != null)
                        {
                            //if has gum check
                            GameObject subcar = car.GetComponentInChildren<MergedShootingControllerScript>().gameObject;
                            PersistentInfo.Instance.m_winOrder[subcar.GetComponent<Position>().currentPosition - 1] = PersistentInfo.Instance.m_connectedNames[subcar.GetComponent<MergedShootingControllerScript>().m_playerNum - 1];
                        }
                    }
                    else
                    {
                        //if not a player name ai
                        PersistentInfo.Instance.m_winOrder[car.GetComponent<Position>().currentPosition - 1] = "AI";
                    }

                    if (car.GetComponent<CarManagerScript>().m_playerNum - 1 < PersistentInfo.Instance.m_carDesigns.Count)
                    {
                        //if a player car set the design in the windesigns based on position
                        PersistentInfo.Instance.m_winDesigns[car.GetComponent<Position>().currentPosition - 1] = PersistentInfo.Instance.m_carDesigns[car.GetComponent<CarManagerScript>().m_playerNum - 1];
                        if (car.GetComponentInChildren<MergedShootingControllerScript>() != null)
                        {
                            //if has gun add thats design too
                            GameObject subcar = car.GetComponentInChildren<MergedShootingControllerScript>().gameObject;
                            PersistentInfo.Instance.m_winDesigns[subcar.GetComponent<Position>().currentPosition - 1] = PersistentInfo.Instance.m_carDesigns[subcar.GetComponent<MergedShootingControllerScript>().m_playerNum - 1];
                        }
                    }
                    else
                    {
                        //if its ai get it from ai designs
                        PersistentInfo.Instance.m_winDesigns[car.GetComponent<Position>().currentPosition - 1] = PersistentInfo.Instance.m_AIDesigns[(car.GetComponent<CarManagerScript>().m_playerNum - 1) - (PersistentInfo.Instance.m_carDesigns.Count)];
                    }
                }
                //Debug.Log("Multiplayer Manager");
                //Debug.Log("Name 1: " + PersistentInfo.Instance.m_winOrder[0]);
                //Debug.Log("Name 2: " + PersistentInfo.Instance.m_winOrder[1]);
                //Debug.Log("Name 3: " + PersistentInfo.Instance.m_winOrder[2]);
                //Debug.Log("Name 4: " + PersistentInfo.Instance.m_winOrder[3]);
                //Debug.Log("Name 5: " + PersistentInfo.Instance.m_winOrder[4]);
                //Debug.Log("Name 6: " + PersistentInfo.Instance.m_winOrder[5]);
                //Debug.Log("Name 7: " + PersistentInfo.Instance.m_winOrder[6]);
                //Debug.Log("Name 8: " + PersistentInfo.Instance.m_winOrder[7]);

                SceneManager.LoadScene(4);
                break;
            default:
                break;
        }
    }


    private void OnDestroy()
    {
        //when this scene closed shutdown server and client but done clear its data
        //unregister events to prevetn memory issues (static stuff)
        UnregisterEvenets();
        Client.Instance.Shutdown(false);
        if (PersistentInfo.Instance.m_currentPlayerNum == 1)
        {
            Server.Instance.Shutdown(false);
        }
    }
}
