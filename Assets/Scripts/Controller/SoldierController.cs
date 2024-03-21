using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

public class SoldierController : MonoBehaviour
{
    public static SoldierController INSTANCE;

    public Platoon Army, Airf, Marine;
    private Platoon currentPlatoon;
    private void Awake()
    {
        INSTANCE = this;
    }
// Test
    public Soldier[] GetAllSoldiersFrom(DefenseType type)
    {
        return type switch
        {
            DefenseType.AIRFORCE => Airf.Soldiers.ToArray(),
            DefenseType.MARINE => Marine.Soldiers.ToArray(),
            DefenseType.ARMY => Army.Soldiers.ToArray(),
        };
    }
    
    public int[] getState()
    {
        currentPlatoon = Army;
        
        return Army.GetState()
            .Concat(new []{-1,-1,-1})
            .Concat(Airf.GetState())
            .Concat(new []{-1,-1,-1})
            .Concat(Marine.GetState())
            .ToArray();
    }

    public void loadState(int[] state)
    {
        if (state.Length % 3 != 0)
            throw new ArgumentException("Wrong Length, was " + state.Length);

        currentPlatoon = Army;
        for (int i = 0; i < state.Length; i+=3)
        {
            Tuple<int, int, int> triple = new Tuple<int, int, int>(state[i], state[i + 1], state[i + 2]);
            if (triple.Item1 == -1 && triple.Item2 == -1 && triple.Item3 == -1)
            {
                NextPlatoon();
                continue;
            }

            currentPlatoon.createSoldier(triple.Item1,triple.Item2,triple.Item3);

        }
        
    }

    private void NextPlatoon()
    {
        if (currentPlatoon == Airf) currentPlatoon = Marine;
        if (currentPlatoon == Army) currentPlatoon = Airf;
    }
    
}
