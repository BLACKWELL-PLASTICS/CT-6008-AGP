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
    GameObject[] m_startPoints;

    private void Start()
    {
        for (int i = 0; i < PersistentInfo.Instance.m_connectedUsers; i++)
        {
            Vector3 pos = m_startPoints[i].transform.position;
            Quaternion rot = m_startPoints[i].transform.rotation;

            if (i == PersistentInfo.Instance.m_currentPlayerNum - 1)
            {
                Instantiate(m_DivableCar, pos, rot);
            }
            else
            {
                Instantiate(m_onlineCar, pos, rot);
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
        NetUtility.S_MAKE_MOVE += OnMoveServer;

        //Client
        NetUtility.C_MAKE_MOVE += OnMoveClient;
    }
    void UnregisterEvenets()
    {

    }

    //Server
    void OnMoveServer(NetMessage a_msg, NetworkConnection a_connection)
    {
        NetMakeMove netMakeMove = a_msg as NetMakeMove;
        Server.Instance.Broadcast(netMakeMove);
    }

    //Client
    void OnMoveClient(NetMessage a_msg)
    {
        NetMakeMove netMakeMove = a_msg as NetMakeMove; 
        if (netMakeMove.m_Player != PersistentInfo.Instance.m_currentPlayerNum)
        {
            m_activeCars[netMakeMove.m_Player - 1].transform.position = new Vector3(netMakeMove.m_XPos, netMakeMove.m_YPos, netMakeMove.m_ZPos);
            m_activeCars[netMakeMove.m_Player - 1].transform.rotation = new Quaternion(netMakeMove.m_XRot, netMakeMove.m_YRot, netMakeMove.m_ZRot, netMakeMove.m_WRot);
        }
    }
}
