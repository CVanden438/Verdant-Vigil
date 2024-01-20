using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public WeaponSO testItem;
    public WeaponSO testItem2;

    public Hotbar hotbar;

    private void Start()
    {
        Debug.Log("Calling AddItem for testItem");
        AddItem(testItem);

        Debug.Log("Calling AddItem for testItem2");
        AddItem(testItem2);
    }

    public void AddItem(ItemSO itemData, int stackSize = 1)
    {
        // Check if the item is stackable and if it already exists in the inventory
        if (stackSize > 1 && items.Exists(i => i.itemData == itemData))
        {
            InventoryItem existingItem = items.Find(i => i.itemData == itemData);
            existingItem.stackSize += stackSize;
        }
        else
        {
            InventoryItem newItem = new InventoryItem
            {
                itemData = itemData,
                stackSize = stackSize
            };
            items.Add(newItem);
        }
        Debug.Log("hihi");
        UpdateUI();
    }

    public void RemoveItem(ItemSO itemData, int stackSize = 1)
    {
        // Check if the item is stackable and if it already exists in the inventory
        if (stackSize > 1 && items.Exists(i => i.itemData == itemData))
        {
            InventoryItem existingItem = items.Find(i => i.itemData == itemData);
            existingItem.stackSize -= stackSize;

            // Remove the item from the inventory if the stack size becomes zero
            if (existingItem.stackSize <= 0)
            {
                items.Remove(existingItem);
            }
        }
        else
        {
            InventoryItem itemToRemove = items.Find(i => i.itemData == itemData);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        // Implement logic to update the UI with the current inventory state
        Debug.Log("here");
        hotbar.UpdateHotbar(items);
    }
}
