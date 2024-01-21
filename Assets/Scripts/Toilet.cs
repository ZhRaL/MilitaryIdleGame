using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

public class Toilet : MonoBehaviour, IGatherable
{
    public Rest rest;
    public bool Occupied;
    private Soldier _soldier;
    private float distanceSoldierGoDown = .3f;
    public RoutingPoint RoutingPoint;
    public bool unlocked;
    private int _level;

    public int Level
    {
        get => _level;
        set => _level = value;
    }

    public void SoldierSitDown(Soldier soldier)
    {
        _soldier = soldier;
        soldier.anim.SetBool("isRunning", false);
        Vector3 newPos = soldier.transform.position;
        newPos.y -= distanceSoldierGoDown;
        soldier.transform.position = newPos;
        GameObject rb = Instantiate(soldier.RadialBarPrefab, soldier.transform);
        rb.transform.rotation = Camera.main.transform.rotation;
        rb.GetComponent<RadialBar>().Initialize(rest.getWaitingAmount(), SoldierGetUp);
    }

    private void SoldierGetUp()
    {
        Occupied = false;
        Vector3 newPos = _soldier.transform.position;
        newPos.y += distanceSoldierGoDown;
        _soldier.transform.position = newPos;

        RoutingPoint.LetSoldierMove(_soldier);
        rest.ToiletFree();
        _soldier = null;
    }

    public void Upgrade()
    {
        Level++;
        logger.log("Toilet was upgraded to Level " + _level);
    }

    public int GetData()
    {
        return Level;
    }
}