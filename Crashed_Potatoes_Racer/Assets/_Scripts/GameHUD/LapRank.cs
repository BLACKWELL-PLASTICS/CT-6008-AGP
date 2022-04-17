using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapRank : MonoBehaviour
{
    public Animator lapAnimator;

    void Start()
    {
        lapAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //Debugging purposes; replace with Oli's code when the player has completed a lap and call "UpdateRank()"
        switch (Input.inputString)
        {
            case "1":
                UpdateRank(1);
                break;
            case "2":
                UpdateRank(2);
                break;
            case "3":
                UpdateRank(3);
                break;
        }
    }

    public void UpdateRank(int playerLap)
    {
        lapAnimator.SetInteger("playerLap", playerLap);
    }
}
