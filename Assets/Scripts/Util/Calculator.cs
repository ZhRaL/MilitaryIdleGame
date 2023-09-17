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

        private List<ObjDefEntity> startingValues;
        
        public static Calculator INSTANCE;

        public Calculator()
        {
            INSTANCE ??= this;
            InitalizeData();
        }

        private void InitalizeData()
        {
            foreach (DefenseType defenseType in Enum.GetValues(typeof(DefenseType)))
            {
                foreach (ObjectType objectType in Enum.GetValues(typeof(ObjectType)))
                {

                    if (objectType != ObjectType.JET && objectType != ObjectType.TANK && objectType != ObjectType.SHIP)
                    {
                        startingValues.Add(new ObjDefEntity
                        {
                            DefenseType = defenseType,
                            ObjectType = objectType,
                            startingCost = 10,
                            startingReward = 2
                        });
                    }
                }
            }
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.JET,
                startingCost = 10,
                startingReward = 2
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.SHIP,
                startingCost = 10,
                startingReward = 2
            });
            startingValues.Add(new ObjDefEntity
            {
                ObjectType = ObjectType.TANK,
                startingCost = 10,
                startingReward = 2
            });
        }


        private ObjDefEntity getEntity(ObjDefEntity entity)
        {
            return startingValues.Find(deff => deff.Equals(entity));
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