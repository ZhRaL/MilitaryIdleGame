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
}