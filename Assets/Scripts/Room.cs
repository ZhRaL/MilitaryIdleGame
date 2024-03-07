using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Util;

public class Room : IManageItem
{
    [SerializeField] private List<Bed> beds;
    public override List<Item> Items => new(beds);
    
}