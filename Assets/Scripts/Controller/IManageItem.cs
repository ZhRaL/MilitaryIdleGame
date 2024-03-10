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
        private protected WaitingService _waitingService;
        
        public abstract List<Item> Items { get; }
        DefenseType DefenseType => _defenseType;
        private protected WaitingService TheWaitingService { get; set; }
        private protected List<SoldierWalkUtil> WalkingSoldiers { get; } = new();
        Transform WaitingPosParent => _waitingPosParent;

        private void Start()
        {
            TheWaitingService = new WaitingService(_waitingPosParent);
        }
        
        void Update()
        {
            var copyOfWalkingSoldiers = new List<SoldierWalkUtil>(WalkingSoldiers);
            copyOfWalkingSoldiers.ForEach(soldierWalkUtil => soldierWalkUtil.Update());
            TheWaitingService.Update();
        }

        public virtual void Init(int[] levels)
        {
            
            if (levels.Length > Items.Count) 
                throw new ArgumentException("invalid Amount!");

            for (int i = 0; i < Items.Count; i++)
            {
                int level = levels[i];
                Item item = Items[i];
                if (level > 0)
                {
                    item.Unlocked = true;
                    item.Level = level;
                    item.Parent = this;
                }
                else item.gameObject.SetActive(false);
            }
        }

        public virtual void PlaceSoldier(Soldier soldier)
        {
            Item targetItem = GetNextFreeItem();
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
        
        private protected void moveSoldierTo(Soldier soldier, Transform target, Action executeWhenReached)
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
        
        private protected Item GetNextFreeItem()
        {
            return Items.FirstOrDefault(item => item.Unlocked && !item.Occupied);
        }

        public virtual int[] GetState()
        {
            return Items.Select(item => item.Level).ToArray();
        }
        
        public void ItemIsFree()
        {
            Soldier freeS = TheWaitingService.Shift();
            if (freeS != null) PlaceSoldier(freeS);
        }

    }
}