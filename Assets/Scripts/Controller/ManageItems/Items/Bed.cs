using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Interfaces;
using UnityEngine;
using Util;

public class Bed : Item
{
    public Transform bedTransform;
    public int GetData()
    {
        return Level;
    }
}