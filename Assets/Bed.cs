using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public bool unlocked;
    public bool occupied;
    public RoutingPoint RoutingPoint;
    public int level = 0;
    private Soldier _soldier;

    public int Level
    {
        get => level;
        set
        {
            if (value > 0) unlocked = true;
            level = value;
        }
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
        RoutingPoint.LetSoldierMove(_soldier);
        _soldier = null;
    }

    public float getWaitingAmount()
    {
        return 5f;
    }
}