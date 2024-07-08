using System;
using DefaultNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;

public class ShopItem : MonoBehaviour
{
    public Reward Reward;
    [FormerlySerializedAs("Cost")] public Cost cost;

    public Image costImage;
    public TMP_Text costText;
    private void Start()
    {
        if (costImage)
        {
            costImage.sprite = FindObjectOfType<ShopManager>().GetSprite(cost.type.ToString());
            costText.text = cost.amount.ToString();
        }
    }

    public void Buy()
    {
        if (!RichEnough())
            return;
        if (Validate())
            Reward.Checkout();
    }

    private bool Validate()
    {
        switch (cost.type)
        {
            case Enums.Costs.MONEY:
                return InAppBuyManager.INSTANCE.Collect(cost.amount);

            case Enums.Costs.ADVERTISMENT:
                return AdManager.INSTANCE.Show();

            case Enums.Costs.GOLD:
                GameManager.INSTANCE.Gold -= cost.amount;
                return true;

            case Enums.Costs.BADGES:
                GameManager.INSTANCE.Badges -= cost.amount;
                return true;

            default:
                throw new ArgumentOutOfRangeException("Not a valid Type");
        }
    }

    private bool RichEnough()
    {
        switch (cost.type)
        {
            case Enums.Costs.MONEY:
                return true;

            case Enums.Costs.ADVERTISMENT:
                return true;

            case Enums.Costs.GOLD:
                return GameManager.INSTANCE.Gold >= cost.amount;

            case Enums.Costs.BADGES:
                return GameManager.INSTANCE.Badges >= cost.amount;

            default:
                throw new ArgumentOutOfRangeException("Not a valid Type");
        }
    }
}
