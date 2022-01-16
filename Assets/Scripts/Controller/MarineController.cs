using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MarineController : MonoBehaviour, IController
{
    public GameObject[] ShipPrefabs;

    public Ship Ship1, Ship2, Ship3;

    public GameObject Baustelle_1, Baustelle_2;

    private void Start()
    {
        Ship2.gameObject.SetActive(false);
        Ship3.gameObject.SetActive(false);
    }

    public int[] getState()
    {
        int[] x = {Ship1.Level_Duration, Ship1.Level_Reward, Ship2.Level_Duration, Ship2.Level_Reward, Ship3.Level_Duration, Ship3.Level_Reward};
        Debug.Log("getState: "+x.ArrayToPrint());
        return x;

    }
    public void loadState(int[] state)
    {
        Debug.Log("loadState: "+state.ArrayToPrint());
        Ship1.Level_Duration = state[0];
        Ship1.Level_Reward = state[1];

        if (state[2] != 0)
        {
            Ship2.gameObject.SetActive(true);
            Ship2.Level_Duration = state[2];
            Ship2.Level_Reward = state[3];
        }
        // Spawn "Under Contruction"
        else
        {
            Baustelle_1.SetActive(true);
        }

        if (state[4] != 0)
        {
            Ship3.gameObject.SetActive(true);
            Ship3.Level_Duration = state[4];
            Ship3.Level_Reward = state[5];
        }
        // Spawn "Under Contruction"
        else
        {
            Baustelle_2.SetActive(true);
        }
    }

    public bool isObjectUnlocked(int i)
    {
        if (i == 1)
            return (Ship2.Level_Reward > 0);
        if (i == 2)
            return Ship3.Level_Reward > 0;
        return false;

    }

    public void BuySecondPier()
    {
        Baustelle_1.SetActive(false);
        Ship2.gameObject.SetActive(true);
        Ship2.Level_Duration = 1;
        Ship2.Level_Reward = 1;
        GameManager.INSTANCE.SaveGame();
    }
    public void BuyThirdPier()
    {
        Baustelle_2.SetActive(false);
        Ship3.gameObject.SetActive(true);
        Ship3.Level_Duration = 1;
        Ship3.Level_Reward = 1;
    }
    
    
}