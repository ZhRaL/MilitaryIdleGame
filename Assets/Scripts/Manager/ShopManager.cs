using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Icons[] icons;

    public Sprite GetSprite(string key)
    {
        return icons.ToList().FirstOrDefault(a => a.key == key).icon;
    }
}

[Serializable]
public struct Icons
{
    public string key;
    public Sprite icon;
}
