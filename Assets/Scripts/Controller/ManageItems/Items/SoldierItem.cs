using Util;

namespace DefaultNamespace
{
    public class SoldierItem : Item
    {
        public Soldier Soldier { get; set; }

        public override JsonItem ToJson()
        {
            return new SoldierItemJO()
            {
                Name = Soldier.name,
                SpeedLevel = Soldier.LVL_Speed,
                MissionRewardLevel = Soldier.LVL_Reward,
                CritLevel = Soldier.LVL_Crit
            };
        }

        public virtual void Init()
        {
            
        }
    }

    public class SoldierCritItem : SoldierItem
    {
        public override void Init()
        {
            ObjectType = new ObjectType
            {
                defenseType = Soldier.SoldierType,
                objectType = GenericObjectType.SOLDIER_CRIT
            };
        }
    }
    public class SoldierSpeedItem : SoldierItem
    {
        public override void Init()
        {
            ObjectType = new ObjectType
            {
                defenseType = Soldier.SoldierType,
                objectType = GenericObjectType.SOLDIER_SPEED
            };
        }
    }
    public class SoldierRewardItem : SoldierItem
    {
        public override void Init()
        {
            ObjectType = new ObjectType
            {
                defenseType = Soldier.SoldierType,
                objectType = GenericObjectType.SOLDIER_REWARD
            };
        }
    }
}
