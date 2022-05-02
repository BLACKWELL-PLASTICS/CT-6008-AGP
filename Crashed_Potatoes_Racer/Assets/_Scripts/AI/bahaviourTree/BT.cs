using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT : BTBase
{
    //composities
    private Selector rootSelector; //power up or none
    private Selector powerUpSelector;
    private Sequencer rocketSequence;
    private Sequencer floatySequence;
    private Sequencer sizeSequence;
    private Sequencer boostSequence;

    //conditons
    private IsRocket rocketCheck;
    private InRange rangeCheck;
    private InRangeReverse rangeCheckBack;
    private IsFloaty floatyCheck;
    private IsSize sizeCheck;
    private IsBoost boostCheck;

    //nodes
    private WaypointState waypointNode;
    private RocketState rocketFire;
    private FloatyState floatyFire;
    private SizeState sizeIncrease;
    private BoostState boostStart;

    public BT(AIPlayer owner) : base(owner)
    {

        rootSelector = new Selector(owner);
        powerUpSelector = new Selector(owner);
        rocketSequence = new Sequencer(owner);
        floatySequence = new Sequencer(owner);
        sizeSequence = new Sequencer(owner);
        boostSequence = new Sequencer(owner);

        waypointNode = new WaypointState(owner);
        rocketFire = new RocketState(owner);
        floatyFire = new FloatyState(owner);
        sizeIncrease = new SizeState(owner);
        boostStart = new BoostState(owner);

        rocketCheck = new IsRocket(owner);
        rangeCheck = new InRange(owner);
        rangeCheckBack = new InRangeReverse(owner);
        floatyCheck = new IsFloaty(owner);
        sizeCheck = new IsSize(owner);
        boostCheck = new IsBoost(owner);

        //linking nodes
        Root = rootSelector;

        rootSelector.AddNode(powerUpSelector);
        //rootSelector.AddNode(waypointNode);

        powerUpSelector.AddNode(rocketSequence);
        powerUpSelector.AddNode(floatySequence);
        powerUpSelector.AddNode(sizeSequence);
        powerUpSelector.AddNode(boostSequence);

        rocketSequence.AddNode(rocketCheck);
        rocketSequence.AddNode(rangeCheck);
        rocketSequence.AddNode(rocketFire);

        floatySequence.AddNode(floatyCheck);
        floatySequence.AddNode(rangeCheckBack);
        floatySequence.AddNode(floatyFire);

        sizeSequence.AddNode(sizeCheck);
        sizeSequence.AddNode(rangeCheck);
        sizeSequence.AddNode(sizeIncrease);

        boostSequence.AddNode(boostCheck);
        boostSequence.AddNode(boostStart);


    }
}
