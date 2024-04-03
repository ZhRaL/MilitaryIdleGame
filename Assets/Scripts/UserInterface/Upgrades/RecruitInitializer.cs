using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Refresh();

        activateBuy();

        selectFirstIcon();
    }

    private void OnEnable()
    {
        selectFirstIcon();
    }

    public void Refresh()
    {
        Soldier[] soldiers = SoldierController.INSTANCE.GetAllSoldiersFrom(DefenseType);
        RecruitTemplate[] childs = GetComponentsInChildren<RecruitTemplate>();

        foreach (Soldier soldier in soldiers)
        {
            if (childs.Count(y => y.Soldier.Equals(soldier)) > 0)
                continue;

            GameObject go = Instantiate(SoldierIconPrefab, transform);
            go.transform.SetSiblingIndex(transform.childCount - 1);
            var x = go.GetComponent<RecruitTemplate>();
            x.Init(soldier, UpgradeScript);
        }

        buyNewSoldierGameObject.transform.SetSiblingIndex(transform.childCount - 1);

        BuyNewSoldierUpgrade.gameObject.SetActive(false);
        UpgradeScript.gameObject.SetActive(true);
    }

    private void activateBuy()
    {
        buyNewSoldierGameObject.transform.SetSiblingIndex(transform.childCount - 1);

        Button button = buyNewSoldierGameObject.AddComponent<Button>();
        button.onClick.AddListener(UpgradeBuy);
    }

    private void UpgradeBuy()
    {
        UpgradeScript.gameObject.SetActive(false);
        BuyNewSoldierUpgrade.gameObject.SetActive(true);
        var upgradeDto = new UpgradeDto
        {
            IconBackground = null,
            Icon = null,
            title = "George",
            description = "There will be an awesome description for this specific soldier",
            level = 0,
            upgradeCost = 0,
            currentReward = 0,
            diffReward = 0,
            item = null,
            moneyItem = false
        };
        upgradeDto.upgradeAction += () => UpgradeScript.gameObject.SetActive(true);
        upgradeDto.upgradeAction += Refresh;

        BuyNewSoldierUpgrade.Init(upgradeDto, DefenseType);
    }

    private void selectFirstIcon()
    {
        IconScript x = GetComponentInChildren<IconScript>();
        if (x) Selected(x);
    }

    public void Selected(IconScript child)
    {
        Select = child;
        child.highlightMe();
        child.SingleSelect.Invoke(child);
    }
}