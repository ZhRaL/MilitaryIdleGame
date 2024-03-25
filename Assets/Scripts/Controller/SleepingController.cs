using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class SleepingController : MonoBehaviour, IController
{
    [SerializeField] private Room roomArmy, roomAirF, roomMar;

    public IManageItem ArmyManager => roomArmy;
    public IManageItem AirforceManager => roomAirF;
    public IManageItem MarineManager => roomMar;
    
}