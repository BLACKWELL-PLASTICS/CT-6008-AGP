//////////////////////////////////////////////////
/// Created:                                   ///
/// Author:                                    ///
/// Edited By: Iain Farlow                     ///
/// Last Edited: 25/05/2022                    ///
//////////////////////////////////////////////////

using System.Collections;
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
        if (lap <= 2) { // This can be changed depending on lap limit
            if (hasBeenChecked.All(x => x)) { // if all of the waypoints are checked
                lap++; // increase lap
                checkpointNumber = 0;
                for (int i = 0; i < hasBeenChecked.Length; i++) {
                    hasBeenChecked[i] = false;
                }
            }
        } else {
            // Disable Controller / AI script
            if (gameObject.name == "Car_Reg(Clone)" || gameObject.name == "Car_MergeDrive(Clone)" || (gameObject.tag == "DisplayGunBase" && transform.parent.gameObject.name == "Car_MergeShoot(Clone)")) {

                if (gameObject.GetComponent<Controller>() != null) {
                    gameObject.GetComponent<Controller>().enabled = false;
                } 

                if(gameObject.GetComponent<MergedTimer>() != null){
                    Destroy(gameObject.GetComponent<MergedTimer>());
                }

            } else if (gameObject.GetComponent<AIPlayer>() != null)
            {
                gameObject.GetComponent<AIPlayer>().enabled = false;
                Destroy(gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>());
            }
            // Lock Position
            gameObject.transform.position = gameObject.transform.position;
            
            if (!isFinished)
            {
                // Send Packet Here
                NetFinished netFinished = new NetFinished();
                netFinished.m_Player = GetComponent<CarManagerScript>().m_playerNum;
                netFinished.m_Action = NetFinished.ACTION.INDEVIDUAL;
                if (Client.Instance.m_driver.IsCreated)
                {
                    Client.Instance.SendToServer(netFinished);
                }
            }

            isFinished = true;
            // Will then need to check on the server if all players have send the finish packet
            // to load into next scene.
            // THIS SCRIPT HAS BEEN WRITTEN IN THE WinCheck.cs script
        }
    }
}

