namespace Util
{
    public class ObjDefEntity
    {
        public DefenseType DefenseType;
        public ObjectType ObjectType;
        public int startingCost;
        public int startingReward;

        public override bool Equals(object obj)
        {
            // Überprüfen, ob das übergebene Objekt null ist
            if (obj == null)
            {
                return false;
            }

            // Überprüfen, ob das übergebene Objekt ein ObjDefEntity-Objekt ist
            if (!(obj is ObjDefEntity otherEntity))
            {
                return false;
            }

            // Vergleichen von DefenseType und ObjectType
            return this.DefenseType == otherEntity.DefenseType &&
                   this.ObjectType.objectType == otherEntity.ObjectType.objectType;
        }
    }
}