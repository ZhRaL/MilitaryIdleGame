using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextProvider
{
    public static Dictionary<string, string> Dictionary = new()
    {
        {"chair","cool description for a chair"}
    };

    public static string getDescription(string key)
    {
        string result;
        Dictionary.TryGetValue(key, out result);
        return result;
    }
}
