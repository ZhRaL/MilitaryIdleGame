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


    void OnEnable()
    {
        index = transform.GetSiblingIndex();
        title.text = type.toFriendlyName() + " " + (index + 1);
        int level = DataProvider.INSTANCE.GetLevel(DefenseType.ARMY, type, index);
        if (level == 0) hideStat();
        tx_reward.text = "" + Calculator.INSTANCE.getReward(new ObjDefEntity() { ObjectType = type }, level);
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