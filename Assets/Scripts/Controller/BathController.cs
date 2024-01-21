using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class BathController : MonoBehaviour, IController
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

    public bool isObjectUnlocked(int i)
    {
        throw new System.NotImplementedException();
    }

    public int getLevelLevel(int index)
    {
        throw new NotImplementedException();
    }

    public int getTimeLevel(int index)
    {
        throw new NotImplementedException();
    }

    public void upgrade_Level(int index)
    {
        throw new NotImplementedException();
    }

    public void upgrade_Time(int index)
    {
        throw new NotImplementedException();
    }

    public void PlaceSoldier(Soldier soldier)
    {
        getRest(soldier.SoldierType).PlaceSoldier(soldier);
    }
    
    private Rest getRest(Soldier.SoldierTypeEnum type)
    {
        if (type == Soldier.SoldierTypeEnum.ARMY) return armyRest;
        if (type == Soldier.SoldierTypeEnum.AIRFORCE) return airForceRest;
        if (type == Soldier.SoldierTypeEnum.MARINE) return marineRest;

        throw new ArgumentException("not a valid type");
    }

    
}