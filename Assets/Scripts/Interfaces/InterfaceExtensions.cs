using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
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

        public static Transform[] GetAllChildren(this Transform TransformP)
        {
            if (!TransformP) return null;
            List<Transform> childs = new();
            foreach (Transform child in TransformP)
            {
                childs.Add(child);
            }

            return childs.ToArray();
        }
    }
}