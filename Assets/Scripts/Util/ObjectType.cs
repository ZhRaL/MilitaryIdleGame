using System;

namespace Util
{
    [Serializable]
    public struct ObjectType
    {
        public DefenseType defenseType;
        public GenericObjectType objectType;

        public static ObjectType Kit => new ObjectType
        {
            objectType = GenericObjectType.KITCHEN
        };
        public static ObjectType Bat => new ObjectType
        {
            objectType = GenericObjectType.BATH
        };
        public static ObjectType Sle => new ObjectType
        {
            objectType = GenericObjectType.SLEEPING
        };

        public ObjectType(GenericObjectType objectType) : this()
        {
            this.objectType = objectType;
        }
    }

    public enum GenericObjectType
    {
        KITCHEN,
        BATH,
        SLEEPING,

        JET_MONEY,
        JET_TIME,
        TANK_MONEY,
        TANK_TIME,
        SHIP_MONEY,
        SHIP_TIME,
        
        SOLDIER_SPEED,
        SOLDIER_CRIT,
        SOLDIER_REWARD
    }

    public enum UpgradeType
    {
        KITCHEN,
        BATH,
        SLEEPING,

        JET_MONEY,
        JET_TIME,
        TANK_MONEY,
        TANK_TIME,
        SHIP_MONEY,
        SHIP_TIME,
        
        SOLDIER_SPEED,
        SOLDIER_CRIT,
        SOLDIER_REWARD,
        LOCKED,
        BIG_LOCKED,
        Soldier
    }

}