using System;
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



    public void DoAction(Soldier outcome)
    {
        ExecuteAction(outcome);
    }
    
    private void ExecuteAction(Soldier soldier)
    {
        act?.Invoke(soldier);
        if (waitAfterAction) return;
        soldier.StartNextRun();
    }
    
    public void LetSoldierMove(Soldier soldier)
    {
        soldier.StartNextRun();
    }
}
