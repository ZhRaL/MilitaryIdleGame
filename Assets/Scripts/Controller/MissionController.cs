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
    }
}