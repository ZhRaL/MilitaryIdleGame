using System;
using DefaultNameSpace;
using UnityEngine;
using Util;

public class ShopItem : MonoBehaviour
{
    public Reward Reward;
    public Cost Cost;

    public void Buy()
    {
        if (Validate())
            Reward.Checkout();
    }

    private bool Validate()
    {
        switch (Cost.type)
        {
            case Enums.Costs.MONEY:
                return InAppBuyManager.INSTANCE.Collect(Cost.amount);

            case Enums.Costs.ADVERTISMENT:
                return AdManager.INSTANCE.Show();

            case Enums.Costs.GOLD:
                GameManager.INSTANCE.Gold -= Cost.amount;
                return true;

            case Enums.Costs.BADGES:
                GameManager.INSTANCE.Badges -= Cost.amount;
                return true;

            default:
                throw new ArgumentOutOfRangeException("Not a valid Type");
        }

        return false;
    }
}
