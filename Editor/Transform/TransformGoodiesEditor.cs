using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TransformGoodiesEditor
{
    
    [MenuItem("CONTEXT/Transform/Destroy All Children")]
    public static void DestroyAllChildren(MenuCommand menuCommand)
    {
        Transform transform = menuCommand.context as Transform;
        TransformGoodies.DestroyAllChildren(transform);
    }

}
