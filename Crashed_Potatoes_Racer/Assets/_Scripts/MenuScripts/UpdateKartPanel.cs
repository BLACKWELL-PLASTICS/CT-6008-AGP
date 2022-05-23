using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateKartPanel : MonoBehaviour
{
    [SerializeField]
    private string[] kartNames;
    [SerializeField]
    private Sprite[] kartSprites;

    public Image targetImage;
    public TextMeshProUGUI targetText;

    void Start()
    {
        targetImage = GameObject.Find("(7) LobbyPanel/PlayerKartPanel/KartImage").GetComponent<Image>();
        targetText = GameObject.Find("(7) LobbyPanel/PlayerKartPanel/KartName").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateLobbyPanel(int kartIndex)
    {
        for (int i = 0; i < kartSprites.Length; i++)
        {
            if (i == kartIndex)
            {
                targetImage.sprite = kartSprites[i];
                targetText.text = kartNames[i];
            }
        }
    }
}
