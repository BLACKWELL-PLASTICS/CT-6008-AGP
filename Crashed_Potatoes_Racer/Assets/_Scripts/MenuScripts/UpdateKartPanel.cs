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
        if (GameObject.Find("(7) LobbyPanel/PlayerKartPanel/KartImage") != null)
        {
            targetImage = GameObject.Find("(7) LobbyPanel/PlayerKartPanel/KartImage").GetComponent<Image>();
        }
        if (GameObject.Find("(9) SingleplayerPanel/PlayerKartPanel/KartImage") != null)
        {
            targetImage = GameObject.Find("(9) SingleplayerPanel/PlayerKartPanel/KartImage").GetComponent<Image>();
        }
        if (GameObject.Find("(7) LobbyPanel/PlayerKartPanel/KartName") != null)
        {
            targetText = GameObject.Find("(7) LobbyPanel/PlayerKartPanel/KartName").GetComponent<TextMeshProUGUI>();
        }
        if (GameObject.Find("(9) SingleplayerPanel/PlayerKartPanel/KartName") != null)
        {
            targetText = GameObject.Find("(9) SingleplayerPanel/PlayerKartPanel/KartName").GetComponent<TextMeshProUGUI>();
        }
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
