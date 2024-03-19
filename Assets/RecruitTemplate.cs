using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecruitTemplate : MonoBehaviour
{
    private TMP_Text name;

    public Soldier Soldier { get; }

    public GameObject movSpeedPrefab, missionRewardPrefab, critPrefab;

    public RecruitTemplate(Soldier soldier)
    {
        Soldier = soldier;
        name.text = Soldier.name;
        // TODO - add correct Portrait

        var MoveIcon = movSpeedPrefab.GetComponent<IconScript>();
        MoveIcon.InitializePreview(SpeedUpgrade,null,Soldier.ToItem(Soldier.SoldierUpgradeType.SPEED));
    }

    private void SpeedUpgrade(IconScript script)
    {
        throw new System.NotImplementedException();
    }
}
