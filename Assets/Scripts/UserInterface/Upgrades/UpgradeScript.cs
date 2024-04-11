using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class UpgradeScript : MonoBehaviour
{
    private UpgradeDto current;
    
    public Image IconBackground, Icon;
    public TextMeshProUGUI title, description, upgradeCost, currentReward, diffReward, sliderTx;
    public Slider slider;
    public Button upgradeBtn;
    private SliderValues _sliderValues => new (slider);

    private void Start()
    {
        GameManager.INSTANCE.OnMoneyChanged += checkBalance;
    }

    private void checkBalance()
    {
        if (GameManager.INSTANCE.Gold > 10)
            upgradeBtn.interactable = true;
        else upgradeBtn.interactable = false;
    }

    public void selectionChanged(UpgradeDto upgrade)
    {
        current = upgrade;
        if(upgrade.IconBackground!=null)
            IconBackground.sprite = upgrade.IconBackground;
        Sprite lockedSprite = GameManager.INSTANCE.DataProvider.IconProvider.GetIcon(UpgradeType.BIG_LOCKED);
        Icon.sprite = (upgrade.Icon == lockedSprite)?GameManager.INSTANCE.DataProvider.IconProvider.GetIcon(UpgradeType.LOCKED):upgrade.Icon;
        title.text = upgrade.title;
        description.text = upgrade.description;
        _sliderValues.setValue(upgrade.level);
        upgradeCost.text = ""+upgrade.upgradeCost;
        currentReward.text = "" + upgrade.currentReward;
        diffReward.text = "+ " + upgrade.diffReward;
        sliderTx.text = "Level " + upgrade.level;
        upgradeBtn.GetComponentInChildren<TMP_Text>().text = upgrade.level<=0 ? "UNLOCK": "UPGRADE";


        if (GameManager.INSTANCE.Gold < upgrade.upgradeCost)
        {
            upgradeBtn.interactable = false;
            upgradeBtn.onClick.RemoveAllListeners();
        }
        else
        {
            upgradeBtn.interactable = true;
            upgradeBtn.onClick.RemoveAllListeners();
            upgradeBtn.onClick.AddListener(() => GameManager.INSTANCE.Gold -= upgrade.upgradeCost);
            upgradeBtn.onClick.AddListener(Refresh);
            upgradeBtn.onClick.AddListener(upgrade.upgradeAction);
        }
    }

    private void Refresh()
    {
        current.level++;
        current.upgradeCost = GameManager.INSTANCE.DataProvider.GetCost(current.item, current.moneyItem);
        current.currentReward = GameManager.INSTANCE.DataProvider.GetReward(current.item, current.moneyItem);
        current.diffReward = GameManager.INSTANCE.DataProvider.GetRewardDiff(current.item, current.moneyItem);
        if (Icon.sprite == GameManager.INSTANCE.DataProvider.IconProvider.GetIcon(UpgradeType.LOCKED))
        {
            current.Icon = GameManager.INSTANCE.DataProvider.IconProvider.GetIcon(current.item.ToUpgradeType());
            Icon.sprite = current.Icon;
        }

        upgradeBtn.GetComponentInChildren<TMP_Text>().text = current.level<=0 ? "UNLOCK": "UPGRADE";
        
        selectionChanged(current);
    }
}
