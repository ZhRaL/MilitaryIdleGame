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
            .Where(g => g.level > 0)
            .Sum(gather => gather.reward);
        
        tmp.text = ""+value;
    }
}
