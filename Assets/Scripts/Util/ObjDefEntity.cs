namespace Util
{
    public class ObjDefEntity
    {
        public ObjectType ObjectType;
        public int startingCost;
        public float startingReward; // Float because the required time is also a 'reward'  
        public bool timeValueNeeded;

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
            return ObjectType.objectType == otherEntity.ObjectType.objectType;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}