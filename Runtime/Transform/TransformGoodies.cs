using UnityEngine;

public static class TransformGoodies
{
    public static Bounds GetBounds(Transform transform, bool alignWithWorldAxis = true)
    {
        Bounds bounds;

        Quaternion originalRotation = transform.rotation;
        if (alignWithWorldAxis)
        {
            // temporary reset the rotation in order to calculate bounding boxes when alligned to the world axis
            transform.rotation = Quaternion.identity;
        }

        var rs = transform.GetComponentsInChildren<Renderer>();
        if (rs.Length == 0) return new Bounds(transform.position, Vector3.one);
        bounds = new Bounds(rs[0].bounds.center, rs[0].bounds.size);
        foreach (var r in rs)
        {
            bounds.Encapsulate(r.bounds);
        }


        if (alignWithWorldAxis)
        {
            transform.rotation = originalRotation;
        }

        return bounds;
    }

    public static void DestroyAllChildren(Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            ObjectHelper.SmartDestroy(transform.GetChild(i).gameObject);
        }
    }
}
