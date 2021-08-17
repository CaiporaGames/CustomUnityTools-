using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class BugReporterEditor : EditorWindow
{
    string bugReporterName = "";
    string bugDescription = "";

    GameObject buggyGO;

   [MenuItem("Window / Bug Reporter")]
   public static void ShowWindow()
    {
        GetWindow(typeof(BugReporterEditor));
    }

    public BugReporterEditor()
    {
        titleContent = new GUIContent("Bug Reporter");
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Bug Reporter");
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;

        GUILayout.Space(10);
        bugReporterName = EditorGUILayout.TextField("Bug Name", bugReporterName);
        GUILayout.Label("Scene Name: " + EditorSceneManager.GetActiveScene().name);
        GUILayout.Space(10);
        GUILayout.Label("Time: " + System.DateTime.Now);

        GUILayout.Space(10);
        buggyGO = (GameObject)EditorGUILayout.ObjectField("Bug GameObject", buggyGO,typeof(GameObject), true);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Description", GUILayout.MaxWidth(80));
        bugDescription = EditorGUILayout.TextArea(bugDescription, GUILayout.MaxHeight(75));
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        if (GUILayout.Button("Save Bug"))
        {
            SaveBug();
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Save Bug With Screenshot"))
        {
            SaveBugWithScreenshot();
        }


        GUILayout.EndVertical();
    }

    void SaveBug()
    {
        Directory.CreateDirectory("Assets/BugReports/" + bugReporterName);
        StreamWriter sw = new StreamWriter("Assets/BugReports/" + bugReporterName + "/" + bugReporterName + ".txt");
        sw.WriteLine(bugReporterName);
        sw.WriteLine(System.DateTime.Now.ToString());
        sw.WriteLine(EditorSceneManager.GetActiveScene().name);
        sw.WriteLine(bugDescription);
        sw.Close();
    }

    void SaveBugWithScreenshot()
    {
        Directory.CreateDirectory("Assets/BugReports/" + bugReporterName);
        StreamWriter sw = new StreamWriter("Assets/BugReports/" + bugReporterName + "/" + bugReporterName + ".txt");
        sw.WriteLine(bugReporterName);
        sw.WriteLine(System.DateTime.Now.ToString());
        sw.WriteLine(EditorSceneManager.GetActiveScene().name);
        sw.WriteLine(bugDescription);
        sw.Close();

        ScreenCapture.CaptureScreenshot("Assets/BugReports/" + bugReporterName + "/" + bugReporterName + "Screenshot" + ".png");
    }
}
