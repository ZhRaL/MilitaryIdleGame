using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class RecruitInitializer : MonoBehaviour
{
    public GameObject SoldierIconPrefab;
    public UpgradeScript UpgradeScript;
    public DefenseType DefenseType;
    public GameObject buyNewSoldierGameObject;
    public BuyScript BuyNewSoldierUpgrade;

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
        UpgradeScript.gameObject.SetActive(true);

        activateBuy();
        
        selectFirstIcon();
    }

    private void activateBuy()
    {
        buyNewSoldierGameObject.transform.SetSiblingIndex(transform.childCount-1);

        Button button = buyNewSoldierGameObject.AddComponent<Button>();
        button.onClick.AddListener(UpgradeBuy);
    }

    private void UpgradeBuy()
    {
        UpgradeScript.gameObject.SetActive(false);
        BuyNewSoldierUpgrade.gameObject.SetActive(true);
        var x = new UpgradeDto
        {
            IconBackground = null,
            Icon = null,
            title = "New Soldier",
            description = "TBD",
            level = 0,
            upgradeCost = 0,
            currentReward = 0,
            diffReward = 0,
            item = null,
            moneyItem = false
        };
        x.upgradeAction += () => UpgradeScript.gameObject.SetActive(true);
        BuyNewSoldierUpgrade.Init(x, DefenseType);
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
