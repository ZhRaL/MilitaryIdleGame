using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class RecruitmentController : MonoBehaviour, IController
{
    public const string SUFFIX_AMOUNT_EFFECTIVNESS = " $";
    public const string SUFFIX_AMOUNT_CAPACITY = "";

    public GameObject Rest1, Rest2, Rest3, Rest4;

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
            {
                Rest2.SetActive(true);
            }

            if (value >= 3)
            {
                Rest3.SetActive(true);
            }

            if (value >= 4)
            {
                Rest4.SetActive(true);
            }

            tx_Cap.text = value + SUFFIX_AMOUNT_CAPACITY;
        }
    }

    private void Start()
    {

    }

    public int[] getState()
    {
        int[] x = {CapLevel, EffLevel};
        Debug.Log("getState: " + x.ArrayToPrint());
        return x;
    }

    public void loadState(int[] state)
    {
        Debug.Log("loadState: " + state.ArrayToPrint());
        CapLevel = state[0];
        EffLevel = state[1];
    }

    public bool isObjectUnlocked(int i)
    {
        return i + 1 <= CapLevel;
    }

    public int getLevelLevel(int index)
    {
        throw new System.NotImplementedException();
    }

    public int getTimeLevel(int index)
    {
        throw new System.NotImplementedException();
    }

    public void upgrade_Level(int index)
    {
        throw new System.NotImplementedException();
    }

    public void upgrade_Time(int index)
    {
        throw new System.NotImplementedException();
    }

    public void BuyRest()
    {
        CapLevel++;
    }
}
