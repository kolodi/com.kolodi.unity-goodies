using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    private Vector3 lastMousePos;
    private Vector2 minMaxDistance = new Vector2 { x = 1f, y = 10f };
    private float zoomPercentage = 0.1f;

    private Vector3 focalPoint = Vector3.zero;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            var delta = Input.mousePosition - lastMousePos;
            transform.RotateAround(focalPoint, Vector3.up, delta.x);
            transform.RotateAround(focalPoint, transform.right, -delta.y);
            lastMousePos = Input.mousePosition;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            var delta = Input.mouseScrollDelta.y;
            var direction = transform.position - focalPoint;
            float distance = direction.magnitude;

            float newDistance = distance - (delta * distance * zoomPercentage);
            newDistance = Mathf.Clamp(newDistance, minMaxDistance.x, minMaxDistance.y);

            transform.position = focalPoint + direction.normalized * newDistance;
        }
    }
}
