using System;
using System.Collections;
using System.Collections.Generic;
using Quests;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public const string DESC_AMOUNT = "Schalten sie {0] mal {1} frei";
    public const string DESC_LEVEL = "Erreichen sie Level von {0} bei {1}";
    public const string DESC_AMOUNT_LEVEL = "Erreichen sie {0} mal bei {1} ein Level von {2}";

    public int index;
    public TMP_Text tx_Description, tx_Progress, tx_Reward;
    
    public Requirement Requirement;
    public int rewardAmount;
    
    
    
    public static string GetDescription(Requirement req)
    {
        return req.reqType switch
        {
            ReqType.AMOUNT => String.Format(DESC_AMOUNT, req.reqType, req.reqObject.ToString()),
            ReqType.LEVEL => String.Format(DESC_LEVEL, req.amount, req.reqObject.ToString()),
            ReqType.AMOUNT_LEVEL => String.Format(DESC_AMOUNT_LEVEL, req.amount, req.reqObject.ToString(),
                req.levelAmount),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}


