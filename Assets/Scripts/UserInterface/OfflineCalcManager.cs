using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfflineCalcManager : MonoBehaviour
{
    public TMP_Text amountText,tx_BadgeAmount;
    private int moneyEarnedAmount;
    public int badgeCost = 30;
    public Slider _Slider;
    public TMP_Text sliderText,tx_MaxOffline;
    private void Start()
    {
        moneyEarnedAmount = GameManager.INSTANCE.OfflineCalculator.GetOfflineAmount();
        var offlineTime = GameManager.INSTANCE.OfflineCalculator.GetOfflineTime();
        
        // Dont Calculate if just offline for a very short period of time
        if(offlineTime<100) 
            gameObject.SetActive(false);
        
        amountText.text = "" + moneyEarnedAmount;
        tx_BadgeAmount.text = "" + badgeCost;

        // Initialize-Advertisment
        var maxOfflineTime = GameManager.INSTANCE.OfflineCalculator.validOfflineTime;
        
        tx_MaxOffline.text += maxOfflineTime/3600+"h";
        
        _Slider.value = (float) offlineTime / maxOfflineTime;

        var hour = offlineTime / 3600;
        var minutes = offlineTime % 3600 / 60;

        sliderText.text = ((hour > 0) ? hour + "h " : "")+minutes+"min";
    }
    
    public void SelectSingle()
    {
        GameManager.INSTANCE.Gold += moneyEarnedAmount;
        gameObject.SetActive(false);
    }
    public void SelectDouble()
    {
        // If Ad-Manager is Ready -> Show Add
        
        // If Ad watched -> getDoubleIncome
        gameObject.SetActive(false);
    }
    public void SelectTriple()
    {
        if (GameManager.INSTANCE.Badges >= badgeCost)
        {
            GameManager.INSTANCE.Badges -= badgeCost;
            GameManager.INSTANCE.Gold += moneyEarnedAmount * 3;
            gameObject.SetActive(false);
        }
        else
        {
            // TODO: Load Shop and Buy Option
            gameObject.SetActive(false);
        }

    }
}
