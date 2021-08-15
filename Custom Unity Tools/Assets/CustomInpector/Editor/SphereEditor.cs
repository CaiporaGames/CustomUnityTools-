using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Sphere))]
public class SphereEditor : Editor
{
    
    public override void OnInspectorGUI()
    {

        Sphere sphere = (Sphere)target;
        EditorGUILayout.LabelField("Oscilates between a base size");
        sphere.baseSize = EditorGUILayout.Slider("Base Size", sphere.baseSize, 0.1f, 2);
        sphere.transform.localScale = Vector3.one * sphere.baseSize;

    }
}
