using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitCameraController_R : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target;

    [Header("Camera Settings")]
    public float distance = 6f;
    public float height = 2f;
    public float sensitivity = 100f;
    public float smoothTime = 0.1f;

    [Header("Pitch Clamp")]
    public float minPitch = -20f;
    public float maxPitch = 60f;

    private float yaw;
    private float pitch;

    private Vector3 currentVelocity;
    private Vector3 currentPosition;

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

        // Handle camera rotation
        yaw += lookInput.x * sensitivity * Time.deltaTime;
        pitch -= lookInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Desired position
        Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance) + Vector3.up * height;

        // Smooth move
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);

        // Always look at target
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}