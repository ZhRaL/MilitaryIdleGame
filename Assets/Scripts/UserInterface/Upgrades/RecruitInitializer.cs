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

    private IconScript Select;
    private void Start()
    {
        Soldier[] soldiers=SoldierController.INSTANCE.GetAllSoldiersFrom(DefenseType);
        
        foreach (Soldier soldier in soldiers)
        {
            GameObject go = Instantiate(SoldierIconPrefab, transform);
            var x = go.GetComponent<RecruitTemplate>();
            x.Init(soldier,UpgradeScript);
        }

        selectFirstIcon();
    }

    private void selectFirstIcon()
    {

            IconScript x = GetComponentInChildren<IconScript>();
            if (x is DoubleIconScript ds)
            {
                ds.MoneyButtonPressed();
                return;
            }
            if (x) Selected(x);
        
    }
    
    public void Selected(IconScript child)
    {
        Select = child;
        child.highlightMe();
        child.SingleSelect.Invoke(child);
    }
    
}
