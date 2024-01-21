using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;
using Util;

public class Rest : MonoBehaviour
{
    public Toilet[] toilets;
    public int speed;
    public int unlockedToilets;

    public int index;

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

    public Toilet getFreeToilet()
    {
        return toilets.FirstOrDefault(toilet => toilet.unlocked && !toilet.Occupied);
    }

    public float getWaitingAmount()
    {
        return 4f;
    }

    public void Init(int amount, int level)
    {
        if (amount >= toilets.Length)
        {
            Debug.LogError("Amount greater than array Length");
            return;
        }
        
        for (int i = 0; i < toilets.Length; i++)
        {
            if(i<amount)
                toilets[i].unlocked = true;
            else toilets[i].gameObject.SetActive(false);
        }

        unlockedToilets = amount;
        speed = level;
    }


    
    public int GetLevelForToilet(int index)
    {
        if (index < toilets.Length)
            return toilets[index].Level;
        return -1;
    }

    public void PlaceSoldier(Soldier soldier)
    {
        Toilet targetToilet = getFreeToilet();
        if (targetToilet != null)
        {
            targetToilet.Occupied = true;
            moveSoldierTo(soldier, targetToilet.transform, () => targetToilet.SoldierSitDown(soldier));
        }
        else
        {
            WaitingService.addSoldier(soldier);
        }
    }
    
    public void ToiletFree()
    {
        Soldier freeS = WaitingService.Shift();
        if (freeS != null) PlaceSoldier(freeS);
    }
    
    private void moveSoldierTo(Soldier soldier, Transform target, Action executeWhenReached)
    {
        soldier.anim.SetBool("isRunning", true);
        _walkingSoldiers.Add(new SoldierWalkUtil(soldier, target, executeWhenReached, removeWalkingSoldier));
    }
    
    public void removeWalkingSoldier(SoldierWalkUtil walk)
    {
        _walkingSoldiers.Remove(walk);
    }
    
    public void BuyTable()
    {
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.getReward(
                new ObjDefEntity() { DefenseType = this.DefenseType, ObjectType = ObjectType.CHAIR }, unlockedToilets))
        {
            Toilet chair = toilets[unlockedToilets++];
            chair.Occupied = false;
            chair.unlocked = true;
            chair.gameObject.SetActive(true);
        }
    }

    public void LevelUpTable()
    {
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.getReward(
                new ObjDefEntity() { DefenseType = this.DefenseType, ObjectType = ObjectType.CHAIR }, unlockedToilets))
        {
            speed++;
        }
    }

    public void UpgradeChair(int index)
    {
        logger.log("Try to Upgrade Chair Nr: "+index);
        if(index < toilets.Length)
            toilets[index].Upgrade();
    }
    
}
