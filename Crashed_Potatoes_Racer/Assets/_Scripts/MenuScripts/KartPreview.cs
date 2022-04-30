//////////////////////////////////////////////////
/// Created:                                   ///
/// Author:                                    ///
/// Edited By: Iain Farlow                     ///
/// Last Edited: 04/04/2022                    ///
//////////////////////////////////////////////////

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class KartPreview : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    //Each type of part
    public enum TYPE
    {
        CAR = 1,
        WHEEL = 2,
        GUN = 3
    }

    //Serialize Fields for assigning parts to script
    [SerializeField]
    private TYPE m_type;
    [SerializeField]
    private int m_choice;
    //Car this relates to
    [SerializeField]
    GameObject[] m_displayCars;

    [SerializeField]
    GameObject m_GMM;

    //Currently selected part
    public static GameObject m_currentBody;
    public static GameObject m_currentWheels;
    public static GameObject m_currentGun;


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
        //targetObject.GetComponent<Renderer>().material = objectMaterial;

        for (int i = 0; i < textUpdate.Length; i++)
        {
            targetText[i].text = textUpdate[i];
        }

        //Switch on part of the model being targeted
        switch (m_type)
        {
            //for car/ body fo the car
            case (TYPE.CAR):
                //save selection
                m_GMM.GetComponent<GameMenuManager>().m_currentBody = m_choice;

                //ensure all others are hidden
                foreach (GameObject displayCar in m_displayCars)
                {
                    displayCar.SetActive(false);
                }
                //show selected
                m_displayCars[m_choice].SetActive(true);
                //assign to current body
                m_currentBody = m_displayCars[m_choice];
                break;
            case (TYPE.WHEEL):
                //save selection
                m_GMM.GetComponent<GameMenuManager>().m_currentWheels = m_choice;
                //Get all the wheels on the display car
                GameObject[] wheels = new GameObject[12];
                int wheelCount = 0;
                for (int i = 0; i < m_currentBody.transform.childCount; i++)
                {
                    if (m_currentBody.transform.GetChild(i).gameObject.tag == "DisplayWheels")
                    {
                        wheels[wheelCount] = m_currentBody.transform.GetChild(i).gameObject;
                        wheelCount++;
                    }
                }
                //ensure all others are hidden
                foreach (GameObject wheel in wheels)
                {
                    wheel.SetActive(false);
                }
                //show selected
                wheels[m_choice].SetActive(true);
                //assign to current body
                m_currentWheels = wheels[m_choice];
                break;
            case (TYPE.GUN):
                //save selection
                m_GMM.GetComponent<GameMenuManager>().m_currentGun = m_choice;
                //Get gun base for current car
                GameObject gunBase = null;
                for (int i = 0; i < m_currentBody.transform.childCount; i++)
                {
                    if (m_currentBody.transform.GetChild(i).gameObject.tag == "DisplayGunBase")
                    {
                        m_currentBody.transform.GetChild(i).gameObject.SetActive(true);
                        gunBase = m_currentBody.transform.GetChild(i).gameObject;
                    }
                }
                //Get all the guns on the display car's gun base
                GameObject[] guns = new GameObject[3];
                int gunCount = 0;
                for (int i = 0; i < gunBase.transform.childCount; i++)
                {
                    if (gunBase.transform.GetChild(i).gameObject.tag == "DisplayGun")
                    {
                        guns[gunCount] = gunBase.transform.GetChild(i).gameObject;
                        gunCount++;
                    }
                }
                //ensure all others are hidden
                foreach (GameObject gun in guns)
                {
                    gun.SetActive(false);
                }
                //show selected
                guns[m_choice].SetActive(true);
                //assign to current body
                m_currentGun = guns[m_choice];
                break;
            default:
                //Default means type passed in isn't expected
                Debug.LogError("Unknown Type!");
                break;
        }
    }
}