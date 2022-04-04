using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class KartPreview : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public enum TYPE
    {
        CAR = 1,
        WHEEL = 2,
        GUN = 3
    }

    [SerializeField]
    private TYPE m_type;
    [SerializeField]
    private int m_choice;
    
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

        //switch (m_type)
        //{
        //    case (TYPE.CAR):
        //        PersistentInfo.Instance.m_carDesigns[0].m_carChoice = m_choice;
        //        break;
        //    case (TYPE.WHEEL):
        //        PersistentInfo.Instance.m_carDesigns[0].m_wheelChoice = m_choice;
        //        break;
        //    case (TYPE.GUN):
        //        PersistentInfo.Instance.m_carDesigns[0].m_gunChoice = m_choice;
        //        break;
        //    default:
        //        Debug.LogError("Unknown Type!");
        //        break;
        //}
    }
}
