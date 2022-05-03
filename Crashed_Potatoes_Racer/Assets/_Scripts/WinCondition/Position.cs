using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public int currentPosition;

    // Update is called once per frame
    void Update()
    {
        if (currentPosition < 1) {
            currentPosition = 1;
        }
        if (currentPosition > 8) {
            currentPosition = 8;
        }
    }

    public void MoveUpOne() {
        currentPosition++;
    }
    public void MoveDownOne() {
        currentPosition--;
    }
}
