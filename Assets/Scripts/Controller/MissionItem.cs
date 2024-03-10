using UnityEngine;

namespace DefaultNamespace
{
    public abstract class MissionItem : Item
    {
        public int MoneyLevel { get; set; }
        public abstract Transform Waypoints { get; set; }

        public void MoneyUpgrade()
        {
            MoneyLevel++;
        }
    }
}