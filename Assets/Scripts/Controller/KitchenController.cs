using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Util;

public class KitchenController : MonoBehaviour, IController
{
    [SerializeField] private Table _tableArmy, _tableAirForce, _tableMarine;
    
    /**
     * State: (ArmyCount,ArmyLevel,AirFCount,AirFLevel,MarCount,MarLevel)
     */
    public int[] getState()
    {
        return _tableArmy.getState()
            .Concat(_tableAirForce.getState())
            .Concat(_tableMarine.getState())
            .ToArray();
    }

    public void loadState(int[] state)
    {
        if (state.Length != 12) throw new ArgumentException("Wrong Length of Array");

        _tableArmy.Init(state[..4]);
        _tableAirForce.Init(state[4..8]);
        _tableMarine.Init(state[8..12]);
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
        getTable(soldier.SoldierType).PlaceSoldier(soldier);
    }
    
    private Table getTable(Soldier.SoldierTypeEnum type)
    {
        if (type == Soldier.SoldierTypeEnum.ARMY) return _tableArmy;
        if (type == Soldier.SoldierTypeEnum.AIRFORCE) return _tableAirForce;
        if (type == Soldier.SoldierTypeEnum.MARINE) return _tableMarine;

        throw new ArgumentException("not a valid type");
    }
}