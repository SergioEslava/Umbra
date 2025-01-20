using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public InventoryItem(ItemData _itemData, int _quantity)
    {
        itemData = _itemData;
        quantity = _quantity;
    }

    [Tooltip("Item identification for this inventory item")]
    public ItemData itemData;
    [Tooltip("Quantity of the item")]
    public int quantity;          
}
