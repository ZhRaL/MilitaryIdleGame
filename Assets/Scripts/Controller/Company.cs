using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Company : IManageItem
    {
        [SerializeField] private List<Vehicle> vehicles;

        public override List<Item> Items => new(vehicles);

        public UnityAction GetMoneyUpgradeMethod(int index)
        {
            return vehicles[index].MoneyUpgrade;
        }

        public override void Init(int[] levels)
        {
            if (levels.Length > 2 * Items.Count)
                throw new ArgumentException("invalid Amount!");

            for (int i = 0; i < Items.Count; i++)
            {
                int timeLevel = levels[2*i];
                int moneyLevel = levels[2*i + 1];
                
                Item item = Items[i];
                if (timeLevel > 0 && item.isMissionItem())
                {
                    item.Unlocked = true;
                    item.Parent = this;
                    
                    MissionItem missionItem = (MissionItem) item;

                    item.Level = timeLevel;
                    missionItem.MoneyLevel = moneyLevel;
                }
                else
                {
                    // TODO - Add Baustellen Prefab
                    item.gameObject.SetActive(false);
                }
            }
        }
        
        public override void PlaceSoldier(Soldier soldier)
        {
            // TODO - hie rweiter machen
            MissionItem targetItem = (MissionItem) GetNextFreeItem();
            if (targetItem != null)
            {
                targetItem.Occupied = true;
                WalkingSoldiers.Add(
                    new SoldierWalkUtil(soldier, null, () => targetItem.SoldierSitDown(soldier), 
                        RemoveWalkingSoldier, .2f, targetItem.Waypoints.GetAllChildren()));

            }
            else
            {
                TheWaitingService.addSoldier(soldier);
            }
        }
        
        public override int[] GetState()
        {
            return vehicles.Select(tank => new[] { tank.Level, tank.MoneyLevel }).SelectMany(arr => arr).ToArray();
        }
    }
}