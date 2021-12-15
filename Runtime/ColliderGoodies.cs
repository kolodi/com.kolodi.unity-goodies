using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGoodies
{
    public static class ColliderGoodies
    {
        public static void EncapluslateAllChildren(this BoxCollider box)
        {

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
}
