using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

public class Platoon : MonoBehaviour
{
    public GameObject SoldierPrefab;
    public List<Soldier> Soldiers = new();
    public GameObject parentRoute;

    public void createSoldier(int speed, int reward, int crit, string name)
    {
        GameObject go = Instantiate(SoldierPrefab, transform);
        Soldier so = go.GetComponent<Soldier>();
        so.name = "John Doe";
        so.LVL_Speed = speed;
        so.LVL_Reward = reward;
        so.LVL_Crit = crit;
        so.parentRoute = parentRoute;
        so.SoldierName = name;
        Soldiers.Add(so);
    }

    public void Load(JsonManageItem<SoldierItemJO> levels)
    {
        foreach (var ji in levels.SaveItems)
        {
            SoldierItemJO jo = ji as SoldierItemJO ?? new SoldierItemJO();
            createSoldier(jo.SpeedLevel, jo.MissionRewardLevel, jo.CritLevel, jo.Name);
        }
    }

    public JsonManageItem<SoldierItemJO> Save()
    {
        JsonManageItem<SoldierItemJO> item = new JsonManageItem<SoldierItemJO>();

        foreach (Transform trans in transform)
        {
            Soldier so = trans.GetComponent<Soldier>();
            SoldierItemJO jo = new SoldierItemJO
            {
                Json_Level = -1,
                Name = so.SoldierName,
                SpeedLevel = so.LVL_Speed,
                MissionRewardLevel = so.LVL_Reward,
                CritLevel = so.LVL_Crit,
            };
            item.AddItem(jo);
        }

        return item;
    }
}