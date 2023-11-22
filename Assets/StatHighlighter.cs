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
        logger.log("Highltighter+Enable");
        highlight();
    }

    public void highlight()
    {
        var gatherers = GetComponentsInChildren<StatGather>();
        
        StatGather x = gatherers
            .Where(g => g.level > 0)
            .OrderBy(gather => gather.reward).First();
        
        if (x != null)
        {
            lowest = x.tx_reward;
            lowest.color = lowestColor;
        }

        StatGather y = gatherers
            .Where(g => g.level > 0)
            .OrderBy(gather => gather.reward).Last();
        
        if (y != null)
        {
            highest = y.tx_reward;
            highest.color = highestColor;
        }
    }
}
