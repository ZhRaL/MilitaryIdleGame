using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class BathController : MonoBehaviour, IController
{
    [SerializeField] private Rest armyRest, airForceRest, marineRest;

    public IManageItem ArmyManager => armyRest;
    public IManageItem AirforceManager => airForceRest;
    public IManageItem MarineManager => marineRest;
}