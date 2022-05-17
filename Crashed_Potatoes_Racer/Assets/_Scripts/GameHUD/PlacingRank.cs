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

    public void UpdateRank(int playerRanking)
    {
        placingAnimator.SetInteger("playerRanking", playerRanking);
    }
}
