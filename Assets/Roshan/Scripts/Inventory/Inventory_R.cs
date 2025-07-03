using System.Collections.Generic;
using UnityEngine;

public class Inventory_R : MonoBehaviour
{
    public List<InventoryItem_R> items = new List<InventoryItem_R>();
    public int selectedIndex = 0;

    public void AddItem(string itemName, GameObject prefab)
    {
        items.Add(new InventoryItem_R(itemName, prefab));
        Debug.Log($"Picked up: {itemName} | Prefab: {prefab}");
    }

    // Drop the selected item at the given position
    public void DropSelectedItem(Vector3 dropPosition)
    {
        if (items.Count > 0 && selectedIndex >= 0 && selectedIndex < items.Count)
        {
            var selectedItem = items[selectedIndex];
            Debug.Log("Trying to instantiate: " + selectedItem.prefab);

            if (selectedItem.prefab != null)
            {
                GameObject obj = GameObject.Instantiate(selectedItem.prefab, new Vector3(0,2,0), Quaternion.identity);
                Debug.Log("Instantiated object: " + obj.name + " at " + obj.transform.position);
            }
            else
            {
                Debug.LogWarning("No prefab assigned for: " + selectedItem.itemName);
            }

            Debug.Log("Dropped: " + selectedItem.itemName);
            items.RemoveAt(selectedIndex);

            if (selectedIndex >= items.Count)
                selectedIndex = items.Count - 1;
            if (selectedIndex < 0)
                selectedIndex = 0;
        }
    }
}