﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Utility;

public class RoutingPoint : MonoBehaviour
{

    public UnityEvent<Soldier> act;

    public bool waitAfterAction;

    public List<Soldier> waitingSoldiers;

    public GameObject radialBar_Prefab;
    public void DoAction(Soldier outcome)
    {
        // TODO - check for: Nothing to do here
        
        // Theres is still a queue
        if (waitingSoldiers.Count > 0)
        {
            waitingSoldiers.Add(outcome);
            return;
        }
        
        waitingSoldiers.Add(outcome);
        ExecuteAction(outcome);
    }
    
    private void ExecuteAction(Soldier soldier)
    {
        act?.Invoke(soldier);
        if (waitAfterAction) return;
        waitingSoldiers.First().ActionDone();
        waitingSoldiers.RemoveAt(0);
    }

    public void LetSoldierMove()
    {
        waitingSoldiers.FirstOrDefault()?.StartNextRun();
        waitingSoldiers.RemoveAt(0);

        if (waitingSoldiers.Count > 0)
        {
            ExecuteAction(waitingSoldiers.First());
        }        
    }

    public void startRadialBar()
    {
        var rb = Instantiate(radialBar_Prefab, waitingSoldiers.First().transform);
        rb.GetComponent<RadialBar>().Initialize(3,end);
    }

    public void end()
    {
        Debug.Log("finished");
    }
}
