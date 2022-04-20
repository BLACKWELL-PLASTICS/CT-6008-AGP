using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PersistentInfo : MonoBehaviour
{
    public int m_connectedUsers { get; set; }
    public List<string> m_connectedNames { get; set; }
    public List<CarDesigns> m_carDesigns { get; set; }
    public int m_levelNum { get; set; }
    public int m_currentPlayerNum { get; set; }
    public string m_currentPlayerName { get; set; }
    public CarDesigns m_carDesign { get; set; }

    public static PersistentInfo Instance { set; get; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            m_connectedNames = new List<string>();
            m_carDesigns = new List<CarDesigns>();
            m_levelNum = 0;
            m_carDesign = new CarDesigns();
        }
    }
    public void Clear()
    {
        m_connectedUsers = 0;
        m_connectedNames.Clear();
        m_carDesigns.Clear();
        m_levelNum = 0;
        m_currentPlayerNum = 0;
    }
}

[System.Serializable]
public class CarDesigns
{
    public int m_carChoice = 0;
    public int m_wheelChoice = 0;
    public int m_gunChoice = 0;
}