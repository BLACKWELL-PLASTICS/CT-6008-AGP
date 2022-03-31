using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PersistentInfo : MonoBehaviour
{
    public int m_connectedUsers { get; set; }
    public List<string> m_connectedNames { get; set; }
    public int m_currentPlayerNum { get; set; }
    public string m_currentPlayerName { get; set; }
    //public CarDesigns[] m_carDesigns { get; set; }

    public static PersistentInfo Instance { set; get; }
    void Awake()
    {
        Instance = this;
        m_connectedNames = new List<string>();
        //m_carDesigns = new CarDesigns[8];
        //foreach (CarDesigns carDesigns in m_carDesigns)
        //{
        //    carDesigns.Initialise();
        //}
    }
    public void Clear()
    {
        m_connectedUsers = 0;
        m_connectedNames.Clear();
        m_currentPlayerNum = 0;
        //foreach (CarDesigns carDesigns in m_carDesigns)
        //{
        //    carDesigns.Initialise();
        //}
    }
}

//public class CarDesigns
//{
//    public int m_carChoice = 0;
//    public int m_wheelChoice = 0;
//    public int m_gunChoice = 0;

//    public void Initialise()
//    {
//        m_carChoice = 0;
//        m_wheelChoice = 0;
//        m_gunChoice = 0;
//    }
//}