using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Util;

public class DataProvider : MonoBehaviour
{
    public Table armyTable, airforceTable, marineTable;
    public Rest[] rests;
    public Room armyRoom, airforceRoom, marineRoom;



    public static DataProvider INSTANCE;

    private void Awake()
    {
        if (INSTANCE == null) INSTANCE = this;
    }

    public int GetLevel(DefenseType defType, ObjectType objType, int index)
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
                // TODO
            },
            ObjectType.BED => defType switch
            {   
                DefenseType.ARMY => armyRoom.GetLevelForTable(index),
                DefenseType.AIRFORCE => airforceRoom.GetLevelForTable(index),
                DefenseType.MARINE => marineRoom.GetLevelForTable(index)
            },
        };

    }
    
    
    public UnityAction getUpgradeMethod(DefenseType defType, ObjectType objType, int index)
    {
        return objType switch
        {
            ObjectType.CHAIR => defType switch
            {   
                DefenseType.ARMY => () => armyTable.UpgradeChair(index),
                DefenseType.AIRFORCE => () => airforceTable.UpgradeChair(index),
                DefenseType.MARINE => () => marineTable.UpgradeChair(index),
            }
        };
    }


}
