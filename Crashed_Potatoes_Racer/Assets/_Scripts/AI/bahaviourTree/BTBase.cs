using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTBase 
{
    protected AIPlayer Owner { get; private set; } 
    public Node Root { get; protected set; } 

    public BTBase(AIPlayer owner)
    {
        Owner = owner;
    }
    public void Update()
    {
        Root.Update();
    }
}
