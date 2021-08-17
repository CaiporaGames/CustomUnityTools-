using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GridMapCriator : EditorWindow
{
    Vector2 offset;
    Vector2 drag;
    List<List<Node>> nodes;
    List<List<PartScripts>> parts;
    GUIStyle empty;
    Vector2 nodePosition;
    StyleManager styleManager;
    bool isErasing;
    Rect menuBar;
    GUIStyle currentStyle;
    GameObject theMap;

    [MenuItem("Window / Grid Map Creator")]
    static void OpenWindow()
    {
        GridMapCriator window = (GridMapCriator)GetWindow<GridMapCriator>();
        window.titleContent = new GUIContent("Grid Map Creator");
    }

    private void OnEnable()
    {
        SetupStyles();
        SetupMap();
        SetUpNodesNParts();        
    }

    void SetupMap()
    {
        try
        {
            theMap = GameObject.FindGameObjectWithTag("Map");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        if (theMap == null)
        {
            theMap = new GameObject("Map");
            theMap.tag = "Map";
        }
    }

    void SetupStyles()
    {
        try
        {
            styleManager = GameObject.FindGameObjectWithTag("StyleManager").GetComponent<StyleManager>();

            for (int i = 0; i < styleManager.buttonStyles.Length; i++)
            {
                styleManager.buttonStyles[i].nodeStyle = new GUIStyle();
                styleManager.buttonStyles[i].nodeStyle.normal.background = styleManager.buttonStyles[i].icon;
            }
        }
        catch (Exception e){ Debug.LogWarning(e); }
        empty = styleManager.buttonStyles[0].nodeStyle;
        currentStyle = styleManager.buttonStyles[1].nodeStyle;
    }

    void SetUpNodesNParts()
    {
        parts = new List<List<PartScripts>>();
        nodes = new List<List<Node>>();

        for (int i = 0; i < 20; i++)
        {
            nodes.Add(new List<Node>());
            parts.Add(new List<PartScripts>());
            for (int j = 0; j < 10; j++)
            {
                nodePosition.Set(i * 30, j * 30);
                nodes[i].Add(new Node(nodePosition, 30, 30, empty));
                parts[i].Add(null);
            }
        }
    }

    private void OnGUI()
    {
        DrawGrid();
        DrawNodes();
        DrawMenuBar();
        ProcessGrid(Event.current);
        ProcessNodes(Event.current);
        if (GUI.changed)
        {
            Repaint();
        }
    }

    void DrawMenuBar()
    {
        menuBar = new Rect(0, 0, position.width, 20);
        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();
        for (int i = 0; i < styleManager.buttonStyles.Length; i++)
        {
            if (GUILayout.Toggle((currentStyle == styleManager.buttonStyles[i].nodeStyle), new GUIContent(styleManager.buttonStyles[i].buttonTex),
                EditorStyles.toolbarButton, GUILayout.Width(80)))
            {
            currentStyle = styleManager.buttonStyles[i].nodeStyle;
            }
        }
      
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    void ProcessNodes(Event e)
    {
        int row = (int)((e.mousePosition.x - offset.x) / 30);
        int col = (int)((e.mousePosition.y - offset.y) / 30);

        if ((e.mousePosition.x - offset.x) < 0 || (e.mousePosition.x - offset.x) > 600 || (e.mousePosition.y - offset.y) < 0 || (e.mousePosition.y - offset.y) > 300)
        {

        }
        else
        {
            if (e.type == EventType.MouseDown)
            {
                if (nodes[row][col].style.normal.background.name == "Empty")
                {
                    isErasing = false;

                }
                else
                {
                    isErasing = true;
                }

                PaintNodes(row, col);
            }
            if (e.type == EventType.MouseDrag)
            {
                PaintNodes(row, col);
                e.Use();
            }
        }
    }

    void PaintNodes(int row, int col)
    {
        if (isErasing)
        {
            nodes[row][col].SetStyle(empty);
            GUI.changed = true;
        }
        else
        {
            if (parts[row][col] == null)
            {
                nodes[row][col].SetStyle(currentStyle);
                GameObject go = Instantiate(Resources.Load("Prefabs" + currentStyle.normal.background.name)) as GameObject;
                go.name = currentStyle.normal.background.name;
                go.transform.position = new Vector3(col * 10 , 0, row * 10);
                go.transform.parent = theMap.transform;
                parts[row][col] = go.GetComponent<PartScripts>();
                parts[row][col].name = go.name;
                parts[row][col].col = col;
                parts[row][col].style = currentStyle;
                GUI.changed = true;
            }
        }
    }
    void DrawNodes()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                nodes[i][j].Draw();
            }
        }
    }

    void ProcessGrid(Event e)
    {
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnMouseDrag(e.delta);
                }
                break;
        }
    }

    void OnMouseDrag(Vector2 delta)
    {
        drag = delta;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                nodes[i][j].Drag(delta);
            }
        }
        GUI.changed = true;
    }

    void DrawGrid()
    {
        int widthDivider = Mathf.CeilToInt(position.width/20);
        int heightDivider = Mathf.CeilToInt(position.height/20);

        Handles.BeginGUI();
        Handles.color = new Color(0.5f,0.5f,0.5f,0.2f);

        offset += drag;

        Vector3 newOffset = new Vector3(offset.x % 20, offset.y % 20, 0);

        for (int i = 0; i < widthDivider; i++)
        {
            Handles.DrawLine(new Vector3(20 * i, -20, 0) + newOffset, new Vector3(20 * i, position.height, 0) + newOffset);
        }

        for (int i = 0; i < heightDivider; i++)
        {
            Handles.DrawLine(new Vector3(-20, 20 * i, 0) + newOffset, new Vector3(position.height, 20 * i, 0) + newOffset);
        }

        Handles.EndGUI();
    }
}
