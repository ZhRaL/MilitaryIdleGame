using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces;
using UnityEditor;
using UnityEngine;
using Util;

public class OfflineCalculator
{
    private float percentage = .6f;
    
    private DateTime  savedTime;
    
    public float validOfflineTime = 60 * 60;
    
    private const string saveString = "OFFLINE_CALC";

    public KitchenController kitchenController;
    public RouteManager routeManager;
    
    public OfflineCalculator()
    {
        string savedStartTime = PlayerPrefs.GetString(saveString, string.Empty);
        if (!string.IsNullOrEmpty(savedStartTime))
        {
            savedTime = DateTime.Parse(savedStartTime);
        }
    }
    
    public void safeTime()
    {
        logger.log("I saved: "+DateTime.Now);
        PlayerPrefs.SetString(saveString, DateTime.Now.ToString());
        PlayerPrefs.Save(); // PlayerPrefs speichern (wichtig!)
    }

    public void calculateReward()
    {
        TimeSpan elapsedTime = DateTime.Now - savedTime;

        int diff  = (int) elapsedTime.TotalSeconds;

        float hourlyReward = calculate();

    }

    private float calculate()
    {
        float amount = 0;
        // TODO
      /*  List<Soldier> soldiers = recruitmentController.GetSoldiers();
        
        foreach (Soldier soldier in soldiers)
        {
            int soldierAmount = recruitmentController.getSoldierTypeAmount(soldier.SoldierType);
            float roundTrip = getNettoRunningTime(soldier, soldierAmount)
                              + getTimeEating(soldier, soldierAmount)
                              + getTimePooing(soldier, soldierAmount)
                              + getTimeSleeping(soldier, soldierAmount)
                              + getTimeForMission(soldier, soldierAmount);
            float numberRT = (60 * 60) / roundTrip;
            float moneyEarned = numberRT * getMissionMoney(soldier);
            amount += moneyEarned;
        }
        */

        return amount;
    }

    private float getTimeEating(Soldier soldier, int soldierAmount)
    {
        IManageItem table = kitchenController.GetItemManager(soldier.SoldierType);
        var eZ = table.GetAverageTime();
        eZ += (1 - (soldierAmount / table.GetAmountOfUnlockedItems())) * eZ;
        return eZ;
    }
    
    private float getTimePooing(Soldier soldier, int soldierAmount)
    {
        IManageItem table = kitchenController.GetItemManager(soldier.SoldierType);
        var eZ = table.GetAverageTime();
        eZ += (1 - (soldierAmount / table.GetAmountOfUnlockedItems())) * eZ;
        return eZ;
    }
    
    private float getTimeSleeping(Soldier soldier, int soldierAmount)
    {
        IManageItem table = kitchenController.GetItemManager(soldier.SoldierType);
        var eZ = table.GetAverageTime();
        eZ += (1 - (soldierAmount / table.GetAmountOfUnlockedItems())) * eZ;
        return eZ;
    }

    private float getNettoRunningTime(Soldier soldier, int soldierAmount)
    {
        return routeManager.getRouteLength(soldier.SoldierType);
    }

    private float getTimeForMission(Soldier soldier, int soldierAmount)
    {
        float time;
        int amount;
        switch (soldier.SoldierType)
        {
       //     case DefenseType.ARMY:
       //         time = armyController.getAverageTime();
       //         amount = armyController.unlockedVehics();
       //         break;
       //     case DefenseType.MARINE:
       //         time = marineController.getAverageTime();
       //         amount = marineController.unlockedVehics();
       //         break;
       //     case DefenseType.AIRFORCE:
       //         time = airforceController.getAverageTime();
       //         amount = airforceController.unlockedVehics();
       //         break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        time += (1 - (soldierAmount / amount)) * time;
        return time;
    }
    

    private float getMissionMoney(Soldier soldier)
    {
        // Calculate average of all active vehicles and multiply it with amount of active vehicles
        return 0;
    }
}
