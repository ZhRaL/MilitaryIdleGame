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
    private void Awake()
    {
        INSTANCE = this;
    }

    public Soldier[] GetAllSoldiersFrom(DefenseType type)
    {
        return type switch
        {
            DefenseType.AIRFORCE => Airf.Soldiers.ToArray(),
            DefenseType.MARINE => Marine.Soldiers.ToArray(),
            DefenseType.ARMY => Army.Soldiers.ToArray(),
        };
    }
    
    public JsonController Save()
    {
        JsonController contr = new JsonController();
        contr.AddManager(Army.Save());
        contr.AddManager(Airf.Save());
        contr.AddManager(Marine.Save());
        return contr;
    }
    
    public void Load(JsonController state)
    {
        if (state == null)
            state = new JsonController();
        
        Army.Load(state.GetAt(0));
        Airf.Load(state.GetAt(1));
        Marine.Load(state.GetAt(2));

    }
    
}
