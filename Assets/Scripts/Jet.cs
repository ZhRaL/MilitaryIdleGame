using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Jet : MonoBehaviour
{
    public RoutingPoint routingPoint;
    public bool unlocked;
    public bool occupied;
    private Soldier _soldier;

    public int rewardLevel;
    public int durationLevel;
    
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
        return 6f;
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
        GameManager.INSTANCE.gold += 10;
    }

    public void LetSoldierMove()
    {
        _soldier.gameObject.SetActive(true);
        routingPoint.LetSoldierMove(_soldier);
    }

    public void soldierEntry(Soldier soldier)
    {
        _soldier = soldier;
        MissionStart();
        soldier.gameObject.SetActive(false);
    }

    private void soldierExit()
    {
        occupied = false;
        _soldier.gameObject.SetActive(true);
    }

    private float getMissionDuration()
    {
        return 6f;
    }

    public bool Init(int reward, int duration)
    {
        if (reward <= 0 || duration <= 0) return false;
        
        rewardLevel = reward;
        durationLevel = duration;
        unlocked = true;
        return true;
    }
}
