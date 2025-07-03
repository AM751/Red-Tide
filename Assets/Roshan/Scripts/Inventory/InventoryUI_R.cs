using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class InventoryUI_R : MonoBehaviour
{
    public Inventory_R inventory;
    public GameObject panel;
    public TextMeshProUGUI itemsText;

    private PlayerInputActions_R inputActions;
    private bool showInventory = false;

    private void Awake()
    {
        inputActions = new PlayerInputActions_R();

        inputActions.Player.OpenInventory.performed += ctx => ToggleInventory();
        inputActions.Player.InventoryUp.performed += ctx => { if (showInventory) MoveSelection(-1); };
        inputActions.Player.InventoryDown.performed += ctx => { if (showInventory) MoveSelection(1); };
        inputActions.Player.Drop.performed += ctx => TryDrop();
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
        panel.SetActive(showInventory);

        if (showInventory)
        {
            itemsText.text = "";
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (i == inventory.selectedIndex)
                    itemsText.text += $"> <b>{inventory.items[i].itemName}</b>\n";
                else
                    itemsText.text += $"  {inventory.items[i].itemName}\n";
            }
        }
    }

    private void ToggleInventory()
    {
        showInventory = !showInventory;
    }

    private void MoveSelection(int direction)
    {
        if (inventory.items.Count == 0) return;
        inventory.selectedIndex += direction;
        if (inventory.selectedIndex < 0) inventory.selectedIndex = inventory.items.Count - 1;
        if (inventory.selectedIndex >= inventory.items.Count) inventory.selectedIndex = 0;
    }

    private void TryDrop()
    {
        if (!showInventory || inventory.items.Count == 0) return;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            float dropDistance = 2f;
            Vector3 dropDirection = playerObj.transform.forward.normalized;
            Vector3 dropPosition = playerObj.transform.position + dropDirection * dropDistance;

            inventory.DropSelectedItem(dropPosition);
        }
    }
}