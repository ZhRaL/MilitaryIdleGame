using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using Util;

public class Table : MonoBehaviour, IManageItem
{
    [SerializeField] private List<Chair> chairs;
    public int speed;
    public int unlockedChairs;
    public Transform waitingPosParent;
    public DefenseType DefenseType;

    private List<SoldierWalkUtil> _walkingSoldiers = new();

    public WaitingService WaitingService;

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

    public Chair GetNextFreeChair()
    {
        return chairs.FirstOrDefault(chair => chair.Unlocked && !chair.Occupied);
    }

    public void Init(int[] levels)
    {
        if (levels.Length > chairs.Count) throw new ArgumentException("invalid Amount!");

        for (int i = 0; i < chairs.Count; i++)
        {
            int level = levels[i];
            Chair currentBed = chairs[i];
            if (level > 0)
            {
                currentBed.Level = level;
                unlockedChairs++;
            }
            else currentBed.gameObject.SetActive(false);
        }
    }

    public void PlaceSoldier()
    {
        throw new NotImplementedException();
    }

    public int GetLevelForItem(int index)
    {
        throw new NotImplementedException();
    }

    public void UpgradeItem(int index)
    {
        throw new NotImplementedException();
    }

    public UnityAction GetUpgradeMethod(int index)
    {
        throw new NotImplementedException();
    }

    public float GetAverageTime()
    {
        throw new NotImplementedException();
    }

    public int GetAmountOfUnlockedItems()
    {
        throw new NotImplementedException();
    }

    public void PlaceSoldier(Soldier soldier)
    {
        Chair targetChair = GetNextFreeChair();
        if (targetChair != null)
        {
            targetChair.Occupied = true;
            moveSoldierTo(soldier, targetChair.transform, () => targetChair.SoldierSitDown(soldier));
        }
        else
        {
            WaitingService.addSoldier(soldier);
        }
    }

    private void moveSoldierTo(Soldier soldier, Transform target, Action executeWhenReached)
    {
        soldier.anim.SetBool("isRunning", true);
        _walkingSoldiers.Add(new SoldierWalkUtil(soldier, target, executeWhenReached, removeWalkingSoldier));
    }

    public void ChairFree()
    {
        Soldier freeS = WaitingService.Shift();
        if (freeS != null) PlaceSoldier(freeS);
    }

    public void removeWalkingSoldier(SoldierWalkUtil walk)
    {
        _walkingSoldiers.Remove(walk);
    }

    public void BuyTable()
    {
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.getReward(
                new ObjDefEntity() { DefenseType = this.DefenseType, ObjectType = ObjectType.CHAIR }, unlockedChairs))
        {
            Chair chair = chairs[unlockedChairs++];
            chair.Occupied = false;
            chair.Unlocked = true;
            chair.gameObject.SetActive(true);
        }
    }

    public void LevelUpTable()
    {
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.getReward(
                new ObjDefEntity() { DefenseType = this.DefenseType, ObjectType = ObjectType.CHAIR }, unlockedChairs))
        {
            speed++;
        }
    }

    public void UpgradeChair(int index)
    {
        logger.log("Try to Upgrade Chair Nr: " + index);
        if (index < chairs.Count)
            chairs[index].Upgrade();
    }
    public int[] getState()
    {
        return chairs.Select(chair => chair.Level).ToArray();
    }

    public int GetLevelForTable(int index)
    {
        if (index < chairs.Count)
            return chairs[index].Level;
        return -1;
    }

    public float getAverageTime()
    {
        return chairs.Average(chair => chair.getTimeForRound()) 
            * chairs.Count();
    }
}