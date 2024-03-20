using System;
using UnityEngine;
using System.Collections.Generic;
using Util;

[Serializable]
public class IconProvider
{
    [SerializeField] private List<IconEntry> iconEntries = new();

    public Sprite GetIcon(UpgradeType key)
    {
        return iconEntries.Find(s => s.key == key).icon;
    }
}

[Serializable]
public class IconEntry
{
    public UpgradeType key;
    public Sprite icon;
}