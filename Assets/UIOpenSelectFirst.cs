using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpenSelectFirst : MonoBehaviour
{
    private void OnEnable()
    {
        var x = GetComponentInChildren<DataCollector>();
        x.OnClick();
    }
}
