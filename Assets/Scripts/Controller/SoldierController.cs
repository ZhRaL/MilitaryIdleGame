using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Util;

[Serializable]
public class SoldierController : MonoBehaviour
{
    public static SoldierController INSTANCE;

    private void Awake()
    {
        INSTANCE = this;
    }

    public Platoon Army, Airf, Marine;

    public Soldier[] GetAllSoldiersFrom(DefenseType type)
    {
        return type switch
        {
            DefenseType.AIRFORCE => Airf.Soldiers.ToArray(),
            DefenseType.MARINE => Marine.Soldiers.ToArray(),
            DefenseType.ARMY => Army.Soldiers.ToArray(),
        };
    }

    public Platoon GetPlatoon(DefenseType type)
    {
        return type switch
        {
            DefenseType.ARMY => Army,
            DefenseType.AIRFORCE => Airf,
            DefenseType.MARINE => Marine
        };
    }
    
    public JsonController<SoldierItemJO> Save()
    {
        JsonController<SoldierItemJO> contr = new JsonController<SoldierItemJO>();
        contr.AddManager(Army.Save());
        contr.AddManager(Airf.Save());
        contr.AddManager(Marine.Save());
        return contr;
    }
    
    public void Load(JsonController<SoldierItemJO> state)
    {
        if (state == null)
            state = JsonController<SoldierItemJO>.Default(new SoldierItemJO());
        
        Army.Load(state.GetAt(0));
        Airf.Load(state.GetAt(1));
        Marine.Load(state.GetAt(2));

    }
    
}
