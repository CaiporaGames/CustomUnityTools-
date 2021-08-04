using UnityEditor;
using UnityEngine;

public class SimpleObjectSpawner : EditorWindow
{
    string objectName = "";
    int objectId = 1;
    GameObject objectToSpawn;
    float objectScale = 1;
    float spawnRadius = 1;
    float spawnHeight = 0;

    [MenuItem("Tools/Simple Object Spawner")]//create the tab show on the unity
    public static void ShowWindow()//static: we can call the script even without a gameObject attach to it
    {
        //this is a method inherite from the editorwindow class
        GetWindow(typeof(SimpleObjectSpawner));
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn New Object",EditorStyles.boldLabel);

        objectName = EditorGUILayout.TextField("Object Name");
        objectId = EditorGUILayout.IntField("Object ID", objectId);
        objectScale = EditorGUILayout.Slider("Object Scale", objectScale, 0.5f,3f);
        spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius);
        spawnHeight = EditorGUILayout.FloatField("Spawn Height", spawnHeight);
        objectToSpawn = EditorGUILayout.ObjectField("Prefab to spawn", objectToSpawn, typeof(GameObject),false) as GameObject;

        if (GUILayout.Button("Spawn Object"))
        {
            SpawnObject();

        }
    }

    void SpawnObject()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("Error: Please assign an object to be spawned!");
            return;
        }

        if (objectName == string.Empty)
        {
            Debug.LogError("Error: Please, enter a name for the object");
            return;
        }

        Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = new Vector3(spawnCircle.x, spawnHeight, spawnCircle.y);

        GameObject newObject = Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
        newObject.name = objectName + objectId;
        newObject.transform.localPosition = Vector3.one * objectScale;

        objectId++;
    }
}
