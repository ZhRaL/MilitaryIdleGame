namespace Util
{
    public enum ObjectType
    {
        CHAIR,BED,TOILET,JET,SHIP,TANK
    }
    
    public static class ObjectTypeExtensions
    {
        public static char GetLetter(this ObjectType defenseType)
        {
            switch (defenseType)
            {
                case ObjectType.CHAIR:
                    return 'C';
                case ObjectType.BED:
                    return 'B';
                case ObjectType.TOILET:
                    return 'T';
                case ObjectType.JET:
                    return 'J';
                case ObjectType.SHIP:
                    return 'S';
                case ObjectType.TANK:
                    return 'P';
                default: return '_';
            }
        }
    }
}