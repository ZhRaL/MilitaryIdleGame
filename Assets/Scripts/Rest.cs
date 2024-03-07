using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Util;

public class Rest : IManageItem
{
    [SerializeField] private List<Toilet> _toilets;
    public override List<Item> Items => new(_toilets);
}
