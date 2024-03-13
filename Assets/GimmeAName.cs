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
            logger.log("NR: " + item.Index);
            if (item is MissionItem missionItem)
            {
                return;
            }

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
            if (x) Selected(x);
        }
    }


    public void Selected(IconScript child)
    {
        selected = child;
        HighLightManager.highlight(child.GetComponent<Image>());
        UpgradeScript.selectionChanged(BuildUpgradeDto());
    }

    public UpgradeDto BuildUpgradeDto()
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