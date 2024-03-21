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
        ToUpgrade(Soldier.LVL_Speed, () =>
        {
            Soldier.LVL_Speed++;
            script.Item.Level++;
        },Soldier.SoldierUpgradeType.SPEED); 
    } 
    
    private void RewardUpgrade(IconScript script)
    {
        ToUpgrade(Soldier.LVL_Reward, () => 
        {
            Soldier.LVL_Reward++;
            script.Item.Level++;
        },Soldier.SoldierUpgradeType.REWARD); 
    } 
    
    private void CritUpgrade(IconScript script)
    {
        ToUpgrade(Soldier.LVL_Crit, () => 
        {
            Soldier.LVL_Crit++;
            script.Item.Level++;
        },Soldier.SoldierUpgradeType.CRIT); 
    } 

    private void ToUpgrade(int level, UnityAction upgradeAction, Soldier.SoldierUpgradeType type) 
    {
        UpgradeScript.selectionChanged(new UpgradeDto
        {
            IconBackground = null,
            Icon = DataProvider.INSTANCE.IconProvider.GetIcon(UpgradeType.SOLDIER_SPEED),   // HardCoded
            title = "Soldier "+Soldier.Index,
            description = "TBD",
            level = level,
            upgradeAction = upgradeAction,
            upgradeCost = GameManager.INSTANCE.DataProvider.GetCost(Soldier.ToItem(type)), 
            currentReward = GameManager.INSTANCE.DataProvider.GetReward(Soldier.ToItem(type)), 
            diffReward = GameManager.INSTANCE.DataProvider.GetRewardDiff(Soldier.ToItem(type)), 
        });
    }
}




























