
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PoissonDiscSampler))]
public class PoissonDiscEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        PoissonDiscSampler mapGen = (PoissonDiscSampler)target;
        if(GUILayout.Button("Generate"))
        {
            
            mapGen.Sample();
        }
    }
}