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
    public TextMeshProUGUI title, description;
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
        
        upgradeBtn.onClick.RemoveAllListeners();
        upgradeBtn.onClick.AddListener(upgrade.upgradeAction);
    }
}
