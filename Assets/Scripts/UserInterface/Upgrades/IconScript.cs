using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class IconScript : MonoBehaviour
{
    public Image iconImage, upgradeArrowImage;
    public TMP_Text tx_level;
    public Item Item;
    private protected GimmeAName Parent;

    public delegate void parentSelected(IconScript script);
    public delegate void parentDoubleSelected(DoubleIconScript script,bool isMoney);

    public parentSelected SingleSelect;
    public parentDoubleSelected DoubleSelect;

    public virtual void InitializePreview(GimmeAName parent, Item item)
    {
        InitializePreview(parent.Selected,parent.DoubleSelect,item);
    }
    
    public virtual void InitializePreview(parentSelected singleSelect,parentDoubleSelected doubleSelect, Item item)
    {
        Item = item;
        SingleSelect = singleSelect;
        DoubleSelect = doubleSelect;

        IconProvider iconProvider = GameManager.INSTANCE.DataProvider.IconProvider;

        if (item.Level > 0)
        {
            iconImage.sprite = iconProvider.GetIcon(item.ToUpgradeType());
            tx_level.text = "" + item.Level;

        }

        else
        {
            iconImage.sprite = iconProvider.GetIcon(UpgradeType.LOCKED);
            tx_level.text = "";
        }
        
        item.OnLevelUp += ResetOnLevelUpdate;
        
        if (Upgradable(item))
            upgradeArrowImage.gameObject.SetActive(true);
        else upgradeArrowImage.gameObject.SetActive(false);

        AddButton();
        
    }

    private protected void ResetOnLevelUpdate(int newlevel)
    {
        if(newlevel==1)
            iconImage.sprite = GameManager.INSTANCE.DataProvider.IconProvider.GetIcon(Item.ToUpgradeType());
        tx_level.text = "" + newlevel;
        
        if (Upgradable(Item))
            upgradeArrowImage.gameObject.SetActive(true);
        else upgradeArrowImage.gameObject.SetActive(false);
    }

    protected virtual bool Upgradable(Item item)
    {
        int costs = GameManager.INSTANCE.DataProvider.GetCost(item.ObjectType.defenseType, item.ObjectType, item.Index);
        return GameManager.INSTANCE.Gold >= costs;
    }

    protected virtual void AddButton()
    {
        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(() => SingleSelect(this));
        button.onClick.AddListener(highlightMe);
    }

    public void highlightMe()
    {
        HighLightManager.highlight(GetComponent<Image>());
    }
}