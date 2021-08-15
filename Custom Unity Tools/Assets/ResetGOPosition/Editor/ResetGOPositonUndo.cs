using UnityEditor;
using UnityEngine;
using System.Linq;

public class ResetGOPositonUndo
{
    [MenuItem("Window / Reset the selected object position")]

    static void ResetPosition()
    {
        var transforms = Selection.gameObjects.Select(go => go.transform).ToArray();
        var so = new SerializedObject(transforms);
        //you can use shift+right click on property name in inspecto to see their paths
        so.FindProperty("m_LocalPosition").vector3Value = Vector3.zero;
        so.ApplyModifiedProperties();
    }
}
