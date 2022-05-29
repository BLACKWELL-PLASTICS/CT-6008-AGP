using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTBase //class that acts as a base for the behaviour tree - by anna
{
    protected AIPlayer Owner { get; private set; } //getter setter to the ai script
    public Node Root { get; protected set; } //getter and setter for node script

    public BTBase(AIPlayer owner) //constructor sets ai script to owner
    {
        Owner = owner;
    }
    public void Update() //runs the root nodes update function
    {
        Root.Update();
    }
}
