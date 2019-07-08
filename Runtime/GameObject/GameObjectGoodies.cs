using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectGoodies
{

    /// <summary>
    /// Add a component to the game object if it not yet esists
    /// </summary>
    /// <typeparam name="T">Type of the component</typeparam>
    /// <param name="child">Component</param>
    /// <returns></returns>
    static public T AddComponentIfNotExists<T>(this GameObject child) where T : Component
    {
        T result = child.GetComponent<T>();
        if (result == null)
        {
            result = child.gameObject.AddComponent<T>();
        }
        return result;
    }
}
