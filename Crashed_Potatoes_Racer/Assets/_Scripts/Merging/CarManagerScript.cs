//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 26/01/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManagerScript : MonoBehaviour
{
    public int m_playerNum;
    public bool m_mergeOn;
    public GameObject m_gameManagerHolder;

    public Vector3 m_oPos;
    public Vector3 m_OriginalScale;

    [SerializeField]
    Material m_canMergeMaterial;
    [SerializeField]
    Material m_normalMaterial;
    [SerializeField]
    GameObject m_mergeTrigger;

    MultiplayerManager m_mm;

    // Start is called before the first frame update
    void Start()
    {
        //get multiplayer manager
        if (m_gameManagerHolder != null)
        {
            m_mm = m_gameManagerHolder.GetComponent<MultiplayerManager>();
        }
        m_mergeOn = false;
    }

    public void ToggleMerging(bool m_sendToServer)
    {
        m_mergeOn = !m_mergeOn;
        GameObject mergeUI = null;
        //get ui element to show merging
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "MergeUI")
            {
                mergeUI = transform.GetChild(i).gameObject;
            }
        }
        if (m_mergeOn)
        {
            mergeUI.GetComponent<Renderer>().material = m_canMergeMaterial;
            //send data that option to emrge is available  
            if (m_sendToServer)
            {
                NetMerge netMerge = new NetMerge();
                netMerge.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                netMerge.m_Action = NetMerge.ACTION.ACTIVATE;
                netMerge.m_Other = 0;
                netMerge.m_XPos = 0;
                netMerge.m_YPos = 0;
                netMerge.m_ZPos = 0;
                netMerge.m_XRot = 0;
                netMerge.m_YRot = 0;
                netMerge.m_ZRot = 0;
                netMerge.m_WRot = 0;
                Client.Instance.SendToServer(netMerge);
            }
        }
        else
        {
            mergeUI.GetComponent<Renderer>().material = m_normalMaterial;
            //send data that option to emrge is not available  
            if (m_sendToServer)
            {
                NetMerge netMerge = new NetMerge();
                netMerge.m_Player = PersistentInfo.Instance.m_currentPlayerNum;
                netMerge.m_Action = NetMerge.ACTION.DEACTIVATE;
                netMerge.m_Other = 0;
                netMerge.m_XPos = 0;
                netMerge.m_YPos = 0;
                netMerge.m_ZPos = 0;
                netMerge.m_XRot = 0;
                netMerge.m_YRot = 0;
                netMerge.m_ZRot = 0;
                netMerge.m_WRot = 0;
                Client.Instance.SendToServer(netMerge);
            }
        }
    }
    public void EnteredMerge(GameObject a_other)
    {
        //merge the cars
        m_mm.MergeCars(this.gameObject, a_other);
    }
}
