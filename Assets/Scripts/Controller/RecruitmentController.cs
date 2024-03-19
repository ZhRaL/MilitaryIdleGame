using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class RecruitmentController : IController
{
    public IManageItem ArmyManager { get; }
    public IManageItem AirforceManager { get; }
    public IManageItem MarineManager { get; }
    
    public int[] getState()
    {
        throw new System.NotImplementedException();
    }

    public void loadState(int[] state)
    {
        throw new System.NotImplementedException();
    }
}
