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
    private int index;  // starts with 0
    
    [SerializeField]
    private TextMeshProUGUI _txLevel;
    [FormerlySerializedAs("upgradeImg")] public GameObject upgradeArrowImg;

    public Image IconChildImage;
    private void Start()
    {
        index = transform.GetSiblingIndex();
        currentLevel = DataProvider.INSTANCE.getLevel(defType, objectType, index);
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
            upgradeAction = DataProvider.INSTANCE.getUpgradeMethod(defType,objectType,index)
        };
        
        UpgradeScript.selectionChanged(dto);
    }

    public void checkBalance()
    {
        var entity = new ObjDefEntity() { DefenseType = defType, ObjectType = objectType };
        // TODO - get Level of this index, first chair, second chair etc..
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.getCost(entity, currentLevel))
            upgradeArrowImg.SetActive(true);
        else upgradeArrowImg.SetActive(false);
    }
    
}
