using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class RecruitInitializer : MonoBehaviour
{
    public GameObject SoldierIconPrefab;
    public UpgradeScript UpgradeScript;
    public DefenseType DefenseType;
    private void Start()
    {
        Soldier[] soldiers=SoldierController.INSTANCE.GetAllSoldiersFrom(DefenseType);
        
        foreach (Soldier soldier in soldiers)
        {
            GameObject go = Instantiate(SoldierIconPrefab, transform);
            var x = go.GetComponent<RecruitTemplate>();
            x.Init(soldier,UpgradeScript);
        }
    }
}
