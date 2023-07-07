using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Util;

public class Room : MonoBehaviour
{
    [SerializeField]
    private List<Bed> beds;
    private List<SoldierWalkUtil> _walkingSoldiers = new List<SoldierWalkUtil>();
    private int unlockedBeds;
    
    public WaitingService WaitingService;
    public Transform waitingPosParent;

    private void Start()
    {
        WaitingService = new WaitingService(waitingPosParent);
    }

    public void Init(int[] levels)
    {
        if (levels.Length > beds.Count) throw new ArgumentException("invalid Amount!");
        
        for (int i = 0; i < beds.Count; i++)
        {
            int level = levels[i];
            Bed currentBed = beds[i];
            if (level > 0)
            {
                currentBed.Level = level;
                unlockedBeds++;
            }
            else currentBed.gameObject.SetActive(false);
        }


    }

    private Bed GetNextFreeBed()
    {
        return beds.FirstOrDefault(chair => chair.unlocked && !chair.occupied);
    }

    public void PlaceSoldier(Soldier soldier)
    {
        Bed bed = GetNextFreeBed();
        if (bed != null)
        {
            bed.occupied = true;
            soldier.anim.SetBool("isRunning",true);
            _walkingSoldiers.Add(new SoldierWalkUtil(soldier, null, () => bed.SoldierLayDown(soldier), removeWalkingSoldier,.2f,bed.RoutList.ToArray()));
        }
        else
        {
            WaitingService.addSoldier(soldier);
        }
    }

    public void BedFree()
    {
        Soldier freeS = WaitingService.Shift();
        if(freeS!=null) PlaceSoldier(freeS);
    }
    
    private void removeWalkingSoldier(SoldierWalkUtil walk)
    {
        _walkingSoldiers.Remove(walk);
    }
    
    private void Update()
    {
        var copyOfWalkingSoldiers = new List<SoldierWalkUtil>(_walkingSoldiers);
        copyOfWalkingSoldiers.ForEach(soldierWalkUtil => soldierWalkUtil.Update());
        WaitingService.Update();
    }

    public int[] getState()
    {
        return beds.Select(bed => bed.Level).ToArray();
    }

    public void BuyBed()
    {
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.CalculateReward("SLAC", unlockedBeds))
        {
            Bed bed = beds[unlockedBeds++];
            bed.occupied = false;
            bed.unlocked = true;
            bed.gameObject.SetActive(true);
        }
    }

    public void LevelUpBeds()
    {
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.CalculateReward("SLSC", unlockedBeds))
        {
            beds.ForEach(bed => bed.Level++);
        }
        
    }
}