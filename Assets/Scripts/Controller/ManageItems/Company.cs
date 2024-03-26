using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Util;

namespace DefaultNamespace
{
    public class Company : IManageItem
    {
        [SerializeField] private List<Vehicle> vehicles;

        public override List<Item> Items => new(vehicles);

        public override void PlaceSoldier(Soldier soldier)
        {
            // TODO - hie rweiter machen
            MissionItem targetItem = (MissionItem)GetNextFreeItem();
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

        public override void ItemIsFree()
        {
            Soldier freeS = TheWaitingService.Shift();
            if (freeS != null) PlaceSoldier(freeS);
        }

        public override void Load(JsonManageItem levels)
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                // Hier noch null dan nwird auch neues MissionItem gemacht, unterbinden durch null abfrage vorhers
                MissionItemJO jo = levels.GetIndex(i) as MissionItemJO ?? new MissionItemJO();
                MissionItem item = (MissionItem)Items[i];
                
                if (jo != null)
                {
                    item.Unlocked = true;
                    item.Parent = this;
                    item.Level = jo.Level;
                    item.MoneyLevel = jo.MoneyLevel;
                    continue;
                }

                {
                    // TODO - Add Baustellen Prefab
                    item.gameObject.SetActive(false);
                }
            }
        }
    }
}