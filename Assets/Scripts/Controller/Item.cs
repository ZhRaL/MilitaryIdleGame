using System;
using UnityEngine;
using Util;

namespace DefaultNamespace
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private RoutingPoint _routingPoint;
        [SerializeField] private ObjectType _objectType;

        public ObjectType ObjectType
        {
            set => _objectType = value;
            get => _objectType;
        }

        public int Level { get; set; }
        public int Index => transform.GetSiblingIndex();
        public bool Occupied { get; set; }
        public bool Unlocked { get; set; }
        public SoldierItemBehaviour SoldierItemBehaviour { get; set; }
        public IManageItem Parent { get; set; }

        public RoutingPoint RoutingPoint
        {
            get => _routingPoint;
            set => _routingPoint = value;
        }

        private void Start()
        {
            SoldierItemBehaviour = new(this);
        }

        public void Upgrade()
        {
            Level++;
        }

        public float TimeNeeded()
        {
            // TODO
            return 10 - Calculator.INSTANCE.getTimeReductionReward(Level);
        }

        public virtual void SoldierSitDown(Soldier soldier)
        {
            SoldierItemBehaviour.SoldierSitDown(soldier);
        }

        public void SoldierGetUp()
        {
            SoldierItemBehaviour.SoldierGetUp();
        }

        public bool isMissionItem()
        {
            return this is MissionItem;
        }
    }
}