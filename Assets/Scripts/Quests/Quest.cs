using System;
using System.Collections;
using System.Collections.Generic;
using Quests;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Quest : MonoBehaviour
{
    public const string DESC_AMOUNT = "Schalten sie {0] mal {1} frei";
    public const string DESC_LEVEL = "Erreichen sie Level {0} bei {1}";
    public const string DESC_AMOUNT_LEVEL = "Erreichen sie {0} mal bei {1} ein Level von {2}";
    
    public TMP_Text tx_Description, tx_Progress, tx_Reward;
    
    public int rewardAmount;
    public QuestModel model;
    
    public void Init(UnityAction<Quest> callBackQuestComplete, QuestModel model) {
        this.model=model;
        tx_Description.text = GetDescription(model.Requirement);
        tx_Reward.text = this.model.RewardAmount.ToString();
    }
    
    public static string GetDescription(Requirement req)
    {
        return req.reqType switch
        {
            ReqType.AMOUNT => String.Format(DESC_AMOUNT, req.reqType, req.reqObject.objectType.ToString()),
            ReqType.LEVEL => String.Format(DESC_LEVEL, req.amount, req.reqObject.objectType.ToString()),
            ReqType.AMOUNT_LEVEL => String.Format(DESC_AMOUNT_LEVEL, req.amount, req.reqObject.objectType.ToString(),
                req.levelAmount),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}


