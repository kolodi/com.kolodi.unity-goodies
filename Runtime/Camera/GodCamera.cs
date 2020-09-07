using UnityEngine;

namespace Kolodi.CameraGoodies
{

    public class GodCamera : MonoBehaviour
    {

        [SerializeField] float movementSpeed = 20f;
        [SerializeField] float movementSpeedMultiplier = 4f;
        [SerializeField] float rotationSpeed = 10f;


        private void Update()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            float multiplier = 1f;
            if (Input.GetKey(KeyCode.LeftShift)) multiplier = movementSpeedMultiplier;
            transform.Translate(Vector3.right * h * movementSpeed * multiplier * Time.deltaTime, Space.Self);
            transform.Translate(Vector3.forward * v * movementSpeed * multiplier * Time.deltaTime, Space.Self);
            //if (Input.GetMouseButtonDown(1))
            //{
            //    Cursor.lockState = CursorLockMode.Locked;
            //}
            if (Input.GetMouseButton(1))
            {
                var y = Input.GetAxis("Mouse Y");
                var x = Input.GetAxis("Mouse X");
                transform.Rotate(Vector3.right, -y * rotationSpeed, Space.Self);
                transform.Rotate(Vector3.up, x * rotationSpeed, Space.World);

            }
            //if (Input.GetMouseButtonUp(1))
            //{
            //    Cursor.lockState = CursorLockMode.None;
            //}
        }
    }
}