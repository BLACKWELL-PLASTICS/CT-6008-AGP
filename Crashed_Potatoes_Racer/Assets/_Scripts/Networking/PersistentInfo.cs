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

    public static PersistentInfo Instance { set; get; }
    void Awake()
    {
        Instance = this;
        m_connectedNames = new List<string>();
    }
    public void Clear()
    {
        m_connectedUsers = 0;
        m_connectedNames.Clear();
        m_currentPlayerNum = 0;
    }
}
