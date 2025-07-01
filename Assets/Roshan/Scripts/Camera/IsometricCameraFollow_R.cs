using UnityEngine;

public class IsometricCameraFollow_R : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target;

    [Header("Offset from target (world space)")]
    public Vector3 offset = new Vector3(-10f, 15f, -10f);

    [Header("Smoothness")]
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Desired camera position in world space
        Vector3 desiredPosition = target.position + offset;

        // Smooth move
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Always look at the player
        transform.LookAt(target.position);
    }
}