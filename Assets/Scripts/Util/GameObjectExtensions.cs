using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class GameObjectExtensions
    {
        public static List<Transform> GetAllChildren(this GameObject parent)
        {
            List<Transform> children = new List<Transform>();

            foreach (Transform child in parent.transform)
            {
                children.Add(child);
            }

            return children;
        }

        public static string toFriendlyName(this ObjectType type)
        {
            switch (type)
            {
                case ObjectType.CHAIR:
                    return "Chair";
                case ObjectType.BED:
                    break;
                case ObjectType.TOILET:
                    break;
                case ObjectType.JET_AMOUNT:
                    break;
                case ObjectType.JET_TIME:
                    break;
                case ObjectType.SHIP_AMOUNT:
                    break;
                case ObjectType.SHIP_TIME:
                    break;
                case ObjectType.TANK_AMOUNT:
                    return "Tank";
                case ObjectType.TANK_TIME:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return "";
        }
    }
}