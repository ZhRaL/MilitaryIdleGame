using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class GimmeAName : MonoBehaviour
{
    [SerializeField] private ObjectType _objectType;

    public Transform AvailablesParent;

    public GameObject IconPrefab;

    private IconScript selected;
    public UpgradeScript UpgradeScript;
    public bool SelectFirst;

    public ObjectType ObjType => _objectType; 
    private void Start()
    {
        var tl = GameManager.INSTANCE.GetTopLevel(_objectType);
        IManageItem imanag;
        switch (_objectType.defenseType)
        {
            case DefenseType.ARMY:
                imanag = tl.ArmyManager;
                break;
            case DefenseType.AIRFORCE:
                imanag = tl.AirforceManager;
                break;
            case DefenseType.MARINE:
                imanag = tl.MarineManager;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }


        foreach (Item item in imanag.Items)
        {
            GameObject go = Instantiate(IconPrefab, AvailablesParent);
            IconScript script = go.GetComponent<IconScript>();
            script.InitializePreview(this, item);
        }

        selectFirstIcon();
    }

    private void OnEnable()
    {
        selectFirstIcon();
    }

    private void selectFirstIcon()
    {
        if (SelectFirst)
        {
            IconScript x = GetComponentInChildren<IconScript>();
            if (x is DoubleIconScript ds)
            {
                ds.MoneyButtonPressed();
                return;
            }
            if (x) Selected(x);
        }
    }


    public void Selected(IconScript child)
    {
        selected = child;
        child.highlightMe();
        UpgradeScript.selectionChanged(BuildUpgradeDto());
    }

    public void DoubleSelect(DoubleIconScript child, bool isMoney)
    {
        if(!child.Item) return;
        UpgradeScript.selectionChanged(BuildMissionUpgradeDto(child,isMoney));
    }

    private UpgradeDto BuildMissionUpgradeDto(DoubleIconScript child, bool isMoney)
    {
        return isMoney
            ? new UpgradeDto
            {
                Icon = child.MoneyIconImage.sprite,
                title = "Mission Reward",
                description = "DescriptionManager",
                level = ((MissionItem) child.Item).MoneyLevel,
                upgradeAction = ((MissionItem) child.Item).Upgrade,
                upgradeCost = GameManager.INSTANCE.DataProvider.GetCost(child.Item, true),
                currentReward = GameManager.INSTANCE.DataProvider.GetReward(child.Item, true),
                diffReward = GameManager.INSTANCE.DataProvider.GetRewardDiff(child.Item, true)
            }
            : new UpgradeDto
            {
                IconBackground = null,
                Icon = child.iconImage.sprite,
                title = "Time Reduction",
                description = "DescriptionManager",
                level = child.Item.Level,
                upgradeAction = child.Item.Upgrade,
                upgradeCost = GameManager.INSTANCE.DataProvider.GetCost(child.Item),
                currentReward = GameManager.INSTANCE.DataProvider.GetReward(child.Item),
                diffReward = GameManager.INSTANCE.DataProvider.GetRewardDiff(child.Item)
            };
    }

    private UpgradeDto BuildUpgradeDto()
    {
        return new UpgradeDto
        {
            Icon = selected.iconImage.sprite,
            title = "To be decided",
            description = "DescriptionManager",
            level = selected.Item.Level,
            upgradeAction = selected.Item.Upgrade,
            upgradeCost = GameManager.INSTANCE.DataProvider.GetCost(selected.Item),
            currentReward = GameManager.INSTANCE.DataProvider.GetReward(selected.Item),
            diffReward = GameManager.INSTANCE.DataProvider.GetRewardDiff(selected.Item)
        };
    }
}