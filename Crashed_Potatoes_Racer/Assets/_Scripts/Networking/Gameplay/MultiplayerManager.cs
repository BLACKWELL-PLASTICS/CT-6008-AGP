using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

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
    GameObject m_obstacle;

    Dictionary<int, int> m_mergeCars = new Dictionary<int, int>();
    Dictionary<int, int> m_demergeCars = new Dictionary<int, int>();

    private void Start()
    {
        for (int i = 0; i < m_startPoints.Length; i++)
        {
            if (PersistentInfo.Instance.m_currentPlayerNum == 1)
            {
                Vector3 pos = m_startPoints[i].transform.position;
                Quaternion rot = m_startPoints[i].transform.rotation;
                GameObject car;
                if (i < PersistentInfo.Instance.m_connectedUsers)
                {
                    if (i == PersistentInfo.Instance.m_currentPlayerNum - 1)
                    {
                        car = Instantiate(m_DivableCar, pos, rot);
                        car.GetComponent<CarManagerScript>().m_playerNum = i + 1;
                        car.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                    }
                    else
                    {
                        car = Instantiate(m_onlineCar, pos, rot);
                        car.GetComponent<CarManagerScript>().m_playerNum = i + 1;
                        car.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                    }
                }
                else
                {
                    car = Instantiate(m_AICar, pos, rot);
                    car.GetComponent<CarManagerScript>().m_playerNum = i + 1;
                    car.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                }
                m_activeCars.Add(car);
            }
            else
            {
                Vector3 pos = m_startPoints[i].transform.position;
                Quaternion rot = m_startPoints[i].transform.rotation;
                GameObject car;
                if (i == PersistentInfo.Instance.m_currentPlayerNum - 1)
                {
                    car = Instantiate(m_DivableCar, pos, rot);
                    car.GetComponent<CarManagerScript>().m_playerNum = i + 1;
                    car.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                }
                else
                {
                    car = Instantiate(m_onlineCar, pos, rot);
                    car.GetComponent<CarManagerScript>().m_playerNum = i + 1;
                    car.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                }
                m_activeCars.Add(car);
            }
        }
    }

    void Awake()
    {
        RegisterEvenets();
    }

    void RegisterEvenets()
    {
        //Server
            //Moving
        NetUtility.S_MAKE_MOVE += OnMoveServer;
            //Merging
        NetUtility.S_MERGE += OnMergeServer;
            //Power Ups
        NetUtility.S_WALL += OnObstacleServer;
        NetUtility.S_GROW += OnSizeIncreaseServer;

        //Client
            //Moving
        NetUtility.C_MAKE_MOVE += OnMoveClient;
            //Merging
        NetUtility.C_MERGE += OnMergeClient;
            //Power Ups
        NetUtility.C_WALL += OnObstacleClient;
        NetUtility.C_GROW += OnSizeIncreaseClient;
    }
    void UnregisterEvenets()
    {

    }

    public void MergeCars(GameObject a_car1, GameObject a_car2)
    {
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
        Client.Instance.SendToServer(netMerge);
    }

    //Server
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
                int mergePlayer;
                if (m_mergeCars.TryGetValue(netMerge.m_Other, out mergePlayer))
                {
                    if (mergePlayer == netMerge.m_Player)
                    {
                        m_mergeCars.Remove(netMerge.m_Other);
                        Server.Instance.Broadcast(netMerge);
                    }
                }
                else
                {
                    m_mergeCars.Add(netMerge.m_Player, netMerge.m_Other);
                }
                break;
            case NetMerge.ACTION.DEMERGE:
                int demergePlayer;
                if (m_demergeCars.TryGetValue(netMerge.m_Other, out demergePlayer))
                {
                    if (demergePlayer == netMerge.m_Player)
                    {
                        m_demergeCars.Remove(netMerge.m_Other);
                        Server.Instance.Broadcast(netMerge);
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


    //Client
    void OnMoveClient(NetMessage a_msg)
    {
        NetMakeMove netMakeMove = a_msg as NetMakeMove; 
        if (netMakeMove.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
        {
            foreach (GameObject car in m_activeCars)
            {
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
                    if (car.GetComponent<CarManagerScript>().m_playerNum == netMerge.m_Player)
                    {
                        car1 = car;
                    }
                    if (car.GetComponent<CarManagerScript>().m_playerNum == netMerge.m_Other)
                    {
                        car2 = car;
                    }
                }
                Vector3 pos = new Vector3(car1.transform.position.x + (car2.transform.position.x - car1.transform.position.x) / 2,
                    car1.transform.position.y + (car2.transform.position.y - car1.transform.position.y) / 2,
                    car1.transform.position.z + (car2.transform.position.z - car1.transform.position.z) / 2);
                Vector3 midDir = (car1.transform.forward.normalized + car2.transform.forward.normalized).normalized;
                Quaternion dir = Quaternion.Euler(midDir.x, midDir.y, midDir.z);



                if (netMerge.m_Player == PersistentInfo.Instance.m_currentPlayerNum)
                {
                    GameObject car = Instantiate(m_mergedDrivePrefab, pos, dir);
                    car.GetComponent<CarManagerScript>().m_playerNum = netMerge.m_Player;
                    car.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum = netMerge.m_Other;
                    car.GetComponent<MergedTimer>().m_maxTimer = m_maxTimer;
                    m_activeCars.Add(car);
                }
                else if (netMerge.m_Other == PersistentInfo.Instance.m_currentPlayerNum)
                {
                    GameObject car = Instantiate(m_mergedShootPrefab, pos, dir);
                    car.GetComponent<CarManagerScript>().m_playerNum = netMerge.m_Player;
                    car.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum = netMerge.m_Other;
                    car.GetComponent<MergedTimer>().m_maxTimer = m_maxTimer;
                    m_activeCars.Add(car);
                }
                else
                {
                    GameObject car = Instantiate(m_mergedOnlinePrefab, pos, dir);
                    car.GetComponent<CarManagerScript>().m_playerNum = netMerge.m_Player;
                    car.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum = netMerge.m_Other;
                    m_activeCars.Add(car);
                }
                m_activeCars.Remove(car1);
                Destroy(car1);
                m_activeCars.Remove(car2);
                Destroy(car2);
                break;
            case NetMerge.ACTION.DEMERGE:
                GameObject mergedCar = null;
                foreach (GameObject car in m_activeCars)
                {
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
                    newCar.GetComponent<CarManagerScript>().m_playerNum = mergedCar.GetComponent<CarManagerScript>().m_playerNum;
                    newCar.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                    m_activeCars.Add(newCar);
                }
                else
                {
                    GameObject newCar = Instantiate(m_onlineCar, leftPos, mergedCar.transform.rotation);
                    newCar.GetComponent<CarManagerScript>().m_playerNum = mergedCar.GetComponent<CarManagerScript>().m_playerNum;
                    newCar.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                    m_activeCars.Add(newCar);
                }

                //Spawn gunners car
                Vector3 rightPos = mergedCar.transform.position + (mergedCar.transform.right);
                if (mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum == PersistentInfo.Instance.m_currentPlayerNum)
                {
                    GameObject newCar = Instantiate(m_DivableCar, rightPos, mergedCar.transform.rotation);
                    newCar.GetComponent<CarManagerScript>().m_playerNum = mergedCar.GetComponentInChildren<MergedShootingControllerScript>().m_playerNum;
                    newCar.GetComponent<CarManagerScript>().m_gameManagerHolder = this.gameObject;
                    m_activeCars.Add(newCar);
                }
                else
                {
                    GameObject newCar = Instantiate(m_onlineCar, rightPos, mergedCar.transform.rotation);
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
                                car.transform.GetChild(car.transform.childCount - 1).transform.rotation = new Quaternion(netMerge.m_XRot, netMerge.m_YRot, netMerge.m_ZRot, netMerge.m_WRot);
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
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netGrow.m_Player)
                        {
                            car.GetComponent<CarManagerScript>().m_oPos = car.transform.position;
                            car.GetComponent<CarManagerScript>().m_OriginalScale = car.transform.localScale;
                            car.transform.localScale = car.GetComponent<CarManagerScript>().m_OriginalScale * 2f;
                            Vector3 pos = transform.position;
                            car.transform.position = new Vector3(pos.x, pos.y + (car.GetComponent<CarManagerScript>().m_OriginalScale.y / 2), pos.y);
                        }
                    }
                    break;
                case NetGrow.ACTION.END:
                    foreach (GameObject car in m_activeCars)
                    {
                        if (car.GetComponent<CarManagerScript>().m_playerNum == netGrow.m_Player)
                        {
                            Vector3 pos = car.transform.position;
                            car.transform.localScale = car.GetComponent<CarManagerScript>().m_OriginalScale;
                            car.transform.position = new Vector3(pos.x, car.GetComponent<CarManagerScript>().m_oPos.y, pos.z);
                        }
                    }
                    break;
                default:
                    Debug.LogError("Unknown Action");
                    break;
            }
        }
    }
}
