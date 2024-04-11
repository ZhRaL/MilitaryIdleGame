using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UpgradeDto
    {
        public Sprite IconBackground, Icon;
        public string title, description;
        public int level;
        public UnityAction upgradeAction;
        public int upgradeCost, currentReward; 
        public float diffReward;
        public Item item;
        public bool moneyItem;
    }
}