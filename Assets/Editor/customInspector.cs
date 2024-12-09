using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class customInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager gm = (GameManager) target;
        if (GUILayout.Button("Reset Playerprefs"))
        {
            gm.ResetAllOwnPlayerPrefs();
        }
        if (GUILayout.Button("Add 10.000 Money"))
        {
            gm.Gold += 10000;
        }
        if (GUILayout.Button("Add 100 Badges"))
        {
            gm.Badges += 100;
        }
    }
}
