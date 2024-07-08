using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoubleIncomeTimer : MonoBehaviour
{
    private float currentTime=0;

    public TMP_Text tx_Time;

    public void Activate(int duration)
    {
        currentTime += duration;
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
        
        int min = Mathf.FloorToInt(currentTime / 60);
        int sec = Mathf.FloorToInt(currentTime % 60);
        
        tx_Time.text = $"{min:00}:{sec:00}";

    }
}
