using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AirforceController : MonoBehaviour,IController
{
    public GameObject[] JetPrefabs;

    public Jet Jet1, Jet2, Jet3;

    public GameObject Baustelle_1, Baustelle_2;

    private void Start()
    {
        Jet2.gameObject.SetActive(false);
        Jet3.gameObject.SetActive(false);
    }

    public int[] getState()
    {
        int[] x = {Jet1.Cap_Level, Jet1.Eff_Level, Jet2.Cap_Level, Jet2.Eff_Level, Jet3.Cap_Level, Jet3.Eff_Level};
        Debug.Log("getState: "+x.ArrayToPrint());
        return x;

    }
    public void loadState(int[] state)
    {
        Debug.Log("loadState: "+state.ArrayToPrint());
        Jet1.Cap_Level = state[0];
        Jet1.Eff_Level = state[1];

        if (state[2] != 0)
        {
            Jet2.gameObject.SetActive(true);
            Jet2.Cap_Level = state[2];
            Jet2.Eff_Level = state[3];
        }
        // Spawn "Under Contruction"
        else
        {
            Baustelle_1.SetActive(true);
        }

        if (state[4] != 0)
        {
            Jet3.gameObject.SetActive(true);
            Jet3.Cap_Level = state[4];
            Jet3.Eff_Level = state[5];
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
            return (Jet2.Eff_Level > 0);
        if (i == 2)
            return Jet3.Eff_Level > 0;
        return false;

    }

    public void BuySecondRunway()
    {
        Baustelle_1.SetActive(false);
        Jet2.gameObject.SetActive(true);
        Jet2.Cap_Level = 1;
        Jet2.Eff_Level = 1;
        GameManager.INSTANCE.SaveGame();
    }
    public void BuyThirdRunway()
    {
        Baustelle_2.SetActive(false);
        Jet3.gameObject.SetActive(true);
        Jet3.Cap_Level = 1;
        Jet3.Eff_Level = 1;
    }
}
