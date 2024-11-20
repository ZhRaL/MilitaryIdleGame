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

        public StatisticsDto statistic_ARMY = new(), statistic_AIRFORCE = new(), statistic_MARINE=new();

        public OfflineCalculator()
        {
            string savedStartTime = PlayerPrefsHelper.GetString(saveString, string.Empty);
            if (!string.IsNullOrEmpty(savedStartTime))
            {
                savedTime = DateTime.Parse(savedStartTime);
            }
            validOfflineTime = PlayerPrefsHelper.GetInt("OfflineTime", 3600);

            routeManager = Object.FindObjectOfType<RouteManager>();

        }

        public void SafeTime()
        {
            PlayerPrefsHelper.SetString(saveString, DateTime.Now.ToString());
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
            CalculateForBranc(DefenseType.ARMY, statistic_ARMY);
            CalculateForBranc(DefenseType.AIRFORCE, statistic_AIRFORCE);
            CalculateForBranc(DefenseType.MARINE, statistic_MARINE);
            
            return statistic_ARMY.GetHourlyReward() + statistic_AIRFORCE.GetHourlyReward() + statistic_MARINE.GetHourlyReward();
        }

        private void CalculateForBranc(DefenseType currentType, StatisticsDto current)
        {
            var soldiers = GameManager.INSTANCE.SoldierController.GetAllSoldiersFrom(currentType);
            Soldier solwSoldier = soldiers.First();
            current.NettoRunTime = getNettoRunningTime(solwSoldier,soldiers.Average(e => e.Speed));
            current.EatingTime = getTimeEating(solwSoldier, soldiers.Length);
            current.PooTime = getTimePooing(solwSoldier, soldiers.Length);
            current.SleepingTime = getTimeSleeping(solwSoldier, soldiers.Length);
            current.MissionTime = getTimeForMission(solwSoldier, soldiers.Length);
            current.Soldier_Amount = soldiers.Length;
            current.Soldier_AvgCrit = (float) soldiers.Average(e => e.LVL_Crit);
            current.Soldier_AvgMissionMultiplier = (float)soldiers.Average(e => e.LVL_Reward);
            current.AvgMissionMoney = GameManager.INSTANCE.GetTopLevel(new ObjectType
            {
                defenseType = DefenseType.ARMY
            }).GetItemManager(currentType).GetAverageMissionMoney();
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

        private float getNettoRunningTime(Soldier soldier, float avgSpeed)
        {
            var length = routeManager.getRouteLength(soldier.SoldierType);
            return length / avgSpeed;
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

            var wholDuration = (soldierAmount / amount) * time;
            return wholDuration;
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

        public void Init()
        {
            calculateHourlyReward();
        }
    }

    public class StatisticsDto
    {
        public float NettoRunTime;
        public float EatingTime;
        public float PooTime;
        public float SleepingTime;
        public float MissionTime;
        public float Soldier_AvgCrit;
        public float Soldier_AvgMissionMultiplier;
        public float Soldier_Amount;
        public float AvgMissionMoney;

        public float GetRoundTurnTime()
        {
            return NettoRunTime + EatingTime + PooTime + SleepingTime + MissionTime;
        }

        public float GetHourlyReward()
        {
            var runCount = 3600 / GetRoundTurnTime();
            float CritChance = .2f;
            var expectedValue = AvgMissionMoney * (1 - CritChance) + (AvgMissionMoney * CritChance * 2);
            return runCount * expectedValue;
        }
    }
}