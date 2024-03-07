using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Util;

public class Table : IManageItem
{
    [SerializeField] private List<Chair> _chairs;
    public List<Item> Items => new(_chairs);
    
}