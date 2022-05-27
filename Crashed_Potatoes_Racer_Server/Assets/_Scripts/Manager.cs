//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 03/02/2022                        ///
/// Edited By:                                 ///
/// Last Edited: 01/03/2022                    ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class Manager : MonoBehaviour
{
    List<ServerInfo> m_servers = new List<ServerInfo>();
    // Start is called before the first frame update
    void Start()
    {
        //initlise server
        Server.Instance.Initlialise(8009);
    }

    void Awake()
    {
        RegisterEvenets();
    }

    void RegisterEvenets()
    {
        //register events to listen for them coming to manager
        ServerUtility.S_SERVER_START += OnServerStart;
        ServerUtility.S_SERVER_END += OnServerEnd;
        ServerUtility.S_LIST_REQUEST += OnListRequest;
    }
    void UnregisterEvenets()
    {
        //unregister not neaded for server but for game is required to ensure memory is disposed of due to static function being used
        ServerUtility.S_SERVER_START -= OnServerStart;
        ServerUtility.S_SERVER_END -= OnServerEnd;
        ServerUtility.S_LIST_REQUEST -= OnListRequest;
    }

    //Menu Server
    void OnServerStart(ServerMessage a_msg, NetworkConnection a_connection)
    {
        //send over data to all clients
        ServerHostStart serverStart = a_msg as ServerHostStart;
        ServerInfo serverInfo = new ServerInfo();
        serverInfo.m_IP = serverStart.m_ServerIP;
        serverInfo.m_Name = serverStart.m_ServerName;
        serverInfo.m_Level = serverStart.m_level;
        m_servers.Add(serverInfo);
        Server.Instance.Broadcast(serverStart);
    }
    void OnServerEnd(ServerMessage a_msg, NetworkConnection a_connection)
    {
        //when server is closed go through list of server and remove it
        ServerHostEnd serverEnd = a_msg as ServerHostEnd;
        for (int i = 0; i < m_servers.Count; i++)
        {
            if (m_servers[i].m_IP == serverEnd.m_ServerIP)
            {
                m_servers.Remove(m_servers[i]);
            }
        }
        Server.Instance.Broadcast(serverEnd);
    }
    void OnListRequest(ServerMessage a_msg, NetworkConnection a_connection)
    {
        //dispatch information to clients who has requested the server list
        foreach (ServerInfo serverInfo in m_servers)
        {
            ServerListRequest serverListRequest = new ServerListRequest();
            serverListRequest.m_ServerIP = serverInfo.m_IP;
            serverListRequest.m_ServerName = serverInfo.m_Name;
            serverListRequest.m_level = serverInfo.m_Level;
            Server.Instance.SendToClient(a_connection, serverListRequest);
        }
    }
    private void OnDestroy()
    {
        UnregisterEvenets();
    }
}


public class ServerInfo
{
    public string m_IP { get; set; }
    public string m_Name { get; set; }
    public int m_Level { get; set; }
}