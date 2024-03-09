using Interfaces;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class Vehicle : MissionItem, IGatherable
    {
        [SerializeField] private Transform _waypoints;

        public Transform Waypoints
        {
            get => _waypoints;
            set => _waypoints = value;
        }

        private SoldierWalkUtil Wayback { get; set; }
        
        public int GetData()
        {
            return Level;
        }
    }
}