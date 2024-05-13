using System;
using System.Collections;
using System.Collections.Generic;
using Quests;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Util;

public class Quest : MonoBehaviour
{
    private const string DESC_AMOUNT = "Schalten sie {0] mal <color=#D0D900>{1}</color> frei";
    private const string DESC_LEVEL = "Erreichen sie bei einem <color=#D0D900>{1}</color> Level {0}";
    private const string DESC_AMOUNT_LEVEL = "Erreichen sie {0} mal bei <color=#D0D900>{1}</color> ein Level von {2}";
    
    public TMP_Text tx_Description, tx_Progress, tx_Reward;
    
    public int rewardAmount;
    public QuestModel model;
    private string armyColor = "#27973CAA";
    private string airForceColor = "#B92E35AA";
    private string marineColor = "#443EADAA";

    public void Init(UnityAction<Quest> callBackQuestComplete, QuestModel model) {
        this.model=model;
        tx_Description.text = GetDescription(model.Requirement);
        tx_Reward.text = this.model.RewardAmount.ToString();
        Colorize(model.Requirement.reqObject.defenseType);
    }

    private void Colorize(DefenseType type)
    {
        var img = GetComponent<Image>();
        Color color;
        switch (type)
        {
            case DefenseType.ARMY:
                ColorUtility.TryParseHtmlString(armyColor, out color);
                break;
            case DefenseType.AIRFORCE:
                ColorUtility.TryParseHtmlString(airForceColor, out color);
                break;
            case DefenseType.MARINE:
                ColorUtility.TryParseHtmlString(marineColor, out color);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        img.color = color;
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


