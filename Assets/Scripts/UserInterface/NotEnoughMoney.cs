using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NotEnoughMoney : MonoBehaviour
{
    public TMP_Text textAmount;
    public TMP_Text tx_badge;
    public string standardText = "Do you want to Buy {0}?";
    public Button BadgeButton, AdButton;


    private int badgeAmount;
    private int moneyAmount;
    public void InitMissingMoney(int amount)
    {
        moneyAmount = amount;
        textAmount.text = string.Format(standardText, amount.ConvertBigNumber());
        badgeAmount = 20; // TODO Caluclate
        tx_badge.text = "" + badgeAmount.ConvertBigNumber();

        InitBadgeButton();
        InitAdButton();
    }

    private void InitAdButton()
    {
        // TODO
    }

    private void InitBadgeButton()
    {
        if (GameManager.INSTANCE.Badges >= badgeAmount)
        {
            BadgeButton.interactable = true;
            BadgeButton.onClick.AddListener(OnBadgeclick);
        }
        else
        {
            BadgeButton.interactable = false;
            BadgeButton.onClick.RemoveAllListeners();
        }
    }

    private void OnBadgeclick()
    {
        GameManager.INSTANCE.Gold += moneyAmount;
        GameManager.INSTANCE.Badges -= badgeAmount;
        Destroy(this);
    }

    private void OnAdClick()
    {
        Debug.Log("Lets look ADS!");
    }
    
}
