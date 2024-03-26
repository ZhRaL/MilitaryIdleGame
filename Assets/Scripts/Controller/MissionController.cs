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

     //   public JsonController Save()
     //   {
     //       Debug.Log("Was called on MissioNControlelr");
     //       return null;
     //   }
//
     //   public void Load(JsonController state)
     //   {
     //       ArmyManager.Load(state.GetAt(0));
     //       AirforceManager.Load(state.GetAt(1));
     //       MarineManager.Load(state.GetAt(2));
     //   }
    }
}