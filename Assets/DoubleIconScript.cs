using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class DoubleIconScript : IconScript
{
    
    public Image MoneyIconImage, MoneyUpgradeArrowImage;

    public TMP_Text MoneyTx_Level, VehicleText;

    public GameObject MoneyP, TimeP;
    

    public override void InitializePreview(GimmeAName parent, Item item)
    {
        logger.log("I am a MissionItem!!");
        Item = item;
        Parent = parent;
        MissionItem mit = (MissionItem)item;

        IconProvider iconProvider = GameManager.INSTANCE.DataProvider.IconProvider;

        // Initialize Time Icon
        iconImage.sprite = iconProvider.GetIcon(getUpgradeType(true, parent.ObjType));
        tx_level.text = "" + item.Level;

        // Initialize Money Icon
        MoneyIconImage.sprite = iconProvider.GetIcon(getUpgradeType(false, parent.ObjType));
        MoneyTx_Level.text = "" + mit.MoneyLevel;

        VehicleText.text = parent.ObjType.objectType + " " + item.Index;
        

        if (Upgradable(item))
            upgradeArrowImage.gameObject.SetActive(true);
        else upgradeArrowImage.gameObject.SetActive(false);

        if (Upgradable(mit))
            MoneyUpgradeArrowImage.gameObject.SetActive(true);
        else MoneyUpgradeArrowImage.gameObject.SetActive(false);

        
        
        AddButtons();
    }

    protected override bool Upgradable(Item item)
    {
        int costs = GameManager.INSTANCE.DataProvider.GetCost(item.ObjectType.defenseType, item.ObjectType, item.Index);
        return GameManager.INSTANCE.gold >= costs;
    }
    
    private bool Upgradable(MissionItem item)
    {
        int costs = GameManager.INSTANCE.DataProvider.GetCost(item.ObjectType.defenseType, item.ObjectType, item.Index);
        return GameManager.INSTANCE.gold >= costs;
    }

    protected void AddButtons()
    {
        Button button = MoneyP.AddComponent<Button>();
        button.onClick.AddListener(() => MoneyButtonPressed());
        
        button = TimeP.AddComponent<Button>();
        button.onClick.AddListener(() => TimeButtonPressed());
    }

    public void TimeButtonPressed()
    {
        Parent.Selected(this, false);
        HighLightManager.highlight(TimeP.GetComponent<Image>());
    }

    public void MoneyButtonPressed()
    {
        Parent.Selected(this, true);
        HighLightManager.highlight(MoneyP.GetComponent<Image>());
    }


    private UpgradeType getUpgradeType(bool isTime, ObjectType type)
    {
        switch (type.objectType)
        {
            case GenericObjectType.JET:
                return isTime ? UpgradeType.JET_TIME : UpgradeType.JET_MONEY;
            case GenericObjectType.TANK:
                return isTime ? UpgradeType.TANK_TIME : UpgradeType.TANK_MONEY;
            case GenericObjectType.SHIP:
                return isTime ? UpgradeType.SHIP_TIME : UpgradeType.SHIP_MONEY;
            default:
                throw new ArgumentException("Wrong Caller?!");
        }
    }
}