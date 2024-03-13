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
        SHIP_TIME
        
}

}