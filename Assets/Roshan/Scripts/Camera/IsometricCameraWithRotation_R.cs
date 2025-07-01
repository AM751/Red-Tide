using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricCameraWithRotation_R : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target;

    [Header("Camera Orbit Settings")]
    public float distance = 10f;
    public float height = 15f;
    public float rotationSpeed = 50f;
    public float pitch = 45f;

    [Header("Pitch Clamp")]
    public float minPitch = 30f;
    public float maxPitch = 60f;

    [Header("Smoothness")]
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    private float yaw = 45f;

    private PlayerInputActions_R inputActions;
    private Vector2 lookInput;

    private void Awake()
    {
        inputActions = new PlayerInputActions_R();

        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Handle input for rotation
        yaw += lookInput.x * rotationSpeed * Time.deltaTime;
        pitch -= lookInput.y * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Calculate camera rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Desired position
        Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance) + Vector3.up * height;

        // Smooth move
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Look at target
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}