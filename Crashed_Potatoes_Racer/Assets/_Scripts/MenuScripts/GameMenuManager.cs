//////////////////////////////////////////////////
/// Created:                                   ///
/// Author:                                    ///
/// Edited By: Henry Northway                  ///
/// Last Edited: 04/05/2022                    ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    //Currently selected part
    public int m_currentBody;
    public int m_currentWheels;
    public int m_currentGun;

    void Start()
    {
        if (!usesIainScript)
        {
            // enable raycast
            //foreach (var raycaster in Transform.FindObjectsOfType<GraphicRaycaster>())
            //{
            //    raycaster.enabled = true;
            //}
            SetActiveMenu(activeMenu);
        }
    }

    private void Update()
    {
        //Activates the current back button when the B-button is pressed
        if (Input.GetButtonDown("B_Button"))
        {
            BackButton();
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

    public void LoadScene(bool a_confirmChoice)
    {
        if (a_confirmChoice)
        {
            //save current selection to persistent info's car
            PersistentInfo.Instance.m_carDesign.m_carChoice = m_currentBody;
            //save current selection to persistent info's wheels
            PersistentInfo.Instance.m_carDesign.m_wheelChoice = m_currentWheels;
            //save current selection to persistent info's gun
            PersistentInfo.Instance.m_carDesign.m_gunChoice = m_currentGun;

            if (Client.Instance.m_driver.IsCreated)
            {
                NetCustomiserUpdate netCustomiserUpdate = new NetCustomiserUpdate();
                netCustomiserUpdate.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                netCustomiserUpdate.m_CarBody = PersistentInfo.Instance.m_carDesign.m_carChoice;
                netCustomiserUpdate.m_CarWheels = PersistentInfo.Instance.m_carDesign.m_wheelChoice;
                netCustomiserUpdate.m_CarGun = PersistentInfo.Instance.m_carDesign.m_gunChoice;
                Client.Instance.SendToServer(netCustomiserUpdate);
            }
        }

        foreach (var raycaster in Transform.FindObjectsOfType<GraphicRaycaster>())
        {
            raycaster.enabled = true;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        SceneManager.UnloadSceneAsync(1);
    }
}
