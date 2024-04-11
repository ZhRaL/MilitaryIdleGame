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
        DoubleSelect = parent.DoubleSelect;
        
        IconProvider iconProvider = GameManager.INSTANCE.DataProvider.IconProvider;
        
        Item = item;
        MissionItem mit = (MissionItem)item;
        
        if (item.Level > 0)
        {
            // Initialize Time Icon

            iconImage.sprite = iconProvider.GetIcon(getUpgradeType(true, parent.ObjType));
            tx_level.text = "" + item.Level;
            item.OnLevelUp += ResetOnLevelUpdate;
            
            // Initialize Money Icon

            MoneyIconImage.sprite = iconProvider.GetIcon(getUpgradeType(false, parent.ObjType));
            MoneyTx_Level.text = "" + mit.MoneyLevel;
            mit.OnMoneyLevelUp += ResetOnMoneyLevelUpdate;
        }
        else
        {
            iconImage.sprite = iconProvider.GetIcon(UpgradeType.LOCKED);
            tx_level.text = "";
            item.OnLevelUp += ResetOnLevelUpdate;
            
            MoneyP.SetActive(false);
            
        }
        
        // Baustellen Schild verschwinden bei Kauf
        // Beide Buttons erscheinen bei Unlocking
        // PlayerPrefsHelper für Oerview und GetAllKeys
        
        VehicleText.text = parent.ObjType.objectType + " " + item.Index;
        

        if (Upgradable(item))
            upgradeArrowImage.gameObject.SetActive(true);
        else upgradeArrowImage.gameObject.SetActive(false);

        if (Upgradable(mit))
            MoneyUpgradeArrowImage.gameObject.SetActive(true);
        else MoneyUpgradeArrowImage.gameObject.SetActive(false);
        
        AddButtons();
    }
    
    private protected void ResetOnMoneyLevelUpdate(int newlevel)
    {
        MoneyTx_Level.text = "" + newlevel;
        

        if (Upgradable((MissionItem)Item))
            MoneyUpgradeArrowImage.gameObject.SetActive(true);
        else MoneyUpgradeArrowImage.gameObject.SetActive(false);

    }

    protected override bool Upgradable(Item item)
    {
        int costs = GameManager.INSTANCE.DataProvider.GetCost(item.ObjectType.defenseType, item.ObjectType, item.Index);
        return GameManager.INSTANCE.Gold >= costs;
    }
    
    private bool Upgradable(MissionItem item)
    {
        int costs = GameManager.INSTANCE.DataProvider.GetCost(item.ObjectType.defenseType, item.ObjectType, item.Index);
        return GameManager.INSTANCE.Gold >= costs;
    }

    protected void AddButtons()
    {
        
        var button = MoneyP.AddComponent<Button>();
        button.onClick.AddListener(() => MoneyButtonPressed());
        
        var button2 = TimeP.AddComponent<Button>();
        button2.onClick.AddListener(() => TimeButtonPressed());
    }

    public void TimeButtonPressed()
    {
        DoubleSelect(this,false);
        HighLightManager.highlight(TimeP.GetComponent<Image>());
    }

    public void MoneyButtonPressed()
    {
        DoubleSelect(this, true);
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