using System;

namespace Util
{
    public enum DefenseType
    {
        ARMY,
        AIRFORCE,
        MARINE
        

    }

public static class DefenseTypeExtensions
{
    public static char GetLetter(this DefenseType defenseType)
    {
        switch (defenseType)
        {
            case DefenseType.ARMY:
                return 'A';
            case DefenseType.AIRFORCE:
                return 'R';
            case DefenseType.MARINE:
                return 'M';
            default:
                throw new InvalidOperationException("Ungültiger Verteidigungstyp");
        }
    }
}
}