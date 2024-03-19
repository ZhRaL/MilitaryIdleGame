using System;

namespace Util
{
    [Serializable]
    public struct ObjectType
    {
        public DefenseType defenseType;
        public GenericObjectType objectType;
    }

    public enum GenericObjectType
    {
        KITCHEN,
        BATH,
        SLEEPING,

        JET,
        TANK,
        SHIP,
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
        SOLDIER_REWARD
        
}

}