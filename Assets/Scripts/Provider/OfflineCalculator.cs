using System;
using System.Linq;
using DefaultNamespace;
using Interfaces;
using UnityEngine;
using Util;
using Object = UnityEngine.Object;

namespace Provider
{
    public class OfflineCalculator
    {
        private float percentage = .6f;

        private DateTime savedTime;


        // In Seconds
        public int validOfflineTime = 0;

        private const string saveString = "OFFLINE_CALC";

        private KitchenController kitchenController;
        public RouteManager routeManager;

        private int amount;

        private bool initialized;
        private int offTime;

        private float hourlyReward;

        public OfflineCalculator()
        {
            string savedStartTime = PlayerPrefs.GetString(saveString, string.Empty);
            if (!string.IsNullOrEmpty(savedStartTime))
            {
                savedTime = DateTime.Parse(savedStartTime);
            }
            validOfflineTime = PlayerPrefsHelper.GetInt("OfflineTime", 3600);

            routeManager = Object.FindObjectOfType<RouteManager>();
        }

        public void SafeTime()
        {
            PlayerPrefs.SetString(saveString, DateTime.Now.ToString());
            PlayerPrefsHelper.SetInt("OfflineTime", validOfflineTime);
            PlayerPrefs.Save();
        }

        public void AddOfflineTime(int seconds)
        {
            if (seconds > 0)
                validOfflineTime += seconds;
            PlayerPrefsHelper.SetInt("OfflineTime", validOfflineTime);
            PlayerPrefs.Save();
        }

        public int CalculateOnlineAmountFor(int seconds)
        {
            return CalculateReward(seconds);
        }

        private int CalculateReward(int seconds)
        {
            hourlyReward = calculateHourlyReward();

            return (int)((float)seconds / 3600 * hourlyReward);
        }

        private void CalculateOfflineAmount()
        {
            var elapsedTime = DateTime.Now - savedTime;

            offTime = (int)elapsedTime.TotalSeconds;
            offTime = Mathf.Min(offTime, validOfflineTime);

            amount = CalculateReward(offTime);
            initialized = true;
        }

        public int GetOfflineTime()
        {
            return offTime;
        }

        public int GetOfflineAmount()
        {
            if (!initialized)
                CalculateOfflineAmount();
            return (int)(amount * percentage);
        }

        private float calculateHourlyReward()
        {
            float amount = 0;
            var soldiers = GameManager.INSTANCE.SoldierController.GetAllSoldiers();

            foreach (Soldier soldier in soldiers)
            {
                int soldierAmount = soldiers.Count(x => x.SoldierType == soldier.SoldierType);

                float singleRoundTrip = getNettoRunningTime(soldier)
                                        + getTimeEating(soldier, soldierAmount)
                                        + getTimePooing(soldier, soldierAmount)
                                        + getTimeSleeping(soldier, soldierAmount)
                                        + getTimeForMission(soldier, soldierAmount);
                float amountOfAllRoundTrips = (60 * 60) / singleRoundTrip;
                float moneyEarned = amountOfAllRoundTrips * getMissionMoney(soldier);
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
            return length / soldier.Speed;
        }

        private float getTimeForMission(Soldier soldier, int soldierAmount)
        {
            float time;
            int amount;
            IManageItem manager = GameManager.INSTANCE.GetTopLevel(new ObjectType
            {
                objectType = GenericObjectType.SOLDIER_SPEED
            }).GetItemManager(soldier.SoldierType);

            time = manager.GetAverageTime();
            amount = manager.GetAmountOfUnlockedItems();

            time += (1 - (soldierAmount / amount)) * time;
            return time;
        }


        private float getMissionMoney(Soldier soldier)
        {
            IManageItem manager = GameManager.INSTANCE.GetTopLevel(new ObjectType
            {
                objectType = GenericObjectType.SOLDIER_SPEED
            }).GetItemManager(soldier.SoldierType);

            int avg = 0;
            foreach (var managerItem in manager.Items)
            {
                if (managerItem.Level < 1) continue;

                var reward = (int)Calculator.INSTANCE.GetReward(managerItem.ObjectType.ToMoney(), managerItem.Level);
                reward *= 1 + soldier.LVL_Reward / 100;
                reward *= 1 + ((soldier.LVL_Crit / 2) / 100);
                avg += reward;
            }

            // /= UnlockedItems -> *= UnlockedItems 

            return avg;
        }
    }
}