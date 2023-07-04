using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Toilet : MonoBehaviour
{
    public Rest rest;
    public bool Occupied;
    private Soldier _soldier;
    private float distanceSoldierGoDown = .3f;
    public RoutingPoint RoutingPoint_AirF,RoutingPoint_Marine,RoutingPoint_Army;
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

        switch (_soldier.SoldierType)
        {
            case Soldier.SoldierTypeEnum.MARINE : RoutingPoint_Marine.LetSoldierMove(_soldier);
                break;
            case Soldier.SoldierTypeEnum.ARMY : RoutingPoint_Army.LetSoldierMove(_soldier);
                break;
            case Soldier.SoldierTypeEnum.AIRFORCE : RoutingPoint_AirF.LetSoldierMove(_soldier);
                break;

        }
        _soldier = null;
    }
}
