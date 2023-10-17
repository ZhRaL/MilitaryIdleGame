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
    [FormerlySerializedAs("ObjectType")] public ObjectType objectType;
    [SerializeField] private int index = -1; // starts with 0

    [SerializeField] private TextMeshProUGUI _txLevel;
    [FormerlySerializedAs("upgradeImg")] public GameObject upgradeArrowImg;

    public Image IconChildImage;

    private void Start()
    {
        if (index == -1)
            index = transform.GetSiblingIndex();
        currentLevel = DataProvider.INSTANCE.GetLevel(defType, objectType, index);
        if(_txLevel!=null)
        _txLevel.text = "" + currentLevel;
        GameManager.INSTANCE.OnMoneyChanged += checkBalance;
        checkBalance();
    }

    public void OnClick()
    {
        var currentReward =
            Calculator.INSTANCE.getReward(new ObjDefEntity() { DefenseType = defType, ObjectType = objectType },
                currentLevel);
        var nextLevelReward =
            Calculator.INSTANCE.getReward(new ObjDefEntity() { DefenseType = defType, ObjectType = objectType },
                currentLevel + 1);
        var upgradeCost = Calculator.INSTANCE.getCost(
            new ObjDefEntity() { DefenseType = defType, ObjectType = objectType },
            currentLevel + 1);

        UpgradeDto dto = new UpgradeDto()
        {
            title = gameObject.name,
            Icon = IconChildImage.sprite,
            level = currentLevel,
            description = TextProvider.getDescription("chair"),
            upgradeAction = DataProvider.INSTANCE.getUpgradeMethod(defType, objectType, index),
            upgradeCost = (int) upgradeCost,
            currentReward = (int) currentReward,
            diffReward = nextLevelReward - currentReward
        };
        if(_txLevel!=null)
        dto.upgradeAction += () => _txLevel.text = "" + ++currentLevel;
        dto.upgradeAction += () => GameManager.INSTANCE.gold -= upgradeCost;
        dto.upgradeAction += () => OnClick();
        UpgradeScript.selectionChanged(dto);
        Image image = GetComponent<Image>();
        HighLightManager.highlight(image);
    }

    public void checkBalance()
    {
        var entity = new ObjDefEntity() { DefenseType = defType, ObjectType = objectType };
        // TODO - get Level of this index, first chair, second chair etc..
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.getCost(entity, currentLevel + 1))
            upgradeArrowImg.SetActive(true);
        else upgradeArrowImg.SetActive(false);
    }
}