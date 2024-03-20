using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Util;

public class RecruitTemplate : MonoBehaviour
{
    [SerializeField]
    private TMP_Text tx_name;

    public Soldier Soldier { get; set; }

    public GameObject movSpeedPrefab, missionRewardPrefab, critPrefab;

    public UpgradeScript UpgradeScript;
    
    public void Init(Soldier soldier, UpgradeScript upgradeScript)
    {
        UpgradeScript = upgradeScript;
        Soldier = soldier;
        tx_name.text = Soldier.name;
        // TODO - add correct Portrait

        var MoveIcon = movSpeedPrefab.GetComponent<IconScript>();
        MoveIcon.InitializePreview(SpeedUpgrade,null,Soldier.ToItem(Soldier.SoldierUpgradeType.SPEED));
        
        var RewardIcon = missionRewardPrefab.GetComponent<IconScript>();
        RewardIcon.InitializePreview(RewardUpgrade,null,Soldier.ToItem(Soldier.SoldierUpgradeType.REWARD));

        var CritIcon = critPrefab.GetComponent<IconScript>();
        CritIcon.InitializePreview(CritUpgrade,null,Soldier.ToItem(Soldier.SoldierUpgradeType.CRIT));

    }

    private void SpeedUpgrade(IconScript script)
    {
        ToUpgrade(Soldier.LVL_Speed, () => Soldier.LVL_Speed++);
    }
    
    private void RewardUpgrade(IconScript script)
    {
        Soldier.LVL_Reward++;
        logger.log("SOldier "+Soldier.name+" Upgraded Reward to "+script.Item.Level);
    }
    
    private void CritUpgrade(IconScript script)
    {
        Soldier.LVL_Crit++;
        logger.log("SOldier "+Soldier.name+" Upgraded Crit to "+script.Item.Level);
    }

    private void ToUpgrade(int level, UnityAction upgradeAction)
    {
        UpgradeScript.selectionChanged(new UpgradeDto
        {
            IconBackground = null,
            Icon = DataProvider.INSTANCE.IconProvider.GetIcon(UpgradeType.SOLDIER_SPEED),   // HardCoded
            title = "Soldier "+Soldier.Index,
            description = "TBD",
            level = level,
            upgradeAction = upgradeAction,
            upgradeCost = 0,
            currentReward = 1,
            diffReward = 2
        });
    }
}
