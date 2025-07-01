using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController_R : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;

    private Vector2 moveInput;
    private bool isSprinting;

    private CharacterController controller;
    private PlayerInputActions_R inputActions;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new PlayerInputActions_R();

        // Setup Move action
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Setup Sprint action
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
        Move();
    }

    private void Move()
{
    // Convert 2D input to 3D
    Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

    // Clamp to avoid faster diagonal movement
    if (move.magnitude > 1f)
        move.Normalize();

    // Make movement relative to camera
    move = Camera.main.transform.TransformDirection(move);
    move.y = 0f; // Keep movement horizontal

    float speed = isSprinting ? sprintSpeed : walkSpeed;

    // Move using CharacterController
    controller.SimpleMove(move * speed);

    // Rotate to face movement direction
    if (move.sqrMagnitude > 0.01f)
    {
        Quaternion targetRotation = Quaternion.LookRotation(move);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }
}
}