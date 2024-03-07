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
    public Rest armyRest, airforceRest, marineRest;
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
                DefenseType.ARMY => armyTable.GetLevelForItem(index),
                DefenseType.AIRFORCE => airforceTable.GetLevelForItem(index),
                DefenseType.MARINE => marineTable.GetLevelForItem(index)
            },
            ObjectType.BED => defType switch
            {   
                DefenseType.ARMY => armyRoom.GetLevelForItem(index),
                DefenseType.AIRFORCE => airforceRoom.GetLevelForItem(index),
                DefenseType.MARINE => marineRoom.GetLevelForItem(index)
            },
            ObjectType.TOILET => defType switch
            {   
                DefenseType.ARMY => armyRest.GetLevelForItem(index),
                DefenseType.AIRFORCE => airforceRest.GetLevelForItem(index),
                DefenseType.MARINE => marineRest.GetLevelForItem(index)
            },
            
            ObjectType.JET_AMOUNT => airForceController.getLevelLevel(index),
            ObjectType.JET_TIME => airForceController.getTimeLevel(index),
            
            ObjectType.SHIP_AMOUNT => marineController.getLevelLevel(index),
            ObjectType.SHIP_TIME => marineController.getTimeLevel(index),
            
            ObjectType.TANK_AMOUNT => armyController.getLevelLevel(index),
            ObjectType.TANK_TIME => armyController.getTimeLevel(index),
            

        };

    }
    
    
    public UnityAction getUpgradeMethod(DefenseType defType, ObjectType objType, int index)
    {
        return objType switch
        {
            ObjectType.CHAIR => defType switch
            {   
                DefenseType.ARMY => () => armyTable.GetUpgradeMethod(index),
                DefenseType.AIRFORCE => () => airforceTable.GetUpgradeMethod(index),
                DefenseType.MARINE => () => marineTable.GetUpgradeMethod(index),
            },
            ObjectType.BED => defType switch
            {   
                DefenseType.ARMY => () => armyRoom.GetUpgradeMethod(index),
                DefenseType.AIRFORCE => () => airforceRoom.GetUpgradeMethod(index),
                DefenseType.MARINE => () => marineRoom.GetUpgradeMethod(index),
            },
            ObjectType.TOILET => defType switch
            {   
                DefenseType.ARMY => () => armyRest.GetUpgradeMethod(index),
                DefenseType.AIRFORCE => () => airforceRest.GetUpgradeMethod(index),
                DefenseType.MARINE => () => marineRest.GetUpgradeMethod(index),
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
