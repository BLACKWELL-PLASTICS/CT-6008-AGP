//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 07/05/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTimer : MonoBehaviour
{
    [SerializeField]
    private float time = 3.0f;

    private void Start()
    {
        Destroy(this, time);
    }
}
