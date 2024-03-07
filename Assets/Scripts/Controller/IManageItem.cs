using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Util;

namespace DefaultNamespace
{
    public abstract class IManageItem : MonoBehaviour
    {
        [SerializeField] private DefenseType _defenseType;
        [SerializeField] private Transform _waitingPosParent;
        private WaitingService _waitingService;
        private List<SoldierWalkUtil> walkingSoldiers;
        
        List<Item> Items { get; set; }
        DefenseType DefenseType => _defenseType;
        WaitingService WaitingService { get; set; }
        List<SoldierWalkUtil> WalkingSoldiers => walkingSoldiers = new();
        Transform WaitingPosParent => _waitingPosParent;

        private void Start()
        {
            WaitingService = new WaitingService(_waitingPosParent);
        }
        
        void Update()
        {
            var copyOfWalkingSoldiers = new List<SoldierWalkUtil>(WalkingSoldiers);
            copyOfWalkingSoldiers.ForEach(soldierWalkUtil => soldierWalkUtil.Update());
            WaitingService.Update();
        }

        public void Init(int[] levels)
        {
            
            if (levels.Length > Items.Count) 
                throw new ArgumentException("invalid Amount!");

            for (int i = 0; i < Items.Count; i++)
            {
                int level = levels[i];
                Item item = Items[i];
                if (level > 0)
                {
                    item.Level = level;
                    item.Parent = this;
                }
                else item.gameObject.SetActive(false);
            }
        }

        void PlaceSoldier(Soldier soldier)
        {
            Item targetItem = GetNextFreeItem();
            if (targetItem != null)
            {
                targetItem.Occupied = true;
                moveSoldierTo(soldier, targetItem.transform, () => targetItem.SoldierSitDown(soldier));
            }
            else
            {
                WaitingService.addSoldier(soldier);
            }
        }
        
        private void moveSoldierTo(Soldier soldier, Transform target, Action executeWhenReached)
        {
            soldier.anim.SetBool("isRunning", true);
            WalkingSoldiers.Add(new SoldierWalkUtil(soldier, target, executeWhenReached, RemoveWalkingSoldier));
        }
        
        public void RemoveWalkingSoldier(SoldierWalkUtil walk)
        {
            WalkingSoldiers.Remove(walk);
        }

        public int GetLevelForItem(int index)
        {
            return Items[index].Level;
        }
        
        public UnityAction GetUpgradeMethod(int index)
        {
            return Items[index].Upgrade;
        }

        public float GetAverageTime()
        {
            return Items.Average(s => s.TimeNeeded());
        }

        public int GetAmountOfUnlockedItems()
        {
            return Items.Count(s => s.Unlocked);
        }
        
        private Item GetNextFreeItem()
        {
            return Items.FirstOrDefault(item => item.Unlocked && !item.Occupied);
        }

        public int[] GetState()
        {
            return Items.Select(item => item.Level).ToArray();
        }
        
        public void ItemIsFree()
        {
            Soldier freeS = WaitingService.Shift();
            if (freeS != null) PlaceSoldier(freeS);
        }

    }
}