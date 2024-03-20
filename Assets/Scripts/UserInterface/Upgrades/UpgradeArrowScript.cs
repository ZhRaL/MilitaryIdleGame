using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArrowScript : MonoBehaviour
{
    private DataCollector[] child;
    void Start()
    {
        child = GetComponentsInChildren<DataCollector>();
        GameManager.INSTANCE.OnMoneyChanged += () =>
        {
            foreach (var bla in child)
            {
                bla.checkBalance();
            }
        };
    }


}
