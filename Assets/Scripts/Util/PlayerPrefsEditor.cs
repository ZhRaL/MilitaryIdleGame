using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(GameManager))]
public class PlayerPrefsEditor : Editor
{
    private Dictionary<string, object> list;
    private void OnEnable()
    {
        list = PlayerPrefsHelper.GetAllKeys();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Zeige die PlayerPrefs im Inspector an
        foreach (var key in list)
        {
            EditorGUILayout.LabelField(key.Key, key.Value.ToString());
        }
    }
}