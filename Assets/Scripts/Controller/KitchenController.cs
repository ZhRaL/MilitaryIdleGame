using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class KitchenController : MonoBehaviour, IController
{
    [SerializeField] private Table _tableArmy, _tableAirForce, _tableMarine;
    
    public IManageItem ArmyManager => _tableArmy;
    public IManageItem AirforceManager => _tableAirForce;
    public IManageItem MarineManager => _tableMarine;
    
    public int[] getState()
    {
        return _tableArmy.GetState()
            .Concat(_tableAirForce.GetState())
            .Concat(_tableMarine.GetState())
            .ToArray();
    }

    public void loadState(int[] state)
    {
        if (state.Length != 12) 
            throw new ArgumentException("Wrong Length of Array");

        _tableArmy.Init(state[..4]);
        _tableAirForce.Init(state[4..8]);
        _tableMarine.Init(state[8..12]);
    }
}