using UnityEngine;
using UnityEngine.EventSystems;

public class OnlinePreview : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    [SerializeField]
    private GameObject[] onlinePanels;
    [SerializeField]
    private bool isCreatingServer;

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
        //Updates the preview when the player selects a menu option
        if (isCreatingServer)
        {
            onlinePanels[0].SetActive(true);
            onlinePanels[1].SetActive(false);
        }
        else
        {
            onlinePanels[0].SetActive(false);
            onlinePanels[1].SetActive(true);
        }
    }
}
