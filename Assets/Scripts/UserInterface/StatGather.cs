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
    public DefenseType defType;
    [FormerlySerializedAs("reward")] public float value;
    public int level;


    void OnEnable()
    {
        level = DataProvider.INSTANCE.GetLevel(defType, type, 0);
        
        value = Calculator.INSTANCE.GetReward(type, level);
        tx_reward.text = "" + value;
    }
    
}