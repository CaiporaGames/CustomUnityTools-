using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyDesignerWindow : EditorWindow
{

    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D rogueSectionTexture;

    Color headerSectionColor = new Color(13f/255f, 32f/255f,44f/255,1f);

    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;

    static MageData mageData;
    static WarriorData warriorData;
    static RogueData rogueData;

    public static MageData MageInfo { get { return mageData; } }
    public static WarriorData WarriorInfo { get { return warriorData; } }
    public static RogueData RogueInfo { get { return rogueData; } }

    [MenuItem("Window/Enemy Designer")]
   static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    private void OnEnable()
    {
        InitTextures();
    }

    public static void InitData()
    {

    }

    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1,1);
        headerSectionTexture.SetPixel(0,0,headerSectionColor);

        headerSectionTexture.Apply();

        mageSectionTexture = Resources.Load<Texture2D>("Icons/blue");
        rogueSectionTexture = Resources.Load<Texture2D>("Icons/green");
        warriorSectionTexture = Resources.Load<Texture2D>("Icons/orange");
    }

    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
    }

    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        mageSection.x = 0;
        mageSection.y = 50;
        mageSection.width = Screen.width / 3;
        mageSection.height = Screen.width - 50;

        warriorSection.x = Screen.width / 3;
        warriorSection.y = 50;
        warriorSection.width = Screen.width / 3;
        warriorSection.height = Screen.width - 50;


        rogueSection.x = Screen.width / 3 * 2;
        rogueSection.y = 50;
        rogueSection.width = Screen.width / 3;
        rogueSection.height = Screen.width - 50;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
    }

    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
            GUILayout.Label("Mage");
        GUILayout.EndArea();
    }

    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);
            GUILayout.Label("Mage");
        GUILayout.EndArea();
    }

    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);
            GUILayout.Label("Warrior");

        GUILayout.EndArea();
    }

    void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogueSection);
            GUILayout.Label("Rogue");

        GUILayout.EndArea();
    }
   
}
