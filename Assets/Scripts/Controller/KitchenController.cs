using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KitchenController : MonoBehaviour, IController
{
    [SerializeField] private Table _tableArmy, _tableAirForce, _tableMarine;
    private List<SoldierWalkUtil> _walkingSoldiers = new List<SoldierWalkUtil>();
    /**
     * State: (ArmyCount,ArmyLevel,AirFCount,AirFLevel,MarCount,MarLevel)
     */
    public int[] getState()
    {
        return new[]
        {
            _tableArmy.unlockedChairs, _tableArmy.speed, _tableAirForce.unlockedChairs, _tableAirForce.speed,
            _tableMarine.unlockedChairs, _tableMarine.speed
        };
    }

    public void loadState(int[] state)
    {
        if (state.Length != 6) throw new ArgumentException("Wrong Length of Array");

        _tableArmy.Init(state[0], state[1]);
        _tableAirForce.Init(state[2], state[3]);
        _tableMarine.Init(state[4], state[5]);
    }

    public bool isObjectUnlocked(int i)
    {
        throw new System.NotImplementedException();
    }

    public void PlaceSoldier(Soldier soldier)
    {
        Debug.Log("type is:" + soldier.SoldierType);
        Chair targetChair = getTable(soldier.SoldierType).GetNextFreeChair();
        targetChair.Occupied = true;
        moveSoldierTo(soldier, targetChair.transform, () => targetChair.SoldierSitDown(soldier));
    }
    
    private void GetUp(Soldier soldier)
    {
        
    }


    private void Update()
    {
        var copyOfWalkingSoldiers = new List<SoldierWalkUtil>(_walkingSoldiers);
        copyOfWalkingSoldiers.ForEach(soldierWalkUtil => soldierWalkUtil.Update());
    }

    private Table getTable(Soldier.SoldierTypeEnum type)
    {
        if (type == Soldier.SoldierTypeEnum.ARMY) return _tableArmy;
        if (type == Soldier.SoldierTypeEnum.AIRFORCE) return _tableAirForce;
        if (type == Soldier.SoldierTypeEnum.MARINE) return _tableMarine;

        throw new ArgumentException("not a valid type");
    }

    private void moveSoldierTo(Soldier soldier, Transform target, Action executeWhenReached)
    {
        _walkingSoldiers.Add(new SoldierWalkUtil(soldier, target, executeWhenReached, removeWalkingSoldier));
    }

    public void removeWalkingSoldier(SoldierWalkUtil walk)
    {
        _walkingSoldiers.Remove(walk);
    }
}