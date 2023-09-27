using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;
using Util;

public class Rest : MonoBehaviour
{
    public Toilet[] toilets;
    private int level;
    public int unlockedToilets;

    public DefenseType DefenseType;

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

    public void BuyToilet()
    {
        
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.getReward(new ObjDefEntity(){DefenseType = this.DefenseType, ObjectType = ObjectType.TOILET}, unlockedToilets))
        {
            Toilet toilet = toilets[unlockedToilets++];
            toilet.Occupied = false;
            toilet.unlocked = true;
            toilet.gameObject.SetActive(true);
        }
    }

    public void LevelUpSpeed()
    {
        if (GameManager.INSTANCE.gold > Calculator.INSTANCE.getReward(new ObjDefEntity(){DefenseType = this.DefenseType, ObjectType = ObjectType.TOILET}, unlockedToilets))
        {
            level++;
        }
    }
    
    public int GetLevelForTable(int index)
    {
        if (index < toilets.Length)
            return toilets[index].Level;
        return -1;
    }
}
