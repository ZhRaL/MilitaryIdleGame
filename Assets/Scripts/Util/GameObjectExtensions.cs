using System;
using System.Collections.Generic;
using DefaultNamespace;
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
          //  switch (type)
          //  {
          //      case ObjectType.CHAIR:
          //          return "Chair";
          //      case ObjectType.BED:
          //          break;
          //      case ObjectType.TOILET:
          //          break;
          //      case ObjectType.JET_AMOUNT:
          //          return "Jet";
          //      case ObjectType.JET_TIME:
          //          break;
          //      case ObjectType.SHIP_AMOUNT:
          //          return "Ship";
          //      case ObjectType.SHIP_TIME:
          //          break;
          //      case ObjectType.TANK_AMOUNT:
          //          return "Tank";
          //      case ObjectType.TANK_TIME:
          //          break;
          //      default:
          //          throw new ArgumentOutOfRangeException(nameof(type), type, null);
          //  }

            return "No Name available";
        }
        
        public static UpgradeType ToUpgradeType(this Item item)
        {
            logger.log("Item was: "+item.ObjectType.objectType);
            switch (item.ObjectType.objectType)
            {
                case GenericObjectType.BATH : return UpgradeType.BATH;
                case GenericObjectType.KITCHEN : return UpgradeType.KITCHEN;
                case GenericObjectType.SLEEPING : return UpgradeType.SLEEPING;

                default:
                    throw new ArgumentOutOfRangeException(nameof(item.ObjectType.objectType));
            }
        }
    }
}