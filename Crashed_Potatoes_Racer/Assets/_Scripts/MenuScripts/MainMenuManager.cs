using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menuPanels;
    [SerializeField]
    private Animator[] menuAnimator;
    [SerializeField]
    private Button[] activeButton;

    private int activeMenu = 0;

    private void Start()
    {
        //Sets the title screen as the first loaded menu
        SetActiveMenu(activeMenu);
    }

    public void ButtonMenuTransition(int targetMenu)
    {
        //Deactivates the current menu and begins playing a menu transition
        menuAnimator[activeMenu].SetBool("isActive", false);
        StartCoroutine(AnimationDuration(menuAnimator[activeMenu], targetMenu));
    }

    IEnumerator AnimationDuration(Animator currentAnimator, int targetMenu)
    {
        //Plays the menu's transition and then activates the "targetMenu"
        AnimatorClipInfo[] currentAnimation = currentAnimator.GetCurrentAnimatorClipInfo(0);

        yield return new WaitForSeconds(currentAnimation[0].clip.length);
        SetActiveMenu(targetMenu);
    }

    public void SetActiveMenu(int targetMenu)
    {
        //Sets the active menu to be value given by the integer "targetMenu"
        for (int i = 0; i < menuPanels.Length; i++)
        {
            if (i == targetMenu)
            {
                menuPanels[i].SetActive(true);
                activeButton[i].Select();
            }
            else
            {
                menuPanels[i].SetActive(false);
            }
        }

        activeMenu = targetMenu;
    }

    public void PopUpMenu(int targetMenu)
    {
        //Keeps the UI of the previous "activeMenu" and creates activates the new "targetMenu"
        for (int i = 0; i < menuPanels.Length; i++)
        {
            if (i == targetMenu)
            {
                menuPanels[i].SetActive(true);
                activeButton[i].Select();
            }
        }

        activeMenu = targetMenu;
    }
}
