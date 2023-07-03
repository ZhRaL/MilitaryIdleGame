using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private List<Bed> beds;
    private List<SoldierWalkUtil> _walkingSoldiers = new List<SoldierWalkUtil>();


    public void Init(int[] levels)
    {
        if (levels.Length > beds.Count) throw new ArgumentException("invalid Amount!");
        
        for (int i = 0; i < beds.Count; i++)
        {
            int level = levels[i];
            Bed currentBed = beds[i];
            if (level > 0) currentBed.Level = level;
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
        bed.occupied = true;
        moveSoldierTo(soldier, bed.transform, () => bed.SoldierLayDown(soldier));
    }

    private void moveSoldierTo(Soldier soldier, Transform target, Action executeWhenReached)
    {
        _walkingSoldiers.Add(new SoldierWalkUtil(soldier, target, executeWhenReached, removeWalkingSoldier));
    }

    private void removeWalkingSoldier(SoldierWalkUtil walk)
    {
        _walkingSoldiers.Remove(walk);
    }
    
    private void Update()
    {
        var copyOfWalkingSoldiers = new List<SoldierWalkUtil>(_walkingSoldiers);
        copyOfWalkingSoldiers.ForEach(soldierWalkUtil => soldierWalkUtil.Update());
    }

    public int[] getState()
    {
        return beds.Select(bed => bed.Level).ToArray();
    }

    public void loadState(int[] state)
    {
        if (state.Length != 4) throw new ArgumentException("invalid amount");
        for (int i = 0; i < beds.Count; i++)
        {
            if (i > 0) beds[i].Level = i;
            else beds[i].gameObject.SetActive(false);
        }
        
    }

    public bool isObjectUnlocked(int i)
    {
        throw new NotImplementedException();
    }
}