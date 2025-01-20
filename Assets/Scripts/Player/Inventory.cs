using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    // List of items in the inventory
    public List<InventoryItem> items = new List<InventoryItem>();
    // Add an item to the inventory
    public void AddItem(ItemData _item, int _amount = 1)
    {
        // Check if the item already exists in the inventory
        InventoryItem _existingItem = items.Find(i => i.itemData.itemName.Equals(_item.itemName));
        if (_existingItem != null)
            _existingItem.quantity += _amount; 
        else
            items.Add(new InventoryItem(_item, _amount));                      
    }

    // Remove a quantity of an item from the inventory
    public void RemoveItem(ItemData _item, int _amount = 1)
    {
        InventoryItem _existingItem = items.Find(i => i.itemData.itemName.Equals(_item.itemName));
        if (_existingItem != null)
        {
            _existingItem.quantity -= _amount;
            // If the quantity reaches 0 or less, remove the item from the inventory
            if (_existingItem.quantity <= 0)
            {
                items.Remove(_existingItem);
            }
        }
        else
        {
            Debug.LogWarning($"Item {_item.itemName } not found in inventory!");
        }
    }

    // Check if the inventory contains a specific item
    public bool HasItem(ItemData item)
    {
        return items.Exists(i => i.itemData.itemName.Equals(item.itemName) && i.quantity > 0);
    }

    // Get the quantity of a specific item
    public int GetItemQuantity(ItemData item)
    {
        InventoryItem existingItem = items.Find(i => i.itemData.itemName == item.itemName);
        return existingItem != null ? existingItem.quantity : 0;
    }

    // Clear the inventory
    public void ClearInventory()
    {
        items.Clear();
    }
    
}
