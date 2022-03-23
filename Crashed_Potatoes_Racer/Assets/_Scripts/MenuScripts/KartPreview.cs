using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class KartPreview : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    [SerializeField]
    private GameObject targetObject;
    public Material objectMaterial;

    [SerializeField]
    private TextMeshProUGUI[] targetText;
    public string[] textUpdate;

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
        //Updates the model and text when the player selects a customisable option
        targetObject.GetComponent<Renderer>().material = objectMaterial;

        for (int i = 0; i < textUpdate.Length; i++)
        {
            targetText[i].text = textUpdate[i];
        }
    }
}
