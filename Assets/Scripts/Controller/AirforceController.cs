using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;
using Util;

public class AirforceController : MonoBehaviour, IController
{
    public List<Jet> jets;
    private List<SoldierWalkUtil> _walkingSoldiers = new List<SoldierWalkUtil>();

    public GameObject Baustelle_1_Prefab;
    private Vector3 positionOffset = new Vector3(-4.4f, -1.284768f, 0);

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
        return jets.Select(jet => new[] { jet.rewardLevel, jet.durationLevel }).SelectMany(arr => arr).ToArray();
    }

    public void loadState(int[] state)
    {
        if (state.Length != 6) throw new ArgumentException("illegal amount");

        int index = 0;

        foreach (var jet in jets)
        {
            if (!jet.Init(state[index++], state[index++]))
            {
                Instantiate(Baustelle_1_Prefab, jet.transform.position + positionOffset, Quaternion.Euler(0, 90, 0));
                jet.gameObject.SetActive(false);
            }
        }
    }

    public bool isObjectUnlocked(int i)
    {
        throw new NotImplementedException();
    }

    public void BuySecondRunway()
    {
        //  Baustelle_1.SetActive(false);
        //  Jet2.gameObject.SetActive(true);
        //  Jet2.Cap_Level = 1;
        //  Jet2.Eff_Level = 1;
        //  GameManager.INSTANCE.SaveGame();
    }


    public void PlaceSoldier(Soldier soldier)
    {

        Jet jet = getFreeJet();
        if (jet != null)
        {
            jet.occupied = true;
            moveSoldierTo(soldier, jet.waypoints, () => jet.soldierEntry(soldier));
        }
        else
        {
            WaitingService.addSoldier(soldier);
        }
    }

    private Jet getFreeJet()
    {
        return jets.FirstOrDefault(jet => jet.unlocked && !jet.occupied);
    }

    private void moveSoldierTo(Soldier soldier, Transform[] wayPoints, Action executeWhenReached)
    {
        _walkingSoldiers.Add(
            new SoldierWalkUtil(soldier, null, executeWhenReached, removeWalkingSoldier, .2f, wayPoints));
    }

    public void removeWalkingSoldier(SoldierWalkUtil walk)
    {
        _walkingSoldiers.Remove(walk);
    }
    public void JetFree()
    {
        Soldier freeS = WaitingService.Shift();
        if(freeS!=null) PlaceSoldier(freeS);
    }
}