using UnityEngine;
using UnityEditor;
using System.Collections;
//using System.Reflection;

[CustomEditor(typeof(StructurePlacer), true)]
public class FortBuildingEditor : Editor
{
    GUISkin skin;
    SerializedObject character;
    bool showWindow;

    void OnEnable()
    {
        StructurePlacer motor = (StructurePlacer)target;
    }

    public override void OnInspectorGUI()
    {
        if (!skin) skin = Resources.Load("skin1") as GUISkin;
        GUI.skin = skin;

        StructurePlacer motor = (StructurePlacer)target;

        if (!motor) return;

        GUILayout.BeginVertical("Fort Building", "window");

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        

        EditorGUILayout.Space();     

        EditorGUILayout.BeginVertical();        

        base.OnInspectorGUI();
      
        GUILayout.EndVertical();
        EditorGUILayout.EndVertical();        

        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }   
}


