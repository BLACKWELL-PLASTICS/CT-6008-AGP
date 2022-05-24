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
        if (GameObject.Find("Car_Reg(Clone)")) {
            switch (GameObject.Find("Car_Reg(Clone)").GetComponent<WinCondition>().lap + 1) {
                case 1:
                    UpdateRank(1);
                    break;
                case 2:
                    UpdateRank(2);
                    break;
                case 3:
                    UpdateRank(3);
                    break;
            }
        } else if (GameObject.Find("Car_MergeDrive(Clone)")) {
            switch (GameObject.Find("Car_MergeDrive(Clone)").GetComponent<WinCondition>().lap + 1) {
                case 1:
                    UpdateRank(1);
                    break;
                case 2:
                    UpdateRank(2);
                    break;
                case 3:
                    UpdateRank(3);
                    break;
            }
        } else if (GameObject.Find("Car_MergeShoot(Clone)")) {
            switch (GameObject.Find("Car_MergeShoot(Clone)").GetComponent<WinCondition>().lap + 1) {
                case 1:
                    UpdateRank(1);
                    break;
                case 2:
                    UpdateRank(2);
                    break;
                case 3:
                    UpdateRank(3);
                    break;
            }
        }
    }

    public void UpdateRank(int playerLap)
    {
        lapAnimator.SetInteger("playerLap", playerLap);
    }
}
