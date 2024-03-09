using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace DefaultNamespace
{
    public class MissionController : MonoBehaviour, IController
    {
        [SerializeField] private Company companyArmy, companyAirforce, companyMarine;

        public IManageItem ArmyManager => companyArmy;
        public IManageItem AirforceManager => companyAirforce;
        public IManageItem MarineManager => companyMarine;
        
        public int[] getState()
        {
            return ArmyManager.GetState()
                .Concat(AirforceManager.GetState())
                .Concat(MarineManager.GetState())
                .ToArray();
        }

        public void loadState(int[] state)
        {
            if (state.Length != 18) 
                throw new ArgumentException("Wrong Length of Array");

            ArmyManager.Init(state[..6]);
            AirforceManager.Init(state[6..12]);
            MarineManager.Init(state[12..18]);
        }
    }
}