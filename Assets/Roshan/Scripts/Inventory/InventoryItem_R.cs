using UnityEngine;

[System.Serializable]
public class InventoryItem_R
{
    public string itemName;
    public GameObject prefab;

    public InventoryItem_R(string name, GameObject obj)
    {
        itemName = name;
        prefab = obj;
    }
}