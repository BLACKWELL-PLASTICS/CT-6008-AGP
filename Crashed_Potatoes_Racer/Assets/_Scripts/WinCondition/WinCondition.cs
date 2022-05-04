﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WinCondition : MonoBehaviour
{
    public GameObject[] array;
    public bool[] hasBeenChecked;

    public int lap;
    public int checkpointNumber;

    public bool isFinished = false;

    void Awake()
    {
        array = GameObject.FindGameObjectsWithTag("Waypoints");
        lap = 0;
        // Set array length
        hasBeenChecked = new bool[GameObject.FindGameObjectsWithTag("Waypoints").Length];
        hasBeenChecked[0] = true;
        checkpointNumber = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // if the lap is less than or equal to 3
        if (lap <= 3) { // This can be changed depending on lap limit
            if (hasBeenChecked.All(x => x)) { // if all of the waypoints are checked
                lap++; // increase lap
                checkpointNumber = 0;
                for (int i = 0; i < hasBeenChecked.Length; i++) {
                    hasBeenChecked[i] = false;
                }
            }
        } else {
            // Disable Controller / AI script
            isFinished = true;
            if (gameObject.name == "Car_Reg(Clone)") {
                gameObject.GetComponent<Controller>().enabled = false;
            } else {
                gameObject.GetComponent<AIPlayer>().enabled = false;
            }
            // Lock Position
            gameObject.transform.position = gameObject.transform.position;

            // Send Packet Here
            NetFinished netFinished = new NetFinished();
            netFinished.m_Player = GetComponent<CarManagerScript>().m_playerNum;
            netFinished.m_Action = NetFinished.ACTION.INDEVIDUAL;z
            Client.Instance.SendToServer(netFinished);
            // Will then need to check on the server if all players have send the finish packet
            // to load into next scene.
            // THIS SCRIPT HAS BEEN WRITTEN IN THE WinCheck.cs script
        }
    }
}

