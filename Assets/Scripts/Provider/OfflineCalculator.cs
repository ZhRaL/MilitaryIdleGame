using System;
using System.Linq;
using DefaultNamespace;
using Interfaces;
using UnityEngine;
using Util;

namespace Provider
{
    public class OfflineCalculator
    {
        private float percentage = .6f;

        private DateTime savedTime;

        // In Seconds
        public int validOfflineTime = 60 * 60;

        private const string saveString = "OFFLINE_CALC";

        private KitchenController kitchenController;
        public RouteManager routeManager;

        private int amount;

        private bool initialized;

        public OfflineCalculator()
        {
            string savedStartTime = PlayerPrefs.GetString(saveString, string.Empty);
            if (!string.IsNullOrEmpty(savedStartTime))
            {
                savedTime = DateTime.Parse(savedStartTime);
            }
        }

        public void SafeTime()
        {
            PlayerPrefs.SetString(saveString, DateTime.Now.ToString());
            PlayerPrefs.Save();
        }

        private void CalculateReward()
        {
            TimeSpan elapsedTime = DateTime.Now - savedTime;

            int diff = (int)elapsedTime.TotalSeconds;
            diff = Mathf.Min(diff, validOfflineTime);

            float hourlyReward = calculate();

            amount = (int) ((diff / 3600) * hourlyReward);
            initialized = true;
        }

        public int GetOfflineAmount()
        {
            if (!initialized)
                CalculateReward();
            return amount;
        }

        private float calculate()
        {
            float amount = 0;
            var soldiers = GameManager.INSTANCE.SoldierController.GetAllSoldiers();

            foreach (Soldier soldier in soldiers)
            {
                int soldierAmount = soldiers.Count(x => x.SoldierType == soldier.SoldierType);
              
                float roundTrip = getNettoRunningTime(soldier)
                                  + getTimeEating(soldier, soldierAmount)
                                  + getTimePooing(soldier, soldierAmount)
                                  + getTimeSleeping(soldier, soldierAmount)
                                  + getTimeForMission(soldier, soldierAmount);
                float numberRT = (60 * 60) / roundTrip;
                float moneyEarned = numberRT * getMissionMoney(soldier);
                amount += moneyEarned;
            }
          

            return amount;
        }

        private float getTimeEating(Soldier soldier, int soldierAmount)
        {
            IManageItem manager = GameManager.INSTANCE.GetTopLevel(ObjectType.Kit).GetItemManager(soldier.SoldierType);
            
            var eZ = manager.GetAverageTime();
            eZ += (1 - (soldierAmount / manager.GetAmountOfUnlockedItems())) * eZ;
            return eZ;
        }

        private float getTimePooing(Soldier soldier, int soldierAmount)
        {
            IManageItem manager = GameManager.INSTANCE.GetTopLevel(ObjectType.Bat).GetItemManager(soldier.SoldierType);
            
            var eZ = manager.GetAverageTime();
            eZ += (1 - (soldierAmount / manager.GetAmountOfUnlockedItems())) * eZ;
            return eZ;
        }

        private float getTimeSleeping(Soldier soldier, int soldierAmount)
        {
            IManageItem manager = GameManager.INSTANCE.GetTopLevel(ObjectType.Sle).GetItemManager(soldier.SoldierType);
            
            var eZ = manager.GetAverageTime();
            eZ += (1 - (soldierAmount / manager.GetAmountOfUnlockedItems())) * eZ;
            return eZ;
        }

        private float getNettoRunningTime(Soldier soldier)
        {
            var length = routeManager.getRouteLength(soldier.SoldierType);
            // TODO involve soldier.movementSpeed
            return length;
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
}