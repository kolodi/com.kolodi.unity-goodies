using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// SceneViewWindow class.
/// </summary>
public class ScenesWindowEditor : EditorWindow
{

    private const string SETTINGS_FILE_PATH_KEY = "UnityGoodies_Scenes_SettingsFilePath";

    private static ScenesData scenesData;

    /// <summary>
    /// Tracks scroll position.
    /// </summary>
    private Vector2 scrollPos;

    /// <summary>
    /// Initialize window state.
    /// </summary>
    [MenuItem("Window/Scenes")]
    internal static void Init()
    {
        // EditorWindow.GetWindow() will return the open instance of the specified window or create a new
        // instance if it can't find one. The second parameter is a flag for creating the window as a
        // Utility window; Utility windows cannot be docked like the Scene and Game view windows.
        var window = (ScenesWindowEditor)GetWindow(typeof(ScenesWindowEditor), false, "Scenes");
        window.position = new Rect(window.position.xMin + 100f, window.position.yMin + 100f, 200f, 400f);

        string settingsFilePath = EditorPrefs.GetString(SETTINGS_FILE_PATH_KEY);
        if (string.IsNullOrEmpty(settingsFilePath))
        {
            var paths = new List<string> {
                Path.Combine(Application.dataPath, "scenes.json"),
                Path.Combine(Application.dataPath, "Scenes", "scenes.json"),
                Path.Combine(Application.dataPath, "_app", "Scenes", "scenes.json"),
            };
            foreach (var p in paths)
            {
                if (File.Exists(p))
                {
                    settingsFilePath = p;
                    EditorPrefs.SetString(SETTINGS_FILE_PATH_KEY, settingsFilePath);
                    break;
                }
            }
        }

        if (string.IsNullOrEmpty(settingsFilePath))
        {
            CreateNewSettings();
        }
        else
        {
            try
            {
                string json = File.ReadAllText(settingsFilePath);
                scenesData = JsonUtility.FromJson<ScenesData>(json);

            }
            catch (Exception ex)
            {
                Debug.Log(ex);
                CreateNewSettings();
            }
        }
    }

    static void CreateNewSettings()
    {
        scenesData = new ScenesData()
        {
        };
    }

    /// <summary>
    /// Called on GUI events.
    /// </summary>
    internal void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos, false, false);



        //GUILayout.Label("Scenes In Folder", EditorStyles.boldLabel);

        //string folderPath = Path.Combine(Application.dataPath, EDITOR_LEVELS_FILE_DIRECTORY);
        //string[] files = Directory.GetFiles(folderPath, "*.unity");
        //for (var i = 0; i < files.Length; i++)
        //{
        //    var pressed = GUILayout.Button(i + ": " + Path.GetFileNameWithoutExtension(files[i]), new GUIStyle(GUI.skin.GetStyle("Button")) { alignment = TextAnchor.MiddleLeft });
        //    if (pressed)
        //    {
        //        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        //        {
        //            EditorSceneManager.OpenScene(files[i]);
        //        }
        //    }
        //}






        foreach (var g in scenesData.SceneGroups)
        {
            g.Visisble = EditorGUILayout.Foldout(g.Visisble, g.GroupName);
            if (g.Visisble)
            {
                foreach (var s in g.Scenes)
                {
                    if (GUILayout.Button(s.SceneName))
                    {
                        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        {
                            EditorSceneManager.OpenScene(s.ScenePath);
                        }
                    }
                }
            }
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Space(10);

        if (GUILayout.Button("Find Scenes"))
        {
            var rootFolder = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, "Scenes");
            if (string.IsNullOrEmpty(rootFolder) == false)
            {
                try
                {
                    var dirs = Directory.GetDirectories(rootFolder);
                    scenesData.SceneGroups = new List<SceneGroup>();
                    foreach (var d in dirs)
                    {
                        var info = new DirectoryInfo(d);

                        var scenes = AssetDatabase.FindAssets("t:Scene", new string[] { d.Substring(d.IndexOf("Assets")) });
                        if (scenes.Length > 0)
                        {
                            scenesData.SceneGroups.Add(new SceneGroup
                            {
                                GroupName = info.Name,
                                Scenes = scenes.Select(scene_guid =>
                                {
                                    var scenePath = AssetDatabase.GUIDToAssetPath(scene_guid);
                                    var sceneInfo = new FileInfo(scenePath);
                                    return new SceneData()
                                    {
                                        SceneName = sceneInfo.Name,
                                        ScenePath = scenePath
                                    };
                                }).ToList()
                            });

                        }

                    }

                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }
        if (GUILayout.Button("Save Settings"))
        {
            var path = EditorUtility.SaveFilePanel(
                "Save JSON settings",
                Application.dataPath,
                "scenes.json",
                "json");

            try
            {
                var json = JsonUtility.ToJson(scenesData);
                File.WriteAllText(path, json);
                EditorPrefs.SetString(SETTINGS_FILE_PATH_KEY, path);
            }
            catch (Exception ex)
            {
                EditorUtility.DisplayDialog("Error", ex.Message, "Close");
            }

        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}

[Serializable]
public class ScenesData
{
    public List<SceneGroup> SceneGroups;
}

[Serializable]
public class SceneGroup
{
    public string GroupName;
    public List<SceneData> Scenes;
    public bool Visisble;
}

[Serializable]
public class SceneData
{
    public string SceneName;
    public string ScenePath;
}