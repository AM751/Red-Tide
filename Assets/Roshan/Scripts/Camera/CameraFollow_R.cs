using UnityEngine;

public class CameraFollow_R : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target;

    [Header("Offset from target")]
    public Vector3 offset = new Vector3(0f, 5f, -8f);

    [Header("Smooth settings")]
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.LookAt(target);
    }
}