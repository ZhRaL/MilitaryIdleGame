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

        private protected List<SoldierWalkUtil> WalkingSoldiers { get; } = new();
        
        Transform WaitingPosParent => _waitingPosParent;

        private protected WaitingService TheWaitingService
        {
            get
            {
                if (_waitingService == null)
                {
                    _waitingService = new WaitingService(_waitingPosParent);
                }
                return _waitingService;
            }
        }
        
        void Update()
        {
            var copyOfWalkingSoldiers = new List<SoldierWalkUtil>(WalkingSoldiers);
            copyOfWalkingSoldiers.ForEach(soldierWalkUtil => soldierWalkUtil.Update());
            TheWaitingService.Update();
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

        public virtual JsonManageItem<T> Save<T>()
        {
            JsonManageItem<T> mi = new JsonManageItem<T>();
            Items.ForEach(x =>
            {
                if (x.Unlocked && x.ToJson() is T te)
                {
                    mi.AddItem(te);
                }
            });
            return mi;
        }

        public virtual void Load<T>(JsonManageItem<T> levels)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (levels.GetIndex(i) == null)
                    Items[i].Load(this, null);
                if (levels.GetIndex(i) is JsonItem ji)
                    Items[i].Load(this, ji);
            }
        }

        public virtual void ItemIsFree()
        {
            Soldier freeS = TheWaitingService.Shift();
            if (freeS != null) PlaceSoldier(freeS);
        }
    }
}