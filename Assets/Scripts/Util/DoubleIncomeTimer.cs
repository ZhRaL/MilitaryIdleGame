using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoubleIncomeTimer : MonoBehaviour
{
    private float currentTime = 0;

    public TMP_Text tx_Time;

    public void Activate(int durationInSeconds)
    {
        currentTime += durationInSeconds;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (currentTime <= 0)
        {
            currentTime = 0;
            gameObject.SetActive(false);
            return;
        }

        currentTime -= Time.deltaTime;

        int hour = Mathf.FloorToInt(currentTime / 3600);
        int min = Mathf.FloorToInt(currentTime / 60);
        int sec = Mathf.FloorToInt(currentTime % 60);
        if (hour > 0)
            tx_Time.text = $"{hour:0}:{min:00}:{sec:00}";
        else
            tx_Time.text = $"{min:00}:{sec:00}";

    }
}
