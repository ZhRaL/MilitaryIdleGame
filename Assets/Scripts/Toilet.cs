using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

public class Toilet : Item, IGatherable
{
    public int GetData()
    {
        return Level;
    }
}