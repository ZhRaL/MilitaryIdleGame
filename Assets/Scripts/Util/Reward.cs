using System;
using DefaultNameSpace;
using UnityEngine.Events;

namespace Util
{
    [Serializable]
    public class Reward
    {
        public int amount;
        public Enums.Rewards type;
        public UnityEvent SpecialAction;
        
        public void Checkout()
        {
            switch (type)
            {
                case Enums.Rewards.MONEY:
                    GameManager.INSTANCE.Gold += amount;
                    break;
                case Enums.Rewards.BADGES:
                    GameManager.INSTANCE.Badges += amount;
                    break;
                case Enums.Rewards.SPECIAL:
                    SpecialAction?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Please Specify concrete Class!!");
            }
        }
    }
}