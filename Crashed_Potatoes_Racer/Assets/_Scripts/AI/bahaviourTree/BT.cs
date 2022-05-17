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
    private Sequencer gumSequence;
    private Sequencer birdSequence;

    //conditons
    private IsRocket rocketCheck;
    private InRange rangeCheck;
    private InRangeReverse rangeCheckBack;
    private IsFloaty floatyCheck;
    private IsSize sizeCheck;
    private IsBoost boostCheck;
    private IsGum gumCheck;
    private IsBird birdCheck;

    //nodes
    //private WaypointState waypointNode;
    private RocketState rocketFire;
    private FloatyState floatyFire;
    private SizeState sizeIncrease;
    private BoostState boostStart;
    private GumState gumFire;
    private PoopState birdFire;

    public BT(AIPlayer owner) : base(owner)
    {

        rootSelector = new Selector(owner);
        powerUpSelector = new Selector(owner);
        rocketSequence = new Sequencer(owner);
        floatySequence = new Sequencer(owner);
        sizeSequence = new Sequencer(owner);
        boostSequence = new Sequencer(owner);
        gumSequence = new Sequencer(owner);
        birdSequence = new Sequencer(owner);

        //waypointNode = new WaypointState(owner);
        rocketFire = new RocketState(owner);
        floatyFire = new FloatyState(owner);
        sizeIncrease = new SizeState(owner);
        boostStart = new BoostState(owner);
        gumFire = new GumState(owner);
        birdFire = new PoopState(owner);

        rocketCheck = new IsRocket(owner);
        rangeCheck = new InRange(owner);
        rangeCheckBack = new InRangeReverse(owner);
        floatyCheck = new IsFloaty(owner);
        sizeCheck = new IsSize(owner);
        boostCheck = new IsBoost(owner);
        gumCheck = new IsGum(owner);
        birdCheck = new IsBird(owner);

        //linking nodes
        Root = rootSelector;

        rootSelector.AddNode(powerUpSelector);
        //rootSelector.AddNode(waypointNode);

        powerUpSelector.AddNode(rocketSequence);
        powerUpSelector.AddNode(floatySequence);
        powerUpSelector.AddNode(sizeSequence);
        powerUpSelector.AddNode(boostSequence);
        powerUpSelector.AddNode(gumSequence);
        powerUpSelector.AddNode(birdSequence);

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

        gumSequence.AddNode(gumCheck);
        gumSequence.AddNode(rangeCheckBack);
        gumSequence.AddNode(gumFire);

        birdSequence.AddNode(birdCheck);
        birdSequence.AddNode(birdFire);
    }
}
