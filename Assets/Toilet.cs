using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    public Rest rest;
    public bool Occupied;
    private Soldier _soldier;
    private float distanceSoldierGoDown = .3f;
    public RoutingPoint RoutingPoint;
    public bool unlocked;
    
    
    public void SoldierSitDown(Soldier soldier)
    {
        _soldier = soldier;
        soldier.anim.SetBool("isRunning",false);
        Vector3 newPos = soldier.transform.position;
        newPos.y -= distanceSoldierGoDown;
        soldier.transform.position = newPos;
        GameObject rb = Instantiate(soldier.RadialBarPrefab, soldier.transform);
        rb.transform.rotation = Camera.main.transform.rotation;
        rb.GetComponent<RadialBar>().Initialize(rest.getWaitingAmount(),SoldierGetUp);
    }

    private void SoldierGetUp()
    {
        Occupied = false;
        Vector3 newPos = _soldier.transform.position;
        newPos.y += distanceSoldierGoDown;
        _soldier.transform.position = newPos;

        RoutingPoint.LetSoldierMove(_soldier);
        _soldier = null;
    }
}
