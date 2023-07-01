using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RadialBar : MonoBehaviour
{
    public Image fill;
    public float amount = -1;

    public float currentValue, maxValue;
    private Action action;


    public void Initialize(float maxValue, Action action)
    {
        this.maxValue = maxValue;
        this.action = action;
        currentValue = maxValue;
        amount = 1;
    }

    private void Update()
    {
        if (amount <= -1) return;

        amount = currentValue / maxValue;

        if (amount > 0)
        {
            currentValue -= Time.deltaTime;
            fill.fillAmount = amount;
        }
        else
        {
            Destroy(this.gameObject);
            action.Invoke();
        }
    }
}