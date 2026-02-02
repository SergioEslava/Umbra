using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [Tooltip("Name of the item")]
    public string itemName;          
    [TextArea]
    [Tooltip("Description of the item")]
    public string itemDescription;   
}
