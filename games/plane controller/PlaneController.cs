using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float constantSpeed = 200f;
    public float pitchSpeed = 10f;
    public float yawSpeed = 10f;
    public float rollSpeed = 10f;

    private float rollInput;
    private float pitchInput;
    private float yawInput;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HandleInputs()
    {
        rollInput = Input.GetAxis("Roll");
        pitchInput = Input.GetAxis("Pitch");
        yawInput = Input.GetAxis("Yaw");
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        // Calculate desired rotation based on inputs
        Quaternion rotation = Quaternion.Euler(pitchInput * pitchSpeed * Time.fixedDeltaTime,
                                               yawInput * yawSpeed * Time.fixedDeltaTime,
                                               -rollInput * rollSpeed * Time.fixedDeltaTime);

        // Apply rotation
        rb.rotation *= rotation;

        // Calculate desired velocity based on forward direction
        Vector3 desiredVelocity = transform.forward * constantSpeed;

        // Apply linear interpolation to velocity
        rb.velocity = Vector3.Lerp(rb.velocity, desiredVelocity, Time.fixedDeltaTime);
    }
}
