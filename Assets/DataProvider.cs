using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Util;

public class DataProvider : MonoBehaviour
{
    public Table armyTable, airforceTable, marineTable;
    public Rest armyRest, airforceRest, marineRest;


    public static DataProvider INSTANCE;

    private void Awake()
    {
        if (INSTANCE == null) INSTANCE = this;
    }

    public int getLevel(DefenseType defType, ObjectType objType, int index)
    {
        return objType switch
        {
            ObjectType.CHAIR => defType switch
            {   
                DefenseType.ARMY => armyTable.GetLevelForTable(index),
                DefenseType.AIRFORCE => airforceTable.GetLevelForTable(index),
                DefenseType.MARINE => marineTable.GetLevelForTable(index)
            },
            ObjectType.TOILET => defType switch
            {   
                DefenseType.ARMY => armyTable.GetLevelForTable(index),
                DefenseType.AIRFORCE => airforceTable.GetLevelForTable(index),
                DefenseType.MARINE => marineTable.GetLevelForTable(index)
            },
        };

    }
    
    
    public UnityAction getUpgradeMethod(DefenseType defType, ObjectType objType, int index)
    {
        return objType switch
        {
            ObjectType.CHAIR => defType switch
            {   
                DefenseType.ARMY => () => armyTable.UpgradeChair(index)
            }
        };
    }


}
