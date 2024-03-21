using Interfaces;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class Vehicle : MissionItem
    {
        [SerializeField] private Transform _waypoints;

        public override Transform Waypoints
        {
            get => _waypoints;
            set => _waypoints = value;
        }
        
    }
}