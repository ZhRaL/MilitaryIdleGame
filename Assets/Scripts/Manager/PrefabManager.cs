using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject[] prefabs;

    public static PrefabManager Instance;

    private void Awake()
    {
        Instance ??= this;
    }

    public GameObject GetPrefabByString(string s)
    {
        return prefabs.First(e => e.name.Equals(s));
    }
}
