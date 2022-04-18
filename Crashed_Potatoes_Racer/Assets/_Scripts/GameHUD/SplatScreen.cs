using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatScreen : MonoBehaviour
{
    public Animator splatAnimator;

    void Start()
    {
        splatAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //Debugging purposes; replace with Oli's code when the player is hit with the power-up and call "SplatAnimation()"
        switch (Input.inputString)
        {
            case "0":
                SplatAnimation();
                break;
        }
    }

    public void SplatAnimation()
    {
        splatAnimator.SetTrigger("coverScreen");
    }
}
