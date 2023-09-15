using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;

public class DataCollector : MonoBehaviour
{
    private int currentLevel;
    
    public UpgradeScript UpgradeScript;
    public DefenseType defType;
    public ObjectType ObjectType;
    private int index;  // starts with 0
    
    [SerializeField]
    private TextMeshProUGUI _txLevel;
    [FormerlySerializedAs("upgradeImg")] public GameObject upgradeArrowImg;

    public Image IconChildImage;
    private void Start()
    {
        index = transform.GetSiblingIndex();
        currentLevel = DataProvider.INSTANCE.getLevel(defType, ObjectType, index);
        _txLevel.text = ""+currentLevel;
        GameManager.INSTANCE.OnMoneyChanged += checkBalance;
        checkBalance();
    }
    
    public void OnClick()
    {
        UpgradeDto dto = new UpgradeDto()
        {
            title = gameObject.name,
            Icon = IconChildImage.sprite,
            level = currentLevel,
            description = TextProvider.getDescription("chair"),
            upgradeAction = DataProvider.INSTANCE.getUpgradeMethod(defType,ObjectType,index)
        };
        UpgradeScript.selectionChanged(dto);
    }

    public void checkBalance()
    {
        if (GameManager.INSTANCE.gold > 1000)
            upgradeArrowImg.SetActive(true);
        else upgradeArrowImg.SetActive(false);
    }
    
}
