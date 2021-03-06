using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector]
    public Path path;

    public Color anchorColor = Color.red;
    public Color controlColor = Color.white;
    public Color segmentColor = Color.green;
    public Color selectedSegmentColor = Color.yellow;

    public float anchorDiamenter = 0.1f;
    public float controlDiamenter = 0.075f;
    public bool displayControlPoints = true;

    public void CreatePath()
    {
        path = new Path(transform.position);
    }

    private void Reset()
    {
        CreatePath();
    }
}