using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

public class Platoon : MonoBehaviour
{
    public GameObject SoldierPrefab;
    public List<Soldier> Soldiers=new();
    public GameObject parentRoute;

    public void createSoldier(int speed, int reward, int crit)
    {
        GameObject go = Instantiate(SoldierPrefab, transform);
        Soldier so = go.GetComponent<Soldier>();
        so.LVL_Speed = speed;
        so.LVL_Reward = reward;
        so.LVL_Crit = crit;
        so.parentRoute = parentRoute;
        Soldiers.Add(so);
    }


    public int[] GetState()
    {
        List<int> result = new();
        for (int i = 0; i < transform.childCount; i++)
        {
            Soldier so = transform.GetChild(i).gameObject.GetComponent<Soldier>();
            result.Add(so.LVL_Speed);
            result.Add(so.LVL_Reward);
            result.Add(so.LVL_Crit);

        }

        return result.ToArray();
    }
}
