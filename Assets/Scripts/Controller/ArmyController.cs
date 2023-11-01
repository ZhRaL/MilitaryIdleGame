using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class ArmyController : MonoBehaviour, IController
{
    public List<Tank> tanks;
    private List<SoldierWalkUtil> _walkingSoldiers = new List<SoldierWalkUtil>();

    public GameObject Baustelle_1_Prefab;
    private Vector3 positionOffset = new (-0.05f, -0.78f, -0.12f);
    
    public WaitingService WaitingService;
    public Transform waitingPosParent;
   
    private void Start()
    {
        WaitingService = new WaitingService(waitingPosParent);
    }
    private void Update()
    {
        var copyOfWalkingSoldiers = new List<SoldierWalkUtil>(_walkingSoldiers);
        copyOfWalkingSoldiers.ForEach(soldierWalkUtil => soldierWalkUtil.Update());
        WaitingService.Update();
    }

    public int[] getState()
    {
        return tanks.Select(tank => new[] { tank.rewardLevel, tank.durationLevel }).SelectMany(arr => arr).ToArray();
    }

    public void loadState(int[] state)
    {
        if (state.Length != 6) throw new ArgumentException("illegal amount");

        int index = 0;

        foreach (var tank in tanks)
        {
            if (!tank.Init(state[index++], state[index++]))
            {
                Instantiate(Baustelle_1_Prefab, tank.transform.position + positionOffset, Quaternion.Euler(0, 140, 0));
                tank.gameObject.SetActive(false);
            }
        }
    }

    public bool isObjectUnlocked(int i)
    {
        throw new NotImplementedException();
    }

    public int getLevelLevel(int index)
    {
        if (index < tanks.Count)
            return tanks[index].rewardLevel;
        throw new ArgumentException("invalid index " + index);
    }
    
    public int getTimeLevel(int index)
    {
        if (index < tanks.Count)
            return tanks[index].durationLevel;
        throw new ArgumentException("invalid index " + index);
    }

    public void upgrade_Level(int index)
    {
        if (index >= tanks.Count)
            throw new ArgumentException("invalid index " + index);
        Tank ship = tanks[index];
        ship.LevelUpReward();
    }

    public void upgrade_Time(int index)
    {
        if (index >= tanks.Count)
            throw new ArgumentException("invalid index " + index);
        Tank ship = tanks[index];
        ship.LevelUpDuration();
    }

    public void PlaceSoldier(Soldier soldier)
    {
        Tank tank = getFreeTank();
        if (tank != null)
        {
            tank.occupied = true;
            moveSoldierTo(soldier, tank.waypoints, () => tank.soldierEntry(soldier));
        }
        else
        {
            WaitingService.addSoldier(soldier);
        }
    }

    private Tank getFreeTank()
    {
        return tanks.FirstOrDefault(tank => tank.unlocked && !tank.occupied);
    }

    private void moveSoldierTo(Soldier soldier, Transform[] wayPoints, Action executeWhenReached)
    {
        _walkingSoldiers.Add(
            new SoldierWalkUtil(soldier, null, executeWhenReached, removeWalkingSoldier, .2f, wayPoints));
    }

    public void removeWalkingSoldier(SoldierWalkUtil walk)
    {
        _walkingSoldiers.Remove(walk);
    }

    public void TankFree()
    {
        Soldier freeS = WaitingService.Shift();
        if(freeS!=null) PlaceSoldier(freeS);
    }
}
