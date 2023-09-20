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
                ObjectType = ObjectType.CHAIR,
                startingCost = 10,
                startingReward = 2
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.TOILET,
                startingCost = 10,
                startingReward = 2
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.BED,
                startingCost = 10,
                startingReward = 2
            });
            
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.JET_AMOUNT,
                startingCost = 10,
                startingReward = 2
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.JET_TIME,
                startingCost = 10,
                startingReward = 2
            });
            
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.SHIP_AMOUNT,
                startingCost = 10,
                startingReward = 2
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.SHIP_TIME,
                startingCost = 10,
                startingReward = 2
            });
            
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.TANK_AMOUNT,
                startingCost = 10,
                startingReward = 2
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.TANK_TIME,
                startingCost = 10,
                startingReward = 2
            });
        }


        private ObjDefEntity getEntity(ObjDefEntity entity)
        {
            return startingValues.Find(deff => deff.ObjectType.Equals(entity.ObjectType));
        }

        public float getReward(ObjDefEntity entity, int level)
        {
            return Mathf.RoundToInt(getEntity(entity).startingReward*Mathf.Pow(rewardMultiplier, level - 1));
        }
        
        public float getCost(ObjDefEntity entity, int level)
        {
            return Mathf.RoundToInt(getEntity(entity).startingCost*Mathf.Pow(costMultiplier, level - 1));
        }

        
    }
}