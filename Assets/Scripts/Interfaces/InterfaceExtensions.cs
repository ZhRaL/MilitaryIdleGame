using System;
using DefaultNamespace;
using Util;

namespace Interfaces
{
    public static class InterfaceExtensions
    {
        public static IManageItem GetItemManager(this IController controller,DefenseType defenseType)
        {
            switch(defenseType)
            {
                case DefenseType.ARMY : return controller.ArmyManager;
                case DefenseType.AIRFORCE: return controller.AirforceManager;
                case DefenseType.MARINE: return controller.MarineManager;
                default:
                    throw new ArgumentOutOfRangeException(nameof(defenseType), defenseType, null);
            }
        }
    }
}