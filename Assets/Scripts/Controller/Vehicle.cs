using Interfaces;
using UnityEditor;

namespace DefaultNamespace
{
    public class Vehicle : MissionItem, IGatherable
    {
        public int GetData()
        {
            return Level;
        }
    }
}