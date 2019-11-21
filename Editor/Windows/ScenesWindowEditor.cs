using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// SceneViewWindow class.
/// </summary>
public class ScenesWindowEditor : EditorWindow
{
    /// <summary>
    /// The folder the levels file is located when the game is hosted in the editor.
    /// </summary>
    private const string EDITOR_LEVELS_FILE_DIRECTORY = "Scenes";

    /// <summary>
    /// Tracks scroll position.
    /// </summary>
    private Vector2 scrollPos;

    /// <summary>
    /// Initialize window state.
    /// </summary>
    [MenuItem("Window/Scene View")]
    internal static void Init()
    {
        // EditorWindow.GetWindow() will return the open instance of the specified window or create a new
        // instance if it can't find one. The second parameter is a flag for creating the window as a
        // Utility window; Utility windows cannot be docked like the Scene and Game view windows.
        var window = (SceneViewWindow)GetWindow(typeof(SceneViewWindow), false, "Scene View");
        window.position = new Rect(window.position.xMin + 100f, window.position.yMin + 100f, 200f, 400f);
    }

    /// <summary>
    /// Called on GUI events.
    /// </summary>
    internal void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos, false, false);



        GUILayout.Label("Scenes In Folder", EditorStyles.boldLabel);

        string folderPath = Path.Combine(Application.dataPath, EDITOR_LEVELS_FILE_DIRECTORY);
        string[] files = Directory.GetFiles(folderPath, "*.unity");
        for (var i = 0; i < files.Length; i++)
        {
            var pressed = GUILayout.Button(i + ": " + Path.GetFileNameWithoutExtension(files[i]), new GUIStyle(GUI.skin.GetStyle("Button")) { alignment = TextAnchor.MiddleLeft });
            if (pressed)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(files[i]);
                }
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}