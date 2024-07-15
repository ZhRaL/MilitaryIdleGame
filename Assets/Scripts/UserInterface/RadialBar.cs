using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;


public class RadialBar : MonoBehaviour
{
    public Image fill;
    public float amount = -1;

    public float currentValue, maxValue;
    private Action action;
    private ActionBefore before;

    public void Initialize(float maxValue, Action action)
    {
        this.maxValue = maxValue;
        this.action = action;
        currentValue = maxValue;
        amount = 1;
    }

    public void Initialize(float maxValue, Action action, ActionBefore before)
    {
        Initialize(maxValue, action);
        this.before = before;
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        if (amount <= -1) return;

        amount = currentValue / maxValue;

        if (amount > 0)
        {
            currentValue -= Time.deltaTime;
            fill.fillAmount = amount;

            if (before != null && currentValue < before.time)
            {
                before.Action.Invoke();
            }
        }
        else
        {
            Destroy(gameObject);
            action.Invoke();
        }
    }
}