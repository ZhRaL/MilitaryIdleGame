using System;
using System.Collections.Generic;
using System.Linq;
using Controller.ManageItems.Items;
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

        public GameObject constructionParent;

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

        public override void Load<T>(JsonManageItem<T> levels)
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                MissionItem item = (MissionItem)Items[i];

                if (levels.GetIndex(i) == null)
                {
                    GameObject constructionObject = constructionParent.transform.GetChild(i - 1).gameObject;
                    constructionObject.SetActive(true);
                    item.gameObject.SetActive(false);
                    item.OnLevelUp += _ => constructionObject.SetActive(false);
                    continue;
                }

                if (levels.GetIndex(i) is JsonItem ji)
                {
                    MissionItemJO jo = ji as MissionItemJO ?? new MissionItemJO();
                    item.Unlocked = true;
                    item.Parent = this;
                    item.Level = jo.Json_Level;
                    item.MoneyLevel = jo.MoneyLevel;
                }
            }
        }

        public override float GetAverageMissionMoney()
        {
            return (float)Items.OfType<MissionItem>().Average(e => e.MoneyLevel);
        }
    }
}