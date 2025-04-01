using DefaultNamespace;
using UnityEngine;

namespace Controller.ManageItems.Items
{
    public class VehicleAnimatorHook : MonoBehaviour
    {
        private MissionItem _missionItem;
        public GameObject SmokeEffect;

        public void Init(MissionItem item)
        {
            _missionItem = item;
        }
        
        public void MissionEnd()
        {
            _missionItem.MissionEnd();
        }

        public void getBackDelayed()
        {
            _missionItem.getBackDelayed();
        }
    }
}