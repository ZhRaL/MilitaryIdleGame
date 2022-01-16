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
            gm.ResetPlayerprefs();
        }
    }
}
