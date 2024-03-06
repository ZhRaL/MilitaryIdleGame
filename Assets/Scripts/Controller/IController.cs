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

        int[] getState();
        void loadState(int[] state);


    }
}