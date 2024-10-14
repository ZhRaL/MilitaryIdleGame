using DefaultNamespace;
using UnityEngine;

namespace Controller.ManageItems.Items
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