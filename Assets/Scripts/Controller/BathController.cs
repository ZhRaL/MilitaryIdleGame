using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class BathController : MonoBehaviour
{
    [SerializeField] private Rest armyRest, airForceRest, marineRest;

    public int[] getState()
    {
        return new[]
        {
            armyRest.unlockedToilets, armyRest.speed, airForceRest.unlockedToilets, airForceRest.speed,
            marineRest.unlockedToilets, marineRest.speed
        };
    }

    public void loadState(int[] state)
    {
        if (state.Length != 6) throw new ArgumentException("Wrong Length of Array");

        armyRest.Init(state[0], state[1]);
        airForceRest.Init(state[2], state[3]);
        marineRest.Init(state[4], state[5]);
   

    }

    public IManageItem GetItemManager(DefenseType defenseType)
    {
        throw new NotImplementedException();
    }

    public void PlaceSoldier(Soldier soldier)
    {
        getRest(soldier.SoldierType).PlaceSoldier(soldier);
    }
    
    private Rest getRest(DefenseType type)
    {
        if (type == DefenseType.ARMY) return armyRest;
        if (type == DefenseType.AIRFORCE) return airForceRest;
        if (type == DefenseType.MARINE) return marineRest;

        throw new ArgumentException("not a valid type");
    }

    
}