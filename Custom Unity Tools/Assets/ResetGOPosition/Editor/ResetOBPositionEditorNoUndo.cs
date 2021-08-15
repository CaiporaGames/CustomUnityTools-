using UnityEditor;
using UnityEngine;

public class ResetOBPositionEditorNoUndo
{
    [MenuItem("Window / Reset select objects position( No Undo)")]
    static void ResetPosition()
    {
        //this action will not be undoable
        foreach (var go in Selection.gameObjects)
        {
            go.transform.localPosition = Vector3.zero;
        }
    }
}
