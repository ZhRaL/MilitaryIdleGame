using System;
using System.Diagnostics;
using UnityEngine.Rendering;
using Util;

namespace DefaultNamespace
{
    public interface IController
    {
        IManageItem ArmyManager { get; }
        IManageItem AirforceManager { get; }
        IManageItem MarineManager { get; }

        // JsonController Save();
        // void Load(JsonController state);
        // look at Class InterfaceExtensions


    }
}