using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectHelper
{
    /// <summary>
    /// Destroy immediately if used in unity editor and not in play mode,
    /// otherwise regular destroy
    /// </summary>
    /// <param name="o">Object to destroy</param>
    public static void SmartDestroy(Object o)
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            Object.Destroy(o);
        }
        else
        {
            UnityEditor.Undo.DestroyObjectImmediate(o);
        }
#else
        Object.Destroy(o);
#endif
    }

    /// <summary>
    /// Unified API for instantiating objects in Editor and at runtime.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prefab">Object to be instantiated</param>
    /// <param name="parent">Instantiated object's parent (optional)</param>
    /// <returns>Instantiated object</returns>
    public static T SmartInstantiate<T>(T prefab, Transform parent = null) where T : Object
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            return Object.Instantiate<T>(prefab, parent);
        }
        else
        {
            T o = UnityEditor.PrefabUtility.InstantiatePrefab(prefab) as T;
            if (parent != null && o is GameObject)
            {
                GameObject go = o as GameObject;
                go.transform.SetParent(parent);
            }
            return o;
        }
#else
        return Object.Instantiate<T>(prefab, parent);
#endif
    }


}