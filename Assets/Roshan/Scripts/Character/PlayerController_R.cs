using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController_R : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;

    [Header("Interaction Control")]
    public bool canMove = true;

    [Header("Inventory")]
    public Inventory_R inventory;    // Reference to Inventory_R
    public float pickupRange = 2f;

    private Vector2 moveInput;
    private bool isSprinting;

    private CharacterController controller;
    private PlayerInputActions_R inputActions;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new PlayerInputActions_R();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Sprint.performed += ctx => isSprinting = true;
        inputActions.Player.Sprint.canceled += ctx => isSprinting = false;

        inputActions.Player.Pickup.performed += ctx => TryPickup();
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
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        if (move.magnitude > 1f)
            move.Normalize();

        if (Camera.main != null)
        {
            move = Camera.main.transform.TransformDirection(move);
            move.y = 0f;
        }

        float speed = isSprinting ? sprintSpeed : walkSpeed;
        controller.SimpleMove(move * speed);

        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void TryPickup()
    {
        if (inventory == null) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Pickup"))
            {
                var pickupItem = hit.GetComponent<Pickupitem_R>();
                if (pickupItem != null)
                {
                    inventory.AddItem(pickupItem.itemName, pickupItem.itemPrefab);
                    Debug.Log($"Picked up {pickupItem.itemName} with prefab {pickupItem.itemPrefab}");
                }
                else
                {
                    inventory.AddItem(hit.name, null);
                    Debug.LogWarning("No Pickupitem_R script on picked object: " + hit.name);
                }
                Destroy(hit.gameObject);
                break;
            }
        }
    }
}