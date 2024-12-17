using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StatSum : MonoBehaviour
{
    public StatGather[] gatherers;
    public TMP_Text tmp;
    private void OnEnable()
    {
        float value = gatherers
            .Where(g => g.averageLevel > 0)
            .Sum(gather => gather.maxValue);
        
        tmp.text = Mathf.Round(value * 10f) / 10f + "";
    }
}
