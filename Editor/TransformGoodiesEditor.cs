using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityGoodies;

namespace UnityGoodiesEditor
{

    public class TransformGoodiesEditor
    {

        [MenuItem("CONTEXT/Transform/Destroy All Children")]
        public static void DestroyAllChildren(MenuCommand menuCommand)
        {
            Transform transform = menuCommand.context as Transform;
            transform.DestroyAllChildren();
        }


        [MenuItem("GameObject/Unity Goodies/Destroy All Children", false, 10)]
        static void CreateCustomGameObject(MenuCommand menuCommand)
        {
            Selection.activeTransform.DestroyAllChildren();
        }

    }

}
