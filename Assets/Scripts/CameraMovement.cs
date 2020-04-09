using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    private const float Y_MIN_ANGLE = 5.0f;
    private const float Y_MAX_ANGLE = 50.0f;

    [Tooltip("Target the camera will look at")]
    [SerializeField] public Transform target = null;

    [Header("Camera settings")]
    [Tooltip("Distance between camera and target")]
    [SerializeField]
    private float distance = 10f;
    private float yaw = 0f;
    private float pitch = 0f;
    [SerializeField]
    private float sensiX = 4f;
    [SerializeField]
    private float sensiY = 1f;

    [SerializeField] private float rotationSmoothTime = 0.12f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensiX;
        pitch -= Input.GetAxis("Mouse Y") * sensiY;

        pitch = Mathf.Clamp(pitch, Y_MIN_ANGLE, Y_MAX_ANGLE);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw),
            ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distance;
    } 
}
