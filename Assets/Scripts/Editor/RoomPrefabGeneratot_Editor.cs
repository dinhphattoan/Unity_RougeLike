using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor (typeof (RoomPrefabGenerator))]
public class Room : Editor
{
    public override void OnInspectorGUI() {
		RoomPrefabGenerator mapGen = (RoomPrefabGenerator)target ;
        DrawDefaultInspector ();


		if (GUILayout.Button ("Generate")) {
			mapGen.GenerateMap();
		}
	}
}