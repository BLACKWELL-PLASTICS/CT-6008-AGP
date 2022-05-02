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
        for (int i = 0; i < 8; i++) {
            isfinished[i] = drivableCars[i].GetComponent<WinCondition>().isFinished;
        }

        if (isfinished.All(x => x)) {
            Debug.Log("ALL PLAYERS HAVE FINISHED THE GAME");
        }
    }
}
