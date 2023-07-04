using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Rest : MonoBehaviour
{
    public Toilet[] toilets;
    private int level;
    public int unlockedToilets;

    public int Level
    {
        get => level;
        set => level = value;
    }

    public Toilet getFreeToilet()
    {
        return toilets.FirstOrDefault(chair => chair.unlocked && !chair.Occupied);
    }

    public float getWaitingAmount()
    {
        return 4f;
    }

    public void Init(int amount, int level)
    {
        if (amount >= toilets.Length)
        {
            Debug.LogError("Amount greater than array Length");
            return;
        }
        
        for (int i = 0; i < toilets.Length; i++)
        {
            if(i<amount)
                toilets[i].unlocked = true;
            else toilets[i].gameObject.SetActive(false);
        }

        unlockedToilets = amount;
        Level = level;
    }
}
