using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Util;

public class StatHighlighter : MonoBehaviour
{
    public TMP_Text lowest, highest;
    public Color lowestColor, highestColor;

    private void OnEnable()
    {
        highlight();
    }

    public void highlight()
    {
        var gatherers = GetComponentsInChildren<StatGather>();
        
        StatGather x = gatherers
            .Where(g => g.averageLevel > 0)
            .OrderBy(gather => gather.maxValue).First();
        
        if (x != null)
        {
            lowest = x.tx_reward;
            lowest.color = lowestColor;
        }

        StatGather y = gatherers
            .Where(g => g.averageLevel > 0)
            .OrderBy(gather => gather.maxValue).Last();
        
        if (y != null)
        {
            highest = y.tx_reward;
            highest.color = highestColor;
        }
    }
}
