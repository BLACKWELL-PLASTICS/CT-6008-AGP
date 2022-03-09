using UnityEngine;
using UnityEngine.EventSystems;

public class KartPreview : MonoBehaviour, ISelectHandler
{
    public GameObject targetObject;
    public Material objectMaterial;

    public void OnSelect(BaseEventData eventData)
    {
        targetObject.GetComponent<Renderer>().material = objectMaterial;
    }
}
