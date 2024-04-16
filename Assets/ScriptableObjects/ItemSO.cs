using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Util;

[CreateAssetMenu(menuName = "CreateItemSO")]
public class ItemSO : ScriptableObject
{
    private DataProvider dataProvider;
    
    public UpgradeDto UpgradeDto {
        get {
            dataProvider = GameManager.INSTANCE.DataProvider;

            var upgradeDto = new UpgradeDto
            {
             //   IconBackground = this.iconBackground,
             //   Icon = this.icon,
             //   title = this.itemName,
             //   description = this.description,
             //   level = DataProvider.INSTANCE.GetLevel(defenseType, objectType, index),
             //  // upgradeAction = DataProvider.INSTANCE.getUpgradeMethod(defenseType, objectType, index),
             //   upgradeCost = DataProvider.INSTANCE.GetCost( objectType, index),
             //   currentReward = DataProvider.INSTANCE.GetReward( objectType, index),
             //   diffReward = DataProvider.INSTANCE.GetRewardDiff( objectType, index)
            };
            return upgradeDto;
        }
    }
    
    public ObjectType objectType;
    public DefenseType defenseType;
    public string itemName;
    public string description;
    public Sprite icon;
    public Sprite iconBackground;

    private int Level;
    private int cost; 
    private int reward;
    private int rewardDiff;
    private int index;
}