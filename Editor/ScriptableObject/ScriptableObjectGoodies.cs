using System;
using System.IO;
using UnityEngine;

public static class ScriptableObjectGoodies
{

    /// <summary>
    /// Create scriptable object asset and save it on disk
    /// </summary>
    /// <param name="scriptableObject"></param>
    /// <param name="path">Relative path in assets folder</param>
    /// <param name="fileName">Filename without extension (it is always .asset)</param>
    /// <returns>The same scriptable object</returns>
    public static ScriptableObject CreateAsset(ScriptableObject scriptableObject, string path = null, string fileName = null)
    {
        var name = string.IsNullOrEmpty(fileName) ? scriptableObject.GetType().Name : fileName;

        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";
        }

        if (Path.GetExtension(path) != string.Empty)
        {
            var subtractedPath = path.Substring(path.LastIndexOf("/", StringComparison.Ordinal));
            path = path.Replace(subtractedPath, string.Empty);
        }

        if (!Directory.Exists(Path.GetFullPath(path)))
        {
            Directory.CreateDirectory(Path.GetFullPath(path));
        }

        string assetPathAndName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(path + "/" + name + ".asset");

        UnityEditor.AssetDatabase.CreateAsset(scriptableObject, assetPathAndName);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();

        return scriptableObject;
    }
}
