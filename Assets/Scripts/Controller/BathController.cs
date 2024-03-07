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
    
    public int[] getState()
    {
        return armyRest.GetState()
            .Concat(airForceRest.GetState())
            .Concat(marineRest.GetState())
            .ToArray();
    }

    public void loadState(int[] state)
    {
        if (state.Length != 9) 
            throw new ArgumentException("Wrong Length of Array");

        armyRest.Init(state[..3]);
        airForceRest.Init(state[3..6]);
        marineRest.Init(state[6..9]);
    }
}