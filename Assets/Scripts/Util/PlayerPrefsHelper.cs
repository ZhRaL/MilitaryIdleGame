using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerPrefsHelper
{
    private static Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        keyValuePairs[key] = value;
    }

    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        keyValuePairs[key] = value;
    }

    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        keyValuePairs[key] = value;
    }

    public static int GetInt(string key, int defaultValue = 0)
    {
        int value = PlayerPrefs.GetInt(key, defaultValue);
        keyValuePairs[key] = value;
        return value;
    }

    public static float GetFloat(string key, float defaultValue = 0.0f)
    {
        float value = PlayerPrefs.GetFloat(key, defaultValue);
        keyValuePairs[key] = value;
        return value;
    }

    public static string GetString(string key, string defaultValue = "")
    {
        string value = PlayerPrefs.GetString(key, defaultValue);
        keyValuePairs[key] = value;
        return value;
    }

    public static List<string> GetAllKeys()
    {
        return keyValuePairs.Keys.ToList();
    }
}