using System;
using UnityEngine;
using Util;

namespace DefaultNamespace
{
    public abstract class Item : MonoBehaviour
    {
        ObjectType ObjectType { set; get; }
        public int Level { get; set; }
        public int Index { get; set; }
        public bool Occupied { get; set; }
        public bool Unlocked { get; set; }
        public SoldierItemBehaviour SoldierItemBehaviour { get; set; }

        private void Start()
        {
            SoldierItemBehaviour = new(this);
            Index = transform.GetSiblingIndex();
        }

        private void Update()
        {
            SoldierItemBehaviour.Update();
        }

        public void Upgrade()
        {
            Level++;
        }

        public float TimeNeeded()
        {
            // TODO
            return -1;
        }

        public void SoldierSitDown(Soldier soldier)
        {
            SoldierItemBehaviour.SoldierSitDown(soldier);
        }

        public void SoldierGetUp()
        {
            SoldierItemBehaviour.SoldierGetUp();
        }
    }
}