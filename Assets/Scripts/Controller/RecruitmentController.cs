using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class RecruitmentController : MonoBehaviour
{

    public Platoon armyPlatoon, airforcePlatoon, marinePlatoon;
    private readonly List<Soldier> soldiers = new();

    public List<Soldier> GetSoldiers()
    {
        return soldiers;
    }

    public int getSoldierTypeAmount(DefenseType type)
    {
        return soldiers.Count(s => s.SoldierType == type);
    }

    public int[] getState()
    {
        throw new System.NotImplementedException();
    }

    public void loadState(int[] state)
    {
        throw new System.NotImplementedException();
    }

    public IManageItem GetItemManager(DefenseType defenseType)
    {
        throw new System.NotImplementedException();
    }
}
