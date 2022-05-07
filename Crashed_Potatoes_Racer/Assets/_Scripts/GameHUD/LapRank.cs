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
        UpdateRank(GameObject.Find("Car_Reg(Clone)").GetComponent<WinCondition>().lap + 1);
    }

    public void UpdateRank(int playerLap)
    {
        lapAnimator.SetInteger("playerLap", playerLap);
    }
}
