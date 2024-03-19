using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Util;

public class RecruitTemplate : MonoBehaviour
{
    [SerializeField]
    private TMP_Text tx_name;

    public Soldier Soldier { get; set; }

    public GameObject movSpeedPrefab, missionRewardPrefab, critPrefab;

    public void Init(Soldier soldier)
    {
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
        Soldier.LVL_Speed++;
        logger.log("SOldier "+Soldier.name+" Upgraded Speed to "+script.Item.Level);
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
}
