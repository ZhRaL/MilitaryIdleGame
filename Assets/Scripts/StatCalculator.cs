using UnityEngine;
using Util;

namespace DefaultNamespace
{
    public class StatCalculator
    {
        public static StatCalculator INSTANCE;

        public StatCalculator()
        {
            INSTANCE ??= this;
        }
        
        public float getStatReward(ObjDefEntity entity, int level)
        {
            float reward = Calculator.INSTANCE.getReward(new ObjDefEntity() { ObjectType = entity.ObjectType }, level);
           // float time = Calculator.INSTANCE.getTimeReductionReward(entity.)
            //return Mathf.RoundToInt(getEntity(entity).startingReward*Mathf.Pow(rewardMultiplier, level - 1));
            return 0;
        }
    }
}