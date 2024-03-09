using System;
using System.Collections.Generic;
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

            for (int i = 0; i < Items.Count; i+=2)
            {
                int timeLevel = levels[i];
                int moneyLevel = levels[i + 1];
                
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
                moveSoldierTo(soldier, targetItem.transform, () => targetItem.SoldierSitDown(soldier));
            }
            else
            {
                TheWaitingService.addSoldier(soldier);
            }
        }
    }
}