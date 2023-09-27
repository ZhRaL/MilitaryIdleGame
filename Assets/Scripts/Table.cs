using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Util;

public class Table : MonoBehaviour
{
    public Chair[] chairs;
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

    public void Init(int amount, int level)
    {
        if (amount >= chairs.Length)
        {
            Debug.LogError("Amount greater than array Length");
            return;
        }

        for (int i = 0; i < chairs.Length; i++)
        {
            if (i < amount)
                chairs[i].Unlocked = true;
            else chairs[i].gameObject.SetActive(false);
        }

        unlockedChairs = amount;
        speed = level;
    }

    public float getWaitingAmount()
    {
        return 4f;
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
        Debug.Log("Try to Upgrade Chair Nr: "+index);
        if(index < chairs.Length)
            chairs[index].Upgrade();
    }

    public int GetLevelForTable(int index)
    {
        if (index < chairs.Length)
            return chairs[index].Level;
        return -1;
    }
}