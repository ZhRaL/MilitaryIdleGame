using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class WaitingService 
{
    public Transform waitingPositionParent;

    private Queue<Soldier> soldiers= new ();
    private List<SoldierWalkUtil> _walkingSoldiers = new ();

    public WaitingService(Transform waitingPositionParent)
    {
        this.waitingPositionParent = waitingPositionParent;
    }

    public void addSoldier(Soldier soldier)
    {
        soldiers.Enqueue(soldier);
        soldier.anim.SetBool("isRunning", true);
        _walkingSoldiers.Add(
            new SoldierWalkUtil(soldier, getFreePos(), () => soldier.anim.SetBool("isRunning",false), removeWalkingSoldier));
    }

    public void Update()
    {
        var copyOfWalkingSoldiers = new List<SoldierWalkUtil>(_walkingSoldiers);
        copyOfWalkingSoldiers.ForEach(soldierWalkUtil => soldierWalkUtil.Update());
    }

    private Transform getFreePos()
    {
        return waitingPositionParent.GetChild(soldiers.Count-1);
    }
    
    public Soldier Shift()
    {
        if(soldiers.Count<=0) 
            return null;
        
            Soldier firstSoldier = soldiers.Dequeue();

        
        // List of soldiers left
        List<Soldier> tempSoldiers = soldiers.ToList();
        // clear list
        soldiers.Clear();
        _walkingSoldiers.Clear();
        // add "new"
        tempSoldiers.ForEach(soldier => addSoldier(soldier));

        firstSoldier.anim.SetBool("isRunning",true);
        return firstSoldier;
    }
    public void removeWalkingSoldier(SoldierWalkUtil walk)
    {
        _walkingSoldiers.Remove(walk);
    }
}