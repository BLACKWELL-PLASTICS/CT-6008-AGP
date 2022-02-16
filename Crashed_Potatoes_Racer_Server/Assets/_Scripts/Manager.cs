using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Server.Instance.Initlialise(8009);
    }

    void Awake()
    {
        RegisterEvenets();
    }

    void RegisterEvenets()
    {
        ServerUtility.S_SERVER_START += OnServerStart;
        ServerUtility.S_SERVER_END += OnServerEnd;
    }
    void UnregisterEvenets()
    {

    }

    //Menu Server
    void OnServerStart(ServerMessage a_msg, NetworkConnection a_connection)
    {
        ServerHostStart serverStart = a_msg as ServerHostStart;
        Server.Instance.Broadcast(serverStart);
    }
    void OnServerEnd(ServerMessage a_msg, NetworkConnection a_connection)
    {
        ServerHostEnd serverEnd = a_msg as ServerHostEnd;
        Server.Instance.Broadcast(serverEnd);
    }
}
