﻿using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class KitchenController : MonoBehaviour, IController
{
    public const string SUFFIX_AMOUNT_EFFECTIVNESS = " $";
    public const string SUFFIX_AMOUNT_CAPACITY = "";

    public GameObject Table1,Table2,Table3;

    private int _effLevel, _capLevel;
    public Text tx_Cap, tx_Eff;

    public int EffLevel
    {
        get => _effLevel;
        set => _effLevel = value;
    }

    public int CapLevel
    {
        get => _capLevel;
        set
        {
            _capLevel = value;

            if (value >= 2)
            {Table2.SetActive(true);
            }

            if (value >= 3)
            {
                Table3.SetActive(true);
            }
            tx_Cap.text = value + SUFFIX_AMOUNT_CAPACITY;
        }
    }

    private void Start()
    {
        Table2.gameObject.SetActive(false);
        Table3.gameObject.SetActive(false);
    }

    public int[] getState()
    {
        int[] x = {CapLevel,EffLevel};
        Debug.Log("getState: "+x.ArrayToPrint());
        return x;

    }
    public void loadState(int[] state)
    {
        Debug.Log("loadState: "+state.ArrayToPrint());
        CapLevel = state[0];
        EffLevel = state[1];
    }

    public bool isObjectUnlocked(int i)
    {
        return i+1<=CapLevel;

    }

    public void BuyTable()
    {
        CapLevel++;
    }    
}
