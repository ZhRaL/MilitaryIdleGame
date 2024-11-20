using System;
using System.Collections;
using System.Collections.Generic;
using Provider;
using UnityEngine;
using Util;

public class StatisticsManager
{
    private OfflineCalculator _calc;

    public StatisticsManager(OfflineCalculator calc)
    {
        _calc = calc;
        // Init
        _calc.Init();
    }

    public float GetCurrentValues(ObjectType type)
    {
        StatisticsDto dto = type.defenseType switch
        {
            DefenseType.ARMY => _calc.statistic_ARMY,
            DefenseType.AIRFORCE => _calc.statistic_AIRFORCE,
            DefenseType.MARINE => _calc.statistic_MARINE,
            _ => throw new ArgumentException("not a valid Enum!")

        };
        
        return type.objectType switch
        {
            GenericObjectType.BATH => dto.PooTime,
            GenericObjectType.KITCHEN => dto.EatingTime,
            GenericObjectType.SLEEPING => dto.SleepingTime,
            GenericObjectType.SHIP_TIME => dto.MissionTime, 
            GenericObjectType.JET_TIME => dto.MissionTime, 
            GenericObjectType.TANK_TIME => dto.MissionTime,
            _ => throw new ArgumentException("Cannot handle this ObjectType!")
        };
    }

    public float GetMaxValues(ObjectType type, int level)
    {
        var missionDuration = Calculator.INSTANCE.GetReward(type, level);
        return 3600 / missionDuration;
    }
    
}