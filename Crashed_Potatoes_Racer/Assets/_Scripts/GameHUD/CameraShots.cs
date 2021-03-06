using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShots : MonoBehaviour
{
    [SerializeField]
    private Camera[] cameraObjects;
    [SerializeField]
    private GameObject gameHUD;

    private bool isHUDActive = true;

    public void Update()
    {
        switch (Input.inputString)
        {
            case "-":
                //Resets the camera to the kart's perspective
                ActivateCamera(9);
                break;
            case "1":
                ActivateCamera(0);
                break;
            case "2":
                ActivateCamera(1);
                break;
            case "3":
                ActivateCamera(2);
                break;
            case "4":
                ActivateCamera(3);
                break;
            case "5":
                ActivateCamera(4);
                break;
            case "6":
                ActivateCamera(5);
                break;
            case "7":
                ActivateCamera(6);
                break;
            case "8":
                ActivateCamera(7);
                break;
            case "=":
                ToggleHUD();
                break;
        }
    }

    public void ActivateCamera(int targetCamera)
    {
        for (int i = 0; i < cameraObjects.Length; i++)
        {
            if (i == targetCamera)
            {
                cameraObjects[i].gameObject.SetActive(true);
            }
            else
            {
                cameraObjects[i].gameObject.SetActive(false);
            }
        }
    }

    public void ToggleHUD()
    {
        if (isHUDActive)
        {
            gameHUD.SetActive(false);
            isHUDActive = false;
        }
        else
        {
            gameHUD.SetActive(true);
            isHUDActive = true;
        }
    }
}
