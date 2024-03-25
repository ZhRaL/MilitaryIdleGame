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
        
        public override void ItemIsFree()
        {
            Soldier freeS = TheWaitingService.Shift();
            if (freeS != null) PlaceSoldier(freeS);
        }
        
        public override void Load(JsonManageItem levels)
        {
  
          // TODO_OLD  for (int i = 0; i < Items.Count; i++)
          // TODO_OLD  {
          // TODO_OLD      int timeLevel = levels[2*i];
          // TODO_OLD      int moneyLevel = levels[2*i + 1];
          // TODO_OLD      
          // TODO_OLD      Item item = Items[i];
          // TODO_OLD      if (timeLevel > 0 && item.isMissionItem())
          // TODO_OLD      {
          // TODO_OLD          item.Unlocked = true;
          // TODO_OLD          item.Parent = this;
          // TODO_OLD          
          // TODO_OLD          MissionItem missionItem = (MissionItem) item;
// TODO_OLD
          // TODO_OLD          item.Level = timeLevel;
          // TODO_OLD          missionItem.MoneyLevel = moneyLevel;
          // TODO_OLD      }
          // TODO_OLD      else
          // TODO_OLD      {
          // TODO_OLD          // TODO - Add Baustellen Prefab
          // TODO_OLD          item.gameObject.SetActive(false);
          // TODO_OLD      }
          // TODO_OLD  }
        }
        
        
    }
}