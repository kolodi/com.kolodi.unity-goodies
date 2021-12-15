using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityGoodies;

namespace UnityGoodiesEditor
{

    public class ColliderGoodies : MonoBehaviour
    {
        /// <summary>
        /// Search through all renderers within selected game object and grow/shrink the box collider to encapsulate all these renderers
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("CONTEXT/BoxCollider/Encapsulate All Children")]
        private static void EncapsulateAllChildren(MenuCommand menuCommand)
        {
            BoxCollider box = menuCommand.context as BoxCollider;
            Undo.RecordObject(box, "Encapsulate All Children");

            box.EncapluslateAllChildren();
        }
    }

}
