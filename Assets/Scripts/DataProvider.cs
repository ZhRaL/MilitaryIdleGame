using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using Util;

[Serializable]
public class DataProvider 
{
    private Table armyTable, airforceTable, marineTable;
    private Rest armyRest, airforceRest, marineRest;
    private Room armyRoom, airforceRoom, marineRoom;
    
    private ArmyController armyController;
    private AirforceController airForceController;
    private MarineController marineController;
    private BathController BathController;
    public IconProvider IconProvider;
    
    public static DataProvider INSTANCE => GameManager.INSTANCE.DataProvider;

    public int GetLevel(DefenseType defType, ObjectType objType, int index)
    {
        return objType.objectType switch
        {
            GenericObjectType.KITCHEN => defType switch
            {   
                DefenseType.ARMY => armyTable.GetLevelForItem(index),
                DefenseType.AIRFORCE => airforceTable.GetLevelForItem(index),
                DefenseType.MARINE => marineTable.GetLevelForItem(index)
            },
            GenericObjectType.SLEEPING => defType switch
            {   
                DefenseType.ARMY => armyRoom.GetLevelForItem(index),
                DefenseType.AIRFORCE => airforceRoom.GetLevelForItem(index),
                DefenseType.MARINE => marineRoom.GetLevelForItem(index)
            },
            GenericObjectType.BATH => defType switch
            {   
                DefenseType.ARMY => armyRest.GetLevelForItem(index),
                DefenseType.AIRFORCE => airforceRest.GetLevelForItem(index),
                DefenseType.MARINE => marineRest.GetLevelForItem(index)
            },
            
// TODO           

        };

    }
    
    public UnityAction getUpgradeMethod(DefenseType defType, ObjectType objType, int index)
    {
        return objType.objectType switch
        {
            GenericObjectType.KITCHEN => defType switch
            {   
                DefenseType.ARMY => () => armyTable.GetUpgradeMethod(index),
                DefenseType.AIRFORCE => () => airforceTable.GetUpgradeMethod(index),
                DefenseType.MARINE => () => marineTable.GetUpgradeMethod(index),
            },
            GenericObjectType.SLEEPING => defType switch
            {   
                DefenseType.ARMY => () => armyRoom.GetUpgradeMethod(index),
                DefenseType.AIRFORCE => () => airforceRoom.GetUpgradeMethod(index),
                DefenseType.MARINE => () => marineRoom.GetUpgradeMethod(index),
            },
            GenericObjectType.BATH => defType switch
            {   
                DefenseType.ARMY => () => armyRest.GetUpgradeMethod(index),
                DefenseType.AIRFORCE => () => airforceRest.GetUpgradeMethod(index),
                DefenseType.MARINE => () => marineRest.GetUpgradeMethod(index),
            },
            
            // TODO - Add Vehics
            
        };
    }

    public int GetMoneyLevel(DefenseType defType, ObjectType objType, int index)
    {
        return -1;
    }
    
    public int GetReward(DefenseType defType, ObjectType objType, int index)
    {
        return -1;
    }
    
    public int GetRewardDiff(DefenseType defType, ObjectType objType, int index)
    {
        return -1;
    }
    
    public int GetCost(DefenseType defType, ObjectType objType, int index)
    {
        return -1;
    }
    
    public int GetTimeReduction(DefenseType defType, ObjectType objType, int index)
    {
        return -1;
    }

    public int GetCost(Item item)
    {
        return GetCost(item.ObjectType.defenseType, item.ObjectType, item.Index);
    }
    
    public int GetReward(Item item)
    {
        return GetReward(item.ObjectType.defenseType, item.ObjectType, item.Index);
    }
    
    public int GetRewardDiff(Item item)
    {
        return GetRewardDiff(item.ObjectType.defenseType, item.ObjectType, item.Index);
    }
    

    
}
