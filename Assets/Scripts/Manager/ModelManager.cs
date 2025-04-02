using System;
using DefaultNamespace;
using Foxbird;
using UnityEngine;
using Util;

namespace Manager
{
    public class ModelManager : MonoBehaviour
    {
        public static ModelManager INSTANCE;
        
        [Header("Ships")] public GameObject[] ShipPrefabs;
        [Header("Airplanes")] public GameObject[] JetPrefabs;
        [Header("Tanks")] public GameObject[] TankPrefabs;

        public GameObject GetModelPrefab(DefenseType type, int level)
        {
            int rank = SliderValues.getRankForLevel(level);
            return type switch
            {
                DefenseType.ARMY => TankPrefabs[rank],
                DefenseType.AIRFORCE => JetPrefabs[rank],
                DefenseType.MARINE => ShipPrefabs[rank],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public void Awake()
        {
            INSTANCE = this;
        }
    }
}