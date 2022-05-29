using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour //script for triggering audio, used for ui - By Anna
{
    
    public FMODUnity.EventReference Event; //takes in an fmod event/sound
    public bool PlayOnAwake; //bool for where sound should play on awake

    void Start()
    {
        if (PlayOnAwake) //if true
        {
            PlayOneShot(); //play sound
        }
    }

    public void PlayOneShot() //function for playing fmod sound
    {
        FMODUnity.RuntimeManager.PlayOneShot(Event, gameObject.transform.position); //play at position of gameobject
    }

    public void PlayOneShot2D() //similar function but plays at centre of screen
    {
        FMODUnity.RuntimeManager.PlayOneShot(Event, Vector3.zero);
    }
}
