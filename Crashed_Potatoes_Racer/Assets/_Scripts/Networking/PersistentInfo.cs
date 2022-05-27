using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PersistentInfo : MonoBehaviour
{
    public bool m_singleplayer { get; set; }
    public int m_connectedUsers { get; set; }
    public List<string> m_connectedNames { get; set; }
    public List<CarDesigns> m_carDesigns { get; set; }
    public List<CarDesigns> m_AIDesigns { get; set; }
    public int m_levelNum { get; set; }
    public int m_readyCars { get; set; }
    public int m_currentPlayerNum { get; set; }
    public string m_currentPlayerName { get; set; }
    public CarDesigns m_carDesign { get; set; }
    public string[] m_winOrder = new string[8];
    public CarDesigns[] m_winDesigns = new CarDesigns[8];

    [SerializeField]
    public GameObject[] m_CountdownUI;

    public static PersistentInfo Instance { set; get; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            m_singleplayer = false;
            m_connectedNames = new List<string>();
            m_carDesigns = new List<CarDesigns>();
            m_AIDesigns = new List<CarDesigns>();
            m_readyCars = 0;
            m_levelNum = 0;
            m_carDesign = new CarDesigns();
            m_winOrder = new string[8];
            m_winDesigns = new CarDesigns[8];
            //m_CountdownUI = new GameObject[0];
            //m_lap = 0;
            //m_checkpoint = 0;
        }
    }
    public void Clear()
    {
        m_singleplayer = false;
        m_connectedUsers = 0;
        m_connectedNames.Clear();
        m_carDesigns.Clear();
        m_AIDesigns.Clear();
        m_readyCars = 0;
        m_levelNum = 0;
        m_currentPlayerNum = 0;
        m_winOrder = new string[8];
        m_winDesigns = new CarDesigns[8];
        //m_CountdownUI = new GameObject[0];

        //m_lap = 0;
        //m_checkpoint = 0;
    }
}

[System.Serializable]
public class CarDesigns
{
    public int m_carChoice = 0;
    public int m_wheelChoice = 0;
    public int m_gunChoice = 0;
}