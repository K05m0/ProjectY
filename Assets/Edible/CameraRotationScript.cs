using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationScript : MonoBehaviour
{
    public Transform target; // The central point to rotate around
    public float rotationSpeed = 5f; // Speed of rotation
    public float cameraDistance = 20f;
    public float cameraDownwardRotation = 27f;

    private float _yaw = 0f;
    private bool _isRotating = false;

    private void Start()
    {
        // Rotate the camera around the target
        Quaternion rotation = Quaternion.Euler(cameraDownwardRotation, -135f, 0f);
        Vector3 newPosition = rotation * new Vector3(0f, 0f, cameraDistance * -1) + target.position;
        transform.rotation = rotation;
        transform.position = newPosition;
    }

    void Update()
    {
        // Check if the middle mouse button is pressed down
        if (Input.GetMouseButtonDown(2))
        {
            _isRotating = true;
        }
        // Check if the middle mouse button is released
        else if (Input.GetMouseButtonUp(2))
        {
            _isRotating = false;
        }


        // Rotate the camera if the middle mouse button is pressed
        if (_isRotating)
        {
            // Get the mouse input
            float mouseX = Input.GetAxis("Mouse X");

            // Calculate the rotation amount
            _yaw += mouseX * rotationSpeed;

            // Rotate the camera around the target
            Quaternion rotation = Quaternion.Euler(cameraDownwardRotation, _yaw - 135f, 0f);
            Vector3 newPosition = rotation * new Vector3(0f, 0f, cameraDistance * -1) + target.position;
            transform.rotation = rotation;
            transform.position = newPosition;
        }
    }
}
