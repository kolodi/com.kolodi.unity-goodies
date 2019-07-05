using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

        Quaternion originalRotation = box.transform.rotation;
        // temporary reset the rotation in order to calculate bounding boxes when alligned to the world axis
        box.transform.rotation = Quaternion.identity;

        var rs = box.gameObject.GetComponentsInChildren<Renderer>();
        if (rs.Length == 0) return;
        Bounds bounds = new Bounds(rs[0].bounds.center, rs[0].bounds.size);
        foreach (var r in rs)
        {
            bounds.Encapsulate(r.bounds);
        }

        box.center = box.transform.InverseTransformPoint(bounds.center);
        Vector3 size = box.transform.InverseTransformVector(bounds.size);
        box.size = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z)); // to avoid negative size

        box.transform.rotation = originalRotation;
    }
}
