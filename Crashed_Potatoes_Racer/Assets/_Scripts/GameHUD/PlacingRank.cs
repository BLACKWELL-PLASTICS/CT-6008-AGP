using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingRank : MonoBehaviour
{
    public Animator placingAnimator;

    void Start()
    {
        placingAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //Debugging purposes; replace with Oli's code when the player goes up or down in placing and call "UpdateRank()"
        switch (GameObject.Find("Car_Reg(Clone)").GetComponent<Position>().currentPosition)
        {
            case 1:
                UpdateRank(1);
                break;
            case 2:
                UpdateRank(2);
                break;
            case 3:
                UpdateRank(3);
                break;
            case 4:
                UpdateRank(4);
                break;
            case 5:
                UpdateRank(5);
                break;
            case 6:
                UpdateRank(6);
                break;
            case 7:
                UpdateRank(7);
                break;
            case 8:
                UpdateRank(8);
                break;
        }
    }

    public void UpdateRank(int playerRanking)
    {
        placingAnimator.SetInteger("playerRanking", playerRanking);
    }
}
