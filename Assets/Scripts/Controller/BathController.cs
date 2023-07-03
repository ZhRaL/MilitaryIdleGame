using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class BathController : MonoBehaviour, IController
{
    public Rest Rest1, Rest2, Rest3, Rest4;
    private List<SoldierWalkUtil> _walkingSoldiers = new List<SoldierWalkUtil>();

    public int[] getState()
    {
        return new[]
        {
            Rest1.unlockedToilets, Rest1.Level, Rest2.unlockedToilets, Rest2.Level,
            Rest3.unlockedToilets, Rest3.Level, Rest4.unlockedToilets, Rest4.Level
        };
    }

    public void loadState(int[] state)
    {
        if (state.Length != 8) throw new ArgumentException("Wrong Length of Array");

        Rest1.Init(state[0], state[1]);
        Rest2.Init(state[2], state[3]);
        Rest3.Init(state[4], state[5]);
        Rest4.Init(state[6], state[7]);

    }

    public bool isObjectUnlocked(int i)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        var copyOfWalkingSoldiers = new List<SoldierWalkUtil>(_walkingSoldiers);
        copyOfWalkingSoldiers.ForEach(soldierWalkUtil => soldierWalkUtil.Update());
    }

    public void PlaceSoldier(Soldier soldier)
    {
        Toilet targetToilet = null;

        foreach (var rest in getOrderedRests())
        {
            Toilet tempToilet = rest.getFreeToilet();
            if (tempToilet != null) targetToilet = tempToilet;
        }
        // TODO - not enough toilets for all soldiers

        targetToilet!.Occupied = true;
        moveSoldierTo(soldier, targetToilet.transform, () => targetToilet.SoldierSitDown(soldier));
    }

    private List<Rest> getOrderedRests()
    {
        return new List<Rest>
        {
            Rest1,
            Rest2,
            Rest3,
            Rest4
        }.OrderByDescending(rest => rest.Level).ToList();
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