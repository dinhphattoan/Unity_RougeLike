using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapBackground))]
public class BackgroundMapEditor : Editor {
    public override void OnInspectorGUI() {
        MapBackground mapGen = (MapBackground)target;
        DrawDefaultInspector ();
        if(GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}