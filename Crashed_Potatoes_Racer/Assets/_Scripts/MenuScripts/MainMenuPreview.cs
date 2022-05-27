using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuPreview : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    [SerializeField]
    private TextMeshProUGUI targetText;
    [SerializeField]
    private Image targetImage;
    [TextArea]
    public string textUpdate;
    [SerializeField]
    private Sprite imageUpdate;

    public void OnSelect(BaseEventData eventData)
    {
        //When the player uses WASD, arrow keys or game controller to navigate the menu
        UpdateSelection();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //When the player uses a mouse cursor to navigate the menu
        UpdateSelection();
    }

    void UpdateSelection()
    {
        //Updates the text and image when the player selects a menu option
        targetText.text = textUpdate;

        if (targetImage != null)
        {
            targetImage.sprite = imageUpdate;
        }
    }
}
