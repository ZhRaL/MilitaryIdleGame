using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class Ship : MonoBehaviour
{

    public RoutingPoint routingPoint;
    public bool unlocked;
    public bool occupied;
    private Soldier _soldier;

    public int rewardLevel;
    public int durationLevel;
    public Transform[] waypoints;
    private SoldierWalkUtil wayBack;
    public MarineController _Controller;

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
        return 10 - Calculator.INSTANCE.getTimeReductionReward(durationLevel);
    }
    
    public float getTimeForRound()
    {
        // TODO - more specific value including animation Duration
        return 10;
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
        GameManager.INSTANCE.gold += Calculator.INSTANCE.getReward(new ObjDefEntity(){ObjectType = ObjectType.JET_AMOUNT},rewardLevel);

    }

    public void LetSoldierMove()
    {
        occupied = false;
        _soldier.gameObject.SetActive(true);
        _soldier.anim.SetBool("isRunning",true);
        wayBack = new SoldierWalkUtil(_soldier, null, () => routingPoint.LetSoldierMove(_soldier), RemoveWayBack, .3f,
            waypoints.Reverse().ToArray());
        
        _Controller.ShipFree();
    }

    private void RemoveWayBack(SoldierWalkUtil util)
    {
        wayBack = null;
    }

    private void Update()
    {
        wayBack?.Update();
    }

    public void soldierEntry(Soldier soldier)
    {
        _soldier = soldier;
        MissionStart();
        soldier.gameObject.SetActive(false);
    }
    
    public bool Init(int reward, int duration)
    {
        if (reward <= 0 && duration <= 0) return false;
        
        rewardLevel = reward;
        durationLevel = duration;
        unlocked = true;
        return true;
    }
    
    public void LevelUpReward()
    {
        if(rewardLevel==0) gameObject.SetActive(true);
            rewardLevel++;
    }

    public void LevelUpDuration()
    {
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.getReward(new ObjDefEntity(){ObjectType = ObjectType.JET_AMOUNT}, durationLevel))
        {
            durationLevel++;
        }
    }
}