using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Util;

public class Bed : MonoBehaviour
{
    public bool unlocked;
    public bool occupied;
    public RoutingPoint RoutingPoint;
    public int level = 0;
    private Soldier _soldier;
    public List<Transform> routList;
    private SoldierWalkUtil wayBack;
    public Room room;

    public int Level
    {
        get => level;
        set
        {
            if (value > 0) unlocked = true;
            level = value;
        }
    }

    public List<Transform> RoutList
    {
        get
        {
            if(!routList.Contains(transform))
                routList.Add(transform);
            return routList;
        }
        set => routList = value;
    }

    public void SoldierLayDown(Soldier soldier)
    {
        _soldier = soldier;
        soldier.anim.SetBool("isRunning", false);
        // TODO - Lay Animation
        GameObject rb = Instantiate(soldier.RadialBarPrefab, soldier.transform);
        rb.transform.rotation = Camera.main.transform.rotation;
        rb.GetComponent<RadialBar>().Initialize(getWaitingAmount(), SoldierGetUp);
    }

    private void SoldierGetUp()
    {
        occupied = false;
        room.BedFree();
        _soldier.anim.SetBool("isRunning", true);
        wayBack = new SoldierWalkUtil(_soldier, null, () => RoutingPoint.LetSoldierMove(_soldier), RemoveWayBack, .2f,
            RoutList.ToArray().Reverse().ToArray());
    }

    public float getWaitingAmount()
    {
        return 5f;
    }
    
    private void RemoveWayBack(SoldierWalkUtil util)
    {
        wayBack = null;
    }
    
    private void Update()
    {
        wayBack?.Update();
    }
    public void Upgrade()
    {
        Level++;
         logger.log("Bed was upgraded to Level "+Level);
    }
}