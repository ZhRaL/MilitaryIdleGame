using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public const string SUFFIX_AMOUNT_EFFECTIVNESS = " $";
    public const string SUFFIX_AMOUNT_CAPACITY = " s";


    [SerializeField] private float k_Effect;
    [SerializeField] private float k_Capacity;

    public int Level_Reward;
    public int Level_Duration;

    public Text tx_RewardAmount, tx_Duration;
    public Text tx_UpgradeCostReward,tx_UpgradeCostDuration;
    public Text tx_Level_Reward, tx_Level_Duration;

    public RoutingPoint routingPoint;

    [Header("IdleGameMath")] 
    public float costRate, growthRate, costBase, growthBase;
    
    public float RewardAmount
    {
        get => k_Effect;
        set
        {
            k_Effect = value;
            tx_RewardAmount.text = k_Effect.ConvIntToString() + SUFFIX_AMOUNT_EFFECTIVNESS;
        }
    }

    public float Duration
    {
        get => k_Capacity;
        set
        {
            k_Capacity = value;
            tx_Duration.text = k_Capacity.ConvIntToString() + SUFFIX_AMOUNT_CAPACITY;
        }
    }

    public void LevelUp_Reward()
    {
        Level_Reward++;
        tx_Level_Reward.text = "" + Level_Reward;
        CalcAndUpdateReward();
    }

    private void CalcAndUpdateReward()
    {
        RewardAmount = growthBase * Mathf.Pow(growthRate, Level_Reward);
    }

    public void LevelUp_Duration()
    {
        Duration -= .5f;
        Level_Duration++;
    }

    public void MissionStart()
    {
        GetComponent<Animator>().SetTrigger("Mission_Start");
    }

    public void MissionEnd()
    {
        Reward();
        LetSoldierMove();
    }

    public float calculateDuration()
    {
        // Duration - AnimationDuration
        return Duration - 11;
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        GetComponent<Animator>().SetTrigger("Mission_End");
    }

    public void getBackDelayed()
    {
        StartCoroutine(ExecuteAfterTime(calculateDuration()));
    }

    public void Reward()
    {
        // TODO - Add Crit Value maybe
        GameManager.INSTANCE.gold += RewardAmount;
    }

    public void LetSoldierMove()
    {
        routingPoint.LetSoldierMove();
    }
}