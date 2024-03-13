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

    public void initializePreview(Item item)
    {
        IconProvider iconProvider = GameManager.INSTANCE.DataProvider.IconProvider;

        iconImage.sprite = iconProvider.GetIcon(item.ToUpgradeType());
        tx_level.text = "" + item.Level;
        
        if (Upgradable(item))
            upgradeArrowImage.gameObject.SetActive(true);
        else upgradeArrowImage.gameObject.SetActive(false);
    }

    private bool Upgradable(Item item)
    {
        int costs = GameManager.INSTANCE.DataProvider.GetCost(item.ObjectType.defenseType, item.ObjectType, item.Index);
        return GameManager.INSTANCE.gold >= costs;
    }
}