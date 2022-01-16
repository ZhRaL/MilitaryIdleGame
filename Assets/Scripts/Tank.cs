using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    public const string SUFFIX_AMOUNT_EFFECTIVNESS = " $";
    public const string SUFFIX_AMOUNT_CAPACITY = " s";

    public bool isActive = false;
    [SerializeField]
    private float _reward;
    [SerializeField]
    private float _duration;

    public int Eff_Level;
    public int Cap_Level;
    
    public Text tx_k_Eff;
    public Text tx_k_Cap;

    public RoutingPoint routingPoint;
    private void Start()
    {
        Duration = Duration;
        RewardAmount = RewardAmount;
    }
    public float RewardAmount { get => _reward; set { _reward = value; tx_k_Eff.text = _reward.ConvIntToString() + SUFFIX_AMOUNT_EFFECTIVNESS; } }
    public float Duration { get => _duration; set { _duration = value; tx_k_Cap.text = _duration.ConvIntToString() + SUFFIX_AMOUNT_CAPACITY; } }

    public void K_Eff_LevelUp()
    {
        RewardAmount += 1;
    }

    public void K_CAP_LevelUp()
    {
        Duration -= .5f;
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
