using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Colorize : EditorWindow
{
    Color color;

    [MenuItem("Window/Colorizer")]
    public static void ShowWindow()//This will be called when we press the button on the specified path 
    {
        GetWindow<Colorize>("Colorizer");//this will open a window for us in the editor
    }

    private void OnGUI()
    {
        //all the menu stuff will be here
        GUILayout.Label("Colorize the selected objects", EditorStyles.boldLabel);
        color = EditorGUILayout.ColorField("Color", color);

        if (GUILayout.Button("Colorize"))
        {
            Colorizer();    
        }
    }

    void Colorizer()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            Renderer renderer = go.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.sharedMaterial.color = color;
            }
        }
    }
}
