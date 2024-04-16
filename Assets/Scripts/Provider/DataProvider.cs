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
    

    public IconProvider IconProvider;
    
    public static DataProvider INSTANCE => GameManager.INSTANCE.DataProvider;

    // Obsolete
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
    
    
    
    // correct Methods!
    private int GetReward(ObjectType objType, int level)
    {
        var value = Calculator.INSTANCE.GetReward(objType, level);
        return (int) value;
    }
    
    private int GetRewardDiff(ObjectType objType, int level)
    {
        var value = Calculator.INSTANCE.GetRewardDiff(objType, level);
        return (int) value;
    }
    
    private int GetCost(ObjectType objType, int level)
    {
        var value = Calculator.INSTANCE.GetCost(objType, level);
        return (int) value;
    }
    
    // Item Upgrades
    public int GetCost(Item item)
    {
        return GetCost(item.ObjectType, item.Level);
    }
    
    public int GetReward(Item item)
    {
        return GetReward(item.ObjectType, item.Level);
    }
    
    public int GetRewardDiff(Item item)
    {
        return GetRewardDiff(item.ObjectType, item.Level);
    }
    
    
    // Mission Upgrades
    public int GetCost(Item item, bool isMoney)
    {
        if (item is MissionItem mi)
        {
            ObjectType type = Convert(item.ObjectType,isMoney) ;
            return GetCost(type, isMoney ? mi.MoneyLevel : mi.Level);
        }
        return GetCost(item);
    }
    
    public int GetReward(Item item, bool isMoney)
    {
        if (item is MissionItem mi)
        {
            ObjectType type = Convert(item.ObjectType,isMoney) ;
            return GetReward(type, isMoney ? mi.MoneyLevel : mi.Level);
        }
        return GetReward(item);
    }
    
    public int GetRewardDiff(Item item, bool isMoney)
    {
        if (item is MissionItem mi)
        {
            ObjectType type = Convert(item.ObjectType,isMoney) ;
            return GetRewardDiff(type, isMoney ? mi.MoneyLevel : mi.Level);
        }
        return GetRewardDiff(item);
    }
    
    // Soldier Upgrades
    
    // TODO
    
    private ObjectType Convert(ObjectType type, bool isMoney)
    {
        switch (type.objectType)
        {
            case GenericObjectType.JET_MONEY:
            case GenericObjectType.JET_TIME:
                return new ObjectType() { objectType = isMoney ? GenericObjectType.JET_MONEY : GenericObjectType.JET_TIME };
            case GenericObjectType.TANK_MONEY:
            case GenericObjectType.TANK_TIME:
                return new ObjectType() { objectType = isMoney ? GenericObjectType.TANK_MONEY : GenericObjectType.TANK_TIME };
            case GenericObjectType.SHIP_MONEY:
            case GenericObjectType.SHIP_TIME:
                return new ObjectType() { objectType = isMoney ? GenericObjectType.SHIP_MONEY : GenericObjectType.SHIP_TIME };
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
