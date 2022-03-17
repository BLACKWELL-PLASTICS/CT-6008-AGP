using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menuPanels;
    [SerializeField]
    private Button[] activeButton;

    void Start()
    {
        SetActiveMenu(0);
    }

    public void SetActiveMenu(int menu)
    {
        for (int i = 0; i < menuPanels.Length; i++)
        {
            if (i == menu)
            {
                menuPanels[i].SetActive(true);
                activeButton[i].Select();
            }
            else
            {
                menuPanels[i].SetActive(false);
            }
        }
    }
}
