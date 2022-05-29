//////////////////////////////////////////////////
/// Created:                                   ///
/// Author:Oliver Blackwell                    ///
/// Edited By: Iain Farlow                     ///
/// Last Edited: 25/05/2022                    ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WinCheck : MonoBehaviour
{
    public GameObject[] drivableCars = new GameObject[8];
    bool[] isfinished = new bool[8];

    // Start is called before the first frame update
    void Start()
    {
        drivableCars = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (PersistentInfo.Instance.m_currentPlayerNum == 1)
        {
            drivableCars = GameObject.FindGameObjectsWithTag("Player");

            int withGun = 0;
            for (int i = 0; i < drivableCars.Length; i++)
            {
                if (i < isfinished.Length && i < drivableCars.Length)
                {
                    isfinished[i] = drivableCars[i].GetComponent<WinCondition>().isFinished;
                    if (drivableCars[i].GetComponentInChildren<MergedShootingControllerScript>() != null)
                    {
                        WinCondition[] winCondition = drivableCars[i].GetComponentsInChildren<WinCondition>();
                        if (winCondition.Length > 1)
                        {
                            isfinished[isfinished.Length - 1 - withGun] = winCondition[1].isFinished;
                        }
                        withGun++;
                    }
                }
            }

            if (isfinished.All(x => x))
            {
                Debug.Log("ALL PLAYERS HAVE FINISHED THE GAME");
                NetFinished netFinsihed = new NetFinished();
                netFinsihed.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                netFinsihed.m_Action = NetFinished.ACTION.ALL;
                Server.Instance.Broadcast(netFinsihed);
            }
        }
    }
}
