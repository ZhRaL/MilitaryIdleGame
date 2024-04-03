using System;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class SoldierPlacement : MonoBehaviour
    {
        public Transform routeParent1, routeParent2, routeParent3;

        public static SoldierPlacement INSTANCE;

        private void Awake()
        {
            INSTANCE = this;
        }
        
        public void PlaceSoldier(Soldier soldier)
        {
            Transform parent = getBrancheParent(soldier);
            Tuple<int, RoutingPoint> tuple = getRandomPoint(parent);
            RoutingPoint point = tuple.Item2;
            soldier.transform.position = point.transform.position;
            soldier.currentTarget = tuple.Item1;
        }

        private Transform getBrancheParent(Soldier soldier)
        {
            return soldier.SoldierType switch
            {
                DefenseType.ARMY => routeParent1,
                DefenseType.AIRFORCE => routeParent2,
                DefenseType.MARINE => routeParent3,

                _ => throw new ArgumentOutOfRangeException(nameof(soldier.SoldierType))
            };
        }

        private Tuple<int, RoutingPoint> getRandomPoint(Transform parent)
        {
            int randomValue = Random.Range(0, parent.childCount);
            return new Tuple<int, RoutingPoint>(randomValue, parent.GetChild(randomValue).GetComponent<RoutingPoint>());
        }
    }
}