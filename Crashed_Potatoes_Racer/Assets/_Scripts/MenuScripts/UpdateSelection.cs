using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSelection : MonoBehaviour
{
    [SerializeField]
    private Sprite selectedSprite;
    [SerializeField]
    private Sprite deselectedSprite;

    [SerializeField]
    private Image[] updatedButtons;

    public void ChangeButtonSprites(int targetButton)
    {
        for (int i = 0; i < updatedButtons.Length; i++)
        {
            if (i == targetButton)
            {
                updatedButtons[i].sprite = selectedSprite;
            }
            else
            {
                updatedButtons[i].sprite = deselectedSprite;
            }
        }
    }
}
