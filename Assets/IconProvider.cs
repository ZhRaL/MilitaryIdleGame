using UnityEngine;
using System.Collections.Generic;

public class IconProvider : MonoBehaviour
{
    [SerializeField]
    private List<IconEntry> iconEntries = new List<IconEntry>();
    private Dictionary<string, Sprite> iconDictionary = new Dictionary<string, Sprite>();

    public static IconProvider INSTANCE;
    private void Awake()
    {
        if (INSTANCE == null) INSTANCE = this;
        
        // Initialize the dictionary from the list
        iconDictionary.Clear();
        foreach (var entry in iconEntries)
        {
            if (!iconDictionary.ContainsKey(entry.key))
            {
                iconDictionary.Add(entry.key, entry.icon);
            }
        }
    }

    public Sprite GetIcon(string key)
    {
        if (iconDictionary.ContainsKey(key))
        {
            return iconDictionary[key];
        }
        else
        {
            // Handle missing key
            Debug.LogWarning("Icon key not found: " + key);
            return null; // Or a default icon
        }
    }
}

[System.Serializable]
public class IconEntry
{
    public string key;
    public Sprite icon;
}