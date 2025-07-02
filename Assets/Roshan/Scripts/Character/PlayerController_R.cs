using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController_R : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;

    [Header("Interaction Control")]
    [Tooltip("If false, player cannot move. Use this to disable movement during interactions like zoom.")]
    public bool canMove = true;

    private Vector2 moveInput;
    private bool isSprinting;

    private CharacterController controller;
    private PlayerInputActions_R inputActions;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new PlayerInputActions_R();

        // Bind movement input
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Bind sprint input
        inputActions.Player.Sprint.performed += ctx => isSprinting = true;
        inputActions.Player.Sprint.canceled += ctx => isSprinting = false;
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
        if (!canMove) return;

        Move();
    }

    private void Move()
    {
        // Convert 2D input to 3D
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // Normalize if diagonal
        if (move.magnitude > 1f)
            move.Normalize();

        // Make movement relative to camera direction
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0f; // Flatten to horizontal plane

        float speed = isSprinting ? sprintSpeed : walkSpeed;

        // Apply movement
        controller.SimpleMove(move * speed);

        // Rotate to face movement direction
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}