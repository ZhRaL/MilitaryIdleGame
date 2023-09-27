using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class MarineController : MonoBehaviour, IController
{
    public List<Ship> ships;
    private List<SoldierWalkUtil> _walkingSoldiers = new ();

    public GameObject Baustelle_1_Prefab;
    private Vector3 positionOffset = new Vector3(4.33f, 2.08f, 15.73f);
    private Vector3 thirdShipOffSetToSECOND = new(-9.20f, 0.00f, 0.73f);

    public WaitingService WaitingService;
    public Transform waitingPosParent;
   
    private void Start()
    {
        WaitingService = new WaitingService(waitingPosParent);
    }
    
    private void Update()
    {
        var copyOfWalkingSoldiers = new List<SoldierWalkUtil>(_walkingSoldiers);
        copyOfWalkingSoldiers.ForEach(soldierWalkUtil => soldierWalkUtil.Update());
        WaitingService.Update();
    }

    public int[] getState()
    {
        return ships.Select(ship => new[] { ship.rewardLevel, ship.durationLevel }).SelectMany(arr => arr).ToArray();
    }

    public void loadState(int[] state)
    {
        if (state.Length != 6) throw new ArgumentException("illegal amount of" + state.Length);

        int index = 0;
        GameObject baustelle = null;
        foreach (var ship in ships)
        {
            if (!ship.Init(state[index++], state[index++]))
            {
                if (baustelle == null)
                {
                    Vector3 position = (index<5)?ship.transform.position + positionOffset:ship.transform.position + positionOffset+thirdShipOffSetToSECOND;
                    baustelle = Instantiate(Baustelle_1_Prefab, position,
                        Quaternion.Euler(0, 146.95f, 0));
                }

                ship.gameObject.SetActive(false);
            }
        }
    }

    public bool isObjectUnlocked(int i)
    {
        throw new NotImplementedException();
    }

    public int getLevelLevel(int index)
    {
        if (index < ships.Count)
            return ships[index].rewardLevel;
        throw new ArgumentException("invalid index " + index);
    }
    
    public int getTimeLevel(int index)
    {
        if (index < ships.Count)
            return ships[index].durationLevel;
        throw new ArgumentException("invalid index " + index);
    }

    public void upgrade_Level(int index)
    {
        if (index >= ships.Count)
            throw new ArgumentException("invalid index " + index);
        Ship ship = ships[index];
        ship.LevelUpReward();
    }

    public void upgrade_Time(int index)
    {
        if (index >= ships.Count)
            throw new ArgumentException("invalid index " + index);
        Ship ship = ships[index];
        ship.LevelUpDuration();
    }

    public void PlaceSoldier(Soldier soldier)
    {
        Ship ship = getFreeShip();
        if (ship != null)
        {
            ship.occupied = true;
            moveSoldierTo(soldier, ship.waypoints, () => ship.soldierEntry(soldier));
        }
        else
        {
            WaitingService.addSoldier(soldier);
        }
    }

    private Ship getFreeShip()
    {
        return ships.FirstOrDefault(ship => ship.unlocked && !ship.occupied);
    }

    private void moveSoldierTo(Soldier soldier, Transform[] waypoints, Action executeWhenReached)
    {
        _walkingSoldiers.Add(new SoldierWalkUtil(soldier, null, executeWhenReached, removeWalkingSoldier, .2f,
            waypoints));
    }

    public void removeWalkingSoldier(SoldierWalkUtil walk)
    {
        _walkingSoldiers.Remove(walk);
    }

    public void ShipFree()
    {
        Soldier freeS = WaitingService.Shift();
        if(freeS!=null) PlaceSoldier(freeS);
    }
}