using UnityEngine;
using UnityEngine.InputSystem;

public class SignboardFocusZoom_R : MonoBehaviour
{
    [Header("References")]
    public Transform focusPoint;
    public Transform player;
    public Camera mainCamera;

    [Header("Zoom Settings")]
    public float zoomSpeed = 5f;

    private PlayerInputActions_R inputActions;
    private bool isFocusing = false;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    private bool playerInRange = false;
    private PlayerController_R playerController;

    private void Awake()
    {
        inputActions = new PlayerInputActions_R();

        // Bind the Interact and Cancel inputs
        inputActions.Player.Interact.performed += ctx => TryStartFocus();
        inputActions.Player.Cancel.performed += ctx => TryExitFocus();

        // Cache player controller reference
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController_R>();
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        if (isFocusing)
        {
            // Smoothly move camera to focus point
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, focusPoint.position, Time.deltaTime * zoomSpeed);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, focusPoint.rotation, Time.deltaTime * zoomSpeed);
        }
    }

    private void TryStartFocus()
    {
        if (!playerInRange) return;
        if (isFocusing) return;

        // Save the camera's original transform
        originalCameraPosition = mainCamera.transform.position;
        originalCameraRotation = mainCamera.transform.rotation;

        isFocusing = true;

        // Disable player movement
        if (playerController != null)
        {
            playerController.canMove = false;
        }
    }

    private void TryExitFocus()
    {
        if (!isFocusing) return;

        // Restore original camera position and rotation
        mainCamera.transform.position = originalCameraPosition;
        mainCamera.transform.rotation = originalCameraRotation;

        isFocusing = false;

        // Re-enable player movement
        if (playerController != null)
        {
            playerController.canMove = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Optional: auto-exit zoom if player walks away
            if (isFocusing)
            {
                TryExitFocus();
            }
        }
    }
}