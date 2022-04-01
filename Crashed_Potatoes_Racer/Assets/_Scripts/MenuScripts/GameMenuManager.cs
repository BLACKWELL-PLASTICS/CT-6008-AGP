using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField]
    private bool usesIainScript = false;
    [SerializeField]
    private TextMeshProUGUI menuTitle;
    [SerializeField]
    private string[] menuName;
    [SerializeField]
    private GameObject[] menuPanels;
    [SerializeField]
    private Button[] activeButton;

    private int activeMenu = 1;
    private bool keepGUI = false;

    void Start()
    {
        if (!usesIainScript)
        {
            SetActiveMenu(activeMenu);
        }
    }

    public void SetActiveMenu(int targetMenu)
    {
        //Sets keepGUI as true if a confirmation panel is supposed to appear
        if (targetMenu == 0 || targetMenu == (menuPanels.Length - 1))
        {
            keepGUI = true;
        }

        //Sets the active menu to be value given by the integer "menu"
        for (int i = 0; i < menuPanels.Length; i++)
        {
            if (i == targetMenu)
            {
                menuTitle.text = menuName[i];
                menuPanels[i].SetActive(true);
                activeButton[i].Select();
            }
            else
            {
                //If keepGUI is true, it keeps the GUI active when the confirmation panel appears, but the user cannot interact with it because an overlay is above it
                if (!keepGUI)
                {
                    menuPanels[i].SetActive(false);
                }
            }
        }

        keepGUI = false;
        activeMenu = targetMenu;
    }

    public void BackButton()
    {
        //Activates the previous menu
        if (activeMenu > 0)
        {
            SetActiveMenu(activeMenu - 1);
        }
    }

    public void LoadScene()
    {
        Debug.Log("A new scene will be loaded.");
    }
}
