using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class EnemyDesignerWindow : EditorWindow
{

    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D rogueSectionTexture;

    Texture2D mageTexture;
    Texture2D rogueTexture;
    Texture2D warriorTexture;

    Color headerSectionColor = new Color(13f/255f, 32f/255f,44f/255,1f);

    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;

    Rect mageIconSection;
    Rect rogueIconSection;
    Rect warriorIconSection;

    GUISkin skin;

    static MageData mageData;
    static WarriorData warriorData;
    static RogueData rogueData;

    float iconSize = 80f;

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
        InitData();
        skin = Resources.Load<GUISkin>("GUIStyles/EnemyDesignSkin");
    }

    public static void InitData()
    {
        mageData = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
        warriorData = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
        rogueData = (RogueData)ScriptableObject.CreateInstance(typeof(RogueData));
    }

    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1,1);
        headerSectionTexture.SetPixel(0,0,headerSectionColor);

        headerSectionTexture.Apply();

        mageSectionTexture = Resources.Load<Texture2D>("Icons/blue");
        rogueSectionTexture = Resources.Load<Texture2D>("Icons/green");
        warriorSectionTexture = Resources.Load<Texture2D>("Icons/orange");


        mageTexture = Resources.Load<Texture2D>("Icons/MageIcon");
        warriorTexture = Resources.Load<Texture2D>("Icons/WarriorIcon");
        rogueTexture = Resources.Load<Texture2D>("Icons/RogueIcon");
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

        mageIconSection.x = (mageSection.x + mageSection.width/2f) - iconSize/2;
        mageIconSection.y = mageSection.y + 8;
        mageIconSection.width = iconSize;
        mageIconSection.height = iconSize;

        warriorSection.x = Screen.width / 3;
        warriorSection.y = 50;
        warriorSection.width = Screen.width / 3;
        warriorSection.height = Screen.width - 50;

        warriorIconSection.x = (warriorSection.x + warriorSection.width / 2f) - iconSize / 2;
        warriorIconSection.y = warriorSection.y + 8;
        warriorIconSection.width = iconSize;
        warriorIconSection.height = iconSize;       

        rogueSection.x = Screen.width / 3 * 2;
        rogueSection.y = 50;
        rogueSection.width = Screen.width / 3;
        rogueSection.height = Screen.width - 50;

        rogueIconSection.x = (rogueSection.x + rogueSection.width / 2f) - iconSize / 2;
        rogueIconSection.y = rogueSection.y + 8;
        rogueIconSection.width = iconSize;
        rogueIconSection.height = iconSize;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);

        GUI.DrawTexture(mageIconSection, mageTexture);
        GUI.DrawTexture(warriorIconSection, warriorTexture);
        GUI.DrawTexture(rogueIconSection, rogueTexture);
    }

    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
            GUILayout.Label("Enemy Designer", skin.GetStyle("Header1"));
        GUILayout.EndArea();
    }

    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);

        GUILayout.Space(iconSize + 8);
        GUILayout.Label("Mage", skin.GetStyle("MageHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage", skin.GetStyle("OtherFields"));
        mageData.damageType = (MageDamageType)EditorGUILayout.EnumPopup(mageData.damageType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("OtherFields"));
        mageData.weaponType = (MageWeaponType)EditorGUILayout.EnumPopup(mageData.weaponType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }
        GUILayout.EndArea();
    }

    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);
        GUILayout.Space(iconSize + 8);
        GUILayout.Label("Warrior", skin.GetStyle("WarriorHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Class", skin.GetStyle("OtherFields"));
        warriorData.warriorClassType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.warriorClassType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("OtherFields"));
        warriorData.warriorWeaponType = (WarriorWeaponType)EditorGUILayout.EnumPopup(warriorData.warriorWeaponType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }

        GUILayout.EndArea();
    }

    void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogueSection);
        GUILayout.Space(iconSize + 8);
        GUILayout.Label("Rogue", skin.GetStyle("RogueHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Strategy", skin.GetStyle("OtherFields"));
        rogueData.rogueStrategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(rogueData.rogueStrategyType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("OtherFields"));
        rogueData.rogueWeaponType = (RogueWeaponType)EditorGUILayout.EnumPopup(rogueData.rogueWeaponType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }

        GUILayout.EndArea();
    }
   
}

public class GeneralSettings : EditorWindow
{
    public enum SettingsType
    {
        MAGE, WARRIOR, ROGUE
    }

    static SettingsType dataSettings;
    static GeneralSettings window;

    public static void OpenWindow(SettingsType settings)
    {
        dataSettings = settings;
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    private void OnGUI()
    {
        switch (dataSettings)
        {
            case SettingsType.MAGE:
                DrawSettings((CharacterData)EnemyDesignerWindow.MageInfo);
                break;
            case SettingsType.WARRIOR:
                DrawSettings((CharacterData)EnemyDesignerWindow.WarriorInfo);
                break;
            case SettingsType.ROGUE:
                DrawSettings((CharacterData)EnemyDesignerWindow.RogueInfo);
                break;
            default:
                break;
        }

    }

    void DrawSettings(CharacterData characterData)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        characterData.prefab = (GameObject)EditorGUILayout.ObjectField(characterData.prefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Health");
        characterData.maxHealth = EditorGUILayout.FloatField(characterData.maxHealth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Energy");
        characterData.maxEnergy = EditorGUILayout.FloatField(characterData.maxEnergy);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("% Power");
        characterData.power = EditorGUILayout.Slider(characterData.power, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("% Crit Chance");
        characterData.critChance = EditorGUILayout.Slider(characterData.critChance, 0, characterData.power);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name");
        characterData.name = EditorGUILayout.TextField(characterData.name);
        EditorGUILayout.EndHorizontal();

        if (characterData.prefab == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [PREFAB] before it can be created", MessageType.Warning);
        }
        else if (characterData.name == null || characterData.name.Length < 1)
        {
            EditorGUILayout.HelpBox("This enemy needs a [NAME] before it can be created", MessageType.Warning);

        }
        else if (GUILayout.Button("Finnish and Save",GUILayout.Height(30)))
        {
            SaveCharacterData();
            window.Close();
        }

    }

    void SaveCharacterData()
    {
        string prefabPath;
        string newPrefabPath = "Assets/EnemyDesigner/Prefabs/Characters";
        string dataPath = "Assets/EnemyDesigner/Resources/CharacterData/Data";

        switch (dataSettings)
        {
            case SettingsType.MAGE:
                dataPath += "/Mage/" + EnemyDesignerWindow.MageInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath);
                newPrefabPath += "/Mage/" + EnemyDesignerWindow.MageInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath,typeof(GameObject));
                if (!magePrefab.GetComponent<Mage>())
                {
                    magePrefab.AddComponent(typeof(Mage));
                }

                magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.MageInfo;
                break;
            case SettingsType.WARRIOR:
                dataPath += "/Warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.WarriorInfo, dataPath);
                newPrefabPath += "/Warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.WarriorInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject warriorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!warriorPrefab.GetComponent<Warrior>())
                {
                    warriorPrefab.AddComponent(typeof(Warrior));
                }

                warriorPrefab.GetComponent<Warrior>().warriorData = EnemyDesignerWindow.WarriorInfo;
                break;
            case SettingsType.ROGUE:
                dataPath += "/Rogue/" + EnemyDesignerWindow.RogueInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.RogueInfo, dataPath);
                newPrefabPath += "/Rogue/" + EnemyDesignerWindow.RogueInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.RogueInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject roguePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!roguePrefab.GetComponent<Rogue>())
                {
                    roguePrefab.AddComponent(typeof(Rogue));
                }

                roguePrefab.GetComponent<Rogue>().rogueData = EnemyDesignerWindow.RogueInfo;
                break;
            default:
                break;
        }
    }
}

