using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using Util;

public class DataProvider : MonoBehaviour
{
    public Table armyTable, airforceTable, marineTable;
    public Rest rest1,rest2,rest3,rest4;
    public Room armyRoom, airforceRoom, marineRoom;

    public ArmyController armyController;
    public AirforceController airForceController;
    public MarineController marineController;
    public BathController BathController;


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
            ObjectType.BED => defType switch
            {   
                DefenseType.ARMY => armyRoom.GetLevelForBed(index),
                DefenseType.AIRFORCE => airforceRoom.GetLevelForBed(index),
                DefenseType.MARINE => marineRoom.GetLevelForBed(index)
            },
            
            ObjectType.JET_AMOUNT => airForceController.getLevelLevel(index),
            ObjectType.JET_TIME => airForceController.getTimeLevel(index),
            
            ObjectType.SHIP_AMOUNT => marineController.getLevelLevel(index),
            ObjectType.SHIP_TIME => marineController.getTimeLevel(index),
            
            ObjectType.TANK_AMOUNT => armyController.getLevelLevel(index),
            ObjectType.TANK_TIME => armyController.getTimeLevel(index),
            
            ObjectType.TOILET => BathController.getLevelLevel(index)
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
            },
            ObjectType.BED => defType switch
            {   
                DefenseType.ARMY => () => armyRoom.UpgradeBed(index),
                DefenseType.AIRFORCE => () => airforceRoom.UpgradeBed(index),
                DefenseType.MARINE => () => marineRoom.UpgradeBed(index),
            },

            ObjectType.SHIP_AMOUNT => () => marineController.upgrade_Level(index),
            ObjectType.SHIP_TIME => () => marineController.upgrade_Time(index),

            ObjectType.TANK_AMOUNT => () => armyController.upgrade_Level(index),
            ObjectType.TANK_TIME => () => armyController.upgrade_Time(index),

            ObjectType.JET_AMOUNT => () => airForceController.upgrade_Level(index),
            ObjectType.JET_TIME => () => airForceController.upgrade_Time(index),

            
        };
    }


}
