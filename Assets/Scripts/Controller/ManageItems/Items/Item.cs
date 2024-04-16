using System;
using UnityEngine;
using Util;

namespace DefaultNamespace
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private RoutingPoint _routingPoint;
        [SerializeField] private ObjectType _objectType;

        public delegate void LevelUpEventHandler(int newLevel);
    
        // Ereignis für Levelaufstieg
        public event LevelUpEventHandler OnLevelUp;
        
        public ObjectType ObjectType
        {
            set => _objectType = value;
            get => _objectType;
        }

        private protected int _level;
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnLevelUp?.Invoke(value);
            }
        }

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
            Unlocked = true;
            gameObject.SetActive(true);
        }

        public float TimeNeeded()
        {
            // TODO
            return 10 - Calculator.INSTANCE.GetTimeReductionReward(Level);
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

        public virtual JsonItem ToJson() {
            return new JsonItem() {
                Json_Level = Level
            };
        }

        public void Load(IManageItem parent, JsonItem jsonItem)
        {
            Parent = parent;
            if (jsonItem != null)
            {
                Unlocked = true;
                Level = jsonItem.Json_Level;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
