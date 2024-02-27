using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform[] povs;       // Array of camera POV transforms
    [SerializeField] float speed = 5.0f;     // Speed at which the camera switches POV
    [SerializeField] Vector3 cameraOffset;   // Offset from the plane's position

    private Vector3 targetPosition;
    private int currentIndex = 1;

    private void Update()
    {
        // Switch POV based on user input
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentIndex = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) currentIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) currentIndex = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) currentIndex = 3;

        // Calculate the target position for the camera based on the selected POV
        targetPosition = povs[currentIndex].position + cameraOffset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        // Align the camera's forward direction with the selected POV's forward direction
        transform.forward = Vector3.Lerp(transform.forward, povs[currentIndex].forward, speed * Time.deltaTime);
    }
}
