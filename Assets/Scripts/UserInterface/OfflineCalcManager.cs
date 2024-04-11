using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OfflineCalcManager : MonoBehaviour
{
    public TMP_Text amountText,tx_BadgeAmount;
    private int amount;
    public int badgeCost = 30;

    private void Start()
    {
        var offlineCalculator = GameManager.INSTANCE.OfflineCalculator;

        amount = offlineCalculator.GetOfflineAmount();

        amountText.text = "" + amount;
        tx_BadgeAmount.text = "" + badgeCost;

        // Initialize-Advertisment
    }
    
    public void SelectSingle()
    {
        GameManager.INSTANCE.Gold += amount;
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
            GameManager.INSTANCE.Gold += amount * 3;
            gameObject.SetActive(false);
        }
        else
        {
            // Load Shop and Buy Option
            gameObject.SetActive(false);
        }

    }
}
