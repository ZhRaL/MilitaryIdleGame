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
    public bool isBath;
    
    // Start is called before the first frame update
    void Start()
    {
       BathController BathController = (BathController) GameManager.INSTANCE.BathController;
        if (isBath)
        {
            GameObject Bchild1 = transform.GetChild(0).gameObject;
            GameObject Bchild2 = transform.GetChild(1).gameObject;
            GameObject Bchild3 = transform.GetChild(2).gameObject;
            GameObject Bchild4 = transform.GetChild(3).gameObject;
            GameObject avail1 = transform.GetChild(4).gameObject;
            GameObject avail2 = transform.GetChild(5).gameObject;
            GameObject avail3 = transform.GetChild(6).gameObject;


            if (BathController.Rest2.unlockedToilets > 0)
            {
                Bchild2.SetActive(true);
            }
            else
            {
                avail1.SetActive(true);
            }

            if (BathController.Rest3.unlockedToilets > 0)
            {
                Bchild3.SetActive(true);
            }
            else
            {
                avail2.SetActive(true);
            }

            if (BathController.Rest4.unlockedToilets > 0)
            {
                Bchild4.SetActive(true);
            }
            else
            {
                avail3.SetActive(true);
            }


            return;
        }
        // Else: Place under-construction-signs
        
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
