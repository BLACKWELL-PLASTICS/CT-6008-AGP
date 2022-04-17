using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] GameObject[] trackMarkers;
    bool[] hasBeenChecked;
    [SerializeField]
    int lap;

    void Awake()
    {
        lap = 0;
        // Set array length
        trackMarkers = new GameObject[GameObject.FindGameObjectsWithTag("Waypoints").Length];
        hasBeenChecked = new bool[trackMarkers.Length];
        // Set array
        trackMarkers = GameObject.FindGameObjectsWithTag("Waypoints");
    }

    // Update is called once per frame
    void Update()
    {
        if (lap <= 3) { // This can be changed depending on lap limit
            int x = 0;
            for (int i = 0; i < trackMarkers.Length; i++) {
                if (trackMarkers[i].GetComponent<Lap>().IsDrivenThroughGetter() == true) {
                    x++;
                }
            }
            if (x == trackMarkers.Length) {
                lap++;
                foreach (GameObject item in trackMarkers) {
                    item.GetComponent<Lap>().IsDrivenThroughSetter();
                }
            }
        } else {
            // Lock Position
            // Send Packet
        }
    }
}

