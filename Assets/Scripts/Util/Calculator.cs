using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class Calculator
    {
        private float rewardMultiplier = 1.2f;
        private float costMultiplier = 1.3f;
        
        private AnimationCurve itemCurve, missionItemCurve;
        
        private List<ObjDefEntity> startingValues = new();

        public static Calculator INSTANCE;
        
        private const float itemBaseLevel=10;
        private const float missionItemBaseLevel=30;

        public Calculator(AnimationCurve itemCurve, AnimationCurve missionItemCurve)
        {
            INSTANCE ??= this;
            this.itemCurve = itemCurve;
            this.missionItemCurve = missionItemCurve;
            
            InitializeData();
        }

        private void InitializeData()
        {
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.KITCHEN
                },
                startingCost = 10,
                startingReward = itemBaseLevel,
                timeValueNeeded = true
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.BATH
                },
                startingCost = 10,
                startingReward = itemBaseLevel,
                timeValueNeeded = true
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.SLEEPING
                },
                startingCost = 10,
                startingReward = itemBaseLevel,
                timeValueNeeded = true
            });

            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.JET_TIME
                },
                startingCost = 10,
                startingReward = missionItemBaseLevel,
                timeValueNeeded = true
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.JET_MONEY
                },
                startingCost = 10,
                startingReward = 20
            });

            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.TANK_TIME
                },
                startingCost = 10,
                startingReward = missionItemBaseLevel,
                timeValueNeeded = true
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.TANK_MONEY
                },
                startingCost = 10,
                startingReward = 20
            });

            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    defenseType = DefenseType.ARMY,
                    objectType = GenericObjectType.SHIP_TIME
                },
                startingCost = 10,
                startingReward = missionItemBaseLevel,
                timeValueNeeded = true
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    defenseType = DefenseType.ARMY,
                    objectType = GenericObjectType.SHIP_MONEY
                },
                startingCost = 10,
                startingReward = 20
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    defenseType = DefenseType.ARMY,
                    objectType = GenericObjectType.SOLDIER_CRIT
                },
                startingCost = 10,
                startingReward = 20
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    defenseType = DefenseType.ARMY,
                    objectType = GenericObjectType.SOLDIER_SPEED
                },
                startingCost = 10,
                startingReward = 20
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    defenseType = DefenseType.ARMY,
                    objectType = GenericObjectType.SOLDIER_REWARD
                },
                startingCost = 10,
                startingReward = 20
            });
        }

        private ObjDefEntity GetEntity(ObjectType type)
        {
            var typCopy = new ObjectType
            {
                defenseType = DefenseType.ARMY,
                objectType = type.objectType
            };
            return startingValues.Find(entity => entity.ObjectType.Equals(typCopy));
        }

        public float GetReward(ObjectType type, int level)
        {
            var entity = GetEntity(type);

            // Money Reward
            if (!entity.timeValueNeeded)
            {
                var exponent = Mathf.Pow(rewardMultiplier, level - 1);
                var startValue = entity.startingReward;
                return Mathf.RoundToInt(startValue * exponent);   
            }
            // Time Reward

            var baseValue = entity.startingReward;
            // Item
            if (baseValue == itemBaseLevel)
            {
                return baseValue - itemCurve.Evaluate(level);
            }

            return baseValue - missionItemCurve.Evaluate(level);
        }

        public float GetCost(ObjectType type, int level)
        {
            var exponent = Mathf.Pow(costMultiplier, level - 1);
            var startValue = GetEntity(type).startingCost;
            return Mathf.RoundToInt(startValue * exponent);
        }

        public float GetRewardDiff(ObjectType type, int currentLevel)
        {
            // TODO - check with Time Upgrades, because Higher Level means lower time?!
            return GetReward(type, currentLevel + 1) - GetReward(type, currentLevel);
        }

        public float GetTimeReductionReward(int level)
        {
            return Mathf.Log(1f + (0.1f + 0.1f * (level / 50f)) * level);
        }
    }
}