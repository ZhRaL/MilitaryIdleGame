using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Util
{
    public class Calculator
    {
        private float rewardMultiplier = 1.2f;
        private float costMultiplier = 1.3f;
        


        private List<ObjDefEntity> startingValues = new();

        public static Calculator INSTANCE;

        public Calculator()
        {
            INSTANCE ??= this;
            InitalizeData();
        }

        private void InitalizeData()
        {
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.KITCHEN
                },
                startingCost = 10,
                startingReward = 20
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.BATH
                },
                startingCost = 10,
                startingReward = 20
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.SLEEPING
                },
                startingCost = 10,
                startingReward = 20
            });

            startingValues.Add(new ObjDefEntity
            {
                ObjectType = new ObjectType
                {
                    objectType = GenericObjectType.JET_TIME
                },
                startingCost = 10,
                startingReward = 20
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
                startingReward = 20
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
                startingReward = 20
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
            return startingValues.Find(entity => entity.ObjectType.Equals(type));
        }

        public float GetReward(ObjectType type, int level)
        {
            var exponent = Mathf.Pow(rewardMultiplier, level - 1);
            var startValue = GetEntity(type).startingReward;
            return Mathf.RoundToInt(startValue * exponent);
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