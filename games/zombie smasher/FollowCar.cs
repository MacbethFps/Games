using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform target; // Reference to the car's transform
    public float smoothSpeed = 5f; // Controls how smoothly the camera follows the car
    public Vector3 offset; // Offset from the car's position
    public float cameraHeight = 10f; // Height of the camera from the car
    public float rotationSpeed = 5f; // Rotation speed for the camera

    private void LateUpdate()
    {
        if (target == null)
        {
            // If the target (car) is not set, do nothing
            return;
        }

        // Calculate the desired camera position based on the car's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Set the camera's height
        desiredPosition.y = cameraHeight;

        // Use SmoothDamp to smoothly interpolate between the current camera position and the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Rotate the camera to look at the car
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
