using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Util;

public class KitchenController : MonoBehaviour, IController
{
    [SerializeField] private Table _tableArmy, _tableAirForce, _tableMarine;
    
    public IManageItem ArmyManager => _tableArmy;
    public IManageItem AirforceManager => _tableAirForce;
    public IManageItem MarineManager => _tableMarine;
    
    
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
}