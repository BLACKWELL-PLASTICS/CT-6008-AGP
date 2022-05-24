using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public FMODUnity.EventReference Event;
    public bool PlayOnAwake;

    void Start()
    {
        if (PlayOnAwake)
        {
            PlayOneShot();
        }
    }

    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event.Path, gameObject);
    }

    public void PlayOneShot2D()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Event, Vector3.zero);
    }
}
