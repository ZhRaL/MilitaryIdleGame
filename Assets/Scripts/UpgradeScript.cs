using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
    private UpgradeDto current;
    
    public Image IconBackground, Icon;
    public TextMeshProUGUI title, description, upgradeCost, currentReward, diffReward, sliderTx;
    public Slider slider;
    public Button upgradeBtn;

    private void Start()
    {
        GameManager.INSTANCE.OnMoneyChanged += checkBalance;
    }

    private void checkBalance()
    {
        if (GameManager.INSTANCE.gold > 10)
            upgradeBtn.interactable = true;
        else upgradeBtn.interactable = false;
    }

    public void selectionChanged(UpgradeDto upgrade)
    {
        if(upgrade.IconBackground!=null)
        IconBackground.sprite = upgrade.IconBackground;
        Icon.sprite = upgrade.Icon;
        title.text = upgrade.title;
        description.text = upgrade.description;
        slider.value = upgrade.level;
        upgradeCost.text = ""+upgrade.upgradeCost;
        currentReward.text = "" + upgrade.currentReward;
        diffReward.text = "+ " + upgrade.diffReward;
        sliderTx.text = "Level " + upgrade.level;

        if (GameManager.INSTANCE.gold < upgrade.upgradeCost)
        {
            upgradeBtn.interactable = false;
            upgradeBtn.onClick.RemoveAllListeners();
        }
        else
        {
            upgradeBtn.interactable = true;
            upgradeBtn.onClick.RemoveAllListeners();
            upgradeBtn.onClick.AddListener(upgrade.upgradeAction);
        }
    }
}