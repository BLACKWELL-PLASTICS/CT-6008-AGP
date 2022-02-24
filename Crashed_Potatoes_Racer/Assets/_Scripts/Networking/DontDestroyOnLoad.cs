using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    //Don't destroy on load for moving to another scene
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
