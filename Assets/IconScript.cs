using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class IconScript : MonoBehaviour
{
    public Image iconImage, upgradeArrowImage;
    public TMP_Text tx_level;
    public Item Item;
    private GimmeAName Parent;

    public void InitializePreview(GimmeAName parent, Item item)
    {
        Item = item;
        Parent = parent;
        
        IconProvider iconProvider = GameManager.INSTANCE.DataProvider.IconProvider;

        iconImage.sprite = iconProvider.GetIcon(item.ToUpgradeType());
        tx_level.text = "" + item.Level;
        
        if (Upgradable(item))
            upgradeArrowImage.gameObject.SetActive(true);
        else upgradeArrowImage.gameObject.SetActive(false);
        
        AddButton();
    }

    private bool Upgradable(Item item)
    {
        int costs = GameManager.INSTANCE.DataProvider.GetCost(item.ObjectType.defenseType, item.ObjectType, item.Index);
        return GameManager.INSTANCE.gold >= costs;
    }

    private void AddButton()
    {
        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener( () => Parent.Selected(this));
    }
    
}