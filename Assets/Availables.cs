using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Availables : MonoBehaviour
{
    public GameObject unAvailablePrefab;
    public IController Controller;

    public Transform[] childs;
    private void Start()
    {
        childs = transform.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < childs.Length; i++)
        {
            var x = childs[i].GetComponentInChildren<DataCollector>();
            if (x == null)
            {
                
            }
        }
    }
    
    
}
