using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;

public class StatGather : MonoBehaviour
{
    public TMP_Text title, tx_reward;
    public ObjectType type;
    [FormerlySerializedAs("level")] public int averageLevel;
    public float currentValue, maxValue;
    public Image fillImage;


    void OnEnable()
    {
        averageLevel = DataProvider.INSTANCE.GetAverageLevel(type);
        currentValue = GameManager.INSTANCE.StatisticsManager.GetCurrentValues(type);
        maxValue = GameManager.INSTANCE.StatisticsManager.GetMaxValues(type,averageLevel);
        
        tx_reward.text = "" + maxValue;
        var capacity = currentValue / maxValue;
        fillImage.fillAmount = 1 - capacity;
    }
    
}