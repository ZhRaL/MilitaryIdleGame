using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class VehicUnlock : MonoBehaviour
{
    public GameObject UnlockedPrefab, LockedPrefab;
    public ObjectType type;
    public UpgradeScript upgradeScript;
    
    // Start is called before the first frame update
    void Start()
    {
        int index1 = DataProvider.INSTANCE.GetLevel(DefenseType.MARINE, type, 1);
        int index2 = DataProvider.INSTANCE.GetLevel(DefenseType.MARINE, type, 2);
        GameObject child1 = transform.GetChild(1).gameObject;
        GameObject child2 = transform.GetChild(2).gameObject;

        if (index1 > 0) child1.SetActive(true);
        else editLocked(Instantiate(LockedPrefab, transform),child1, 1);
        
        if(index2>0) child2.gameObject.SetActive(true);
        else editLocked(Instantiate(LockedPrefab, transform),child2, 2);
        
    }

    private void editLocked(GameObject prefab,GameObject unlocker, int index)
    {
        DataCollector collector = prefab.GetComponent<DataCollector>();
        if (collector == null) throw new ArgumentException("No DataCollector Object");

        collector.UpgradeScript = upgradeScript;
        collector.objectType = type;
        collector.index = index;
        Unlocker uulock = prefab.GetComponent<Unlocker>();
        collector.unlocker = uulock;
        uulock.toUnlock = unlocker;
    }
}
