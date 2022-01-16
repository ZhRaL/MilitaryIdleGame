using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ArmyController : MonoBehaviour, IController
{
    public GameObject[] TankPrefabs;

    public Tank Tank1, Tank2, Tank3;

    public GameObject Baustelle_1, Baustelle_2;

    private void Start()
    {
        Tank2.gameObject.SetActive(false);
        Tank3.gameObject.SetActive(false);
    }

    public int[] getState()
    {
        int[] x = {Tank1.Cap_Level, Tank1.Eff_Level, Tank2.Cap_Level, Tank2.Eff_Level, Tank3.Cap_Level, Tank3.Eff_Level};
        Debug.Log("getState: "+x.ArrayToPrint());
        return x;

    }
    public void loadState(int[] state)
    {
        Debug.Log("loadState: "+state.ArrayToPrint());
        Tank1.Cap_Level = state[0];
        Tank1.Eff_Level = state[1];

        if (state[2] != 0)
        {
            Tank2.gameObject.SetActive(true);
            Tank2.Cap_Level = state[2];
            Tank2.Eff_Level = state[3];
        }
        // Spawn "Under Contruction"
        else
        {
            Baustelle_1.SetActive(true);
        }

        if (state[4] != 0)
        {
            Tank3.gameObject.SetActive(true);
            Tank3.Cap_Level = state[4];
            Tank3.Eff_Level = state[5];
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
            return (Tank2.Eff_Level > 0);
        if (i == 2)
            return Tank3.Eff_Level > 0;
        return false;

    }

    public void BuySecondWay()
    {
        Baustelle_1.SetActive(false);
        Tank2.gameObject.SetActive(true);
        Tank2.Cap_Level = 1;
        Tank2.Eff_Level = 1;
        GameManager.INSTANCE.SaveGame();
    }
    public void BuyThirdWay()
    {
        Baustelle_2.SetActive(false);
        Tank3.gameObject.SetActive(true);
        Tank3.Cap_Level = 1;
        Tank3.Eff_Level = 1;
    }
}
