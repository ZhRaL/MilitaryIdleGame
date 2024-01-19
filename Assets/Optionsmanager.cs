using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optionsmanager : MonoBehaviour
{

    public static Optionsmanager INSTANCE;
    public bool alwaysShowNamesOverSoldiers;

    private void Awake()
    {
        if (INSTANCE == null) INSTANCE = this;
    }
}
