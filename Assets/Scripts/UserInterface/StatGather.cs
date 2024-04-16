using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class StatGather : MonoBehaviour
{
    public TMP_Text title, tx_reward;
    private int index;
    public ObjectType type;
    public DefenseType defType;
    public float reward;
    public int level;


    void OnEnable()
    {
        index = transform.GetSiblingIndex();
        title.text = type.toFriendlyName() + " " + (index + 1);
        level = DataProvider.INSTANCE.GetLevel(defType, type, index);
        
        if (level == 0) hideStat();
        reward = Calculator.INSTANCE.GetReward(type, level);
        tx_reward.text = "" + reward;
    }

    private void hideStat()
    {
        var invis = gameObject.AddComponent(typeof(StatInvisibler)) as StatInvisibler;
        invis.img = GetComponent<Image>();
        invis.tx1 = title;
        invis.tx2 = tx_reward;
        invis.colorize();
    }
}