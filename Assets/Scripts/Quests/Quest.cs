using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
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
    public Button button;
    private UnityAction<Quest> completionAction;
    
    public int rewardAmount;
    public QuestModel model;
    
    private string armyColor = "#27973C";
    private string airForceColor = "#B92E35";
    private string marineColor = "#443EAD";

    public void Init(UnityAction<Quest> callBackQuestComplete, QuestModel model)
    {
        this.completionAction = callBackQuestComplete;
        this.model=model;
        tx_Description.text = GetDescription(model.Requirement);
        tx_Reward.text = this.model.RewardAmount.ToString();
        Colorize(model.Requirement.reqObject.defenseType);
        checkCompletion();
    }

    public void Complete()
    {
        completionAction.Invoke(this);
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

    public void checkCompletion()
    {
        checkProgress();
        if (model.Requirement.isFulFilled())
        {
            button.interactable = true;
            var reward = button.transform.GetChild(0);
            reward.GetChild(0).GetComponent<Image>().changeAlphaValue(1);
            reward.GetChild(1).GetComponent<TMP_Text>().changeAlphaValue(1);
            button.transform.GetChild(1).GetComponent<TMP_Text>().changeAlphaValue(1);
        }
        else
        {
            button.interactable = false;
            var reward = button.transform.GetChild(0);
            reward.GetChild(0).GetComponent<Image>().changeAlphaValue(.5f);
            reward.GetChild(1).GetComponent<TMP_Text>().changeAlphaValue(.5f);
            button.transform.GetChild(1).GetComponent<TMP_Text>().changeAlphaValue(.5f);
        }
    }

    private void checkProgress()
    {
        var man = GameManager.INSTANCE.GetTopLevel(model.Requirement.reqObject).GetItemManager(model.Requirement.reqObject.defenseType);
        string tx;
        switch (model.Requirement.reqType)
        {
            case ReqType.AMOUNT:
                tx = man.Items.Count(x => x.Unlocked) + " / " + model.Requirement.amount;
                break;
            case ReqType.LEVEL:
                tx = man.Items.Max(x => x.Level) + " / " + model.Requirement.amount;
                break;
            case ReqType.AMOUNT_LEVEL:
                tx = man.Items.Count(x => x.Unlocked && x.Level>=model.Requirement.levelAmount) + " / " + model.Requirement.amount;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        tx_Progress.text = tx;
    }
}


